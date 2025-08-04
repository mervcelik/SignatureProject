# RentACar Project 🚗  
Bu proje, [Engin Demiroğ](https://www.udemy.com/user/engindemiro/) tarafından Udemy platformunda sunulan  
**C# .NET Core Clean Architecture & CQRS** eğitim serisinin tamamı (1-4. bölümler) kullanılarak geliştirilmiştir.

## 📚 Eğitim Kaynağı
**C# .NET Core Clean Architecture & CQRS Proje Altyapı Kursu (Bölüm 1)**  
🔗 [Udemy Kurs Linki](https://www.udemy.com/course/c-net-core-clean-architecture-cqrs-proje-altyap-kursu-1)

Bu projede aşağıdaki konular uygulanarak geliştirildi:
- DDD (Domain Driven Design) odaklı Clean Architecture
- SOLID ve Clean Code prensipleri
- Dynamic Search Implementasyonu
- Gelişmiş Entity Framework (EF Core) altyapısı ve Best Practices
- CQRS (Command Query Responsibility Segregation) mimarisi
- Senkron & Asenkron Repository yapıları
- Response Request Pattern (AutoMapper)
- API Katmanı ve servisler

## 🏗️ Proje Yapısı
Bu proje, **nArchitecture.Core** altyapısını kullanarak geliştirilmiştir.  
Katmanlar aşağıdaki şekilde yapılandırılmıştır:

- `RentACar.Domain` 
- `RentACar.Application`
- `RentACar.Persistence`
- `RentACar.Infrastructure` 
- `RentACar.WebAPI`

## 🔧 Kullanılan Teknolojiler
- ASP.NET Core Web API
- EF Core
- AutoMapper
- FluentValidation
- PostgreSQL / SQL Server
- MediatR (CQRS altyapısı için)
- JWT Authentication

## 🚀 Başlamak İçin
Projeyi klonlayın:

```bash
git clone https://github.com/kullaniciadi/RentACar.git
cd RentACar
