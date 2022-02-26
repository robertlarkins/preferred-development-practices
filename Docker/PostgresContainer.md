# Postgres Container

The following imstructions will show how to run a postgres database inside a docker container.
This will allow the database to be accessed from a running application.

See:
- https://www.optimadata.nl/blogs/1/n8dyr5-how-to-run-postgres-on-docker-part-1

## Postgres Image

```ps
docker pull postgres
```


## Run Postgres Container

```ps
docker run --name some-postgres -e POSTGRES_PASSWORD=password -d postgres
```


### Restart Container

```ps
docker restart some-postgres
```

### Stop Container

```ps
docker stop some-postgres
docker rm some-postgres
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

```ps
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' container_name_or_id
```

Use the following to connect to the database
