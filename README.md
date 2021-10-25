# wscore
Xml Web Services for .NET Core (Port from ASP.NET XML Web Services)

## Getting Started

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    //Register GreetingService.
    services.AddTransient<GreetingService>();

    //
    services.AddWSCore(options =>
    {
        options.VirtualPath = "/ws"; //WSDL Path => /ws/GreetingService.asmx?wsdl
        options.EnabledProtocols = WebServiceProtocols.HttpSoap | WebServiceProtocols.HttpSoap12 | WebServiceProtocols.Documentation;
        //options.SoapExtensionReflectorTypes.Add(...);
    });
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    //Configure WSCore.
    app.UseWSCore();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```
