<p align="center"> <img src="log.png" alt="log(ging)" width="80"/> </p>

## Logging in ASP.NET Core using ILoggerFactory-pattern to Azure Application Insight


> **Disclaimer:**
> Images and steps used in this example may change over time, but may be a good reference.

## Goal
**Short answer:** Make logging great again

**Slightly longer answer:** Make the application send performance-data to Application Insight. Also make all logging from the frameworks, and logging done by me, automatically be sent to Azure for further analysis.

### Packages used
* **NLog** - Base package for NLog
* **NLog.Web:AspNetCore** - Adds helpers and layout renderes for the ASP.NET core platform
* **Lightinject** - IOC-container
* **Lightinject.Microsoft.AspNetCore.Hosting** - Enables Lightinject to be used in ASP.NET core applications
* **Microsoft.ApplicationInsight.AspNetCore** - Official package for working with Azure Application Insight
* **Microsoft.ApplicationInsight.NLogTarget** - Allowing you to send NLog messages to Application Insight

# Steps

## 01 - Azure

1. First we have to make ourself a Application Insight resource in Azure.
<p align="center"> <img src="images/Azure-Step1.png" alt="drawing" width="550"/> </p>

<p align="center"> <img src="images/Azure-Step2.png" alt="drawing" width="550"/> </p>

3. Fill in information about your application, and give it a descriptive name
<p align="center"> <img src="images/Azure-Step3.png" alt="drawing" width="550"/> </p>

4. Now we are almost done. Create it, and take note of the Instrumentation Key. We will use it shortly
<p align="center"> <img src="images/Azure-Step4.png" alt="drawing" width="550"/> </p>

## 02 - Code
This is based on the example project in this repo. It is a simple project created using the dotnet cli and this command: `dotnet new webapi`. 

After the template is bootstrapped we need some base-files and interfaces. Big shoutout to @andmos for providing me with the base logfiles for this project. 

File | What it does
------------ | -------------
`ILog.cs` | Is the base log-interface used when logging.
`ILogFactory.cs` | The abstraction of the log-factory. This gets the implemented logger from the IOC-container
`Log.cs` | Gets bootstrapped to log-actions for common `Log Levels`.
`NLogFactory.cs` | Implements a factory that produces logging based on NLog.
`ILogFactoryInitializer` | An abstraction for the builder-method that adds the logger to the ASP.Net Core-app.
`NLogFactoryInitializer` | Implements the `.UseNLog()` used in `Program.cs` to setup NLog for DI.


### Noteable changes to the code:
**Program.cs** - gets logger factory initializer *(wow. good name. thanks)* from container and passes the `WebHostBuilder` to the implementet builder-method.

```csharp
public static void Main(string[] args)
{
    var container = new ServiceContainer();
    var loggerFactoryInitializer = container.GetInstance<ILogFactoryInitializer>();
    var webhostBuilder = CreateWebHostBuilder(args);
    loggerFactoryInitializer.UseLogger(webhostBuilder);
    
    webhostBuilder.Build().Run();
}
``` 
**ValuesController.cs** - Added examples of logging. With `Log Level` *Information* and *Error*.

```csharp
// GET api/values
[HttpGet]
public ActionResult<IEnumerable<string>> Get()
{
    var values = new []{"value1", "value2"};
    _logger.Info($"Request made, returning:{JsonConvert.SerializeObject(values)}");
    return values;
}
```

```csharp
// GET api/values
[HttpGet("ThrowPlease")]
public IActionResult ThrowMePlox()
{
    try
    {
        throw new ArithmeticException("Dividing by 0?? Good luck");
    }
    catch (Exception e)
    {
        _logger.Error("This was bad. not good", e);
        return BadRequest();
    }
}
```


**appsettings.json** - contains the Application Insight Key for this project. This can also easily be set as an Enviromental Variable.
The Application Insight framework will automatically search for this path in `appsettings.json` 

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ApplicationInsights": {
    "InstrumentationKey": "<KEY>"
  },
  "AllowedHosts": "*"
}
```

## 03 - Admire the result
<p align="center"> <img src="images/Result1.png" alt="result1" width="650"/> </p>
<p align="center"> <img src="images/Result2.png" alt="result2" width="650"/> </p>

>Log-entries can be searched and filtered in the `search`-tab in Application Insight. Other performance-data can be found i `Metrics`. 

## Test the example app


```bash
# clone the project
git clone https://github.com/simonkaspersen/Logging-to-Application-Insight-in-ASP.NET-Core.git

# enter the project directory
cd Logging-to-Application-Insight-in-ASP.NET-Core

# Add your key to appsettings.json

# install dependencies
dotnet restore

# build solution
dotnet build

# Run
dotnet run
```

This will open the API at http://localhost:5000.

* Go to `http://localhost:5000/api/values/` to test *Information*-logging
* Go to `http://localhost:5000/api/values/throwplease` to test *Error*-logging


## Links to resources
* [NLog - ASP.NET core](https://github.com/NLog/NLog.Web/wiki/Getting-started-with-ASP.NET-Core-2)
* [NLog - Application Insight](https://cmatskas.com/working-with-application-insights-and-nlog-in-console-apps-net/)

## TODO
- [x] Deside if *Pepsi Max* or *Coca Cola No Sugar* is best
- [x] Implement **NLog**
- [ ] Implement **Log4Net**
- [ ] Implement **Serilog**


## Thanks to
* **@andmos**

