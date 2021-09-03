# How to Setup Mediatr

Install the MediatR NuGet package.

## With AutoFac

Install the following NuGet packages
 - Autofac
 - MediatR.Extensions.Autofac.DependencyInjection

In the `ConfigureContainer` method in Startup.cs (see https://autofac.readthedocs.io/en/latest/integration/aspnetcore.html#startup-class) add

```C#
public void ConfigureContainer(ContainerBuilder builder)
{
    // Mediatr
    var assembliesToRegisterForMediatr = new[]
    {
        typeof(SomeAssemblyBeacon).Assembly // SomeAssemblyBeacon.cs is an empty file that you create at the root of a project to identify the assembly.
    };

    builder.RegisterMediatR(assembliesToRegisterForMediatr);
}
```
