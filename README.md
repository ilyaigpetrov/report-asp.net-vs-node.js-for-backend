# Comparison of ASP.NET MVC and Node.js for Backend Programming

We will compare ASP.NET and Node.js for backend programming.

## Table of contents

- [Comparing Apples to Oranges](#comparing-apples-to-oranges)
- [What is ASP.NET?](#what-is-aspnet)
- [Processing Models](#processing-models)
- [Programming Languages](#programming-languages)
- [Support for Asynchronous Programming](#support-for-asynchronous-programming)
-- [Examples: if-else Flow for Authentication Middleware](#examples-if-else-flow-for-authentication-middleware)
-- [Examples: Asynchronous Calls to HackerNews JSON API](#examples-asynchronous-calls-to-hackernews-json-api)
- [Simplicity](#simplicity)
-- Examples: Hello World
- [Abstractions and Conventions](#abstractions-and-conventions) <small>(ASP.NET MVC 5)</small>
- [Performance](#performance) <small>(ASP.NET MVC 4/Web API)</small>
- [Reliability](#reliability)
- [Learnability](#learnability)
- [Ecosystem](#ecosystem)
- [Conclusion](#conclusion)
- [Materials Used](#materials-used)

### Comparing Apples to Oranges

To compare two distinct concepts we need an aim.  
We can compare apples to oranges only if we know the aim, .e.g. our aim may be to figure out which fruit is less harmful for nutrition of small children, which is better for public speakers (produces less harm to the throat) and so on.  
Purpose described as 'backend programming' is very broad, within it there are tasks which are better suited for Node.js as well as there are such for ASP.NET.  
In this report we will narrow down our comparison to:

1. Support for asynchronous programming.
2. Robustness of the languages.
3. Simplicity of the code and deployment.
4. We will be comparing bleeding edge `ASP.NET 5 Beta` (vNext) and `Node.js` with `harmony` flag.  
The reason for that is that Microsoft is striving to adopt strong sides of Node.js in their latest ASP.NET so it closely resembles and mimics Node.js.  
ASP.NET 5 Beta is a striking competitor within .NET and asynchronous programming though it is not production ready yet.  
The current stable ASP.NET 4.5 (or ASP.NET MVC 5) and Node.js are so different that it's hard to compare them but we will say a few words on it too.

### What is ASP.NET?

ASP.NET  is a web application framework by Microsoft.  
ASP.NET offers common functionality for others frameworks on top of it:  
ASP.NET Web Pages, ASP.NET Web Forms, ASP.NET MVC, etc.  
For example, ASP.NET includes such common functionality as facilities for managing requests, handling sessions and a login security model based around membership.  
These frameworks has different goals, may be used together and will be merged into ASP.NET vNext.

The following diagrams present ASP.NET architecture in relation to other frameworks.

Diagram of ASP.NET 5 by [Scott Hanselman](http://www.hanselman.com/blog/ReleasedASPNETAndWebTools20122InContext.aspx), 2013:  
![ASP.NET Architecture by Scott Hanselman, 2013](http://www.redmond-recap.com/wp-content/uploads/2013/08/image_thumb.png)  


Diagram of ASP.NET 4.5 by [Shailendra Chauhan](http://www.dotnet-tricks.com/Tutorial/aspnet/SaJc221013-Understanding-Detailed-Architecture-of-ASP.NET-4.5.html), 2013:  
[![Architecture of ASP.NET 4.5, 2013](http://www.dotnet-tricks.com/Content/images/aspnet/asp.net4.5architecture.png)](http://www.dotnet-tricks.com/Tutorial/aspnet/SaJc221013-Understanding-Detailed-Architecture-of-ASP.NET-4.5.html)  


Diagram of ASP.NET 5 by [Jouni Heikniemi](http://www.redmond-recap.com/2013/08/22/state-of-asp-net-part-2-one-asp-net/), 2013:  
[![Architecture of ASP.NET, 2013](http://www.redmond-recap.com/wp-content/uploads/2013/08/AspnetCoreStack_thumb.png)](http://www.redmond-recap.com/2013/08/22/state-of-asp-net-part-2-one-asp-net/)  


In 2008 Rick Strahl [defined](http://www.west-wind.com/presentations/howaspnetworks/howaspnetworks.asp) ASP.NET in the following way:
> __ASP.NET is a sophisticated engine using Managed Code for front to back processing of Web Requests.__ ASP.NET is a request processing engine. It takes an incoming request and passes it through its internal pipeline to an end point where you as a developer can attach code to process that request. This engine is actually completely separated from HTTP or the Web Server.

Building web applications in pure ASP.NET without MVC, Web API or other overhead seems possible but it's certainly not a common case and I didn't find any documents explaining when it's needed and how to achieve it.  
So in this article we will assume that at least one of ASP.NET overhead frameworks is used, preferably ASP.NET MVC.

Sources: [asp.net](http://www.asp.net/get-started/websites).

### Processing Models

The main difference between Node.js and ASP.NET frameworks is their processing models.  
Node.js dictates asynchronous processing style while ASP.NET offers programmer a choice.  
Node.js was build around asynchronous model from ground up while `async` keyword appeared in ASP.NET MVC 4.  
Figures below demonstrate differences between synchronous and asynchronous models for a web application.

![Node.js processing model](https://strongloop.com/wp-content/uploads/2014/01/threading_node.png)

As shown Node.js uses one thread for handling requests and many threads to provide asynchronous non-blocking IO (e.g., to the database or to other REST server). Fewer threads means fewer memory for stack allocation and more economic usage of the CPU.

![enter image description here](https://strongloop.com/wp-content/uploads/2014/01/threading_java.png)

The diagram above shows multi threaded server that may be found, e.g., in Java.
In this model the server spawns new thread for handling each request which sleeps on blocking IO operations consuming CPU and memory resources.

So how exactly does ASP.NET work? ASP.NET doesn't use one thread but instead uses restricted number of threads from the pool and queues requests to it. Threads may be terminated on asynchronous operations like in Node.js. However, ASP.NET processing model is more prone to context switching which implies additional CPU costs. More than that as ASP.NET and .NET were not designed with asynchronous programming in mind some libraries may still offer no support for it or offer "fake asynchrony", this makes .NET freedom of async choice quite restricted.

Sources:  
[What Makes Node.js Faster Than Java?](https://strongloop.com/strongblog/node-js-is-faster-than-java/)  
[Node.js Thesis: IO is Expensive](http://blog.mixu.net/2011/02/01/understanding-the-node-js-event-loop/)  
[ASP.NET Thread Usage on IIS 7.5, IIS 7.0, and IIS 6.0](http://blogs.msdn.com/b/tmarq/archive/2007/07/21/asp-net-thread-usage-on-iis-7-0-and-6-0.aspx)  
[Node.js and Context Switching](http://stackoverflow.com/questions/16707098/node-js-kernel-mode-threading)  
[Opinionated Node.js vs. Non-opinionated ASP.NET](http://stackoverflow.com/a/11060092/521957)
[How does Asynchronous Model Work](https://msdn.microsoft.com/en-us/magazine/dn802603.aspx)


### Programming Languages

ASP.NET uses C# as its primary language.  
Node.js uses JavaScript and all the languages that can be transpiled to it like CoffeeScript, Microsoft TypeScript and recent EcmaScript2015 (aka ES6).

Without doubt C# is a more powerful language than JavaScript.  
C# offers strict type system and compile-time error checks and in JavaScript you may get it through Facebook Flow static type checker or Microsoft TypeScript.  
C# has classical inheritance model, EcmaScript6 classes offer new syntax for the same prototypical inheritance (which is claimed to be not so powerful).  

However some developers are in fear of .NET software patent system, e.g.:  
- [Why free software shouldn't depend on Mono or C#, Richard Stallman](https://www.fsf.org/news/dont-depend-on-mono)  
- [Comparing Java to .Net](http://en.swpat.org/wiki/Comparing_Java_to_.Net_and_C-sharp).  

I don't see any threat from JavaScript software patents.

JavaScript is a more ubiquitous language though to master Node.js the developer is also required to be acquainted with asynchronous programming style.

### Support for Asynchronous Programming

Both, C# and JavaScript support asynchronous programming.  
C# exerts `async` and `await` keywords.  
As a pioneer Node.js proposed several approaches to asynchronous programming:  

- Using callbacks. This way your code becomes hard to read, and control flow is hardened. See Node.js Pyramid of Doom or http://callbackhell.com for examples. This approached appeared the first. 
- `yield` keyword, co-routines, generators and Promises eliminate your code from deep nested callbacks and makes it easier to read. This approach is demonstrated by Koajs web framework.
- `await` and `async` keywords are part of experimental EcmaScript7 standard and may be used today with transpilers like Babel. This syntax constructs make your code more clear for those not introduced to Promises and generators.

So, C# asynchronous model is more clear.  
Node.js is slightly different though:  

1. It compels you to write your code in asynchronous manner, so most libraries support asynchronous model.
2. It implies fewer context switches as stated by some sources.

#### Examples: if-else Flow for Authentication Middleware

Koa.js and `yield` asynchronous approach:

```javascript
var koa = require('koa');
var app = koa();

// x-response-time

app.use(function *(next){
  var start = new Date;
  yield next;
  var ms = new Date - start;
  this.set('X-Response-Time', ms + 'ms');
});

// logger

app.use(function *(next){
  var start = new Date;
  yield next;
  var ms = new Date - start;
  console.log('%s %s - %s', this.method, this.url, ms);
});

// response

app.use(function *(){
  this.body = 'Welcome to Koa.js!';
});

app.listen(3000);

```
The following example demonstrates `if-else` control flow implemented in asynchronous manner.  
The desired flow is the following:
```javascript
if( isAuthenticatedAsync() ) {
  return 'Success, you are authenticated';
} else {
  return 'Failure, you are not authenticated.'
}
```
First approach, keep state in object passable between callbacks.  
Example inspired by Express.js and Passport.js.  
```javascript
jsonRouter.route('/login')
  .get(
    authenticateMiddleware,
    function loginMiddleware(req, res, next) {
        if (!req.isAuthenticated())
            return res.json('Failure, you are not authenticated.');
        return res.json('Success, you are authenticated');
    }
```
`authenticateMiddleware` keeps its state somewhere in `req` object and checks it with `req.isAuthenticated` method.  

The same approach, but with Koa.js, the state is kept in `this` context object.
```javascript
/* Warning!
   Don't copy this code!
   It may be not idiomatic as it is written by non-experienced programmer.
*/
var koa = require('koa');
var app = koa();

function *isAuthenticated(next) {
  this.isAuthenticated = true;
  return yield next;
}

app.use(isAuthenticated);

app.use(function *(){
  if (!this.isAuthenticated)
    return this.body = 'Failure, you are not authenticated.';
  return this.body = 'Success, you are authenticated'
});
```
Second approach, throw exception and process it the previous middleware.  
Excerpt from Koa.js [examples](https://github.com/koajs/examples/blob/master/base-auth/app.js), middleware `auth` throws exception if the user is not authenticated:
```javascript
var koa = require('koa');
var auth = require('koa-basic-auth');
var app = module.exports = koa();

// custom 401 handling

app.use(function* (next){
  try {
    yield* next;
  } catch (err) {
    if (401 == err.status) {
      this.status = 401;
      this.body = 'cant haz that';
    } else {
      throw err;
    }
  }
});

// require auth

app.use(auth({ name: 'tj', pass: 'tobi' }));

// secret response

app.use(function* (){
  this.body = 'secret';
});
```

#### Examples: Asynchronous Calls to HackerNews JSON API

Popular [HackerNews site](news.ycombinator.com) has a [json api](https://github.com/HackerNews/API).  
Shortly, it works in the following way:
```http
GET https://hacker-news.firebaseio.com/v0/topstories.json
-> responds with an array of items like [10023413,10022014...]
GET https://hacker-news.firebaseio.com/v0/item/10023413.json
-> responds with something like:
{
  "by": "steveklabnik",
  "descendants": 2,
  "id": 10023413,
  "kids": [
    10023645
  ],
  "score": 81,
  "text": "",
  "time": 1438965545,
  "title": "Announcing Rust 1.2",
  "type": "story",
  "url": "http://blog.rust-lang.org/2015/08/06/Rust-1.2.html"
}
```
In these examples we will be retrieving top story title from HN API.  
Here is how it may be done in ASP.NET 5:
```csharp
// Startup.cs, 54 lines
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace HelloWeb
{
  public class Startup
  {
  
    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      var logger = loggerFactory.AddConsole().CreateLogger(this.GetType().Name);
      logger.LogInformation("Configuring...");
      
      app.Run(async (context) => {
        var path = context.Request.Path;
        logger.LogInformation("Has request for "+path+"!");
        if (!path.Equals("/"))
          return;

        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri("https://hacker-news.firebaseio.com");
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          HttpResponseMessage response = await client.GetAsync("/v0/topstories.json?print=pretty");
          logger.LogInformation("Got first response.");
          if (response.IsSuccessStatusCode)
          {
            var arr = await response.Content.ReadAsAsync<dynamic>();
            var responseId = (String) arr[0];
            var itemUrl = String.Format("/v0/item/{0}.json?print=pretty", responseId);
            response = await client.GetAsync(itemUrl);
            logger.LogInformation("Got second response.");
            if (response.IsSuccessStatusCode)
            {
              var dict = await response.Content.ReadAsAsync<dynamic>();
              var title = (String) dict["title"];
              logger.LogInformation(title);
              await context.Response.WriteAsync(title);
              return;
            }
          }
          logger.LogInformation("Error:" + await response.Content.ReadAsStringAsync());
          await context.Response.WriteAsync("Service is not available!");
        }      
      });
    }
  }
}
```
Corresponding bare bone Node.js server script:
```javascript
// server.js
var http = require('http');
var https = require('https');

// Biolerplate code to produce get requests.
function httpsGet(url, handler, errorHandler) {
  https.get(
    url,
    function(response){
      console.log('Got response, receiving body by chunks.');
      response.setEncoding('utf8');
      var data = '';
      response.on(
        'data',
        function (chunk) {
          data += chunk;
        }
      );
      response.on(
        'end',
        function() {
          console.log('Body received, calling handler.');
          if (200 <= response.statusCode && response.statusCode < 400)
            return handler(data);
          return errorHandler(data);
        }
      );
    }
  ).on('error', errorHandler);
}

var server = http.createServer(
  function (req, res) {  
    console.log('Has request!');
    var baseAddress = 'https://hacker-news.firebaseio.com';
    function errorHandler(e) {
      console.log('Error:'+e);
      return res.end('Service is not available!');
    }
    httpsGet(
      baseAddress+'/v0/topstories.json?print=pretty',
      function(data) {
        console.log(data);
        var arr = JSON.parse(data);
        var responseId = arr[0];
        var itemUrl = '/v0/item/'+ responseId +'.json?print=pretty';
        httpsGet(
          baseAddress + itemUrl,
          function(data) {
            var obj = JSON.parse(data);
            var title = obj['title'];
            res.end(title);
          },
          errorHandler
        );
      },
      errorHandler
    );
  }
);

var port = 5001;
server.listen(port);
console.log('Server running at http://localhost:'+port);
```
To get rid of [boilerplate code](https://en.wikipedia.org/wiki/Boilerplate_code) you can use either [request](https://github.com/request/request) or visionmedia [superagent](https://github.com/visionmedia/superagent).  
To counter callback hell let's use `co@tj` library.  
After all these corrections:
```javascript
// server.js, 40 lines
// To run: `node --harmony server.js`
var http = require('http');

var request = require('superagent');
var co = require('co');

var server = http.createServer(
  function (req, res) {  
    console.log('Has request for '+req.url+'!');    
    if (req.url !== '/')
      return res.end();
 
    co(function *() {
      var baseAddress = 'https://hacker-news.firebaseio.com';
      var topUrl = baseAddress+'/v0/topstories.json?print=pretty';
      var httpRes = yield request.get(topUrl);
      console.log('Got first response.');
      if (httpRes.ok) {
        var arr = httpRes.body;
        var responseId = arr[0];
        var itemUrl = '/v0/item/'+ responseId +'.json?print=pretty';
        var httpRes = yield request.get(baseAddress + itemUrl);
        console.log('Got second response.');
        if (httpRes.ok) {
          var obj = httpRes.body;
          var title = obj['title'];
          return res.end(title);
        }
      }
    }).catch(
      function (err) {
        console.log('Error:'+e);
        return res.end('Service is not available!');
      }
    );
  }
);

var port = 5001;
server.listen(port);
console.log('Server running at http://localhost:'+port);
```

### Simplicity

To assess simplicity let's consider hello world examples of Node.js and ASP.NET vNext.  

Node.js project file structure:
```sh
.
├── server.js
└── package.json // Package (or project) description and dependencies.
```

Node.js, Express.js
```javascript
// server.js
var express = require('express');
var app = express();

app.get('/', function (req, res) {
  res.send('Hello World!');
});

app.listen(3000); // Deploy a server on port 3000.
```
Node.js, Koa.js
```javascript
// server.js
var koa = require('koa');
var app = koa();

app.use(function *(){
  this.body = 'Hello World!';
});

app.listen(3000); // Deploy a server on port 3000.
```
Commands used to run samples:
```sh
cd hello-nodejs
npm install
node server.js
```

Now let's look at some corresponding examples for ASP.NET 5 Beta (vNext).  

All examples are taken from https://github.com/aspnet/home and _modified_ for comparison.

"HelloWeb" sample, ASP.NET project file structure:
```sh
.
├── Dockerfile
├── HelloWeb.xproj
├── project.json
├── Properties
│   └── launchSettings.json
├── Startup.cs
└── wwwroot
```
The main file:
```csharp
// Startup.cs
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;

namespace HelloWeb
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.Run(async (context) => {
              await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
```

Commands I've used to run HelloWeb sample.
```sh
cd HelloWeb
dnvm upgrade -u
dnu restore
dnx . web
```

### Abstractions and Conventions
##### ASP.NET MVC 5 vs Node.js

Node.js is "close to the metal". It offers fewer and thinner abstractions.  
So you don't have to configure their overwhelming number of parameters, but instead you are faced with writing the code out of multitude small components.  
This approach gives you flexibility of tailoring the code up to the solution.  
It may be expected that in place of configuration the programmer is supposed to write much more boilerplate code.  
It's not the case with Node.js as it offers all needed facilities from native or third party libraries at the same time not compelling you to use bloated abstractions.  
Node.js programmer is free in choice of programming libraries, has a uniform way of combining them as middlewares, may easily divert from MVC in favor of any other architectural pattern and controls options that may be concealed by abstractions in other frameworks.  
Node.js imposes only a few conventions: asynchronous programming, most servers and frameworks use middlewares (Express.js, Koa.js).

Like Node.js ASP.NET 5 also proposes middlewares and low-level "close to the metal" control. 

But on the other hand ASP.NET MVC 5 is quite different.  
It offers abstractions but doesn't imply much configuration or boilerplate code.  
Instead it leverages convection other configuration principle.  
It means it has conventions which are when followed make your code concise and readable.  
E.g. there is a predefined folder structure for every MVC project to follow, naming conventions for controllers, etc.  
So, yes, ASP.NET imposes upon you its view of things. And if you want to divert from conventions then you are deprived of all this automatic out-of-the-box behavior and faced with manual configuration.  
Now, the question is whether these conventions are good and apt for your tasks, will they make you happy with your task so you don't have to configure everything from scratch.

Conclusion:
- ASP.NET MVC exerts many conventions with which everything works out of the box, but tailoring code for your needs requires more configuration.
- ASP.NET MVC offers many abstractions which may be great for large applications but seem bloated for simple tasks. Also programmer looses sense of control when everything is automated behind abstractions.
- Node.js and ASP.NET 5 impose very few conventions but offer great flexibility.

### Performance
##### ASP.NET MVC 4/Web API vs Node.js

Certainly there are tasks where ASP.NET outperforms Node.js and there are tasks where ASP.NET looses.  
To take advantage of multi core system Node.js must be clustered.  
To outperform with IO-expensive tasks ASP.NET must use `async`/`await` keywords.  

Here is a chart benchmarking ASP.NET Web API and Node.js for asynchronously reading POST body, 2012, [source](http://mikaelkoskinen.net/post/asp-net-web-api-vs-node-js-benchmark-take-2).

![ASP.NET Web API vs Node.js](http://adafyservicesstorage.blob.core.windows.net/mikael/image_76.png)

Chart below compares performance of Node.js and ASP.NET MVC 4 for asynchronously reading json file from filesystem, 2012, [source](http://rarcher.azurewebsites.net/Post/PostContent/19).

![Node.js vs MVC 4](http://rarcher.azurewebsites.net/Images/nodeMvc30.png)

The explanation why asynchronous Node.js is faster than asynchronous ASP.NET may be that it uses fewer context switches.

Node.js is created for fast request handling without heavy computations. In all tasks requiring heavy computations Node.js will certainly loose to ASP.NET.  
In cases where Node.js is not clustered it looses to ASP.NET, e.g. [see this](http://stackoverflow.com/a/10641377/521957).


### Portability

Except some issues Node.js works great on all major platforms.  
ASP.NET is ported partially, e.g. MVC4 on Mono lacks support for async features.  
The forthcoming ASP.NET 5 is expected to cover all major platforms.  
Definitely, currently Node.js wins in portability.  
Sources: [Mono Compatibility](http://www.mono-project.com/docs/about-mono/compatibility/).

### Reliability

Reliability is defined by how robust your program to failures in input, runtime exceptions, programming errors, etc.  
C# is more robust as it offers strict-type system which JavaScript lacks.  
Things get more complicated when dealing with error handling in asynchronous code. It's more robust to use generators with `yield` wrapped in `try`/`catch` instead of error callbacks in Node.js.  
Some programmers are not happy with JavaScript being not robust, e.g. [see this](https://medium.com/@tjholowaychuk/farewell-node-js-4ba9e7f3e52b).


### Learnability

Learnability is defined by how easy it is for a newcomer to pick up the language.  
Being dynamically typed, JavaScript is easier to pick up, but in Node.js to deliver high-quality code you eventually has to know Promises, generators and co-routines. New syntax sugar of upcoming EcmaScript standards like ES2015 and ES7 adds more ways to express the same meaning making non-recent code less readable for newcomers.

### Ecosystem

By ecosystem I mean the availability of third party packages and community support.    
As Node.js is more portable its community is wider. JavaScript is also more ubiquitous than .NET. Provided with the idea of louse coupling Node.js ecosystem is rich and robust.

### Conclusion

Java outperforms C in overwhelming majority of aspects. But if you have to write high-performance code, e.g., system driver, or independent project then Java is no choice in favor of C.  
The following table summarizes comparison.

\+ means "wins"  
\- means "looses"  

| Characterisitc     		| Node.js   | ASP.NET  
| ----------------				|:------:   |:-------:  
| IO Performance	   		| +		 | -  
| Computational Perf.	| -		 | +  
| Asynchronous Programming | required	 | possible  
| Languages		       		| JS looses | C# wins  
| Portability		     		| +		 | -  
| Reliability		     		| -		 | +  
| Ecosystem		 	     	| +		 | -  
| Learnability		   		| +	 	 | -  
| Simplicity		     		| +	 	 | -  
| Flexibility		     		| +	 	 | -  

Flexibility -- how easy it is to tailor framework to your specific needs.

### Materials Used
[To Node.js Or Not To Node.js](http://www.haneycodes.net/to-node-js-or-not-to-node-js/)  
[Is Node.js better than ASP.NET?](https://thomasbandt.com/is-nodejs-better-than-aspnet)  
[The Node.js Philosophy](http://blog.nodejitsu.com/the-nodejs-philosophy/)

The End
---

---

## Marginalia

### Principles behind Node.js and C#/ASP.NET/ASP.NET MVC

TO BE DONE: This chapter is just a draft, don't take it seriously.

ASP.NET MVC embraces Single Responsibility Principle and I'm pretty sure it embraces the whole [SOLID](https://en.wikipedia.org/wiki/SOLID) set of principles of OOP.  

To quote Eilon Lipton:
> ...each of the components in the MVC framework is fairly small and self contained, with single responsibilities. This means that due to their small size you have building blocks that are easier to understand. It also means that you can replace or even alter the building blocks if they don't suit your needs.


| Characterisitc     							| Node.js  	| ASP.NET
| ----------------   								|:------:   	|:-------
| SRP and SoC 									| Yes		 		| SOLID
| Louse Coupling								| Yes		 		| DIP<br/>Dependence on Interfaces
| Extensibility  									| Composition?				| Override of Classes<br/>Composition
| Close to Metal 								| Yes		 		| No, abstract

DIP -- Dependency Inversion Principle

> Separation of Concerns (SoC) – is the process of breaking a computer program into distinct features that overlap in functionality as little as possible. A concern is any piece of interest or focus in a program. Typically, concerns are synonymous with features or behaviors.
http://en.wikipedia.org/wiki/Separation_of_concerns

> Single Responsibility Principle (SRP) – every object should have a single responsibility, and that all its services should be narrowly aligned with that responsibility. On some level Cohesion is considered as synonym for SRP.
http://en.wikipedia.org/wiki/Single_responsibility_principle

Sources:  
[ASP.NET MVC Design Philosophy, Eilon Lipton, 2007](https://web.archive.org/web/20150627050706/http://weblogs.asp.net/leftslipper/asp-net-mvc-design-philosophy)  
[The Node.js Philosophy](http://blog.nodejitsu.com/the-nodejs-philosophy/)