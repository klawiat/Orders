# Api создания заказов

Последняя версия api разработанного в качестве тестового задания на позицию Trainee .Net developer
При реализации сервиса использовались следующие технологии:
- [asp.net web api](https://learn.microsoft.com/en-gb/aspnet/core/fundamentals/apis?view=aspnetcore-7.0)
- [asp.net dependency injection framework](https://learn.microsoft.com/en-gb/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0)
- [Entity Framework](https://learn.microsoft.com/en-gb/aspnet/core/data/entity-framework-6?view=aspnetcore-7.0)
- [postgresql](https://www.postgresql.org/) и [npgsql](https://www.npgsql.org/)
- [swagger](https://swagger.io/)
- [automapper](https://automapper.org/)

## Возможности

- Добавление, редактирование, удаление и просмотр товаров
- Добавление, редактирование, удаление и просмотр заказов
- Автоматическое создание документа swagger (Доступен по ссылке "../swagger/index.html")
- Облегчено добавление нового функционала благодаря использованию подхода "Clean architecture"
- При наличии установленного довера возможно протестировать api вписав всего одну команду

## База данных
 Состоит из 3 таблиц
- Заказы
- Продукты

Схема описанна в файлах миграции на слое "infrastructure"
## Статусы заказа
1. New
2. AwaitingPayment
3. Paid
4. SubmittedForDelivery
5. Delivered
6. Completed

Если при редактировании заказа вписать невалидное значение, то статус установится на дефолтное значение(New)
## Развертывание
Прежде чем приступить нужно клонировать репозитрий в локальную папку командой:
```sh
 git clone https://github.com/klawiat/Orders.git
```
### Первый способ (Docker)
Из папки проекта выполнить команду:
```sh
 docker compose up
```
### Второй способ (Visual Studio)
1. Открыть файл решения в Visual Studio (.\backend\Orders\Orders.sln)

2. В файле programm.cs следует исправить поля: host, port, database, username, password на те, что применены к вашему серверу с бд.

3. Или убрать комментарии в файле appsettings.json и заменить значения в фигурных скобках на ваши
```js
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    //"DefaultConnection": "Host={host};Port={port};Database={database};Username={username};Password={password};"
  }
}
```
Запустить проект Orders.WebApi на своем пк кнопкой run
### Третий способ
Повторить шаг 2 или 3 из второго способа
Выполнить команду 
```sh
dotnet restore "backend/Orders.WebApi/Orders.WebApi.csproj"
```
Далее следует команда
```sh
dotnet run --project ".\Orders\Orders.WebApi\Orders.WebApi.csproj" --launch-profile "Orders.WebApi"
``` 
