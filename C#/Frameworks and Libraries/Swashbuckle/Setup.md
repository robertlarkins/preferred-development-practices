# Swashbuckle Setup

## Getting xml docs from multiple projects

Firstly all the projects that need to provide xml docs to Swashbuckle need to have *XML documentation file* checked, which is found in Project properties > Build > Output.
Then in the SwaggerConfig.cs class ensure `c.IncludeXmlComments` is provided.
However, `IncludeXmlComments` needs to be called for every xml doc that has comments you want to include in the OpenAPI schema.
To do this add the following:
```C#
val xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
xmlFiles.ForEach(xmlFile => swaggerGenOptions.IncludeXmlComments(xmlFile));
```

See:
 - https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio#xml-comments
 - https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/93#issuecomment-458690098
