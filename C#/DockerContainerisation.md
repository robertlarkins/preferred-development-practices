# Docker Containerisation
This provides the steps for setting up Docker containerisation for a .NET Core Application.

## Files to add

### docker Folder

Add a _docker_ folder to your root folder.

Add a Directory.Build.props file with the following contents to stop the Directory.Build.props in the root directory applying:
```
<?xml version="1.0" encoding="utf-8"?>
<Project>
<!-- This is empty to stop the properties from root being applied to docker -->
</Project>
```

### docker-compose.dcproj

```
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" Sdk="Microsoft.Docker.Sdk">
  <PropertyGroup Label="Globals">
    <ProjectVersion>2.1</ProjectVersion>
    <DockerTargetOS>Linux</DockerTargetOS>
    <ProjectGuid>a guid</ProjectGuid>
    <DockerLaunchAction>LaunchBrowser</DockerLaunchAction>
    <DockerServiceUrl>{Scheme}://localhost:{ServicePort}</DockerServiceUrl>
    <DockerServiceName>some name</DockerServiceName>
    <IsPackable>false</IsPackable>
  </PropertyGroup>
  <ItemGroup>
    <None Include="docker-compose.*.yml">
      <DependentUpon>docker-compose.yml</DependentUpon>
    </None>
    <None Include="docker-compose.yml" />
  </ItemGroup> 
  <Target Name="Pack">
  </Target>
</Project>
```

### docker-compose.yml

```
version: '3.4'

services:
  your.service.name:
    environment:
      - environment_variable=value
      - another_env_var=its_value
    ports:
      - '1234:80'
    depends_on:
      - other.service.name1
      - other.service.name2
  your.second.service:
    images: name-of-image
    ports:
      - '1234:1234'
    environment:
      VarName: "var value"
    volumes:
      - location-to-store
    networks:
      default:
        aliases:
          - alternate-name
```


## Dockerfile

Some small insight into why it is structured the way it is:
https://stackoverflow.com/questions/53460002/how-to-use-dotnet-restore-properly-in-dockerfile

## .dockerignore
The .dockerignore file (name *.dockerignore*) lives 

https://docs.docker.com/engine/reference/builder/#dockerignore-file

### Location
The .dockerignore file should be placed at the root of the docker build context.
For a .NET Core Application, this will likely just be at the root of the repo.

### .dockerignore file
This .dockerignore was generated using Visual Studio when adding Docker Support to a project.

```
**/.classpath
**/.dockerignore
**/.env
**/.git
**/.gitignore
**/.project
**/.settings
**/.toolstarget
**/.vs
**/.vscode
**/*.*proj.user
**/*.dbmdl
**/*.jfm
**/azds.yaml
**/bin
**/charts
**/docker-compose*
**/Dockerfile*
**/node_modules
**/npm-debug.log
**/obj
**/secrets.dev.yaml
**/values.dev.yaml
LICENSE
README.md
```
