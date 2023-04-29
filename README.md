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
- Товары
- Заказы
- Связи

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
### Первый способ (Docker)
Перейти в папку проекта(ProjectFolder):
```sh
 cd ./ProjectFolder
```
Клонировать репозиторий:
```sh
 git clone https://github.com/klawiat/Orders.git
```
Выполнить команду:
```sh
 docker compose up
```
### Второй способ (IIS)
Повторить шаги 1,2 из первого способа
Перейти в папку backend
Открыть решение в Visual Studio
Далее следовать инструкции написанной на [сайте](https://metanit.com/sharp/aspnet5/20.1.php)
### Третий способ
Снова повторить 1-2 шаги из первого способа
Выполнить команду 
```sh
dotnet restore "backend/Orders.WebApi/Orders.WebApi.csproj"
```
Далее следует команда
```sh
dotnet publish "backend/Orders.WebApi/Orders.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
``` 
Запустить проект из папки 
backend/Orders.WebApi/bin/publish/net6.0/Orders.WebApi.exe
### Четвертый способ (Visual Studio)
Повторить шаги 1-2 из первого способа
Повторить шаг 1 из третьего способа
Открыть файл решения в Visual Studio
В файле programm.cs следует исправить поля: host, port, database, username, password на те, что применены к вашему серверу с бд.
Или дописать в файл appsettings.json следующую строку
```js
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
+  "ConnectionStrings": { "DefaultConnection": "{Строка подключения}" }
}

```
Запустить проект Orders.WebApi на своем пк

