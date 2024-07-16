# lk-person

## Начало работы
1. Обновить зависимости через `npm i`
2. Необходимо установить в IDE плагины: Nx Console, ESLint, Prettier.
3. Указать для плагина путь к workspace, например
`C:\Users\Developer\Univuz\Contingent\ClientApps\lk-person` 
4. Ваш редактор может предложить применить конфигурацию prettier к настройкам стиля кода,
рекомендуется согласиться

## Сборка и запуск
### Первый способ (локальный)

Запустите модуль/модули, с которыми вы хотите работать на локальном сервере с помощью команды:
```
npx nx serve название_модуля
```

Затем, запустите host модуль:
```
npx nx serve host
```

### Второй способ (Docker)

Соберите через docker-compose микрофронты (кроме host), с которыми вы будете работать и запустите их.\
Запустите host:
```
npx nx serve host
```

**Примечание:** Докер контейнеры содержат сборку для деплоя на production, т.е. если потребуется сделать откладку каких-то изменений, 
то сделать это будет сложно. \
В этом случае используйте либо первый способ, либо в Dockerfile модуля измените команду сборки модуля для development 
и пересоберите модуль
```shell
RUN npx nx run название_модуля:build:development
```

## Важно (!)

#### Каждый модуль не должен зависеть от других модулей, как и в компонентах, моделей, так и в сервисах (исключение: общая библиотека `common`). Проверить зависимости модулей можно через команду `nx graph` или через плагин `NX Console`.

## Создание проекта
1. В каталоге `apps` запустить команду `npx nx generate @nx/angular:remote <название проекта>`.
Лучше из контекстного меню редактора (`NX Generate (UI)`), т.к. в окне больше опций для настройки и удобнее.
   - Обязательно указать название проекта (без заглавных букв, знаков и т.п), путь проекта (`directory`) в виде: `apps/ваш_путь` (хост не указываем!)
   - В дополнительных опциях указать 
     - e2e2TestRunner: none
     - skipTests
     - style: scss
     - unitTestRunner: none
     - port: порт модуля в соответствии с прописанным портом в `docker-compose`
2. После создания проекта, убедитесь что в файле конфигурации `project.json` отсутствует поле `fileReplacements`. Если оно там присутствует, уберите его.

3. Настройте Webpack конфиг в `webpack.config.js` файле для remote следующим образом:
```typescript
const mf = require("@angular-architects/module-federation/webpack");
const share = mf.share;
const path = require("path");
const { withModuleFederationPlugin} = require('@angular-architects/module-federation/webpack');
const sharedLibraries = require("../../common/src/assets/sharedLibraries");

const sharedMappings = new mf.SharedMappings();
sharedMappings.register(
  path.join(__dirname, '../../tsconfig.base.json'),
  [/* mapped paths to share */]);

module.exports = withModuleFederationPlugin({
  name: 'название_модуля',

  exposes: {
    // Adjusted line:
    './Module': 'apps/путь_к_модулю/src/app/remote-entry/entry.module.ts'
  },

  shared: share({
    ...sharedLibraries,
    ...sharedMappings.getDescriptors()
  })
});
```
Содержимое `webpack.prod.config.js` поменяйте на следующее:
```typescript
module.exports = require('./webpack.config');
```

4. #### (!) Обязательно добавьте модуль в переменной `services` в docker-compose (см. Добавление нового пункта в меню) и в файле `common/src/assets/env.js` следующим образом:
```typescript
window["env"]["services"] = [
  ...
  {name: 'название проекта', path: 'путь в адресной строке', remoteURL: 'localhost:указанный порт'},
]
```

5. Сделайте следующие изменения в файле `project.json`:
   - Для команды `serve` замените executor `@nx/angular:webpack-dev-server` на `ngx-build-plus:dev-server`
   - Замените поле `customWebpackConfig` на `extraWebpackConfig` и укажите путь к webpack.config файлу
   - Для команды build замените executor `@nx/angular:webpack-browser` на `ngx-build-plus:browser` и также как
    в команде `serve` и `configuration` замените поле `customWebpackConfig` на `extraWebpackConfig`
```json
    "serve": {
        "executor": "ngx-build-plus:dev-server",
        "options": {
            ...
            "extraWebpackConfig": "apps/путь_к_проекту/webpack.config.js"
        },
    ...
    "build": {
        "executor": "ngx-build-plus:browser,
        "options": {
          ...
          "extraWebpackConfig": "apps/путь_к_проекту/webpack.config.js"
        },
    }
```

### Добавление микрофронта в docker-compose

В папке с проектом добавьте Dockerfile и пропишите в нем следующие команды:
```dockerfile
FROM node:18 as node
WORKDIR /usr/src/app

COPY package.json package-lock.json /usr/src/
RUN npm install --prefer-offline --no-audit
COPY . .
RUN npx kendo-ui-license activate
RUN npx nx build [название вашего проекта]

FROM nginx:stable
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=node /usr/src/app/dist/apps/[название вашего проекта] /usr/share/nginx/html
```
В docker-compose:

```yaml
lk-person-[название проекта]:
    container_name: lk-person-[название проекта]
    build:
        context: ClientApps/lk-person
        dockerfile: apps/[название проекта]/Dockerfile
    ports:
    # Порт указываете следующий по счету после последнего добавленного микрофронта
    - "8451:80" 
    networks:
    - dockerapi
```
### Добавление нового пункта в меню
1. Для нового модуля добавить в `common\src\models\role.ts` название модуля
2. Для нового модуля в `app.component.ts` в `services` указать пункт меню в формате:
   ```typescript
   {text: '<название пункта меню>', icon: '<класс иконки в меню>', url: '/<путь к модулю>', path: '/<путь к модулю>', id: Role.<название модуля>, return: false}
   ```
3. Добавить в `common\assets\env.js`
   - Для нового модуля в `services` указать название модуля
   - Для внешней ссылки в `menuItems` указать пункт меню в формате:
     ```javascript
     {text: '<название пункта меню>', icon: '<класс иконки в меню>', url: '<внешняя ссылка>'}
     ```
4. В docker-compose для хост-контейнера указать (через разделитель `;`)
   - В `Services` для нового модуля название модуля
   ```yaml
   Name__<порядковый номер модуля>=<название модуля>;
   Path__<порядковый номер модуля>=<путь в адресной строке>
    ```
   - В `MenuItems` для внешней ссылки
   ```yaml
    Text__<порядковый номер пункта меню>=<название пункта меню>;
    Url__<порядковый номер пункта меню>=<внешняя ссылка>;
    Icon__<порядковый номер пункта меню>=<класс иконки в меню>
   ```

### Как добавить общие библиотеки
Чтобы добавить библиотеки (и Kendo UI в т.ч.) нужно добавить в `imports` в модуле `entry.module.ts` модуль `SharedModule`.
### Добавление нового компонента
Все компоненты добавляются в `entry.module.ts`, а не `app.module.ts`.
### Добавление общего функционала
Вспомогательные функции, общие модели, директивы, компоненты и пр. находятся в каталоге `common`.\
В `index.ts` необходимо прописать путь до вашего модуля относительно каталога `common`.\
В `shared.module.ts` необходимо экспортировать ваш добавленный функционал в поле `exports`.\
Название экспортируемого функционала должно быть отличным от остальных.

## Линтеры 
###### Если при проверке MR разработчика вы видите, что он совершает ошибки, отлавливаемые линтером, отсылайте его к этому пункту
#### ! рекомендуется запускать перед каждой отправкой изменений для файлов (проектов), с которыми вы работаете
### Eslint
Eslint проверяет стиль и логику кода.\
В коде не должно быть критичных ошибок (`error`), некритичных (`warning`) следует избегать по возможности.
Можно использовать для проверки кода в одном проекте, каталоге или файле.

Пример:
```
npx eslint "apps\education\src\**\*.{ts,tsx}"
```
Пример для всех проектов можно посмотреть в "eslint" в package.json.

Линтер также умеет исправлять некоторые ошибки самостоятельно \
(например чистить не использующиеся импорты или лишние аннотации типов) \
Можно сразу запускать команду fix вместо обычной: она также покажет ошибки, которые не удалось исправить 

Пример:
```
npx eslint --fix "apps\education\src\**\*.{ts,tsx}" 
```
Пример для всех проектов можно посмотреть в "eslint:fix" в package.json.

### Stylelint
Stylelint проверяет файлы стилей.\
В коде не должно быть критичных ошибок (`error`), некритичных (`warning`) следует избегать по возможности.
Можно использовать для проверки кода в одном проекте, каталоге или файле.

Пример:
```
npx stylelint "apps\education\src\**\*.{scss,css}"
```
Пример для всех проектов можно посмотреть в "stylelint" в package.json.

Линтер также умеет исправлять многие ошибки самостоятельно и автоматически форматирует стили. \
Можно сразу запускать команду fix вместо обычной: она также покажет ошибки, которые не удалось исправить

Пример:
```
npx stylelint --fix "apps\education\src\**\*.{scss,css}" 
```
Пример для всех проектов можно посмотреть в "stylelint:fix" в package.json.\
Для любопытных: конфигурацию можно посмотреть в .stylelintrc.json

## Использование шрифтов в стилях (!)
###### Внимательно прочтите этот пункт, описанные в нём правила уменьшают вероятность различия шрифтов в разных браузерах
При переопределении стилей каких-либо элементов необходимо придерживать следующих правил:
- при использовании шрифта `Roboto` всегда четко указывать `font-weight`
```scss
font-family: 'Roboto', sans-serif;
font-weight: 400;
// или
font: 500 18px/22px 'Roboto', sans-serif;
```
Шрифт `Montserrat` был переработан и используется точно так же как и шрифт `Roboto` с указанием `font-weight` \
Он имеет один элиас, его использование зависит от нужной жирности шрифта
  - `Montserrat Thin` - Montserrat 100
  - `Montserrat ExtraLight` - Montserrat 200
  - `Montserrat Light` - Montserrat 300
  - `Montserrat Regular` - Montserrat 400
  - `Montserrat Medium` - Montserrat 500
  - `Montserrat SemiBold` - Montserrat 600
  - `Montserrat Bold` - Montserrat 700
  - `Montserrat ExtraBold` - Montserrat 800
  - `Montserrat Black` - Montserrat 900

Раньше:
```scss
font-family: 'Montserrat Medium', sans-serif;
font-weight: 500;
```

Сейчас:
```scss
font-family: 'Montserrat', sans-serif;
font-weight: 500;
```

- свойство `font` является сокращением, поэтому оно может переопределить неуказанные значения, установленные ранее, заменив на начальные. \
Будьте осторожны с его использованием (! начальное значение `font-weight: 400`)

## Автоформатирование кода
###### Если при проверке MR разработчика вы видите, что код не отформатирован, отсылайте его к этому пункту
#### ! рекомендуется делать только для файлов (проектов), с которыми вы работаете
Можно использовать для форматирования кода в одном проекте, каталоге или файле.\
Например:
```
npx prettier --write "apps\education\src\**\*.{ts,tsx}" 
```
Пример для всех проектов можно посмотреть в "prettier:fix" в package.json.\
Для любопытных: конфигурацию можно посмотреть в .prettierrc

## Помощь по ошибкам в микрофронтах

### NullInjectorError

Если вы наткнулись на такой вид ошибки:

![Alt text](images/nullinjector.png)

Это означает что вы неправильно настроили Webconfig файл и не добавили в модуль 
недостающий пакет \
Чтобы исправить эту проблему, добавьте в `webpack.config.js` в `shared` недостающий пакет, например:

```typescript
shared: share({
  ...
  "@angular/common/http": { singleton: true, eager: true },
})
```
