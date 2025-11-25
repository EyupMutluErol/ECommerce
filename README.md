# 🛒 E-Ticaret Projesi (ASP.NET Core MVC)

Bu proje, **ASP.NET Core MVC** kullanılarak geliştirilmiş bir e-ticaret uygulamasıdır.  
Ürün yönetimi, kullanıcı işlemleri, sepet fonksiyonları ve ödeme entegrasyonu gibi temel e-ticaret özellikleri içerir.

---

## 🚀 Özellikler

- Kullanıcı kayıt & giriş sistemi  
- Ürün listeleme ve kategori yönetimi  
- Sepet yönetimi  
- Sipariş oluşturma  
- Ödeme sistemi entegrasyonu  
- Admin paneli (ürün & kategori ekleme/silme/güncelleme)

---

## 🧰 Kullanılan Teknolojiler

- **Backend:** ASP.NET Core MVC  
- **Veritabanı:** MSSQL  
- **ORM:** Entity Framework Core  
- **Frontend:** HTML, CSS, Bootstrap  
- **Ödeme Servisi:**  Iyzico

---

## 🗄️ Veritabanı ve API Anahtarları Hakkında

Güvenlik nedeniyle:

- Veritabanı bağlantı bilgileri  
- Ödeme işlemleri için gerekli **API Key** ve **Secret Key** değerleri  

GitHub’a yüklenirken projeden kaldırılmıştır.

Bu nedenle projeyi çalıştırmadan önce **kendi bilgilerinizi eklemeniz gerekmektedir.**

---

## 🔐 `appsettings.json` Dosyasını Yapılandırma

Projeyi çalıştırmadan önce aşağıdaki alanları **kendi veritabanı adınız ve API anahtarlarınızla** güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=VERITABANI_ADINIZ;Trusted_Connection=True;"
  },
  "PaymentSettings": {
    "ApiKey": "ODEME_SERVISI_API_KEY",
    "SecretKey": "ODEME_SERVISI_SECRET_KEY"
  }
}
