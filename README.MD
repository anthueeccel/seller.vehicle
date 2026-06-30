![.Net](http://img.shields.io/badge/-v10.0-008999?style=flat-square&logo=.net&logoColor=ffffff) ![last_commit](https://img.shields.io/github/last-commit/anthueeccel/seller.vehicle) ![license](https://img.shields.io/github/license/anthueeccel/seller.vehicle)

<!-- [![build-test](https://github.com/anthueeccel/seller.vehicle/actions/workflows/dotnet.yml/badge.svg)](https://github.com/anthueeccel/seller.vehicle/actions/workflows/dotnet.yml) -->

## Overview

Study simple project to simulate Legacy system and new module integrations.

## Tech Stack

| App            | Framework                  | ORM                   | Database        |
| -------------- | -------------------------- | --------------------- | --------------- |
| **mvc-client** | .NET 10 / ASP.NET Core MVC | NHibernate            | Oracle Database |
| **ms-vehicle** | .NET 10 / Minimal API      | Entity Framework Core | PostgreSQL      |

## Project Rodmap

| Stage          | Description                                                    |
| -------------- | -------------------------------------------------------------- |
| **MS-Vehicle** | .NET 10 Minimal API microservice with PostgreSQL and EF Core   |
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
│    └── Entity Framework Core
│    └── Database: PostgreSQL
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
