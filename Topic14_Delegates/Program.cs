using System;
using System.Collections.Generic;
using System.Linq;

namespace Topic14_Delegates
{
    // ==========================================
    // CUSTOM DELEGATES
    // ==========================================
    public delegate void PrintDelegate(string text);
    public delegate decimal PriceCalculator(decimal basePrice, int quantity);
    public delegate void DownloadCompleteDelegate(string filename, double sizeMB);

    // ==========================================
    // CLASSES FOR EXAMPLES
    // ==========================================

    // Пример 1: WebDriver с callbacks
    public class WebDriver
    {
        private Action<string> onElementFound;
        private Action<string> onElementNotFound;

        public WebDriver(Action<string> found, Action<string> notFound)
        {
            onElementFound = found;
            onElementNotFound = notFound;
        }

        public void FindElement(string xpath)
        {
            bool exists = xpath.Contains("login") || xpath.Contains("button");
            
            if (exists)
                onElementFound?.Invoke($"✅ Элемент найден: {xpath}");
            else
                onElementNotFound?.Invoke($"❌ Элемент не найден: {xpath}");
        }
    }

    // Пример 2: API Client с retry
    public class ApiClient
    {
        public void MakeRequest(Func<bool> requestFunc, Action onSuccess, Action<int> onRetry, int maxRetries = 3)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                if (requestFunc())
                {
                    onSuccess();
                    return;
                }
                onRetry(i + 1);
            }
        }
    }

    // Пример 3: File Downloader с events
    public class FileDownloader
    {
        public event DownloadCompleteDelegate OnDownloadComplete;

        public void Download(string filename)
        {
            Console.WriteLine($"   Downloading {filename}...");
            System.Threading.Thread.Sleep(500);
            double sizeMB = new Random().Next(10, 100) + 0.5;
            OnDownloadComplete?.Invoke(filename, sizeMB);
        }
    }

    // Пример 4: Button с event handler
    public class Button
    {
        public event EventHandler Clicked;

        public void Click()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    // Пример 5: Request Handler (Chain of Responsibility)
    public class RequestHandler
    {
        private Func<string, bool> canHandle;
        private Action<string> process;

        public RequestHandler(Func<string, bool> canHandle, Action<string> process)
        {
            this.canHandle = canHandle;
            this.process = process;
        }

        public bool Handle(string request)
        {
            if (canHandle(request))
            {
                process(request);
                return true;
            }
            return false;
        }
    }

    // ==========================================
    // MAIN PROGRAM
    // ==========================================

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Topic14 — Delegates, Functions, and Functional Programming    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");

            // ==========================================
            // 1. NAMED vs LAMBDA vs ARROW
            // ==========================================
            Console.WriteLine("► 1. NAMED vs LAMBDA vs ARROW FUNCTIONS\n");

            // Named function
            Action printHello = PrintHello;
            printHello();  // Hello from named function

            // Lambda
            Action printHelloLambda = () => Console.WriteLine("   Hello from lambda");
            printHelloLambda();

            // Arrow notation
            Action printHelloArrow = PrintHelloArrow;
            printHelloArrow();

            Console.WriteLine();

            // ==========================================
            // 2. ACTION vs FUNC
            // ==========================================
            Console.WriteLine("► 2. ACTION vs FUNC\n");

            // Action — не возвращает ничего
            Action<int> printNumber = (x) => Console.WriteLine($"   Action: число {x}");
            printNumber(42);

            // Func — возвращает значение
            Func<int, bool> isEven = (x) => x % 2 == 0;
            bool result = isEven(4);
            Console.WriteLine($"   Func: 4 чётное? {result}");

            // Func с несколькими параметрами
            Func<int, int, int> add = (a, b) => a + b;
            Console.WriteLine($"   Func: 3 + 5 = {add(3, 5)}");

            Console.WriteLine();

            // ==========================================
            // 3. PREDICATE
            // ==========================================
            Console.WriteLine("► 3. PREDICATE\n");

            Predicate<int> isPositive = x => x > 0;
            int[] numbers = { -2, -1, 0, 1, 2, 3 };
            int[] positive = Array.FindAll(numbers, isPositive);
            Console.WriteLine($"   Positive numbers: {string.Join(", ", positive)}");

            Console.WriteLine();

            // ==========================================
            // 4. CUSTOM DELEGATES
            // ==========================================
            Console.WriteLine("► 4. CUSTOM DELEGATES\n");

            PriceCalculator retailPrice = (price, qty) => price * qty;
            PriceCalculator wholesalePrice = (price, qty) => price * qty * 0.8m;

            Console.WriteLine($"   Retail price (100 × 5): ${retailPrice(100, 5)}");
            Console.WriteLine($"   Wholesale price (100 × 5): ${wholesalePrice(100, 5)}");

            Console.WriteLine();

            // ==========================================
            // 5. CALLBACKS
            // ==========================================
            Console.WriteLine("► 5. CALLBACKS\n");

            var driver = new WebDriver(
                found => Console.WriteLine($"   {found}"),
                notFound => Console.WriteLine($"   {notFound}")
            );

            driver.FindElement("//*[@id='login']");
            driver.FindElement("//*[@class='nonexistent']");

            Console.WriteLine();

            // ==========================================
            // 6. EVENT HANDLING
            // ==========================================
            Console.WriteLine("► 6. EVENT HANDLING (Events)\n");

            var button = new Button();
            button.Clicked += (sender, e) => Console.WriteLine("   Handler 1: Button clicked!");
            button.Clicked += (sender, e) => Console.WriteLine("   Handler 2: Logging click...");

            button.Click();

            Console.WriteLine();

            // ==========================================
            // 7. CUSTOM EVENTS
            // ==========================================
            Console.WriteLine("► 7. CUSTOM EVENTS WITH CUSTOM DELEGATE\n");

            var downloader = new FileDownloader();

            downloader.OnDownloadComplete += (file, size) =>
                Console.WriteLine($"   ✅ {file} ({size}MB) downloaded!");

            downloader.OnDownloadComplete += (file, size) =>
                Console.WriteLine($"   📊 Logging: {file} - {size}MB");

            downloader.Download("video.mp4");

            Console.WriteLine();

            // ==========================================
            // 8. RETRY LOGIC WITH CALLBACKS
            // ==========================================
            Console.WriteLine("► 8. RETRY LOGIC WITH CALLBACKS\n");

            var client = new ApiClient();
            int attempts = 0;

            client.MakeRequest(
                requestFunc: () => {
                    attempts++;
                    Console.WriteLine($"   Attempt {attempts}...");
                    return attempts == 2;  // Успешно на 2-й попытке
                },
                onSuccess: () => Console.WriteLine("   ✅ Request succeeded!"),
                onRetry: (attempt) => Console.WriteLine($"   ⚠️ Attempt {attempt} failed, retrying...")
            );

            Console.WriteLine();

            // ==========================================
            // 9. CHAIN OF RESPONSIBILITY WITH DELEGATES
            // ==========================================
            Console.WriteLine("► 9. CHAIN OF RESPONSIBILITY (Request Handlers)\n");

            var handlers = new List<RequestHandler>
            {
                new RequestHandler(
                    canHandle: r => r.StartsWith("LOGIN"),
                    process: r => Console.WriteLine($"   🔐 Processing login: {r}")
                ),
                new RequestHandler(
                    canHandle: r => r.StartsWith("DATA"),
                    process: r => Console.WriteLine($"   📊 Processing data: {r}")
                ),
                new RequestHandler(
                    canHandle: r => r.StartsWith("ERROR"),
                    process: r => Console.WriteLine($"   ❌ Processing error: {r}")
                )
            };

            var requests = new[] { "LOGIN alice", "DATA report", "ERROR 404" };
            foreach (var request in requests)
            {
                foreach (var handler in handlers)
                {
                    if (handler.Handle(request))
                        break;
                }
            }

            Console.WriteLine();

            // ==========================================
            // 10. FUNCTIONAL PROGRAMMING WITH LINQ
            // ==========================================
            Console.WriteLine("► 10. FUNCTIONAL PROGRAMMING WITH LINQ\n");

            var data = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Using lambdas with LINQ
            var evenNumbers = data.Where(x => x % 2 == 0).ToList();
            Console.WriteLine($"   Even: {string.Join(", ", evenNumbers)}");

            var doubled = data.Select(x => x * 2).ToList();
            Console.WriteLine($"   Doubled: {string.Join(", ", doubled)}");

            var sum = data.Where(x => x > 3).Aggregate(0, (acc, x) => acc + x);
            Console.WriteLine($"   Sum of numbers > 3: {sum}");

            Console.WriteLine();

            // ==========================================
            // 11. FUNCTION COMPOSITION
            // ==========================================
            Console.WriteLine("► 11. FUNCTION COMPOSITION\n");

            Func<string, string> trim = s => s.Trim();
            Func<string, string> toUpper = s => s.ToUpper();
            Func<string, int> length = s => s.Length;

            // Compose functions
            var pipeline = Compose(Compose(trim, toUpper), length);
            int len = pipeline("  hello world  ");
            Console.WriteLine($"   Trim → ToUpper → Length('  hello world  ') = {len}");

            Console.WriteLine();

            // ==========================================
            // 12. CLOSURE EXAMPLE
            // ==========================================
            Console.WriteLine("► 12. CLOSURE & VARIABLE CAPTURE\n");

            var actions = new List<Action>();

            // ❌ Wrong: all lambdas capture the same variable
            Console.WriteLine("   ❌ Wrong closure:");
            for (int i = 0; i < 3; i++)
            {
                actions.Add(() => Console.WriteLine($"      Value: {i}"));  // All print 3
            }
            foreach (var action in actions)
                action();

            // ✅ Correct: copy variable for each lambda
            Console.WriteLine("   ✅ Correct closure:");
            actions.Clear();
            for (int i = 0; i < 3; i++)
            {
                int copy = i;  // Copy in each iteration
                actions.Add(() => Console.WriteLine($"      Value: {copy}"));
            }
            foreach (var action in actions)
                action();

            Console.WriteLine();

            // ==========================================
            // END
            // ==========================================
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ All examples completed! Check README.md for detailed info     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        }

        // ==========================================
        // HELPER METHODS
        // ==========================================

        static void PrintHello()
        {
            Console.WriteLine("   Hello from named function");
        }

        static Action PrintHelloArrow => () => Console.WriteLine("   Hello from arrow function");

        static Func<TIn, TOut> Compose<TIn, TMid, TOut>(
            Func<TIn, TMid> f1,
            Func<TMid, TOut> f2)
        {
            return x => f2(f1(x));
        }
    }
}
