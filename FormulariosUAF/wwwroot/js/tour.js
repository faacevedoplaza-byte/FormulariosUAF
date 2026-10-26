/* ================================================
   FormulariosUAF — Recorrido guiado (Driver.js)
   ================================================
   Se muestra solo la primera vez que el usuario entra a la vista (se recuerda en este navegador,
   por usuario) y se puede repetir con cualquier botón [data-tour-iniciar].

   uafTour({
       clave: 'regcheq-index',               // una por vista; cambiar la versión vuelve a mostrarlo a todos
       pasos: [
           { titulo: '…', texto: '…' },                       // sin elemento: ventana centrada
           { elemento: '#id', titulo: '…', texto: '…', lado: 'bottom' }
       ]
   });

   Los pasos cuyo elemento no existe o no se ve (según el rol, sin datos, menú lateral cerrado en
   el celular) se omiten. Mientras el recorrido está abierto, vista-en-vivo.js no reemplaza la página.
*/
window.uafTour = function (o) {
    const version = 'v1';
    const usuario = document.body.dataset.uafUsuario || 'anonimo';
    const clave = `uaf-tour:${version}:${o.clave}:${usuario}`;

    const visto = () => { try { return localStorage.getItem(clave) === '1'; } catch { return true; } };
    const marcar = () => { try { localStorage.setItem(clave, '1'); } catch { } };

    function visible(el) {
        if (!el) return false;
        const r = el.getBoundingClientRect();
        return r.width > 0 && r.height > 0 && r.right > 0 && r.left < window.innerWidth;
    }

    function pasos() {
        return o.pasos
            .map(p => ({ p, el: p.elemento ? document.querySelector(p.elemento) : null }))
            .filter(x => !x.p.elemento || visible(x.el))
            .map(x => ({
                element: x.el || undefined,
                popover: { title: x.p.titulo, description: x.p.texto, side: x.p.lado, align: x.p.alinear || 'start' }
            }));
    }

    function iniciar() {
        const crear = window.driver?.js?.driver;
        if (!crear) return; // sin acceso al CDN: la página funciona igual, sin recorrido
        window.uafTourActivo = true;
        const d = crear({
            steps: pasos(),
            showProgress: true,
            progressText: '{{current}} de {{total}}',
            nextBtnText: 'Siguiente',
            prevBtnText: 'Anterior',
            doneBtnText: 'Terminar',
            popoverClass: 'uaf-tour',
            stagePadding: 6,
            stageRadius: 10,
            // Evita acciones reales por accidente durante el recorrido (ej. "Tomar").
            disableActiveInteraction: true,
            onDestroyed: () => { window.uafTourActivo = false; marcar(); }
        });
        d.drive();
    }

    document.addEventListener('click', e => {
        if (e.target.closest('[data-tour-iniciar]')) iniciar();
    });

    // Primera visita: esperar a que la página termine de pintarse.
    if (!visto()) setTimeout(iniciar, 600);
};
