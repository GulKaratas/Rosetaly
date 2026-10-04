/* =============================================
   RentCar Admin Panel — script.js
   ============================================= */

'use strict';

// ─── Sidebar Toggle ───────────────────────────
let sidebarCollapsed = false;
let isMobile = () => window.innerWidth < 992;

function toggleSidebar() {
  const sidebar   = document.getElementById('sidebar');
  const overlay   = document.getElementById('sidebarOverlay');

  if (isMobile()) {
    sidebar.classList.toggle('mobile-open');
    if (overlay) overlay.style.display = sidebar.classList.contains('mobile-open') ? 'block' : 'none';
  } else {
    sidebarCollapsed = !sidebarCollapsed;
    sidebar.classList.toggle('collapsed', sidebarCollapsed);
    localStorage.setItem('sidebarCollapsed', sidebarCollapsed);
  }
}

function closeSidebarMobile() {
  const sidebar = document.getElementById('sidebar');
  const overlay = document.getElementById('sidebarOverlay');
  sidebar.classList.remove('mobile-open');
  if (overlay) overlay.style.display = 'none';
}

function restoreSidebarState() {
  if (!isMobile()) {
    const saved = localStorage.getItem('sidebarCollapsed');
    if (saved === 'true') {
      const sidebar = document.getElementById('sidebar');
      if (sidebar) { sidebar.classList.add('collapsed'); sidebarCollapsed = true; }
    }
  }
}

// ─── Submenu Toggle ──────────────────────────
function toggleSubmenu(submenuId, parentId) {
  const submenu = document.getElementById(submenuId);
  const parent  = document.getElementById(parentId);
  if (!submenu || !parent) return;

  submenu.classList.toggle('open');
  parent.classList.toggle('open');
}

// ─── Date Display ─────────────────────────────
function setCurrentDate() {
  const el = document.getElementById('currentDate');
  if (!el) return;
  const opts = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
  el.textContent = new Date().toLocaleDateString('tr-TR', opts);
}

// ─── Toast Notification ──────────────────────
function showToast(message, type = 'success') {
  const container = document.getElementById('toastContainer');
  if (!container) return;

  const colors = {
    success: { bg: '#10b981', icon: 'fa-circle-check' },
    danger:  { bg: '#ef4444', icon: 'fa-circle-xmark' },
    info:    { bg: '#1a56db', icon: 'fa-circle-info' },
    warning: { bg: '#f59e0b', icon: 'fa-triangle-exclamation' },
  };
  const { bg, icon } = colors[type] || colors.success;
  const id = 'toast-' + Date.now();

  const html = `
    <div id="${id}" class="toast align-items-center border-0 text-white show"
         style="background:${bg};border-radius:var(--radius-sm);box-shadow:var(--shadow-lg);min-width:280px;">
      <div class="d-flex">
        <div class="toast-body d-flex align-items-center gap-2" style="font-family:'DM Sans',sans-serif;font-size:13.5px;font-weight:500;">
          <i class="fa-solid ${icon}"></i> ${message}
        </div>
        <button type="button" class="btn-close btn-close-white me-2 m-auto" onclick="removeToast('${id}')"></button>
      </div>
    </div>`;

  container.insertAdjacentHTML('beforeend', html);
  setTimeout(() => removeToast(id), 3500);
}

function removeToast(id) {
  const el = document.getElementById(id);
  if (el) { el.style.opacity = '0'; el.style.transform = 'translateX(60px)'; el.style.transition = 'all 0.3s'; setTimeout(() => el.remove(), 300); }
}

// ─── Vehicle Data ─────────────────────────────
const vehicles = [
  {
    id: 1,
    brand: 'BMW',
    model: '5 Serisi 520i',
    year: 2023,
    price: 2800,
    fuel: 'Benzin',
    transmission: 'Otomatik',
    km: '12.450 km',
    status: 'Müsait',
    category: 'Sedan',
    img: 'https://images.unsplash.com/photo-1555215695-3004980ad54e?w=600&q=80&auto=format',
  },
  {
    id: 2,
    brand: 'Mercedes',
    model: 'E200 AMG',
    year: 2022,
    price: 3200,
    fuel: 'Dizel',
    transmission: 'Otomatik',
    km: '28.700 km',
    status: 'Kirada',
    category: 'Sedan',
    img: 'https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=600&q=80&auto=format',
  },
  {
    id: 3,
    brand: 'Toyota',
    model: 'RAV4 Hybrid',
    year: 2024,
    price: 2400,
    fuel: 'Hibrit',
    transmission: 'Otomatik',
    km: '4.200 km',
    status: 'Müsait',
    category: 'SUV',
    img: 'https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?w=600&q=80&auto=format',
  },
  {
    id: 4,
    brand: 'Audi',
    model: 'A6 3.0 TDI',
    year: 2023,
    price: 3500,
    fuel: 'Dizel',
    transmission: 'Otomatik',
    km: '18.900 km',
    status: 'Müsait',
    category: 'Sedan',
    img: 'https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=600&q=80&auto=format',
  },
  {
    id: 5,
    brand: 'Tesla',
    model: 'Model 3 LR',
    year: 2024,
    price: 4200,
    fuel: 'Elektrik',
    transmission: 'Otomatik',
    km: '6.800 km',
    status: 'Müsait',
    category: 'Sedan',
    img: 'https://images.unsplash.com/photo-1560958089-b8a1929cea89?w=600&q=80&auto=format',
  },
  {
    id: 6,
    brand: 'Volkswagen',
    model: 'Passat 2.0 TDI',
    year: 2022,
    price: 1800,
    fuel: 'Dizel',
    transmission: 'Manuel',
    km: '42.100 km',
    status: 'Kirada',
    category: 'Sedan',
    img: 'https://images.unsplash.com/photo-1541443131876-f8ef843b2b45?w=600&q=80&auto=format',
  },
  {
    id: 7,
    brand: 'Ford',
    model: 'Explorer ST',
    year: 2023,
    price: 2600,
    fuel: 'Benzin',
    transmission: 'Otomatik',
    km: '9.350 km',
    status: 'Müsait',
    category: 'SUV',
    img: 'https://images.unsplash.com/photo-1494976388531-d1058494cdd8?w=600&q=80&auto=format',
  },
  {
    id: 8,
    brand: 'Volvo',
    model: 'XC60 B4',
    year: 2023,
    price: 3100,
    fuel: 'Hibrit',
    transmission: 'Otomatik',
    km: '15.600 km',
    status: 'Kirada',
    category: 'SUV',
    img: 'https://images.unsplash.com/photo-1526726538690-5cbf956ae2fd?w=600&q=80&auto=format',
  },
  {
    id: 9,
    brand: 'Hyundai',
    model: 'Tucson 1.6T',
    year: 2024,
    price: 1950,
    fuel: 'Benzin',
    transmission: 'Otomatik',
    km: '2.100 km',
    status: 'Müsait',
    category: 'SUV',
    img: 'https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=600&q=80&auto=format',
  },
  {
    id: 10,
    brand: 'BMW',
    model: 'X5 xDrive40i',
    year: 2022,
    price: 4800,
    fuel: 'Benzin',
    transmission: 'Otomatik',
    km: '33.900 km',
    status: 'Müsait',
    category: 'SUV',
    img: 'https://images.unsplash.com/photo-1617914309185-9e274f450e40?w=600&q=80&auto=format',
  },
];

let filteredVehicles = [...vehicles];
let deleteTargetId   = null;

// ─── Fuel Icons ──────────────────────────────
const fuelIcon = {
  Benzin:    'fa-gas-pump',
  Dizel:     'fa-fill-drip',
  Elektrik:  'fa-bolt',
  Hibrit:    'fa-leaf',
};

// ─── Render Cards ─────────────────────────────
function renderVehicles(list) {
  const grid = document.getElementById('vehicleGrid');
  const empty = document.getElementById('emptyState');
  const visibleCount = document.getElementById('visibleCount');
  const resultsText  = document.getElementById('resultsText');

  if (!grid) return;

  if (list.length === 0) {
    grid.innerHTML = '';
    if (empty) empty.style.cssText = 'display:block!important;';
    if (visibleCount) visibleCount.textContent = '0';
    if (resultsText) resultsText.innerHTML = 'Araç <strong>bulunamadı</strong>';
    return;
  }

  if (empty) empty.style.cssText = 'display:none!important;';
  if (visibleCount) visibleCount.textContent = list.length;
  if (resultsText) resultsText.innerHTML = `Toplam <strong>${list.length}</strong> araç listeleniyor`;

  grid.innerHTML = list.map((v, idx) => `
    <div class="col-12 col-sm-6 col-xl-4 vehicle-col" style="animation-delay:${idx * 0.05}s">
      <div class="vehicle-card" data-id="${v.id}">

        <div class="vehicle-img-wrap">
          <img src="${v.img}" alt="${v.brand} ${v.model}" loading="lazy"
               onerror="this.src='https://placehold.co/600x340/e2e8f0/94a3b8?text=${v.brand}'" />
          <span class="vehicle-status ${v.status === 'Müsait' ? 'status-available' : 'status-rented'}">
            <i class="fa-solid ${v.status === 'Müsait' ? 'fa-circle-check' : 'fa-circle-dot'} me-1"></i>${v.status}
          </span>
          <span class="vehicle-fuel">
            <i class="fa-solid ${fuelIcon[v.fuel] || 'fa-gas-pump'}"></i> ${v.fuel}
          </span>
        </div>

        <div class="vehicle-body">
          <div class="vehicle-brand">${v.brand}</div>
          <div class="vehicle-name">${v.model}</div>

          <div class="vehicle-specs">
            <div class="spec-item">
              <i class="fa-solid fa-road"></i>
              <span>${v.km}</span>
            </div>
            <div class="spec-item">
              <i class="fa-solid fa-gears"></i>
              <span>${v.transmission}</span>
            </div>
            <div class="spec-item">
              <i class="fa-solid fa-tag"></i>
              <span>${v.category}</span>
            </div>
            <div class="spec-item">
              <i class="fa-solid fa-calendar"></i>
              <span>${v.year}</span>
            </div>
          </div>

          <div class="vehicle-price-row">
            <div class="vehicle-price">
              <span class="price-amount">₺${v.price.toLocaleString('tr-TR')}</span>
              <span class="price-label">/ günlük</span>
            </div>
            <span class="vehicle-year-badge">${v.year}</span>
          </div>
        </div>

        <div class="vehicle-actions">
          <button class="btn-action btn-detail" onclick="viewDetail(${v.id})" title="Detay">
            <i class="fa-solid fa-eye"></i> Detay
          </button>
          <button class="btn-action btn-edit" onclick="editVehicle(${v.id})" title="Düzenle">
            <i class="fa-solid fa-pen-to-square"></i> Düzenle
          </button>
          <button class="btn-action btn-delete" onclick="openDeleteModal(${v.id})" title="Sil">
            <i class="fa-solid fa-trash-can"></i> Sil
          </button>
        </div>

      </div>
    </div>
  `).join('');
}

// ─── Filter Logic ─────────────────────────────
function filterVehicles() {
  const search = (document.getElementById('searchInput')?.value || '').toLowerCase().trim();
  const brand  = document.getElementById('brandFilter')?.value  || '';
  const status = document.getElementById('statusFilter')?.value || '';
  const fuel   = document.getElementById('fuelFilter')?.value   || '';

  filteredVehicles = vehicles.filter(v => {
    const matchSearch = !search ||
      v.brand.toLowerCase().includes(search) ||
      v.model.toLowerCase().includes(search) ||
      String(v.year).includes(search) ||
      v.category.toLowerCase().includes(search);

    const matchBrand  = !brand  || v.brand  === brand;
    const matchStatus = !status || v.status === status;
    const matchFuel   = !fuel   || v.fuel   === fuel;

    return matchSearch && matchBrand && matchStatus && matchFuel;
  });

  renderVehicles(filteredVehicles);
}

function resetFilters() {
  const ids = ['searchInput', 'brandFilter', 'statusFilter', 'fuelFilter'];
  ids.forEach(id => { const el = document.getElementById(id); if (el) el.value = ''; });
  filteredVehicles = [...vehicles];
  renderVehicles(filteredVehicles);
  showToast('Filtreler sıfırlandı', 'info');
}

// ─── Action Handlers ──────────────────────────
function viewDetail(id) {
  const v = vehicles.find(x => x.id === id);
  if (!v) return;
  showToast(`${v.brand} ${v.model} detayları görüntüleniyor`, 'info');
}

function editVehicle(id) {
  const v = vehicles.find(x => x.id === id);
  if (!v) return;
  showToast(`${v.brand} ${v.model} düzenleme formu açıldı`, 'warning');
}

function openDeleteModal(id) {
  deleteTargetId = id;
  const v = vehicles.find(x => x.id === id);
  const nameEl = document.getElementById('deleteCarName');
  if (nameEl && v) nameEl.textContent = `${v.brand} ${v.model}`;

  const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
  modal.show();
}

function confirmDelete() {
  if (deleteTargetId === null) return;
  const idx = vehicles.findIndex(x => x.id === deleteTargetId);
  if (idx > -1) {
    const name = `${vehicles[idx].brand} ${vehicles[idx].model}`;
    vehicles.splice(idx, 1);
    filteredVehicles = filteredVehicles.filter(x => x.id !== deleteTargetId);
    updateCounters();
    renderVehicles(filteredVehicles);
    showToast(`${name} başarıyla silindi`, 'danger');
  }
  deleteTargetId = null;
  bootstrap.Modal.getInstance(document.getElementById('deleteModal'))?.hide();
}

// ─── Counter Update ───────────────────────────
function updateCounters() {
  const total     = vehicles.length;
  const available = vehicles.filter(v => v.status === 'Müsait').length;
  const rented    = vehicles.filter(v => v.status === 'Kirada').length;

  animateCount('totalCount',     total);
  animateCount('availableCount', available);
  animateCount('rentedCount',    rented);
}

function animateCount(id, target) {
  const el = document.getElementById(id);
  if (!el) return;
  const start    = parseInt(el.textContent) || 0;
  const duration = 400;
  const step     = (target - start) / (duration / 16);
  let current    = start;
  const timer = setInterval(() => {
    current += step;
    if ((step > 0 && current >= target) || (step < 0 && current <= target)) {
      el.textContent = target;
      clearInterval(timer);
    } else {
      el.textContent = Math.round(current);
    }
  }, 16);
}

// ─── Window Resize ────────────────────────────
window.addEventListener('resize', () => {
  if (!isMobile()) {
    const sidebar  = document.getElementById('sidebar');
    const overlay  = document.getElementById('sidebarOverlay');
    if (sidebar) sidebar.classList.remove('mobile-open');
    if (overlay) overlay.style.display = 'none';
  }
});

// ─── Init ─────────────────────────────────────
document.addEventListener('DOMContentLoaded', () => {
  restoreSidebarState();
  setCurrentDate();

  // Only render vehicles on vehicle-list page
  if (document.getElementById('vehicleGrid')) {
    renderVehicles(vehicles);
    updateCounters();
  }

  // Keyboard: ESC closes mobile sidebar
  document.addEventListener('keydown', e => {
    if (e.key === 'Escape') closeSidebarMobile();
  });

  // Animate stats on dashboard
  const statNums = document.querySelectorAll('.stat-card [style*="font-size:26px"]');
  statNums.forEach(el => {
    const target = parseInt(el.textContent.replace(/\D/g, '')) || 0;
    if (target > 0 && target < 10000) {
      el.textContent = '0';
      setTimeout(() => animateCount(el.id, target), 300);
    }
  });
});
