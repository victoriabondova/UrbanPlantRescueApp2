# 🌿 Urban Plant Rescue App - Пълна Техническа Документация

## 📖 За проекта
**Urban Plant Rescue App** е уеб базирано приложение, създадено с **ASP.NET Core 8**, което има за цел да свързва любителите на растенията и да помага 
за спасяването на градската флора. Потребителите могат да докладват за растения в нужда, да подават заявки за тяхното спасяване, да коментират и да следят тяхното състояние.  
Проектът демонстрира умения за работа с релационни бази данни, сигурност, CRUD операции и модерен UI дизайн.

---

## 🚀 Технологичен Стек
Проектът използва най-модерните стандарти в разработката на уеб приложения:
### Backend:
* **Framework:** ASP.NET Core 8
* **Language:** C# 12
* **Architecture:** Model-View-Controller (MVC)
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Tests:** xUnit + Microsoft.EntityFrameworkCore.InMemory
### Frontend:
* **Engine:** Razor Views
* **Styling:** Bootstrap 5 (Responsive Design)(мобилно-адаптивен интерфейс)
* **UX:** Custom CSS анимации и Card-based дизайн
### Сигурност:
* **Identity:** ASP.NET Core Identity (Управление на потребители, пароли и роли)

---

## 📦 Използвани NuGet Пакети
* `Microsoft.EntityFrameworkCore.SqlServer` – Свързване със SQL Server.
* `Microsoft.EntityFrameworkCore.Tools` – Команди за миграция (Add-Migration, Update-Database).
* `Microsoft.AspNetCore.Identity.EntityFrameworkCore` – Система за автентикация.
* `Microsoft.EntityFrameworkCore.Design` - Инструменти за миграции.
* `Microsoft.EntityFrameworkCore.InMemory` - За тестване с In-Memory база данни.
* `Moq` - За създаване на mock обекти в unit тестовете.

---

## 🏗️ Архитектура
Проектът следва архитектурния модел **Model-View-Controller (MVC)** и е разделен на 4 логически слоя:
* **UrbanPlantRescueApp.Data** - Entity модели, DbContext, Entity конфигурации, миграции и валидационни константи
* **UrbanPlantRescueApp.Services** - Бизнес логика, сервизни интерфейси и ViewModels
* **UrbanPlantRescueApp.Web** - MVC контролери, Razor Views, Areas, Program.cs и статични файлове
* **UrbanPlantRescueApp.Tests** - Unit тестове с xUnit (26 теста)
### 1.Entity модели
* **Plant** — Растение с име, описание, снимка, категория и собственик
* **Category** — Категория за класификация на растенията
* **RescueRequest** — Заявка за спасяване на растение
* **Comment** — Коментар към растение
* **PlantCondition** — Докладвано състояние на растение
* **UserProfile** — Профил на потребителя
### 2.Контролери
* **PlantController** — Управлява жизнения цикъл на растенията (CRUD)
* **CategoryController** — Администрира категориите и филтрирането
* **RescueRequestController** — Обработва заявките за спасяване
* **CommentController** — Управлява коментарите към растенията
* **PlantConditionController** — Следи и обновява състоянието на растенията
* **UserProfileController** — Управлява потребителските профили
* **Admin/RescueRequestController** — Административен панел за управление на заявки

---

## 💾 База Данни
Системата използва осем основни релации:
1. **Category ↔ Plants:** One-to-Many (Една категория съдържа много растения).
2. **Plant ↔ RescueRequests:** One-to-Many (Едно растение може да има много заявки за спасяване).
3. **Plant ↔ Comments:** One-to-Many (Едно растение има много коментари).
4. **Plant ↔ PlantConditions:** One-to-Many (Едно растение има много докладвани състояния).
5. **IdentityUser ↔ Plants:** One-to-Many (Един потребител може да добави много растения).
6. **IdentityUser ↔ RescueRequests:** One-to-Many (Един потребител може да има много заявки).
7. **IdentityUser ↔ Comments:** One-to-Many (Един потребител може да има много коментари).
8. **IdentityUser ↔ UserProfile:** One-to-One (един потребител има един профил).

---

## 🔒 Сигурност
* ASP.NET Core Identity с роли (Admin и User)
* AntiForgeryToken защита на всички форми
* Валидация server-side и client-side
* Защита от XSS чрез Razor encoding
* Owner-based authorization
* Custom 404 и 500 страници

---

## ✨ Функционалности

### 🌿 Управление на растения (CRUD)
* Добавяне на нови растения със заглавие, описание, категория, URL на снимка и автоматично записване на собственика
* Редактиране и изтриване само от собственика на растението или Administrator
* Детайлен преглед на всяко растение с история на заявките, коментарите и състоянието
### 📂 Категоризация
* Динамично управление на категории (Стайни растения, Сукуленти, Билки и др.)
* Всяка категория води към списък с растенията от нея
* Защита срещу дублиращи се категории
### 📩 Заявки за спасяване
* Логнатите потребители могат да изпращат заявки за спасяване на конкретни растения
* Проследяване на статуса на заявката (В изчакване / Одобрено)
* Admin може да одобрява заявките от Admin панела
* Собственикът не може да заявява спасяване на собственото си растение
### 💬 Коментари
* Логнатите потребители могат да коментират всяко растение
* Авторът и Admin могат да изтриват коментари
* Показване на автора и датата на коментара
### 🌡️ Състояние на растението
* Собственикът и Admin могат да докладват текущото състояние на растението
* Пет нива на състояние: Критично, Лошо, Стабилно, Добро, Възстановено
* Цветово кодирани статуси за по-добра визуализация
### 👤 Потребителски профил
* Персонализиран профил с име, фамилия, биография и дата на създаване на профила
* Статистика за добавени растения и заявки за спасяване
* Достъпен чрез клик върху потребителското име в навигацията
### 🔍 Търсене и Pagination
* Търсене по име, описание и категория на растението
* Pagination на резултатите по 6 растения на страница
* Запазване на search term при смяна на страница
### 🛡️ Admin Area
* Отделна зона достъпна само за Administrator
* Преглед на всички заявки за спасяване
* Одобряване на заявки за спасяване
### 🔒 Сигурност и роли
* Две роли: **Admin** и **User** с автоматичен seeding при стартиране
* Само логнати потребители могат да добавят растения, коментари и заявки
* Owner-based authorization — само собственикът редактира/трие своите записи
* AntiForgeryToken на всички форми
* Валидация с Data Annotations ([Required], [MaxLength], [MinLength])
* Custom 404 и 500 страници
* Защита от XSS чрез Razor автоматичен encoding

---

## 🧪 Unit тестове
Проектът има **26 unit теста** покриващи основната бизнес логика:
* `PlantServiceTests` — 8 теста
* `CategoryServiceTests` — 5 теста
* `CommentServiceTests` — 5 теста
* `RescueRequestServiceTests` — 4 теста
* `PlantConditionServiceTests` — 4 теста

---

## 👩‍💻 Роли
* **Admin** — Email: `admin@urbanplant.com` | Парола: `Admin123!`
* **User** — Регистрирай се от страницата за регистрация

---

## 📸 Екрани снимки (Галерия)
### Начална Страница(без логнат потрербител)
![Home Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/home.png)

### Начална Страница(с логнат потребител)
![Home Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/homeLogin.png)

### Страница за политика на поверителност
![Privacy Policy Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/privacyPolicy.png)

### Страница за регистрация
![Register Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/register.png)

### Страница за вход
![Login Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/login.png)

### Категории(без логнат потребител)
![Categories Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/categories.png)

### Категории(с логнат потребител)
![Categories Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/categoriesLogin.png)

### Избрана категория
![Categories Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/selectedcategories.png)

### Добавяне на категории
![Categories Add Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/addCategories.png)

### Списък с Растения(без логнат потребител)
![Plant Index](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plants.png)

### Списък с Растения(с логнат потребител)
![Plant Index](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantslogin.png)

### Добавяне на Растение
![Plant Add Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/addPlants.png)

### Детайли за Растение(без логнат потребител)
![Plant Details Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantDetailsNotlogin.png)
(./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantDetailsNotlogin2.png)

### Детайли за Растение(с логнат потребител - собственик)
![Plant Details Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantDetails.png)
(./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantDetails2.png)

### Детайли за Растение(с логнат потребител - друг потебител)
![Plant Details Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantDetailsOther.png)
(./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantDetailsOther2.png)

### Редактиране на Растение
![Plant Edit Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantEdit.png)

### Изтриване на Растение и заявките му
![Plant Delete Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/plantDelete.png)

### Профил на потребител
![UserProfile Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/userProfile.png)

### Редактиране на потребителски профил
![UserProfile Edit Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/userProfileEdit.png)

### Административен панел за заявки за спасяване
![Admin Area Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/adminArea.png)

### Страница на грешка 404
![Not Found Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/pageNotFound.png)

### Страница на грешка 500
![Server Error Page](./UrbanPlantRescueApp.Web/wwwroot/images/screenshots/pageServerError.png)

---

## 🛠️ Инсталация и стартиране
### Изисквания
* .NET 8 SDK
* Microsoft SQL Server
* Visual Studio 2022/2026
### Стъпки
1. Клонирай репото:
```bash
git clone https://github.com/victoriabondova/UrbanPlantRescueApp2.git
```
2. Конфигурирай connection string в `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=UrbanPlantRescueApp2Db;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
  }
}
```
3. Приложи миграциите:
```bash
dotnet ef database update --project UrbanPlantRescueApp.Data --startup-project UrbanPlantRescueApp.Web
```
4. Стартирай проекта:
```bash
dotnet run --project UrbanPlantRescueApp.Web
```
За стартиране на тестовете:
```bash
dotnet test
```

---

## 👩‍💻 Автор
**Виктория Бондова**
* GitHub: [@victoriabondova](https://github.com/victoriabondova)
* Проектът е създаден с учебна цел за усвояване на ASP.NET Advanced.