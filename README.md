# 🚗 Rosetaly — Lüks ve Konforlu Araç Kiralama Platformu

Rosetaly, C# ve .NET 8 teknolojileriyle geliştirilen; müşteri tarafında araç keşfi ve kiralama talebi oluşturma, yönetim tarafında ise filo, rezervasyon ve günlük operasyonları takip etme olanağı sunan bir ASP.NET Core MVC araç kiralama uygulamasıdır. Proje, katmanlı mimari yaklaşımıyla sorumlulukları birbirinden ayırırken, rezervasyon süreçlerini iş kuralları doğrultusunda yönetmeyi ve yönetim panelinde operasyonel görünürlüğü artırmayı hedefler.

---

## ✨ Öne Çıkan Özellikler

### 🚘 Müşteri Arayüzü
- Araçları listeleme, detaylarını görüntüleme ve ana sayfada son eklenen en fazla 10 aktif aracı sergileme.
- Marka, model, kategori, yakıt türü, vites, koltuk sayısı, bagaj kapasitesi ve günlük ücret filtreleme/arama seçenekleri.
- Kategori ve kiralama tarih aralığına göre müsaitlik akışı ve tarih çakışmalarını kontrol etme.
- Alış/iade tarih, saat ve şube seçimleriyle kiralama talebi oluşturma; müşteri ve sürücü bilgilerini rezervasyonla ilişkilendirme.

### 🛠️ Yönetim Paneli & Operasyon Takibi
- **Filo ve Kayıt Yönetimi:** Araç, marka, model, kategori, şube, müşteri ve rezervasyon kayıtlarını tek merkezden yönetme.
- **Canlı Operasyon Ekranı:** Günlük araç alış ve iade operasyonlarını ayrı bölümlerde takip etme; rezervasyon taleplerini inceleme ve onaylama.
- **Modüler Arayüz:** Yönetim paneli içeriğini ve KPI özet kartlarını yeniden kullanılabilir Partial View ve ViewModel bileşenleriyle optimize etme.

### 🤖 Operasyon Özeti ve Akıllı Yardımcı
- Rezervasyon onayında MailKit/MimeKit tabanlı e-posta gönderimi ve rezervasyona özel kupon görseli üretme altyapısı.
- Bekleyen talepler için tarih çakışmalarını değerlendirme, alternatif araç önerileri sunma ve günlük alış/iade/talep sayılarını bir araya getiren operasyon özeti.
- Doğrulanabilir iş kurallarını uygulama kodunda tutup metinsel brifing üretimini ayrı ele alan yapay zekâ destekli operasyon asistanı yaklaşımı.
- Dashboard performansını korumak amacıyla asistan bileşenini sayfa açılışından bağımsız, AJAX üzerinden asenkron yükleme mimarisi.

> **Not:** Yapay zekâ destekli özet, alternatif araç önerileri, e-posta/kupon akışı ve AJAX ile yükleme gibi özelliklerin kapsamı uygulamadaki mevcut entegrasyon seviyesine göre değerlendirilmelidir. Tamamlanmamış veya prototip durumundaki parçalar üretime hazır özellikler olarak değerlendirilmemelidir.

---

## 🧠 Mimari Yapı ve Geliştirme Prensipleri

Proje; arayüz, iş kuralları, veri erişimi, entity ve DTO sorumluluklarını katmanlı bir yapıda ayrıştırırken, endüstri standardı tasarım ve geliştirme prensipleriyle güçlendirilmiştir:

### Katmanlı Mimari (N-Tier Architecture)
- **Rosetaly.WebUI** — Sunum Katmanı: ASP.NET Core MVC controller'ları, Razor Views, yönetim alanı (Areas/Admin), ViewModel'ler ve kullanıcı arayüzü bileşenleri.
- **Rosetaly.BusinessLayer** — İş Katmanı: Servisler, iş kuralları ve uygulama akışlarına ait doğrulamalar.
- **Rosetaly.DataAccessLayer** — Veri Erişim Katmanı: Entity Framework Core context'i, repository uygulamaları ve veritabanı işlemleri.
- **Rosetaly.EntityLayer** — Entity Katmanı: `Car`, `Rental`, `Customer`, `Branch`, `Brand`, `CarModel` ve `Category` gibi veritabanı varlıkları.
- **Rosetaly.DtoLayer** — DTO Katmanı: Katmanlar arasında gerekli verileri taşımak için kullanılan veri transfer nesneleri.

### Tasarım Desenleri ve Teknik Prensipler
- **Repository Pattern & DI:** Veri erişim işlemlerini iş akışlarından ayırma ve Dependency Injection ile servis bağımlılıklarını yönetme.
- **DTO & AutoMapper:** Arayüzün ihtiyaç duyduğu verileri entity modellerinden izole etme ve nesne dönüşümlerini düzenleme.
- **EF Core, LINQ & Include/ThenInclude:** İlişkisel verileri optimize sorgularla veritabanından çekme.
- **Partial View & ViewModel:** Yönetim paneli gibi karmaşık ekranları yeniden kullanılabilir, modüler ve sorumlulukları ayrılmış parçalara bölme.
- **Asenkron İşlemler & AJAX:** Uygun ekranlarda verilerin sayfa akışını kesmeden asenkron olarak yüklenmesini destekleme.
- **FluentValidation & Özel Hata Yönetimi:** Form ve iş verileri için kuralları merkezi tanımlama; bulunamayan sayfalar için lüks konseptine uygun 404 hata deneyimi sunma.

---

## 📅 Rezervasyon ve Müsaitlik Kuralları
- Kiralama talebinde alış ve iade tarih/saatleri ile şube bilgileri toplanır.
- Toplam tutarın araç günlük ücreti ve kiralama süresi üzerinden hesaplanması hedeflenir.
- Müşteri bilgilerinin rezervasyon kaydıyla ilişkilendirilmesi sağlanır.
- Bekleyen talepler ile onaylanmış rezervasyonların farklı durumlar olarak ele alınması hedeflenir.
- Onaylama öncesinde aynı araç için tarih aralığı çakışmalarının kontrol edilmesi, aynı aracın çakışan tarihlerde birden fazla rezervasyona atanmasını önlemeye yönelik temel kuraldır.

---

## 🧰 Teknolojiler

| Alan | Teknolojiler |
| :--- | :--- |
| **Backend** | C#, .NET 8, ASP.NET Core MVC |
| **ORM ve veri erişimi** | Entity Framework Core 8, LINQ, Repository Pattern |
| **Veritabanı** | Microsoft SQL Server |
| **Nesne eşleme** | AutoMapper |
| **Validasyon** | FluentValidation |
| **E-posta** | MailKit, MimeKit |
| **Görsel işleme** | SixLabors.ImageSharp |
| **Frontend** | HTML5, CSS3, Bootstrap 5, JavaScript, jQuery |
| **Asenkron arayüz işlemleri** | AJAX / Fetch API (kullanılan ekranlara göre) |
| **Görsel arayüz yapısı** | Razor Views, Partial Views, ViewModel'ler | 


🖼️ Ekran Görüntüleri
<br><br>
<img src="Images/1.png" width="80%" />
<br><br>
<img src="Images/2.png" width="80%" />
<br><br>
<img src="Images/3.png" width="80%" />
<br><br>
<img src="Images/4.png" width="80%" />
<br><br>
<img src="Images/5.png" width="80%" />
<br><br>
<img src="Images/6.png" width="80%" />
<br><br>
<img src="Images/7.png" width="80%" />
<br><br>
<img src="Images/8.png" width="80%" />
<br><br>
<img src="Images/9.png" width="80%" />
<br><br>
<img src="Images/10.png" width="80%" />
<br><br>
<img src="Images/11.png" width="80%" />
<br><br>
<img src="Images/12.png" width="80%" />
<br><br>
<img src="Images/13.png" width="80%" />
<br><br>
<img src="Images/14.png" width="80%" />
<br><br>
<img src="Images/15.png" width="80%" />
<br><br>
<img src="Images/16.png" width="80%" />
<br><br>
<img src="Images/17.png" width="80%" />
<br><br>
<img src="Images/18.png" width="80%" />
<br><br>
<img src="Images/19.png" width="80%" />
<br><br>
<img src="Images/20.png" width="80%" />
<br><br>
<img src="Images/21.png" width="80%" />
<br><br>
<img src="Images/22.png" width="80%" />
<br><br>
<img src="Images/23.png" width="80%" />
<br><br>
<img src="Images/24.png" width="80%" />
<br><br>
<img src="Images/31.png" width="80%" />
<br><br>
<img src="Images/32.png" width="80%" />
<br><br>
<img src="Images/25.png" width="80%" />
<br><br>
<img src="Images/26.png" width="80%" />
<br><br>
<img src="Images/27.png" width="80%" />
<br><br>
<img src="Images/28.png" width="80%" />
<br><br>
<img src="Images/29.png" width="80%" />
<br><br>
<img src="Images/30.png" width="80%" />

<br><br>
