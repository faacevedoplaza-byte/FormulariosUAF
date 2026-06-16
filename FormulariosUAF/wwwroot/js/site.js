/* ================================================
   FormulariosUAF — JavaScript principal
   ================================================ */


// ---- Toast helper ----
function showToast(message, type) {
    type = type || 'success';
    var container = document.getElementById('toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toast-container';
        document.body.appendChild(container);
    }

    var bgMap = { success: 'bg-success text-white', danger: 'bg-danger text-white', warning: 'bg-warning text-dark', info: 'bg-info text-dark' };
    var bg = bgMap[type] || 'bg-secondary text-white';
    var icon = type === 'success' ? 'check-circle' : 'circle-exclamation';

    var toast = document.createElement('div');
    toast.className = 'toast-msg ' + bg;
    toast.innerHTML = '<i class="fa-solid fa-' + icon + ' me-2"></i>' + message;
    container.appendChild(toast);

    setTimeout(function () { toast.style.opacity = '0'; toast.style.transition = 'opacity 0.3s'; }, 3000);
    setTimeout(function () { toast.remove(); }, 3400);
}

// ---- CSRF token helper ----
function getCsrfToken() {
    var input = document.querySelector('input[name="__RequestVerificationToken"]');
    return input ? input.value : '';
}

// ---- Sidebar toggle ----
document.addEventListener('DOMContentLoaded', function () {
    var toggleBtn = document.getElementById('sidebarToggle');
    var wrapper = document.getElementById('wrapper');
    if (toggleBtn && wrapper) {
        toggleBtn.addEventListener('click', function () {
            wrapper.classList.toggle('sidebar-hidden');
        });
    }

    // Auto-dismiss alerts
    document.querySelectorAll('.alert-dismissible').forEach(function (alert) {
        setTimeout(function () {
            if (typeof bootstrap !== 'undefined') {
                bootstrap.Alert.getOrCreateInstance(alert).close();
            }
        }, 5000);
    });
});

// ---- RUT Formatting ----
function formatRUT(rut) {
    if (!rut) return rut;
    var clean = rut.replace(/[^0-9kK]/g, '');
    if (clean.length < 2) return rut;
    var dv = clean.slice(-1).toUpperCase();
    var body = clean.slice(0, -1);
    var formatted = '';
    while (body.length > 3) { formatted = '.' + body.slice(-3) + formatted; body = body.slice(0, -3); }
    return body + formatted + '-' + dv;
}

// Auto-formatea cualquier campo con clase .rut-input al salir del campo.
// Usa 'focusout' (que sí propaga) para cubrir también campos agregados dinámicamente.
document.addEventListener('focusout', function (e) {
    if (e.target && e.target.classList && e.target.classList.contains('rut-input')) {
        var formatted = formatRUT(e.target.value);
        if (formatted) e.target.value = formatted;
    }
});

// ---- File size check ----
document.addEventListener('change', function (e) {
    if (e.target.type === 'file') {
        var maxMB = 20;
        Array.from(e.target.files).forEach(function (f) {
            if (f.size > maxMB * 1024 * 1024) {
                showToast('El archivo "' + f.name + '" supera el límite de ' + maxMB + ' MB', 'danger');
                e.target.value = '';
            }
        });
    }
});
