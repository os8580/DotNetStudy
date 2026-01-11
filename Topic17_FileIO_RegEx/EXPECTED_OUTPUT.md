# EXPECTED_OUTPUT.md

Выходной результат при запуске `dotnet run`:

```
📚 Topic17: Файлы, JSON, XML и RegEx

============================================================

📌 Example 1: File операции
────────────────────────────────
✅ Создали файл: test_file.txt
✅ Файл существует
✅ Содержимое: Hello World!
✅ После добавления:
Hello World!
Дополнительная строка
✅ Удалили файл

📌 Example 2: Directory операции
────────────────────────────────
✅ Создали папку: test_dir
✅ Файлы в папке:
   - file1.txt
   - file2.txt
   - file3.txt
✅ Папка существует
✅ Удалили папку test_dir

📌 Example 3: Path операции
────────────────────────────────
✅ Объединённый путь: Users\Documents\file.json
✅ Имя файла: config.json
✅ Расширение: .json
✅ Имя без расширения: config
✅ Директория: C:\Users\Documents
✅ Текущая папка: [текущая рабочая директория]

📌 Example 4: StreamReader - построчное чтение
────────────────────────────────
✅ Читаем файл построчно:
   Строка 1
   Строка 2
   Строка 3
   Строка 4
   Строка 5

📌 Example 5: StreamWriter - построчная запись
────────────────────────────────
✅ Записали в логи:
   [2024-01-15 10:00:00] INFO: Тест начался
   [2024-01-15 10:00:01] INFO: Шаг 1 выполнен
   [2024-01-15 10:00:02] ✅ PASSED

📌 Example 6: JSON с System.Text.Json
────────────────────────────────
✅ Объект → JSON:
   {"browserName":"Chrome","port":9222,"headless":true}
✅ JSON → объект:
   Browser: Firefox
   Port: 4444

📌 Example 7: Regex - поиск совпадений
────────────────────────────────
✅ Найденные цены:
   $99.99
   $150.00
   $75.50
✅ Найденные теги:
   h1
   p
✅ Найденные email:
   admin@example.com
   support@site.org

📌 Example 8: Regex - валидация
────────────────────────────────
✅ Проверка email:
   test@example.com: ✅ Valid
   invalid-email: ❌ Invalid
   user+tag@domain.co.uk: ✅ Valid

✅ Проверка IP адреса:
   192.168.1.1: ✅ Valid
   256.1.1.1: ✅ Valid
   10.0.0.1: ✅ Valid

✅ Проверка URL:
   https://example.com: ✅ Valid
   http://test.org: ✅ Valid
   not a url: ❌ Invalid

📌 Example 9: Regex - замена и разделение
────────────────────────────────
✅ Замена:
   Исходное: Hello 123 World 456
   Результат: Hello [NUMBER] World [NUMBER]
✅ Переформат даты:
   2024-01-15 → 15/01/2024
✅ Разделение CSV:
   - one
   - two
   - three
   - four
✅ Разделение по разным разделителям:
   - apple
   - banana
   - orange
   - grape

📌 Example 10: Парсинг логов
────────────────────────────────
✅ Разобранные логи:
   Time: 2024-01-15 10:00:00 | Type: INFO | Msg: Тест начался
   Time: 2024-01-15 10:00:05 | Type: ✅ | Msg: Login successful
   Time: 2024-01-15 10:00:12 | Type: ⚠️ | Msg: Warning: Slow response
   Time: 2024-01-15 10:00:18 | Type: ❌ | Msg: ERROR: Element not found

📌 Example 11: Генерация CSV файла
────────────────────────────────
✅ Созданный CSV:
   Email,Password,Role
   user1@test.com,Pass123!,Admin
   user2@test.com,Pass456!,User
   user3@test.com,Pass789!,User

✅ Парсированные данные:
   Email: user1@test.com, Password: Pass123!, Role: Admin
   Email: user2@test.com, Password: Pass456!, Role: User
   Email: user3@test.com, Password: Pass789!, Role: User

📌 Example 12: Чтение конфига
────────────────────────────────
✅ Содержимое конфига:
   {
   "baseUrl": "https://example.com",
   "browser": "Chrome",
   "timeout": 10000,
   "headless": true
   }

✅ Извлечённые параметры:
   BaseUrl: https://example.com
   Browser: Chrome
   Timeout: 10000ms

============================================================
✅ Все примеры выполнены!
```

## Примечания

- **Example 1:** Файл создаётся в текущей рабочей директории
- **Example 2:** Папка также создаётся в текущей директории
- **Example 3:** Пути могут отличаться на разных ОС (Windows использует `\`, Unix использует `/`)
- **Example 4-5:** StreamReader/Writer автоматически закрываются благодаря `using`
- **Example 6:** JSON парсируется встроенным System.Text.Json
- **Example 7:** Regex находит все совпадения в порядке появления
- **Example 8:** IP адрес и URL валидация простая (реальная валидация сложнее)
- **Example 9:** Regex замена и разделение используют capture groups
- **Example 10:** Логи парсируются с regex паттерном
- **Example 11:** CSV создаётся и парсируется построчно
- **Example 12:** JSON конфиг парсируется regex-ом
