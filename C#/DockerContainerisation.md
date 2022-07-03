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

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" Sdk="Microsoft.Docker.Sdk">
  <PropertyGroup Label="Globals">
    <ProjectVersion>2.1</ProjectVersion>
    <ProjectGuid>a guid</ProjectGuid>
    
    <!-- Specifies additional compose files in a semicolon-delimited list to be sent out to docker-compose.exe for all commands. Relative paths from the docker-compose project file (dcproj) are allowed. -->
    <AdditionalComposeFilePaths></AdditionalComposeFilePaths>

    <!-- Specifies the first part of the filenames of the docker-compose files, without the .yml extension. For example:
    1. DockerComposeBaseFilePath = null/undefined: use the base file path docker-compose, and files will be named docker-compose.yml and docker-compose.override.yml.
    2. DockerComposeBaseFilePath = mydockercompose: files will be named mydockercompose.yml and mydockercompose.override.yml.
    3. DockerComposeBaseFilePath = ..\mydockercompose: files will be up one level.
    default value: docker-compose -->
    <DockerComposeBaseFilePath></DockerComposeBaseFilePath>
    
    <!-- Specifies the extra parameters to pass to the docker-compose build command. For example, --parallel --pull. -->
    <DockerComposeBuildArguments></DockerComposeBuildArguments>
    
    <!-- Specifies the extra parameters to pass to the docker-compose down command. For example, --timeout 500. -->
    <DockerComposeDownArguments></DockerComposeDownArguments>
    
    <!-- If specified, overrides the project name for a docker-compose project. Default value: "dockercompose" + auto-generated hash -->
    <DockerComposeProjectName></DockerComposeProjectName>
    
    <!-- Specifies projects to be ignored by docker-compose tools during debug. This property can be used for any project. File paths can be specified one of two ways:
    1. Relative to dcproj. For example, <DockerComposeProjectsToIgnore>path\to\AngularProject1.csproj</DockerComposeProjectsToIgnore>.
    2. Absolute paths.
    Note: The paths should be separated by the delimiter character ;. -->
    <DockerComposeProjectsToIgnore></DockerComposeProjectsToIgnore>
    
    <!-- Specifies the extra parameters to pass to the docker-compose up command. For example, --timeout 500. -->
    <DockerComposeUpArguments></DockerComposeUpArguments>

    <!-- Controls whether the user project is built in the container. The allowed values of Fast or Regular control which stages are built in a Dockerfile. The Debug configuration is Fast mode by default and Regular mode otherwise. Default value: Fast -->
    <DockerDevelopmentMode></DockerDevelopmentMode>

    <!-- Specifies the launch action to perform on F5 or Ctrl+F5. Allowed values are None, LaunchBrowser, and LaunchWCFTestClient. -->
    <DockerLaunchAction>LaunchBrowser</DockerLaunchAction>

    <!-- Indicates whether to launch the browser. Ignored if DockerLaunchAction is specified. Default value: False -->
    <DockerLaunchBrowser>False</DockerLaunchBrowser>

    <!-- If DockerLaunchAction or DockerLaunchBrowser are specified, then DockerServiceName specifies which service referenced in the docker-compose file gets launched. -->
    <DockerServiceName>some name</DockerServiceName>

    <!-- The URL to use when launching the browser. Valid replacement tokens are "{ServiceIPAddress}", "{ServicePort}", and "{Scheme}". For example: {Scheme}://{ServiceIPAddress}:{ServicePort} -->
    <DockerServiceUrl>{Scheme}://localhost:{ServicePort}</DockerServiceUrl>

    <!-- The target OS used when building the Docker images -->
    <DockerTargetOS>Linux</DockerTargetOS>
  </PropertyGroup>
  <ItemGroup>
    <None Include="docker-compose.yml" />
    <None Include="docker-compose.*.yml">
      <DependentUpon>docker-compose.yml</DependentUpon>
    </None>
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
