# Swagger

## Swagger Generation Options
These go into the `startup.cs` file:


```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddSwaggerGen();
    services.AddOptions<SwaggerGenOptions>()
      .Configure(ConfigureSwaggerGenOptions);
}

private void ConfigureSwaggerGenOptions(SwaggerGenOptions options)
{
    // Use fully qualified object names to avoid name collisions in the swagger schema.
    options.CustomSchemaIds(x => x.FullName);
}
```

Calling the options, if additional parameters are wanted to be injected then use

```C#
services.AddOptions<SwaggerGenOptions>()
		  .Configure<IOptions<MyParameters>>(ConfigureSwaggerGenOptions);
      
private void ConfigureSwaggerGenOptions(SwaggerGenOptions options, IOptions<MyParameters> myParams)
{
    // Use fully qualified object names to avoid name collisions in the swagger schema.
    options.CustomSchemaIds(x => x.FullName);
}
```
