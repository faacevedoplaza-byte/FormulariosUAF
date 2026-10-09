/* ================================================
   FormulariosUAF — Vistas en vivo (RegCheq)
   ================================================
   Cuando el servidor avisa por SignalR (window.uafHub, de notifications.js) que algo cambió, la página
   vuelve a pedir su propia URL (mismos filtros, orden y página) y reemplaza solo los contenedores
   indicados. Para no interrumpir, espera mientras el usuario escribe, tiene un menú abierto o
   `ocupado()` lo indica; los paneles (collapse) abiertos o cerrados se conservan.

   uafVivo({
       contenedores: ['idA', 'idB'],         // elementos que se reemplazan
       eventos: ['RegcheqCambiada'],         // avisos del hub que provocan la recarga
       estado: 'idIndicador',                // opcional: indicador "En vivo"
       ocupado: () => false,                 // opcional: condición extra para esperar
       alActualizar: () => {}                // opcional: después de reemplazar
   });
*/
window.uafVivo = function (o) {
    const hub = window.uafHub;
    const estado = o.estado ? document.getElementById(o.estado) : null;
    const textoEstado = estado?.querySelector('span');
    let pendiente = false, cargando = false, temporizador = null;

    const contenedores = () => o.contenedores.map(id => document.getElementById(id)).filter(Boolean);

    function ocupado() {
        const a = document.activeElement;
        for (const c of contenedores()) {
            if (a && c.contains(a) && a.matches('input, textarea, select')) return true;
            if (Array.from(c.querySelectorAll('textarea')).some(t => t.value.trim() !== '')) return true;
            if (c.querySelector('.collapsing')) return true;
        }
        return o.ocupado ? o.ocupado() : false;
    }

    function pintarEstado(conectado) {
        if (!estado) return;
        estado.classList.remove('hay-cambios');
        estado.classList.toggle('on', conectado);
        estado.title = conectado ? 'La vista se actualiza sola cuando hay cambios'
                                 : 'Sin conexión en vivo: se revisa cada minuto';
        if (textoEstado) textoEstado.textContent = conectado ? 'En vivo' : 'Reconectando…';
    }

    function programar(ms) {
        pendiente = true;
        clearTimeout(temporizador);
        temporizador = setTimeout(recargar, ms);
    }

    async function recargar() {
        if (cargando) return programar(1000);
        if (ocupado()) {
            if (estado && !estado.classList.contains('hay-cambios')) {
                estado.classList.add('hay-cambios');
                if (textoEstado) textoEstado.textContent = 'Hay cambios: se mostrarán al terminar';
            }
            return programar(3000);
        }
        cargando = true;
        try {
            const r = await fetch(location.href, { cache: 'no-store', credentials: 'same-origin' });
            if (!r.ok) return;
            const doc = new DOMParser().parseFromString(await r.text(), 'text/html');
            const nuevos = o.contenedores.map(id => doc.getElementById(id));
            if (nuevos.some(n => !n)) return; // p. ej. la sesión expiró y se devolvió el login
            if (ocupado()) return programar(3000); // empezó a escribir mientras se descargaba

            contenedores().forEach((actual, i) => {
                const nuevo = nuevos[i];
                // Conservar paneles abiertos/cerrados según los dejó el usuario.
                const abiertos = {};
                actual.querySelectorAll('.collapse[id]').forEach(c => abiertos[c.id] = c.classList.contains('show'));
                nuevo.querySelectorAll('.collapse[id]').forEach(c => {
                    if (!(c.id in abiertos)) return;
                    c.classList.toggle('show', abiertos[c.id]);
                    nuevo.querySelectorAll(`[data-bs-target="#${c.id}"]`).forEach(b => {
                        b.classList.toggle('collapsed', !abiertos[c.id]);
                        b.setAttribute('aria-expanded', abiertos[c.id] ? 'true' : 'false');
                    });
                });
                actual.replaceChildren(...Array.from(nuevo.childNodes).map(n => document.adoptNode(n)));
            });
            pendiente = false;
            if (estado?.classList.contains('hay-cambios')) pintarEstado(hub?.state === 'Connected');
            o.alActualizar?.();
        } catch (e) {
            console.warn('No se pudo actualizar la vista en vivo', e);
        } finally {
            cargando = false;
        }
    }

    // Registrarse en el grupo RegCheq del hub (al conectar y en cada reconexión: el grupo se pierde).
    function seguir() { hub.invoke('SeguirRegcheq').catch(() => { }); }

    if (hub) {
        // Agrupa varios avisos seguidos (ej. acción masiva) en una sola recarga.
        o.eventos.forEach(ev => hub.on(ev, () => programar(700)));
        hub.onreconnecting(() => pintarEstado(false));
        hub.onreconnected(() => { seguir(); pintarEstado(true); programar(0); }); // pudo perderse algún aviso
        hub.onclose(() => pintarEstado(false));
        // notifications.js inicia la conexión; esperar a que quede conectada.
        const revisarInicio = setInterval(() => {
            if (hub.state === 'Connected') { clearInterval(revisarInicio); seguir(); pintarEstado(true); }
            else if (hub.state === 'Disconnected') { clearInterval(revisarInicio); pintarEstado(false); }
        }, 500);
    } else {
        pintarEstado(false);
    }

    // Respaldo: sin SignalR (o si se cortó definitivamente) se recarga cada minuto e intenta reconectar.
    setInterval(() => {
        if (hub?.state === 'Connected') return;
        if (hub?.state === 'Disconnected')
            hub.start().then(() => { seguir(); pintarEstado(true); programar(0); }).catch(() => { });
        programar(0);
    }, 60000);

    // Al volver a la pestaña o soltar el foco, aplicar los cambios que quedaron esperando.
    document.addEventListener('visibilitychange', () => { if (!document.hidden && pendiente) programar(300); });
    document.addEventListener('focusout', () => { if (pendiente) programar(1500); });
};
