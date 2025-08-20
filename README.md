# Northwind Database Setup in Docker
---
## Prerequisites

- Docker and Docker Compose installed
- northwind.sql file ready (can be dowloaded from https://github.com/pthom/northwind_psql/blob/master/northwind.sql)
- PostgreSQL container running
  
Docker Compose Configuration
``` 
yamlservices:
  sql:
    image: postgres:latest
    container_name: postgres-db
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=P@ssw0rd
    ports:
      - 5434:5432
    volumes:
      - dbdata:/var/opt/postgres

volumes:
  dbdata:
    name: postgres-data

```
---

## Step-by-Step Setup
1. Start the PostgreSQL Container
``` shell
docker-compose up -d
```

2. Copy SQL File to Container
``` shell
docker cp northwind.sql postgres-db:/tmp/northwind.sql
```

4. Create the Northwind Database
``` shell
docker exec -it postgres-db psql -U postgres -c "CREATE DATABASE northwind;"
```

6. Run the SQL File
``` shell
docker exec -i postgres-db psql -U postgres -d northwind -f /tmp/northwind.sql
```

7. Verify Database Setup
``` shell
# List all databases
docker exec -it postgres-db psql -U postgres -c "\l"
```

---
## Useful Commands
1. Log in to psql:
```shell
docker exec -it postgres-db psql -u postgrespsql -u postgres
```
2. List tables in northwind database
``` shell
docker exec -it postgres-db psql -U postgres -d northwind -c "\dt"
```

3. Connect interactively to explore
``` shell
docker exec -it postgres-db psql -U postgres -d northwind
````
