# Northwind Database Setup in Docker

A RESTful API built with ASP.NET Core on top of the Northwind database, featuring CRUD operations for orders, customers, and products. Implements secure endpoints with ASP.NET Core Identity authentication and uses Entity Framework Core for data access. The database runs in a Docker container with PostgreSQL.

Technologies: ASP.NET Core, PostgreSQL, Entity Framework Core, Docker, REST API, ASP.NET Core Identity

<img width="1860" height="612" alt="image" src="https://github.com/user-attachments/assets/02183066-c9c1-4212-a0fb-a44608e225af" />

<img width="1845" height="546" alt="image" src="https://github.com/user-attachments/assets/dffcbb42-62b2-4dac-a1e8-274e11c9b7d7" />
<img width="1863" height="623" alt="image" src="https://github.com/user-attachments/assets/9ef9e7ee-e39b-424d-9a19-c92cb6f238d3" />



---
## Run it locally

- Docker and Docker Compose installed
- northwind.sql file ready (can be dowloaded from https://github.com/pthom/northwind_psql/blob/master/northwind.sql)
- PostgreSQL container running
  
Docker Compose Configuration
``` 
yamlservices:
  sql:
    image: postgres:latest
    container_name: northwind-db
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
docker cp northwind.sql northwind-db:/tmp/northwind.sql
```

4. Create the Northwind Database
``` shell
docker exec -it northwind-db psql -U postgres -c "CREATE DATABASE northwind;"
```

6. Run the SQL File
``` shell
docker exec -i northwind-db psql -U postgres -d northwind -f /tmp/northwind.sql
```

7. Verify Database Setup
``` shell
# List all databases
docker exec -it northwind-db psql -U postgres -c "\l"
```

---
## Useful Commands
1. Log in to psql:
```shell
docker exec -it northwind-db psql  -U postgres
```
2. List tables in northwind database
``` shell
docker exec -it northwind-db psql -U postgres -d northwind -c "\dt"
```

3. Connect interactively to explore
``` shell
docker exec -it northwind-db psql -U postgres -d northwind
````
