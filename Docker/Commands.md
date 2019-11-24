# Docker Commands

The way the Docker commands are presented here follows the same structure as the official [docker docs](https://docs.docker.com/reference/).


## General

 - [`docker ps`](https://docs.docker.com/engine/reference/commandline/ps/)  
   List running containers. Use `docker ps -a` to list all containers.

## Build


## Run


## Explore

 - [`docker exec -it CONTAINER bash`](https://docs.docker.com/engine/reference/commandline/exec/#run-docker-exec-on-a-running-container)  
   Execute interactive `bash` shell on the container, allowing the container's file system to be explored.  
   The [flags `-i -t` are often written `-it`](https://docs.docker.com/engine/reference/run/#foreground).
   The `CONTAINER` variable is the container's name or UUID (either the long or short ID).
