# UnitOfWork Tasarım Deseni ve WebAPI 🛠️

## Genel Bakış

Bu proje, 'Category' ve 'Product' varlıklarını kullanarak UnitOfWork tasarım desenini bir WebAPI ile gösterir. UnitOfWork deseni, birden fazla repository ile yapılan değişiklikleri tek bir işlemde yönetir ve tutarlılığı sağlar.

## UnitOfWork Deseni Nedir? 🤔

UnitOfWork deseni, veritabanı işlemlerini yönetmek için kullanılan bir tasarım desenidir. İşlem sırasında etkilenmiş nesneleri tutar ve değişikliklerin yazılmasını koordine eder. Temel amacı, tüm değişikliklerin tek bir işlemde yapılmasını sağlayarak tutarlılığı ve geri alma (rollback) yeteneğini sunmaktır. 🔄

## Nasıl Çalışır? ⚙️

1. **UnitOfWork Sınıfı**: Repository'lerin referanslarını tutar ve işlem yaşam döngüsünü yönetir. Değişiklikleri veritabanına kaydetmek için `Complete` metodunu ve değişiklikleri iptal etmek için `Dispose` metodunu sağlar. 

2. **Repository'ler**: Belirli varlıklar (örneğin `Category` ve `Product`) için veri erişim mantığını kapsüller. Her repository, ilgili varlık için CRUD (Oluşturma, Okuma, Güncelleme, Silme) işlemlerini yönetir.

3. **WebAPI Controller'ları**: UnitOfWork kullanarak repository'lerle etkileşime girerler. Değişiklikleri kaydetmek için `Complete` metodunu çağırır veya bir hata durumunda işlemi yönetirler.
