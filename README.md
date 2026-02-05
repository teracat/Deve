![Header](header.png)

[![.NET](https://github.com/teracat/Deve/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/teracat/Deve/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Teracat.Deve)](https://www.nuget.org/packages/Teracat.Deve)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Teracat.Deve)](https://www.nuget.org/packages/Teracat.Deve)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/174edf2ae54941d7bd0e857a338ff162)](https://app.codacy.com/gh/teracat/Deve/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![SonarQube Badge](https://sonarcloud.io/api/project_badges/measure?project=teracat_Deve&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=teracat_Deve)
[![SonarQube Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=teracat_Deve&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=teracat_Deve)
[![SonarQube Security Rating](https://sonarcloud.io/api/project_badges/measure?project=teracat_Deve&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=teracat_Deve)

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
dotnet new deve -n MyProject
```

For more information or alternative options, refer to the [Step-by-Step Installation Guide](https://github.com/teracat/Deve/wiki/Installation#step-by-step-installation-guide) in the Wiki section.

Now, you can also add new modules, features and methods:

### New commands in Version 2

1. Enter into the new project folder:

```bash
cd MyProject
```

2. Create a new module:

```bash
dotnet new deve-module -n Sales -P MyProject --allow-scripts yes
```

3. Create a new feature with CRUD methods:

```bash
dotnet new deve-feature-crud -n Orders -S Order -M Sales -P MyProject --allow-scripts yes
```

This command will generate the following methods: Add, Delete, GetById, GetList and Update.
It will also create all required classes (Requests, Responses, Core, Mappers, Repository, and Validator).

4. Add a new query to get a list of items:

```bash
dotnet new deve-method-query-list -n GetPending -PL Orders -S Order -M Sales -P MyProject --allow-scripts yes
```

5. Add a new query to get a single response item:

```bash
dotnet new deve-method-query -n GetLast -PL Orders -S Order -M Sales -P MyProject --allow-scripts yes
```

6. Add a new command:

```bash
dotnet new deve-method-command -n UpdateStatus -PL Orders -S Order -M Sales -P MyProject --allow-scripts yes
```

7. Create a new feature with no methods:

```bash
dotnet new deve-feature-empty -n Deliveries -S Delivery -M Sales -P MyProject --allow-scripts yes
```
After creating an empty feature, you must add at least one method, for example:

```bash
dotnet new deve-method-command -n SetDelivered -PL Deliveries -S Delivery -M Sales -P MyProject --allow-scripts yes
```

This will generate the following structure:

<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/module-structure.png" title="Module structure"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/module-structure.png" width="157" alt="Module structure"></a> 


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

## Deploy

The project includes GitHub workflows for CI (Continuous Integration) and CD (Continuous Delivery). More information about how automate the deployment in the [Wiki](https://github.com/teracat/Deve/wiki/Deploy) section.

## Diagnostics

Telmetry is added to the Api (also to the MAUI Client sample). More information in the [Wiki](https://github.com/teracat/Deve/wiki/Diagnostics) section.

Grafana:

<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/diagnostics-metrics-grafana.png" title="Diagnostics - Grafana"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/diagnostics-metrics-grafana.png" width="200" alt="Diagnostics - Grafana"></a> 

Zipkin:

<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/diagnostics-traces-zipkin.png" title="Diagnostics - Zipkin"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/diagnostics-traces-zipkin.png" width="250" alt="Diagnostics - Zipkin"></a> 

Sentry:

<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/diagnostics-traces-sentry.png" title="Diagnostics - Sentry"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/diagnostics-traces-sentry.png" width="200" alt="Diagnostics - Sentry"></a> 

Azure Appication Insights:

<a href="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/diagnostics-traces-azure.png" title="Diagnostics - Azure App Insights"><img src="https://raw.githubusercontent.com/teracat/Deve/refs/heads/main/diagnostics-traces-azure.png" width="200" alt="Diagnostics - Azure App Insights"></a> 

## Wiki

Read the documentation in the [Wiki](https://github.com/teracat/Deve/wiki) section for more information (still working on it):

- [Home](https://github.com/teracat/Deve/wiki)
  - [Main Objectives](https://github.com/teracat/Deve/wiki#main-objectives)
  - [Features](https://github.com/teracat/Deve/wiki#features)
  - [Project Structure](https://github.com/teracat/Deve/wiki#project-structure)
  - [Client Embedded vs Sdk](https://github.com/teracat/Deve/wiki#client-embedded-vs-sdk)
  - [Publishable Projects](https://github.com/teracat/Deve/wiki#publishable-projects)
  - [Docker](https://github.com/teracat/Deve/wiki#docker)
- [Installation](https://github.com/teracat/Deve/wiki/Installation)
  - [Prerequisites](https://github.com/teracat/Deve/wiki/Installation#prerequisites)
  - [Step-by-Step Installation Guide](https://github.com/teracat/Deve/wiki/Installation#step-by-step-installation-guide)
  - [New commands in Version 2](https://github.com/teracat/Deve/wiki/Installation#new-commands-in-version-2)
  - [Uninstall](https://github.com/teracat/Deve/wiki/Installation#uninstall)
  - [Install an Older Version](https://github.com/teracat/Deve/wiki/Installation#install-an-older-version)
- [Common](https://github.com/teracat/Deve/wiki/Common)
  - [Constants](https://github.com/teracat/Deve/wiki/Common#constants)
  - [Utils](https://github.com/teracat/Deve/wiki/Common#utils)
  - [IData](https://github.com/teracat/Deve/wiki/Common#idata)
  - [Dto](https://github.com/teracat/Deve/wiki/Common#dto)
  - [Requests & Responses](https://github.com/teracat/Deve/wiki/Common#requests--responses)
  - [Localize](https://github.com/teracat/Deve/wiki/Common#localize)
  - [Log](https://github.com/teracat/Deve/wiki/Common#log)
  - [Cache](https://github.com/teracat/Deve/wiki/Common#cache)
  - [Shield](https://github.com/teracat/Deve/wiki/Common#shield)
- [Api](https://github.com/teracat/Deve/wiki/Api)
  - [Credentials](https://github.com/teracat/Deve/wiki/Api#credentials)
  - [Projects](https://github.com/teracat/Deve/wiki/Api#projects)
  - [User](https://github.com/teracat/Deve/wiki/Api#user)
  - [RateLimiter](https://github.com/teracat/Deve/wiki/Api#ratelimiter)
  - [StatusCode](https://github.com/teracat/Deve/wiki/Api#statuscode)
  - [Authentication](https://github.com/teracat/Deve/wiki/Api#authentication)
- [Auth](https://github.com/teracat/Deve/wiki/Auth)
  - [ITokenManager](https://github.com/teracat/Deve/wiki/Auth#itokenmanager)
  - [ICrypt](https://github.com/teracat/Deve/wiki/Auth#icrypt)
  - [IHash](https://github.com/teracat/Deve/wiki/Auth#ihash)
  - [IAuth](https://github.com/teracat/Deve/wiki/Auth#iauth)
  - [Permissions](https://github.com/teracat/Deve/wiki/Auth#permissions)
- [Core](https://github.com/teracat/Deve/wiki/Core)
  - [Embedded vs Not Embedded](https://github.com/teracat/Deve/wiki/Core#embedded-vs-not-embedded)
  - [MainCore](https://github.com/teracat/Deve/wiki/Core#maincore)
- [Sdk](https://github.com/teracat/Deve/wiki/Sdk)
  - [EnvironmentType](https://github.com/teracat/Deve/wiki/Sdk#environmenttype)
  - [RequestAuthType](https://github.com/teracat/Deve/wiki/Sdk#requestauthtype)
  - [RequestBuilder](https://github.com/teracat/Deve/wiki/Sdk#requestbuilder)
  - [AuthSdk](https://github.com/teracat/Deve/wiki/Sdk#authsdk)
  - [BaseSdk](https://github.com/teracat/Deve/wiki/Sdk#basesdk)
  - [UriQuery](https://github.com/teracat/Deve/wiki/Sdk#uriquery)
  - [LoggingHandlers](https://github.com/teracat/Deve/wiki/Sdk#logginghandlers)
  - [SdkBuilder](https://github.com/teracat/Deve/wiki/Sdk#sdkbuilder)
  - [MainSdk](https://github.com/teracat/Deve/wiki/Sdk#mainsdk)
- [Clients](https://github.com/teracat/Deve/wiki/Clients)
  - [Common](https://github.com/teracat/Deve/wiki/Clients#common)
  - [Console](https://github.com/teracat/Deve/wiki/Clients#console)
  - [Service](https://github.com/teracat/Deve/wiki/Clients#service)
  - [MAUI](https://github.com/teracat/Deve/wiki/Clients#maui)
  - [WPF](https://github.com/teracat/Deve/wiki/Clients#wpf)
- [Diagnostics](https://github.com/teracat/Deve/wiki/Diagnostics)
  - [OpenTelemetry](https://github.com/teracat/Deve/wiki/Diagnostics#open-telemetry)
  - [Sentry](https://github.com/teracat/Deve/wiki/Diagnostics#sentry)
  - [Azure Application Insights](https://github.com/teracat/Deve/wiki/Diagnostics#azure-application-insights)
- [Deploy](https://github.com/teracat/Deve/wiki/Deploy)
  - [Azure App Service](https://github.com/teracat/Deve/wiki/Deploy#azure-app-service)
  - [AKS](https://github.com/teracat/Deve/wiki/Deploy#aks)
- [Changelog](https://github.com/teracat/Deve/wiki/Changelog)
- [Versions](https://github.com/teracat/Deve/wiki/Versions)

## Contributing

If you find any errors or something that could be improved, please let me know by opening a new [Issue](https://github.com/teracat/Deve/issues) or [Discussion](https://github.com/teracat/Deve/discussions).

## License

This project is licensed under the MIT License. See the LICENSE file for more details.
