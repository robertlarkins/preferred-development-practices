# Docker Containerisation
This provides the steps for setting up Docker containerisation for a .NET Core Application.

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
