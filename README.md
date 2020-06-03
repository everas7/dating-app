# Overview

This is a dating site like application.

# Stack
- Back-end: ASP.NET Core 3.0
- Front-end: Angular 9
- Database Manager: SQLite for Development and MySql for Production

# Anatomy

The project is composed by an API built on ASP.NET Core 3.0 and a client webapp built with Angular.

API Structure
- Controllers
- Data
- Migrations
- Models

WebApp Structure
- app
  - app
  - assets
  - environments

# Installing Dependencies

API
```
dotnet restore
```
Web App
```
cd web-app
npm install
```

# Running Project

```
cd DatingApp.API
dotnet watch run
```

```
cd dating-webapp
npm start
```

# Additional Notes
[Inspired by this Course](https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch)

