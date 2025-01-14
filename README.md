Öğrenci ve Ders Yönetim Sistemi - README Dosyası
Bu uygulama, öğrencilerin ders kaydı yapabileceği, öğretim görevlilerinin eklenip yönetilebileceği, derslerin görüntülenebileceği ve öğrenci bilgilerinin tutulabileceği bir Öğrenci ve Ders Yönetim Sistemi sağlar. Program, konsol tabanlı bir kullanıcı arayüzü ile çalışmaktadır.

Özellikler
Öğretim Görevlisi Ekleme

Yeni öğretim görevlileri sisteme eklenebilir.
Öğretim görevlilerine dersler atanabilir.
Öğretim Görevlisi ve Dersleri Silme

Seçilen öğretim görevlisi sistemden kaldırılır.
Bu öğretim görevlisine ait tüm dersler de sistemden silinir.
Öğrenci Ders Kaydı

Öğrenciler, mevcut derslere kaydedilebilir.
Öğrenci adı ve ID'si kayıt esnasında girilmelidir.
Dersleri ve Kayıtlı Öğrencileri Görüntüleme

Tüm dersler, öğretim görevlileri ve kayıtlı öğrenciler listelenebilir.
Dersten Öğrenci Silme

Belirli bir derste kayıtlı öğrencilerden birini silme işlemi yapılabilir.
Veri Kalıcılığı

Program JSON ve XML formatlarını destekler. Veriler students.json, instructors.json ve courses.json dosyalarında saklanır.
Kullanım
Sistemi Başlatma

Program çalıştırıldığında gerekli veri dosyaları otomatik olarak oluşturulur (eğer mevcut değilse).
Konsol Menüsü

Programın ana menüsünden seçim yapılarak işlemler gerçekleştirilebilir. Kullanıcıdan girdi istenirken yanlış girişler kontrol edilerek uygun bir değer girene kadar uyarı yapılır.
Teknik Detaylar
1. Sınıflar
Person: Tüm kişi türleri için temel sınıf.
Instructor: Öğretim görevlilerini temsil eder.
Student: Öğrencileri temsil eder.
Course: Dersleri temsil eder ve derslere kayıtlı öğrencileri tutar.
2. DataManager
Verilerin JSON ve XML formatında kaydedilmesi ve yüklenmesi için gerekli metotları içerir.
Dosya kontrolü ve hata yönetimi yapılmıştır.
Dosya Yapısı
students.json: Öğrenci bilgilerini tutar.
instructors.json: Öğretim görevlisi bilgilerini tutar.
courses.json: Ders bilgilerini ve kayıtlı öğrencileri tutar.
students.xml, instructors.xml, courses.xml: Aynı veriler XML formatında saklanır.
Önemli Notlar
Girdi doğrulama sistemleri, yanlış veri girişlerini önler.
Dosyalar otomatik olarak oluşturulsa da silinmeleri durumunda sistem çalışmaya devam edebilir, ancak önceki kayıtlar kaybolur.
JSON verileri bozulursa, program bunu tespit eder ve kullanıcıya bilgi verir.
Örnek Kullanım Senaryoları
Yeni Bir Öğretim Görevlisi ve Ders Eklemek

Ana menüden "1" seçeneği seçilir.
İsim ve ders bilgileri girildikten sonra bilgiler kaydedilir.
Bir Derse Öğrenci Kaydı Yapmak

Ana menüden "3" seçeneği seçilir.
Ders seçimi yapılır ve öğrenci bilgileri girilir.
Mevcut Dersleri Görüntülemek

Ana menüden "4" seçeneği seçilir.
Tüm dersler ve öğrenciler listelenir.
Bu sistem, temel bir öğrenci ve ders yönetimi ihtiyaçlarını karşılamak için tasarlanmıştır. Sistem, kullanıcı dostu bir arayüz ve veri yönetimi ile kolaylık sağlar.
