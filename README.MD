![.Net](http://img.shields.io/badge/-v10.0-008999?style=flat-square&logo=.net&logoColor=ffffff) [![build-test](https://github.com/anthueeccel/seller.vehicle/actions/workflows/dotnet.yml/badge.svg)](https://github.com/anthueeccel/seller.vehicle/actions/workflows/dotnet.yml) ![last_commit](https://img.shields.io/github/last-commit/anthueeccel/seller.vehicle) ![license](https://img.shields.io/github/license/anthueeccel/seller.vehicle)

## Overview

Study simple project to simulate Legacy system and new module integration.
The ms-vehicle is the new module accessing the legacy app to fetch data of the client and vehicle information.

## Tech Stack

| App            | Framework                                                        |
| -------------- | ---------------------------------------------------------------- |
| **mvc-client** | .NET 10 / ASP.NET Core MVC, NHibernate, Oracle Database (Docker) |
| **ms-vehicle** | .NET 10 / Minimal API, In Memory Data                            |

## Project Rodmap

| Stage          | Description                                                    |
| -------------- | -------------------------------------------------------------- |
| **MS-Vehicle** | .NET 10 Minimal API microservice with In Memory data           |
| **MVC-Client** | .NET 10 MVC monolithic app with NHibernate and Oracle Database |

## Project Structure

```
├── 1. mvc-client (Legacy System)
│    └── ASP.NET Core MVC (.NET 10)
│    └── NHibernate
│    └── Database: Oracle Database
│
├── 2. ms-vehicle (New Module)
│    └── Minimal APIs (.NET 10)
│    └── Database: In Memory
```

## Helpful CLI

**Install Packages (mvc-client)**
`dotnet add package NHibernate`
`dotnet add package FluentNHibernate`
`dotnet add package Oracle.ManagedDataAccess.Core`

**Install Packages (ms-vehicle)**
`dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL`

**Create Oracle Free Database - Docker**
`docker login container-registry.oracle.com`
`docker pull container-registry.oracle.com/database/free:latest`
`docker run -d \`
` --name oracle-free \`
` -p 1521:1521 \`
` -e ORACLE_PWD=oracle123 \`
` container-registry.oracle.com/database/free:latest`

**Create Local secret to avoid exposing sensitive data**
dotnet user-secrets set "ConnectionStrings:OracleDb" "User Id=system;Password=<password>;Data Source=localhost:1521/FREEPDB1;"

### How to test:

1. Start mvc-client: `dotnet run --project mvc-client\mvc-client.csproj`
2. Start ms-vehicle: `dotnet run --project ms-vehicle\seller.csproj`
3. Call `GET http://localhost:5176/vehicles/1` — should return vehicle + client data
4. Stop mvc-client and call `GET http://localhost:5176/vehicles/2` — should return vehicle + error message
5. Call `UPDATE http://localhost:5176/vehicles/2` - should return vehicle and clientId

```
{
  "id": 2,
  "make": "Honda",
  "model": "Class",
  "year": 2023,
  "isActive": false,
  "clientId": 1204
}
```

6. Call `GET http://localhost:5176/vehicles/2` — should return vehicle + client data
