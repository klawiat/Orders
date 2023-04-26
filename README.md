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
- Состоит из 3 таблиц
-- Товары
-- Заказы
-- Связи
- Схема описанна в файлах миграции на слое "infrastructure"
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
В файле programm.cs следует исправить поля: host, port, database, username, password на те, что применены к вашему серверу с бд
Запустить проект Orders.WebApi на своем пк














Markdown is a lightweight markup language based on the formatting conventions
that people naturally use in email.
As [John Gruber] writes on the [Markdown site][df1]

> The overriding design goal for Markdown's
> formatting syntax is to make it as readable
> as possible. The idea is that a
> Markdown-formatted document should be
> publishable as-is, as plain text, without
> looking like it's been marked up with tags
> or formatting instructions.

This text you see here is *actually- written in Markdown! To get a feel
for Markdown's syntax, type some text into the left window and
watch the results in the right.

## Tech

Dillinger uses a number of open source projects to work properly:

- [AngularJS] - HTML enhanced for web apps!
- [Ace Editor] - awesome web-based text editor
- [markdown-it] - Markdown parser done right. Fast and easy to extend.
- [Twitter Bootstrap] - great UI boilerplate for modern web apps
- [node.js] - evented I/O for the backend
- [Express] - fast node.js network app framework [@tjholowaychuk]
- [Gulp] - the streaming build system
- [Breakdance](https://breakdance.github.io/breakdance/) - HTML
to Markdown converter
- [jQuery] - duh

And of course Dillinger itself is open source with a [public repository][dill]
 on GitHub.

## Installation

Dillinger requires [Node.js](https://nodejs.org/) v10+ to run.

Install the dependencies and devDependencies and start the server.

```sh
cd dillinger
npm i
node app
```

For production environments...

```sh
npm install --production
NODE_ENV=production node app
```

## Plugins

Dillinger is currently extended with the following plugins.
Instructions on how to use them in your own application are linked below.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |

## Development

Want to contribute? Great!

Dillinger uses Gulp + Webpack for fast developing.
Make a change in your file and instantaneously see your updates!

Open your favorite Terminal and run these commands.

First Tab:

```sh
node app
```

Second Tab:

```sh
gulp watch
```

(optional) Third:

```sh
karma test
```

#### Building for source

For production release:

```sh
gulp build --prod
```

Generating pre-built zip archives for distribution:

```sh
gulp build dist --prod
```

## Docker

Dillinger is very easy to install and deploy in a Docker container.

By default, the Docker will expose port 8080, so change this within the
Dockerfile if necessary. When ready, simply use the Dockerfile to
build the image.

```sh
cd dillinger
docker build -t <youruser>/dillinger:${package.json.version} .
```

This will create the dillinger image and pull in the necessary dependencies.
Be sure to swap out `${package.json.version}` with the actual
version of Dillinger.

Once done, run the Docker image and map the port to whatever you wish on
your host. In this example, we simply map port 8000 of the host to
port 8080 of the Docker (or whatever port was exposed in the Dockerfile):

```sh
docker run -d -p 8000:8080 --restart=always --cap-add=SYS_ADMIN --name=dillinger <youruser>/dillinger:${package.json.version}
```

> Note: `--capt-add=SYS-ADMIN` is required for PDF rendering.

Verify the deployment by navigating to your server address in
your preferred browser.

```sh
127.0.0.1:8000
```

## License

MIT

**Free Software, Hell Yeah!**

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [dill]: <https://github.com/joemccann/dillinger>
   [git-repo-url]: <https://github.com/joemccann/dillinger.git>
   [john gruber]: <http://daringfireball.net>
   [df1]: <http://daringfireball.net/projects/markdown/>
   [markdown-it]: <https://github.com/markdown-it/markdown-it>
   [Ace Editor]: <http://ace.ajax.org>
   [node.js]: <http://nodejs.org>
   [Twitter Bootstrap]: <http://twitter.github.com/bootstrap/>
   [jQuery]: <http://jquery.com>
   [@tjholowaychuk]: <http://twitter.com/tjholowaychuk>
   [express]: <http://expressjs.com>
   [AngularJS]: <http://angularjs.org>
   [Gulp]: <http://gulpjs.com>

   [PlDb]: <https://github.com/joemccann/dillinger/tree/master/plugins/dropbox/README.md>
   [PlGh]: <https://github.com/joemccann/dillinger/tree/master/plugins/github/README.md>
   [PlGd]: <https://github.com/joemccann/dillinger/tree/master/plugins/googledrive/README.md>
   [PlOd]: <https://github.com/joemccann/dillinger/tree/master/plugins/onedrive/README.md>
   [PlMe]: <https://github.com/joemccann/dillinger/tree/master/plugins/medium/README.md>
   [PlGa]: <https://github.com/RahulHP/dillinger/blob/master/plugins/googleanalytics/README.md>
