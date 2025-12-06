# 🚌 Personel Servis Takip Sistemi

![Project Status](https://img.shields.io/badge/status-tamamland%C4%B1-green)
![Language](https://img.shields.io/badge/language-C%23-blue)
![Framework](https://img.shields.io/badge/framework-.NET-purple)

Bu proje, şirket veya kurumların personel servis süreçlerini dijital ortamda yönetmelerini sağlayan, **C#** dili ile geliştirilmiş bir otomasyon sistemidir. Personel kayıtları, servis güzergahları ve araç atamaları gibi işlemleri tek bir arayüzden yönetmeyi amaçlar.

## 📸 Ekran Görüntüleri

*(Ekran görüntüleri eklenecektir)*

## ✨ Özellikler

Bu proje aşağıdaki temel fonksiyonları içerir:

* **Personel Yönetimi:** Yeni personel ekleme, düzenleme ve silme (CRUD işlemleri).
* **Servis/Araç Tanımlama:** Servis araçlarının plakaları ve kapasite bilgileriyle sisteme kaydedilmesi.
* **Güzergah Takibi:** Hangi servisin hangi güzergaha gideceğinin belirlenmesi.
* **Atama İşlemleri:** Personellerin uygun servislere atanması.
* **Raporlama:** (Varsa) Servis listelerinin görüntülenmesi.
* **Kullanıcı Girişi:** Admin ve kullanıcı yetkilendirme sistemi.

## 🛠️ Kullanılan Teknolojiler

* **Programlama Dili:** C#
* **Arayüz (UI):** Windows Forms (WinForms) / [.NET Core]
* **Veritabanı:** MSSQL (SQL Server) / [SQLite]
* **IDE:** Visual Studio

## 🚀 Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/BaranDpp/Personel-Servis-Takip.git](https://github.com/BaranDpp/Personel-Servis-Takip.git)
    ```

2.  **Veritabanı Bağlantısı:**
    * Proje içerisinde yer alan `App.config` (veya veritabanı sınıfındaki) **Connection String**'i kendi yerel SQL Server ayarlarınıza göre güncelleyin.
    * *(Eğer projenin içinde bir .sql script dosyası varsa)* `Database` klasöründeki scripti SQL Server'da çalıştırarak tabloları oluşturun.

3.  **Projeyi Başlatın:**
    * Visual Studio ile `PersonelServisTakip.sln` dosyasını açın.
    * **Start** butonuna basarak uygulamayı çalıştırın.

## 🤝 Katkıda Bulunma

Bu bir okul projesidir, ancak geliştirmek isterseniz Pull Request gönderebilirsiniz.

1.  Fork'layın.
2.  Yeni bir dal (branch) oluşturun (`git checkout -b feature/Yeni
