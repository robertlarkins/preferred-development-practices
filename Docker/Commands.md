# Docker Commands

The way the Docker commands are presented here follows the same structure as the official [docker docs](https://docs.docker.com/reference/).


## General

- [`docker ps`](https://docs.docker.com/engine/reference/commandline/ps/)  
   List running containers. Use `docker ps -a` to list all containers.
- [`docker images`](https://docs.docker.com/engine/reference/commandline/images/)
  List images.

## Build
To build a Dockerfile open powershell and go to the directory where the Dockerfile is, then run this command
```ps
docker image build --tag name:tag .
```
ensuring the dot on the end is included as this specifies the current directory.
The build command can be called from a different directory to the Dockerfile simply by replacing the `.` with either the absolute directory or the path relative to where the command is being called from.

If the Dockerfile resides in a different location from where the paths inside it start at, then the build command can be called from where the Dockerfile expects to be called from and instead telling the build command where the Dockerfile is located:
```ps
docker image build --tag name:tag -f a/different/directory/Dockerfile .
```

The `name` portion of `--tag` must be lowercase.



See https://docs.docker.com/engine/reference/commandline/build/



## Run

```ps
docker run name:tag
```

## Explore

 - [`docker exec -it CONTAINER bash`](https://docs.docker.com/engine/reference/commandline/exec/#run-docker-exec-on-a-running-container)  
   Execute interactive `bash` shell on the container, allowing the container's file system to be explored.  
   The [flags `-i -t` are often written `-it`](https://docs.docker.com/engine/reference/run/#foreground).
   The `CONTAINER` variable is the container's name or UUID (either the long or short ID).

## Remove

### Remove Image
```powershell
docker rmi IMAGE
```
this is equivalent to
```powershell
docker image rm IMAGE
```

See:
 - https://docs.docker.com/engine/reference/commandline/rmi/
 - https://docs.docker.com/engine/reference/commandline/image_rm/
 
### Remove Container
```powershell
docker rm CONTAINER
```
this is equivalent to
```powershell
docker container rm CONTAINER
```

See:
 - https://docs.docker.com/engine/reference/commandline/rm/
 - https://docs.docker.com/engine/reference/commandline/container_rm/
 
## Temporary Container
Use the following command to create an image from a Dockerfile, run it on port 1880, then delete the container when closed:
```powershell
docker run --rm -p 1880:1880 -it --name mytest $(docker build -q .)
```
