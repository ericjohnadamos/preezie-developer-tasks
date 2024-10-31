# Todo App

> The Todo App is a simple project that is used for developer assessment.


[![npm version](https://img.shields.io/badge/npm-10.8.3-blue)](https://www.npmjs.com/package/npm/v/10.8.3) [![node version](https://img.shields.io/badge/node-20.17.0-red)](https://nodejs.org/en/download/prebuilt-installer) [![.net version](https://img.shields.io/badge/.net-8.0-yellow)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) [![csharp version](https://img.shields.io/badge/csharp-12.0-blue)](https://learn.microsoft.com/en-us/dotnet/csharp) [![react version](https://img.shields.io/badge/react-17.0.2-green)](https://react.dev/)

<img src="https://raw.githubusercontent.com/ericjohnadamos/preezie-developer-tasks/refs/heads/main/assets/backend-openapi.png" width="800" />

## 🚩 Project Resources

| Name | Value |
| --- | --- |
| GitHub repository | https://github.com/ericjohnadamos/preezie-developer-tasks/tree/main/fullstack-task |


## 🎨 Features

* View all todo items
* Create todo item with an incomplete status by default
* Toggle the completion status of a specific todo item
* Soft delete a specific todo item

## 📚 Technology Stack
#### 📗 Back-end
- **Framework**: .NET 8.0
- **IDE**: Visual Studio 2022 or Visual Studio Code
- **📦 NuGet Packages**:
  - MediatR
  - Mapster
  - XUnit
  - FluentValidation
#### 📘 Front-end
- **Framework**: Angular (TypeScript)
- **📦 Packages**:
  - @testing-library/jest-dom
  - react
  - react-dom
  - react-scripts
  - ajv

## 🔧 Setup

#### 📄 Prerequisites
- Visual Studio 2022 or Visual Studio Code
- .NET 8.0 SDK
- Node.js and npm

#### 🔖 Installation

1. Clone the repo
```sh
$ git clone https://github.com/ericjohnadamos/preezie-developer-tasks.git
```
2. Install NuGet packages
```sh
:fullstack-task\src\Preezie.WebAPI$ dotnet restore
```
3. Install NPM packages
```sh
:fullstack-task\src\todo-app$ npm install
```


## 🚀 Running the Application

1. Start the back-end server (https):
```sh
:fullstack-task\src\Preezie.WebAPI$ dotnet run --launch-profile https
```

2. In a separate terminal, start the Angular development server:
```sh
:fullstack-task\src\todo-app$ npm start
```

3. Open you browser and navigate to `http://localhost:3000`

4. Swagger API can be accessed by navigating to `https://localhost:5000`


## 📜 Contact

Eric John Adamos - 📧 ericjohnadamos@gmail.com

Github: https://github.com/ericjohnadamos


[Github-repository-url]: https://github.com/ericjohnadamos/preezie-developer-tasks/tree/main/fullstack-task

