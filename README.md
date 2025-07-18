![Header](header.png)

[![.NET](https://github.com/teracat/Deve/actions/workflows/main-test-all.yml/badge.svg?branch=main)](https://github.com/teracat/Deve/actions/workflows/main-test-all.yml)
[![NuGet](https://img.shields.io/nuget/v/Teracat.Deve)](https://www.nuget.org/packages/Teracat.Deve)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Teracat.Deve)](https://www.nuget.org/packages/Teracat.Deve)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/174edf2ae54941d7bd0e857a338ff162)](https://app.codacy.com/gh/teracat/Deve/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

# Deve
Welcome to the **.NET DEVEloper Template**! This project serves as a starting point for developers who need to create applications with data access requirements. The template is designed to be flexible and adaptable, allowing you to change data access strategies without needing to overhaul the entire project.

![Diagram](diagram.png)

## Installation

1. Install the template from Nuget:
```bash
dotnet new install Teracat.Deve
```

2. Create a project using the template:

```bash
dotnet new deve -n YourNewProjectName
```

For more information or alternative options, refer to the [Step-by-Step Installation Guide](https://github.com/teracat/Deve/wiki/Installation#step-by-step-installation-guide) in the Wiki section.

## Versions & Changelog

To stay up to date with the latest changes and updates to the project, check out the [Changelog](https://github.com/teracat/Deve/wiki/Changelog).  
For more details about the available versions, see the [Versions](https://github.com/teracat/Deve/wiki/Versions) page.  

## Client Samples

Client app samples are provided to show how to use the Core or the Sdk + Api to access the data. More information in the [Wiki](https://github.com/teracat/Deve/wiki/Clients) section.

MAUI Screenshots:

<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-maui-login-windows.png" title="MAUI - Windows: Login View"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-maui-login-windows.png" width="200" alt="MAUI - Windows: Login View"></a> 
<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-maui-cities-windows.png" title="MAUI - Windows: Cities View"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-maui-cities-windows.png" width="200" alt="MAUI - Windows: Cities View"></a> 
<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-maui-login-android.png" title="MAUI - Android: Login View"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-maui-login-android.png" width="100" alt="MAUI - Android: Login View"></a>
<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-maui-cities-android.png" title="MAUI - Android: Cities View"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-maui-cities-android.png" width="100" alt="MAUI - Android: Cities View"></a>


WPF Screenshots:

<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-wpf-login.png" title="WPF - Login Window"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-wpf-login.png" width="150" alt="WPF - Login Window"></a> 
<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-wpf-main.png" title="WPF - Main Window"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/client-wpf-main.png" width="200" alt="WPF - Main Window"></a>


## Wiki

Read the documentation in the [Wiki](https://github.com/teracat/Deve/wiki) section for more information (still working on it):

- [Home](https://github.com/teracat/Deve/wiki)
  - [Main Objectives](https://github.com/teracat/Deve/wiki#main-objectives)
  - [Features](https://github.com/teracat/Deve/wiki#features)
  - [Project Structure](https://github.com/teracat/Deve/wiki#project-structure)
  - [Internal vs External](https://github.com/teracat/Deve/wiki#internal-vs-external)
  - [Client Embedded vs Sdk](https://github.com/teracat/Deve/wiki#client-embedded-vs-sdk)
  - [Publishable Projects](https://github.com/teracat/Deve/wiki#publishable-projects)
  - [Docker](https://github.com/teracat/Deve/wiki#docker)
- [Installation](https://github.com/teracat/Deve/wiki/Installation)
  - [Prerequisites](https://github.com/teracat/Deve/wiki/Installation#prerequisites)
  - [Step-by-Step Installation Guide](https://github.com/teracat/Deve/wiki/Installation#step-by-step-installation-guide)
  - [Uninstall](https://github.com/teracat/Deve/wiki/Installation#uninstall)
  - [Install an Older Version](https://github.com/teracat/Deve/wiki/Installation#install-an-older-version)
- [Common](https://github.com/teracat/Deve/wiki/Common)
  - [Constants](https://github.com/teracat/Deve/wiki/Common#constants)
  - [Utils](https://github.com/teracat/Deve/wiki/Common#utils)
  - [IDataCommon](https://github.com/teracat/Deve/wiki/Common#idatacommon)
  - [Model](https://github.com/teracat/Deve/wiki/Common#model)
  - [Criteria](https://github.com/teracat/Deve/wiki/Common#criteria)
  - [Localize](https://github.com/teracat/Deve/wiki/Common#localize)
  - [Log](https://github.com/teracat/Deve/wiki/Common#log)
- [Api](https://github.com/teracat/Deve/wiki/Api)
  - [Credentials](https://github.com/teracat/Deve/wiki/Api#credentials)
  - [Projects](https://github.com/teracat/Deve/wiki/Api#projects)
  - [Core](https://github.com/teracat/Deve/wiki/Api#core)
  - [User](https://github.com/teracat/Deve/wiki/Api#user)
  - [RateLimiter](https://github.com/teracat/Deve/wiki/Api#ratelimiter)
  - [StatusCode](https://github.com/teracat/Deve/wiki/Api#statuscode)
  - [Authentication](https://github.com/teracat/Deve/wiki/Api#authentication)
  - [Use cases](https://github.com/teracat/Deve/wiki/Api#use-cases)
- [Auth](https://github.com/teracat/Deve/wiki/Auth)
  - [ITokenManager](https://github.com/teracat/Deve/wiki/Auth#itokenmanager)
  - [ICrypt](https://github.com/teracat/Deve/wiki/Auth#icrypt)
  - [IHash](https://github.com/teracat/Deve/wiki/Auth#ihash)
  - [IAuth](https://github.com/teracat/Deve/wiki/Auth#iauth)
  - [Permissions](https://github.com/teracat/Deve/wiki/Auth#permissions)
- [Core](https://github.com/teracat/Deve/wiki/Core)
  - [Embedded vs Not Embedded](https://github.com/teracat/Deve/wiki/Core#embedded-vs-not-embedded)
  - [CoreMain](https://github.com/teracat/Deve/wiki/Core#coremain)
  - [CoreBaseGet](https://github.com/teracat/Deve/wiki/Core#corebaseget)
  - [CoreBaseAll](https://github.com/teracat/Deve/wiki/Core#corebaseall)
  - [Shield](https://github.com/teracat/Deve/wiki/Core#shield)
  - [DataSourceWrappers](https://github.com/teracat/Deve/wiki/Core#datasourcewrappers)
  - [Use cases](https://github.com/teracat/Deve/wiki/Core#use-cases)
  - [Cache](https://github.com/teracat/Deve/wiki/Core#cache)
- [DataSource](https://github.com/teracat/Deve/wiki/DataSource)
  - [IDataSource](https://github.com/teracat/Deve/wiki/DataSource#idatasource)
  - [DataSourceMain](https://github.com/teracat/Deve/wiki/DataSource#datasourcemain)
  - [DataSourceConfig](https://github.com/teracat/Deve/wiki/DataSource#datasourceconfig)
  - [Data](https://github.com/teracat/Deve/wiki/DataSource#data)
  - [DataSourceBaseGet](https://github.com/teracat/Deve/wiki/DataSource#datasourcebaseget)
  - [DataSourceBaseAll](https://github.com/teracat/Deve/wiki/DataSource#datasourcebaseall)
  - [CriteriaHandler](https://github.com/teracat/Deve/wiki/DataSource#criteriahandler)
  - [Use cases](https://github.com/teracat/Deve/wiki/DataSource#use-cases)
- [Sdk](https://github.com/teracat/Deve/wiki/Sdk)
  - [Common](https://github.com/teracat/Deve/wiki/Sdk#common)
  - [External Sdk](https://github.com/teracat/Deve/wiki/Sdk#external-sdk)
  - [Internal Sdk](https://github.com/teracat/Deve/wiki/Sdk#internal-sdk)
  - [Use cases](https://github.com/teracat/Deve/wiki/Sdk#use-cases)
- [Clients](https://github.com/teracat/Deve/wiki/Clients)
  - [Common](https://github.com/teracat/Deve/wiki/Clients#common)
  - [Console](https://github.com/teracat/Deve/wiki/Clients#console)
  - [Service](https://github.com/teracat/Deve/wiki/Clients#service)
  - [MAUI](https://github.com/teracat/Deve/wiki/Clients#maui)
  - [WPF](https://github.com/teracat/Deve/wiki/Clients#wpf)
- [Changelog](https://github.com/teracat/Deve/wiki/Changelog)
- [Versions](https://github.com/teracat/Deve/wiki/Versions)

## Contributing

If you find any errors or something that could be improved, please let me know by opening a new [Issue](https://github.com/teracat/Deve/issues) or [Discussion](https://github.com/teracat/Deve/discussions).

## License

This project is licensed under the MIT License. See the LICENSE file for more details.
