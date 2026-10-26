/*
 * Reproductor de los videos de introducción (Regcheq/Introduccion, Vendedor/Introduccion).
 *
 * Marcado esperado:
 *   <div class="vx-reproductor" data-clave-visto="uaf-video-xxx:v1">
 *     <div class="vx-pantalla">
 *       <section class="vx-escena" data-narracion="Texto que se lee y se subtitula">…</section>
 *       …
 *     </div>
 *   </div>
 *
 * El subtítulo, el botón de inicio y los controles los crea este script. La narración usa la voz en español
 * del navegador (speechSynthesis); sin voz, cada escena dura según su largo y se sigue con los subtítulos.
 * Al llegar a la última escena se guarda {data-clave-visto}:{usuario} en localStorage (lo lee _AvisoVideo).
 *
 * Con data-video="clave" la visita se registra en el servidor (POST /Videos/Registrar, informe en /Admin/Videos):
 * si reprodujo, hasta qué escena llegó, qué escenas vio enteras, pausas, adelantos, retrocesos y tiempo reproducido.
 */
(function () {
    const raiz = document.querySelector('.vx-reproductor');
    if (!raiz) return;
    const pantalla = raiz.querySelector('.vx-pantalla');
    const escenas = Array.from(pantalla.querySelectorAll('.vx-escena'));
    const tts = window.speechSynthesis;
    const claveVisto = (raiz.dataset.claveVisto || 'uaf-video') + ':' + (document.body.dataset.uafUsuario || 'anonimo');

    const crear = (html) => { const t = document.createElement('template'); t.innerHTML = html.trim(); return t.content.firstElementChild; };

    const sub = crear('<div class="vx-subtitulo" aria-live="polite"></div>');
    const inicio = crear(`<button type="button" class="vx-inicio" title="Reproducir">
            <span class="vx-inicio-boton"><i class="fa-solid fa-play ms-1"></i></span>
            <span class="fw-semibold">Reproducir video · con sonido</span></button>`);
    pantalla.append(sub, inicio);

    const controles = crear(`<div class="vx-controles">
            <button type="button" class="btn btn-sm btn-outline-secondary" data-vx="anterior" title="Escena anterior (←)"><i class="fa-solid fa-backward-step"></i></button>
            <button type="button" class="btn btn-sm btn-primary" data-vx="play" title="Reproducir / pausar (espacio)"><i class="fa-solid fa-play"></i></button>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-vx="siguiente" title="Escena siguiente (→)"><i class="fa-solid fa-forward-step"></i></button>
            <div class="vx-progreso"></div>
            <span class="vx-tiempo"></span>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-vx="voz" title="Narración con voz"><i class="fa-solid fa-volume-high"></i></button>
            <button type="button" class="btn btn-sm btn-outline-secondary active" data-vx="subs" title="Subtítulos"><i class="fa-solid fa-closed-captioning"></i></button>
            <button type="button" class="btn btn-sm btn-outline-secondary" data-vx="completa" title="Pantalla completa"><i class="fa-solid fa-expand"></i></button>
        </div>`);
    const sinVoz = crear(`<div class="small text-muted mt-2" hidden>
            <i class="fa-solid fa-circle-info me-1"></i>Este navegador no tiene voz en español: el video avanza solo y se lee con los subtítulos.</div>`);
    raiz.append(controles, sinVoz);

    const boton = nombre => controles.querySelector(`[data-vx="${nombre}"]`);
    const btnPlay = boton('play'), btnVoz = boton('voz');
    const progreso = controles.querySelector('.vx-progreso');
    const tiempo = controles.querySelector('.vx-tiempo');

    let actual = 0, reproduciendo = false, conVoz = !!tts, temporizador = null, turno = 0;

    // ── Registro de la visita ──────────────────────────────────────────────
    const video = raiz.dataset.video;
    const nuevaSesion = () => window.crypto?.randomUUID?.() ||
        'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => { const r = Math.random() * 16 | 0; return (c === 'x' ? r : (r & 3 | 8)).toString(16); });
    let reg, tramoDesde = null;
    function reiniciarRegistro() {
        reg = { sesion: nuevaSesion(), reproducido: false, escenaMaxima: 0, vistas: new Set(), pausas: 0, adelantos: 0,
                retrocesos: 0, ms: 0, completo: false, pantallaCompleta: false };
    }
    reiniciarRegistro();
    // Acumula el tiempo en reproducción (llamar después de cambiar `reproduciendo`).
    function marcarTiempo() {
        const ahora = performance.now();
        if (tramoDesde !== null) reg.ms += ahora - tramoDesde;
        tramoDesde = reproduciendo ? ahora : null;
    }
    function registrar() {
        if (!video) return;
        marcarTiempo();
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value || '';
        const cuerpo = JSON.stringify({
            sesion: reg.sesion, video, reproducido: reg.reproducido, totalEscenas: escenas.length,
            escenaMaxima: reg.escenaMaxima, escenasVistas: [...reg.vistas], pausas: reg.pausas, adelantos: reg.adelantos,
            retrocesos: reg.retrocesos, segundos: Math.round(reg.ms / 1000), completo: reg.completo, conVoz,
            pantallaCompleta: reg.pantallaCompleta
        });
        // keepalive: el envío sobrevive al cierre de la pestaña.
        fetch('/Videos/Registrar', { method: 'POST', keepalive: true, body: cuerpo,
            headers: { 'Content-Type': 'application/json', 'X-CSRF-TOKEN': token } }).catch(() => { });
    }
    setInterval(() => { if (reproduciendo) registrar(); }, 15000);
    document.addEventListener('visibilitychange', () => { if (document.visibilityState === 'hidden') registrar(); });
    document.addEventListener('fullscreenchange', () => { if (document.fullscreenElement) reg.pantallaCompleta = true; });

    // Navegación hecha por el usuario (botones, flechas, barra): cuenta como adelanto o retroceso.
    function irUsuario(i) {
        const destino = Math.max(0, Math.min(escenas.length - 1, i));
        if (destino === actual) return;
        if (destino > actual) reg.adelantos++; else reg.retrocesos++;
        ir(destino);
        registrar();
    }

    // Barra segmentada: una franja por escena (clic = saltar a esa escena).
    const segs = escenas.map((_, i) => {
        const b = crear(`<button type="button" class="vx-seg" title="Escena ${i + 1}"><span></span></button>`);
        b.addEventListener('click', () => irUsuario(i));
        progreso.appendChild(b);
        return b;
    });

    // Voz en español: se prefiere Chile / Latinoamérica.
    let voz = null;
    function elegirVoz() {
        if (!tts) return;
        const pref = ['es-cl', 'es-419', 'es-us', 'es-mx', 'es-ar', 'es-es'];
        const idx = v => { const i = pref.indexOf(v.lang.toLowerCase()); return i < 0 ? 99 : i; };
        voz = tts.getVoices().filter(v => v.lang?.toLowerCase().startsWith('es')).sort((a, b) => idx(a) - idx(b))[0] || null;
        if (!voz && tts.getVoices().length) { conVoz = false; pintarVoz(); sinVoz.hidden = false; }
    }
    if (tts) { elegirVoz(); tts.addEventListener?.('voiceschanged', elegirVoz); }
    else { conVoz = false; sinVoz.hidden = false; }

    const duracionLectura = texto => 2500 + texto.split(/\s+/).length * 400; // sin voz: ~150 palabras por minuto

    function mostrar(i) {
        escenas.forEach((e, j) => e.classList.toggle('activa', j === i));
        // Reiniciar las animaciones de entrada de la escena.
        const e = escenas[i];
        e.classList.remove('activa'); void e.offsetWidth; e.classList.add('activa');
        segs.forEach((s, j) => {
            s.classList.toggle('hecho', j < i);
            const barra = s.firstElementChild;
            barra.style.transition = 'none';
            barra.style.width = j < i ? '100%' : '0';
        });
        tiempo.textContent = `${i + 1} / ${escenas.length}`;
        sub.textContent = e.dataset.narracion;
        if (reg.reproducido) reg.escenaMaxima = Math.max(reg.escenaMaxima, i + 1);
    }

    function animarBarra(i, ms) {
        const barra = segs[i].firstElementChild;
        void barra.offsetWidth;
        barra.style.transition = `width ${ms}ms linear`;
        barra.style.width = '100%';
    }

    function detener() {
        turno++;
        clearTimeout(temporizador);
        if (tts) tts.cancel();
    }

    function reproducirEscena() {
        detener();
        const mio = turno;
        const texto = escenas[actual].dataset.narracion;
        // Solo si la escena terminó sola (detener() cambia el turno antes de cancelar la voz).
        const terminar = () => {
            if (mio !== turno) return;
            reg.vistas.add(actual + 1);
            temporizador = setTimeout(siguiente, 900);
        };
        animarBarra(actual, duracionLectura(texto));

        if (conVoz && tts && voz) {
            // Por frases: Chrome corta los textos largos a los ~15 s.
            const frases = texto.match(/[^.!?¿¡]+[.!?]*|[¿¡][^?!]+[?!]/g) || [texto];
            frases.forEach((f, k) => {
                const u = new SpeechSynthesisUtterance(f.trim());
                u.voice = voz; u.lang = voz.lang; u.rate = 1.02;
                if (k === frases.length - 1) { u.onend = terminar; u.onerror = terminar; }
                tts.speak(u);
            });
        } else {
            temporizador = setTimeout(terminar, duracionLectura(texto));
        }
    }

    function ir(i) {
        actual = Math.max(0, Math.min(escenas.length - 1, i));
        mostrar(actual);
        if (actual === escenas.length - 1) { try { localStorage.setItem(claveVisto, '1'); } catch { } }
        if (reproduciendo) reproducirEscena(); else detener();
    }

    function siguiente() {
        if (actual >= escenas.length - 1) {
            reproduciendo = false; pintarPlay(); segs[actual].classList.add('hecho');
            inicio.hidden = false;
            inicio.lastElementChild.textContent = 'Volver a ver';
            reg.completo = true;
            registrar();
            return;
        }
        ir(actual + 1);
        registrar();
    }

    function play() {
        inicio.hidden = true;
        if (actual >= escenas.length - 1 && !reproduciendo && segs[actual].classList.contains('hecho')) {
            actual = 0;
            reiniciarRegistro(); // "Volver a ver" cuenta como otra reproducción
        }
        reproduciendo = true; pintarPlay();
        reg.reproducido = true;
        mostrar(actual);
        reproducirEscena();
        registrar();
    }
    function pausa() {
        reproduciendo = false; pintarPlay(); detener(); mostrar(actual);
        reg.pausas++;
        registrar();
    }

    function pintarPlay() { btnPlay.innerHTML = `<i class="fa-solid ${reproduciendo ? 'fa-pause' : 'fa-play'}"></i>`; }
    function pintarVoz() {
        btnVoz.innerHTML = `<i class="fa-solid ${conVoz ? 'fa-volume-high' : 'fa-volume-xmark'}"></i>`;
        btnVoz.classList.toggle('active', conVoz);
    }

    inicio.addEventListener('click', play);
    btnPlay.addEventListener('click', () => reproduciendo ? pausa() : play());
    boton('anterior').addEventListener('click', () => irUsuario(actual - 1));
    boton('siguiente').addEventListener('click', () => irUsuario(actual + 1));
    btnVoz.addEventListener('click', () => {
        if (!tts || !voz) return;
        conVoz = !conVoz; pintarVoz();
        if (reproduciendo) reproducirEscena(); // retoma la escena con o sin voz
    });
    boton('subs').addEventListener('click', e => {
        e.currentTarget.classList.toggle('active');
        pantalla.classList.toggle('vx-sin-subtitulos');
    });
    boton('completa').addEventListener('click', () => {
        if (document.fullscreenElement) document.exitFullscreen();
        else pantalla.requestFullscreen?.();
    });
    document.addEventListener('keydown', e => {
        if (e.target.closest('input, textarea, select')) return;
        if (e.key === ' ') { e.preventDefault(); reproduciendo ? pausa() : play(); }
        else if (e.key === 'ArrowRight') irUsuario(actual + 1);
        else if (e.key === 'ArrowLeft') irUsuario(actual - 1);
    });
    // Al salir de la página, que no siga hablando.
    window.addEventListener('pagehide', () => { detener(); registrar(); });

    pintarVoz();
    mostrar(0);
    registrar(); // abrió la página (aunque no llegue a reproducir)
})();
