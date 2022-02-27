# Postgres Container

The following imstructions will show how to run a postgres database inside a docker container.
This will allow the database to be accessed from a running application.

See:
- https://www.optimadata.nl/blogs/1/n8dyr5-how-to-run-postgres-on-docker-part-1
- https://dev.to/shree_j/how-to-install-and-run-psql-using-docker-41j2

## Postgres Image

```ps
docker pull postgres
```


## Run Postgres Container

```ps
docker run --name some-postgres -p 5432:5432 -e POSTGRES_PASSWORD=password -d postgres
```


### Restart Container

```ps
docker restart some-postgres
```

### Stop Container

```ps
docker stop some-postgres
```

```ps
docker rm some-postgres
```

Clean up all images and data
```ps
docker system prune
```


## Dockerfile




## Connect to Container

### 

```ps
su postgres
psql
\conninfo
\q
```


## Connect to Database

Get the container IP:
```ps
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' some-postgres
```
or from https://docs.docker.com/samples/dotnetcore/:
```ps
docker inspect -f "{{ .NetworkSettings.Networks.nat.IPAddress }}" some-postgres
```


- Host: localhost  
  The IP from above doesn't seem to work
- Port: 5432
- Database: postgres

Authentication
- Username: postgres
- Password: password

Use the following to connect to the database
