# Topic16 — NuGet пакеты и управление зависимостями

## Цель

Понять, что такое NuGet, как добавлять пакеты в проект, находить популярные пакеты для QA автоматизации.

---

### Для полного новичка: быстрый маршрут

- Прочитайте: "Что такое NuGet?", "Как добавить пакет?"
- Запомните: популярные пакеты (Selenium, NUnit, Moq)
- Вернитесь к чек-листу в конце

---

## Содержание

1. [Что такое NuGet?](#1-что-такое-nuget)
2. [NuGet.org — маркетплейс пакетов](#2-nugetorg--маркетплейс-пакетов)
3. [Как добавить пакет?](#3-как-добавить-пакет)
4. [Версионирование пакетов](#4-версионирование-пакетов)
5. [Популярные пакеты для QA](#5-популярные-пакеты-для-qa)
6. [NuGet vs npm vs Maven](#6-nuget-vs-npm-vs-maven)
7. [Best practices](#7-best-practices)
8. [Частые ошибки](#8-частые-ошибки)

---

## 1. Что такое NuGet?

### Определение

**NuGet** — это менеджер пакетов для .NET. Это маркетплейс, где разработчики публикуют готовые библиотеки, которые вы можете использовать в своих проектах.

### Аналогия

```
NuGet     = маркетплейс для .NET приложений
npm       = маркетплейс для JavaScript
Maven     = маркетплейс для Java
pip       = маркетплейс для Python
```

### Что находится в NuGet?

- 📦 **Библиотеки** (готовый код от других разработчиков)
- 🔧 **Инструменты** (утилиты для разработки)
- 🎨 **Компоненты** (UI элементы, дизайн системы)
- 🧪 **Тестовые фреймворки** (NUnit, xUnit, Moq)
- 🌐 **Веб фреймворки** (Entity Framework, ASP.NET)

### Что дает NuGet?

```
✅ Не нужно писать код с нуля
✅ Используйте проверенные решения
✅ Автоматическое управление зависимостями
✅ Обновления с одной команды
```

---

## 2. NuGet.org — маркетплейс пакетов

### Где найти пакеты?

Официальный сайт: **https://www.nuget.org**

### Как найти пакет?

```
1. Откройте https://www.nuget.org
2. В поле поиска введите название (например, "Selenium")
3. Выберите нужный пакет
4. Прочитайте описание, версию, популярность
5. Скопируйте команду установки
```

### Пример: поиск Selenium

```
Поиск: "Selenium"
↓
Результат: "Selenium.WebDriver"
↓
Команда: dotnet add package Selenium.WebDriver
```

### Что показывает страница пакета?

```
📌 Название: Selenium.WebDriver
⭐ Рейтинг: 4.5/5
📊 Скачивания: 10M+
👤 Автор: SeleniumHQ
🔗 GitHub: https://github.com/SeleniumHQ/selenium
📖 Документация: https://www.selenium.dev
🔒 Лицензия: Apache 2.0
```

---

## 3. Как добавить пакет?

### Способ 1: dotnet CLI (Рекомендуется)

```bash
# Установить пакет (последняя версия)
dotnet add package Selenium.WebDriver

# Установить конкретную версию
dotnet add package Selenium.WebDriver --version 4.10.0

# Установить в конкретный проект
dotnet add MyProject.csproj package Selenium.WebDriver
```

### Способ 2: Visual Studio GUI

```
1. Правый клик на проект
2. "Manage NuGet Packages"
3. Поиск пакета
4. Нажать "Install"
```

### Способ 3: Редактировать .csproj вручную

```xml
<ItemGroup>
  <PackageReference Include="Selenium.WebDriver" Version="4.10.0" />
  <PackageReference Include="NUnit" Version="3.13.3" />
  <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
</ItemGroup>
```

Затем:

```bash
dotnet restore  # Загружает пакеты
```

### Что происходит при установке?

```
1. dotnet загружает пакет с nuget.org
2. Пакет распаковывается в ~/.nuget/packages
3. Проект получает ссылку на пакет
4. .csproj обновляется (добавляется <PackageReference>)
5. packages.lock.json создаётся (для воспроизводимости)
```

---

## 4. Версионирование пакетов

### Semantic Versioning (SemVer)

```
4.10.0
│  │  └─ Patch версия (исправления багов)
│  └──── Minor версия (новый функционал, совместим)
└─────── Major версия (breaking changes, не совместим)
```

### Примеры версий

```
✅ 4.10.0  → 4.10.1   = безопасно (только исправления)
✅ 4.10.0  → 4.11.0   = обычно безопасно (новый функционал)
❌ 4.10.0  → 5.0.0    = ОПАСНО! (breaking changes)
```

### Как выбрать версию?

```xml
<!-- Точная версия -->
<PackageReference Include="Selenium.WebDriver" Version="4.10.0" />

<!-- Диапазон версий -->
<PackageReference Include="Selenium.WebDriver" Version="[4.0, 5.0)" />
<!-- ↑ от 4.0 (включительно) до 5.0 (не включительно) -->

<!-- Свободный диапазон -->
<PackageReference Include="Selenium.WebDriver" Version="4.*" />
<!-- ↑ любая 4.x.x версия -->
```

### Проверить обновления

```bash
# Список всех пакетов и доступных обновлений
dotnet list package --outdated

# Обновить пакет
dotnet add package Selenium.WebDriver --version 4.15.0

# Обновить все пакеты до последних
dotnet package update
```

---

## 5. Популярные пакеты для QA

### 🧪 Тестирование

| Пакет                 | Версия | Назначение                                 |
| --------------------- | ------ | ------------------------------------------ |
| **NUnit**             | 3.13+  | Unit тестирование (как JUnit в Java)       |
| **xUnit**             | 2.4+   | Unit тестирование (альтернатива NUnit)     |
| **Moq**               | 4.16+  | Mocking библиотека (фейковые объекты)      |
| **Fluent.Assertions** | 6.0+   | Красивые assertions (проверки результатов) |

### 🌐 Веб автоматизация

| Пакет                  | Версия | Назначение                               |
| ---------------------- | ------ | ---------------------------------------- |
| **Selenium.WebDriver** | 4.10+  | WebDriver для автоматизации браузера     |
| **Selenium.Support**   | 4.10+  | Поддержка WebDriver (ожидания, локаторы) |
| **WebDriverManager**   | 2.0+   | Автоматическое управление драйверами     |

### 🔌 API тестирование

| Пакет               | Версия | Назначение                  |
| ------------------- | ------ | --------------------------- |
| **RestSharp**       | 107+   | HTTP клиент для REST API    |
| **Newtonsoft.Json** | 13.0+  | Парсинг JSON                |
| **System.Net.Http** | -      | Встроен в .NET (HttpClient) |

### 📊 Данные и БД

| Пакет                     | Версия | Назначение               |
| ------------------------- | ------ | ------------------------ |
| **Entity Framework Core** | 7.0+   | ORM для работы с БД      |
| **Dapper**                | 2.0+   | Лёгкий ORM для запросов  |
| **SQLite**                | 3.0+   | Встроенная БД для тестов |

### 📝 Логирование

| Пакет       | Версия | Назначение                         |
| ----------- | ------ | ---------------------------------- |
| **log4net** | 2.0+   | Логирование (как Log4j в Java)     |
| **Serilog** | 3.0+   | Современное логирование            |
| **NLog**    | 5.0+   | Логирование (альтернатива log4net) |

### 🛠️ Утилиты

| Пакет               | Версия | Назначение             |
| ------------------- | ------ | ---------------------- |
| **CsvHelper**       | 30.0+  | Парсинг CSV файлов     |
| **ClosedXML**       | 0.95+  | Работа с Excel файлами |
| **HtmlAgilityPack** | 1.11+  | Парсинг HTML           |

---

## 6. NuGet vs npm vs Maven

### Сравнение менеджеров пакетов

| Критерий              | NuGet (.NET)           | npm (Node.js)   | Maven (Java)           | pip (Python)     |
| --------------------- | ---------------------- | --------------- | ---------------------- | ---------------- |
| **Маркетплейс**       | nuget.org              | npmjs.com       | mvnrepository.com      | pypi.org         |
| **Команда установки** | `dotnet add package X` | `npm install X` | `maven dependency:add` | `pip install X`  |
| **Хранилище пакетов** | ~/.nuget/packages      | node_modules/   | ~/.m2/repository       | site-packages/   |
| **Конфиг файл**       | .csproj                | package.json    | pom.xml                | requirements.txt |
| **Популярность**      | ⭐⭐⭐                 | ⭐⭐⭐⭐⭐      | ⭐⭐⭐⭐               | ⭐⭐⭐⭐         |

### Примеры установки

```
# .NET
dotnet add package Selenium.WebDriver

# JavaScript
npm install selenium-webdriver

# Java
<!-- в pom.xml -->
<dependency>
  <groupId>org.seleniumhq.selenium</groupId>
  <artifactId>selenium-java</artifactId>
  <version>4.10.0</version>
</dependency>

# Python
pip install selenium
```

---

## 7. Best Practices

### ✅ DO — Делайте это

```bash
# ✅ Используйте точные версии для production
dotnet add package Selenium.WebDriver --version 4.10.0

# ✅ Проверяйте обновления регулярно
dotnet list package --outdated

# ✅ Используйте одинаковые версии в команде
# Добавляйте package.lock.json в Git

# ✅ Изучайте лицензии пакетов (Apache, MIT, GPL)

# ✅ Используйте только нужные пакеты
# Не добавляйте "на будущее"
```

### ❌ DON'T — Не делайте это

```bash
# ❌ Не используйте * версии в production
<PackageReference Include="Selenium.WebDriver" Version="*" />

# ❌ Не обновляйте major версии без тестирования
# 4.10.0 → 5.0.0 может сломать код

# ❌ Не публикуйте node_modules в Git
# (.nuget/packages игнорируется автоматически)

# ❌ Не используйте пакеты из неизвестных источников
# Проверяйте автора на nuget.org
```

### Пример правильного workflow

```bash
# 1. Добавить пакет локально
dotnet add package Selenium.WebDriver

# 2. Протестировать
dotnet test

# 3. Закоммитить
git add .csproj packages.lock.json
git commit -m "Add Selenium.WebDriver"

# 4. Пушить
git push

# 5. Другие разработчики
git pull
dotnet restore  # загружает пакеты из lock файла
```

---

## 8. Частые ошибки

### ❌ Ошибка 1: Забыли добавить пакет

```csharp
// ❌ Ошибка компиляции: IWebDriver не найден
var driver = new ChromeDriver();

// ✅ Сначала установите пакет
// dotnet add package Selenium.WebDriver
```

### ❌ Ошибка 2: Несовместимые версии

```
❌ Error: Could not resolve Selenium.WebDriver 5.0.0
   because it requires .NET 7.0, but project targets .NET 6.0
```

**Решение:** Обновите .NET или используйте совместимую версию Selenium

```bash
dotnet add package Selenium.WebDriver --version 4.10.0
```

### ❌ Ошибка 3: Забыли dotnet restore

```bash
# ❌ После git pull, пакеты не загружены
dotnet build  # Ошибка: пакеты не найдены

# ✅ Правильно:
dotnet restore  # загружает пакеты из .csproj
dotnet build
```

### ❌ Ошибка 4: Конфликт версий

```xml
<!-- ❌ Разные версии одного пакета -->
<PackageReference Include="Newtonsoft.Json" Version="12.0.0" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.0" />
```

**Решение:** Используйте одну версию

```xml
<!-- ✅ Одна версия -->
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
```

### ❌ Ошибка 5: Breaking change при обновлении

```csharp
// Selenium 4.9.0 (старая версия)
var driver = new ChromeDriver();

// Обновлились на 4.15.0 (новая версия)
var driver = new ChromeDriver();  // ❌ API изменился!

// ✅ Новый API
var driver = new ChromeDriver(options);  // требует параметры
```

**Совет:** Всегда проверяйте релиз ноты перед обновлением major версии

---

## 9. Как найти и оценить пакет?

### Критерии для выбора

```
1. ⭐ Популярность (скачивания, звёзды на GitHub)
2. 🔄 Активность (когда был последний обновление?)
3. 🧪 Тесты (есть ли unit тесты в репозитории?)
4. 📖 Документация (есть ли примеры кода?)
5. 🐛 Issues (много ли открытых багов?)
6. 📜 Лицензия (совместима ли с вашим проектом?)
```

### Красные флаги

```
❌ Пакет не обновлялся 2+ года
❌ 0 документации
❌ Автор — неизвестный разработчик
❌ Много открытых критических bagов
❌ Лицензия GPL (часто конфликтует)
❌ Очень мало скачиваний (может быть ненадёжным)
```

### Зелёные флаги

```
✅ Регулярные обновления (каждый месяц)
✅ Хорошая документация + примеры
✅ 1M+ скачиваний (проверено сообществом)
✅ Активный GitHub репозиторий
✅ MIT или Apache лицензия
✅ Компания стоит за пакетом (Microsoft, JetBrains, etc)
```

---

## 10. ЧЕК-ЛИСТ ПРОВЕРКИ ЗНАНИЙ 🎯

### Вопрос 1: Что такое NuGet?

**Ответ:** Менеджер пакетов для .NET, маркетплейс готовых библиотек на nuget.org

### Вопрос 2: Как установить пакет Selenium?

**Ответ:** `dotnet add package Selenium.WebDriver`

### Вопрос 3: Где хранятся загруженные пакеты?

**Ответ:** В ~/.nuget/packages/ (глобально для всех проектов)

### Вопрос 4: Что такое SemVer?

**Ответ:** Semantic Versioning — система версионирования Major.Minor.Patch (4.10.0)

### Вопрос 5: Если обновился на 5.0.0 от 4.10.0, что может произойти?

**Ответ:** Breaking changes — код может сломаться, нужно перепроверить

### Вопрос 6: Как обновить пакет до новой версии?

**Ответ:** `dotnet add package X --version 4.15.0` или вручную в .csproj

### Вопрос 7: Какие пакеты рекомендуются для QA автоматизации?

**Ответ:** Selenium.WebDriver, NUnit, Moq, RestSharp, Newtonsoft.Json, WebDriverManager

### Вопрос 8: Почему нужен packages.lock.json?

**Ответ:** Для воспроизводимости — все разработчики используют точно такие же версии

---

## Файлы в проекте

- `README.md` — этот файл (теория только, без кода)

---

**Это теоретический курс.** В настоящем проекте используются следующие пакеты:

```
dotnet add package Selenium.WebDriver        # WebDriver для браузеров
dotnet add package NUnit                     # Unit тестирование
dotnet add package Newtonsoft.Json           # JSON парсинг
dotnet add package RestSharp                 # HTTP клиент
```

**Готово к использованию!** ✅
