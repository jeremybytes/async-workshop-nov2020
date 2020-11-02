Workshop: Asynchronous Programming in C# [Custom Workshop Nov 2020]
============================
*Level: Intermediate*  
This repository contains slides, code samples and labs for a custom asynchronous programming workshop. Rather than being an end-to-end view of asynchronous programming, this workshop is based on the following set of topics.

Topics
------
* **Getting Results from Async Methods**  
A look at the right ways to get results from asynchronous methods (and several wrong ways). This includes the importance of freeing the main thread.  
    * Awaiting Tasks  
    * Task Continuations  
    * Proper use of Task.Result  
    * Avoiding Task.GetAwaiter().GetResult()  
    * Avoiding Task.Wait()  

* **Where Continuations Run**  
A look at where post-await code and task continuations run (i.e. the main thread or somewhere else), and why this is important.  
    * Default behavior for post-await code and task continuations  
    * The importance of Task.ConfigureAwait()  
    * Differences for web applications between .NET Core and .NET Framework  

* **Unit Testing**  
A look at writing unit tests for asynchronous methods.  
    * Testing async methods with MSTest  
    * Easily creating asynchronous fake objects  
    * Testing for exceptions  

* **Status and Exceptions**  
A look at success and error states, including checking for exceptions and unwrapping AggregateExceptions.  
    * The danger of unobserved exceptions  
    * Using Task.IsCompleted, Task.IsFaulted  
    * Catching full exceptions with Task (AggregateException)  
    * Catching partial exceptions with await  
    * Dangers of async void methods  
    * Returning null vs. Task.CompletedTask or Task.FromResult  

* **Additional Topics** (if time permits)  
    * Letting asynchrony propagate  
    * Parallel programming and exceptions  

Prerequisites
-------------
This workshop assumes a good understanding of C# (classes, generics, methods, and interfaces) and a basic understanding of using async/await and Task, including running and awaiting asynchronous methods.

To complete the Labs (i.e., hands-on exercises), you will need to have the following installed:  
* **.NET Core 3.1 SDK**  
[https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)  
The .NET Core SDK (Software Development Kit) allows you to build .NET applications.  

* **Visual Studio Code**  
[https://code.visualstudio.com/download](https://code.visualstudio.com/download)  
This is a great all-around editor.  

* **VS Code C# Extension**  
[https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)  
This extension to Visual Studio Code adds syntax highlighing and code completion for C#.  

As an alternative, you can use **Visual Studio 2019** (any edition).  

To follow along with the Samples (which include desktop applications, web applications, web services, and unit tests), **Visual Studio 2019** (any edition) is recommended. The following workloads should be installed:

* **ASP&period;NET and web development**
* **.NET desktop development**

Labs
----
These are the hands-on portions of the workshop. Labs can be completed with Visual Studio Code or Visual Studio 2019. All labs run on Windows, macOS, and Linux.  

* Lab 01 - Recommended Practices and Continuations
* Lab 02 - Unit Testing Asynchronous Methods
* Lab 03 - Working with AggregateException

Each lab consists of the following:

* **Labxx-Instructions** (Markdown)  
A markdown file containing the lab instructions. This includes the scenario, a set of goals, and step-by-step instructions.  
This can be viewed on GitHub or in Visual Studio Code (just click the "Open Preview to the Side" button in the upper right corner).

* **Starter** (Folder)  
This folder contains the starting code for the lab.

* **Completed** (Folder)  
This folder contains the completed solution. If at any time, you get stuck during the lab, you can check this folder for a solution.

Samples
------- 
The Samples folder contains the samples that are shown during the lecture portions. This code is runnable with Visual Studio Code or Visual Studio 2019; however, these samples are Windows-only. (Visual Studio 2019 is recommended due to use of the debugger and integrated unit test runner.)  

*Additional information to be provided*

Additional Resources
--------------------
Links to articles, videos, and additional code samples:  

**Video Series & Articles (by Jeremy)**  
Each of these has a lot of supporting links:  
* [I'll Get Back to You: Task, Await, and Asynchronous Programming in C#](http://www.jeremybytes.com/Demos.aspx#TaskAndAwait)  
* [Run Faster: Parallel Programming in C#](http://www.jeremybytes.com/Demos.aspx#ParallelProgramming)  
* [Learn to Love Lambdas in C# (and LINQ, ToO!)](http://www.jeremybytes.com/Demos.aspx#LLL)  
* [Get Func-y: Delegates in .NET](http://www.jeremybytes.com/Demos.aspx#GF)  

**Related Articles (by Jeremy)**
* ["await.WhenAll" Shows 1 Exception - Here's How to See Them All](https://jeremybytes.blogspot.com/2020/09/await-taskwhenall-shows-one-exception.html)

**Other Resources**  
Stephen Cleary has lots of great articles, books, and practical advice.
* [Don't Block on Async Code](https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html) - Stephen Cleary
* [Async/Await - Best Practices in Asynchronous Programming](https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming) - Stephen Cleary  
* [ASP.NET Core SynchronizationContext](https://blog.stephencleary.com/2017/03/aspnetcore-synchronization-context.html) - Stephen Cleary  
* [There Is No Thread](https://blog.stephencleary.com/2013/11/there-is-no-thread.html) - Stephen Cleary  

Stephen Toub has great articles, too (generally with advanced insights).
* [Do I Need to Dispose of Tasks?](https://devblogs.microsoft.com/pfxteam/do-i-need-to-dispose-of-tasks/) - Stephen Toub
* [ConfigureAwait FAQ](https://devblogs.microsoft.com/dotnet/configureawait-faq/) - Stephen Toub  

For more information, visit [jeremybytes.com](http://www.jeremybytes.com).