# Topic14 — Expected Output

When you run `dotnet run` in this topic, you should see:

```
╔════════════════════════════════════════════════════════════════╗
║ Topic14 — Delegates, Functions, and Functional Programming    ║
╚════════════════════════════════════════════════════════════════╝

► 1. NAMED vs LAMBDA vs ARROW FUNCTIONS

   Hello from named function
   Hello from lambda
   Hello from arrow function

► 2. ACTION vs FUNC

   Action: число 42
   Func: 4 чётное? True
   Func: 3 + 5 = 8

► 3. PREDICATE

   Positive numbers: 1, 2, 3

► 4. CUSTOM DELEGATES

   Retail price (100 × 5): $500
   Wholesale price (100 × 5): $400,0

► 5. CALLBACKS

   ✅ Элемент найден: //*[@id='login']
   ❌ Элемент не найден: //*[@class='nonexistent']

► 6. EVENT HANDLING (Events)

   Handler 1: Button clicked!
   Handler 2: Logging click...

► 7. CUSTOM EVENTS WITH CUSTOM DELEGATE

   Downloading video.mp4...
   ✅ video.mp4 (77,5MB) downloaded!
   📊 Logging: video.mp4 - 77,5MB

► 8. RETRY LOGIC WITH CALLBACKS

   Attempt 1...
   ⚠️ Attempt 1 failed, retrying...
   Attempt 2...
   ✅ Request succeeded!

► 9. CHAIN OF RESPONSIBILITY (Request Handlers)

   🔐 Processing login: LOGIN alice
   📊 Processing data: DATA report
   ❌ Processing error: ERROR 404

► 10. FUNCTIONAL PROGRAMMING WITH LINQ

   Even: 2, 4, 6, 8, 10
   Doubled: 2, 4, 6, 8, 10, 12, 14, 16, 18, 20
   Sum of numbers > 3: 49

► 11. FUNCTION COMPOSITION

   Trim → ToUpper → Length('  hello world  ') = 11

► 12. CLOSURE & VARIABLE CAPTURE

   ❌ Wrong closure:
      Value: 3
      Value: 3
      Value: 3
   ✅ Correct closure:
      Value: 0
      Value: 1
      Value: 2

╔════════════════════════════════════════════════════════════════╗
║ All examples completed! Check README.md for detailed info     ║
╚════════════════════════════════════════════════════════════════╝
```

## Key Concepts Demonstrated

1. **Named Functions** - Traditional method declarations
2. **Lambda Expressions** - Anonymous functions with full syntax
3. **Arrow Functions** - Shorthand lambda notation
4. **Action vs Func** - Understanding the return type difference
5. **Predicates** - Boolean functions for filtering
6. **Custom Delegates** - Creating your own function types
7. **Callbacks** - Passing functions as parameters
8. **Event Handling** - Using events for observer pattern
9. **Retry Logic** - Practical callback usage
10. **Chain of Responsibility** - Using delegates for request processing
11. **Functional LINQ** - Composing operations
12. **Function Composition** - Combining functions
13. **Closures** - Understanding variable capture in lambdas

## What You Should Understand

After running this topic, you should know:

✅ What a delegate is and why it's useful
✅ How to use Action<T> and Func<T, R> in your code
✅ How to write lambda expressions and arrow functions
✅ How callbacks work and when to use them
✅ How to handle events
✅ How to avoid closure bugs in loops
✅ How to apply functional programming patterns in C#
✅ Real-world examples for QA automation testing

---

**Status**: ✅ Ready for practice!
