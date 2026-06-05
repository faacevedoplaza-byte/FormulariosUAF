/* ================================================
   FormulariosUAF — SignalR Notifications (Fase 2)
   ================================================ */

(function () {
    const badge = document.getElementById('notif-badge');

    // Cargar conteo inicial al entrar
    fetch('/api/notificaciones/count')
        .then(r => r.ok ? r.json() : null)
        .then(data => { if (data?.count > 0) updateBadge(data.count); })
        .catch(() => { /* silent — SignalR no disponible */ });

    // Conectar al hub SignalR
    const connection = new signalR.HubConnectionBuilder()
        .withUrl('/hubs/notificaciones')
        .withAutomaticReconnect([0, 2000, 5000, 10000])
        .configureLogging(signalR.LogLevel.Warning)
        .build();

    connection.on('NewNotification', function (notification) {
        showToast(notification.message, 'info');
        incrementBadge();
    });

    connection.start()
        .catch(err => console.warn('SignalR no disponible:', err));

    function updateBadge(count) {
        if (!badge) return;
        badge.textContent = count;
        badge.classList.toggle('d-none', count === 0);
    }

    function incrementBadge() {
        if (!badge) return;
        const current = parseInt(badge.textContent || '0', 10);
        updateBadge(current + 1);
    }
})();
