# Репозиторий проекта BankAssistanceSystem

## Технологии Приложения:
* MS .NET 5 Core
* WPF (MVVM)
* Entity Framework Core 6

## Технологии Сайта:
* MS .NET 5 Core
* ASP.NET (MVC)
* Entity Framework Core 6

## База данных:
* MS SQL
* Файл с базой данной расположен в "src/shared/DataBase"
* Необходимо подключить на сервер - (localdb)\MSSQLLocalDB или же сменить путь подключения в:
    * Для Приложения "src/bas.program.prj/Models/BankDbContext.cs", поле "_connectionString"
    * Для Сайта "src/bas.website.prj/appsettings.Project.json", поле "Connection"

### Сриншоты Приложения:

![alt text](screenshots/Hello.png "Авторизация")
![alt text](screenshots/Main.png "Главное меню с Админ окнами")
![alt text](screenshots/Main2.png "Главное меню с Профилями")
![alt text](screenshots/Main3.png "Главное меню")


### Сриншоты Сайта:

![alt text](screenshots/SMain.png "Главаная страница сайта")
![alt text](screenshots/SMain2.png "Главаная страница калькулятора")
![alt text](screenshots/SMain3.png "Главаная страница калькулятора (авторизация пройдена)")
![alt text](screenshots/SMain4.png "Страница результата расчета")