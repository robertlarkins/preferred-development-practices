# Postgres Container

The following imstructions will show how to run a postgres database inside a docker container.
This will allow the database to be accessed from a running application.

## Postgres Image

```ps
docker pull postgres
```


## Run Postgres Image

```ps
docker run
```


## Dockerfile

```ps
docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -d postgres
```

## Connect to Database

Use the following to connect to the database
