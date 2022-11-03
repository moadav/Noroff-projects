# Human vs Zombie

[![standard-readme compliant](https://img.shields.io/badge/standard--readme-OK-green.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

C# .NET project delivery for the Case assignment at Noroff. It reproduces the functionality requested in the HvZ Case assignment file, as well as creating a web api in .Net core.

The Focus of the API is based on the HvZ game. It contains models to make it possible to communicate every necessary endpoint needed. It has a Game that can have several players and has a gameconfig connected to it. These players can belong to a squad and also communicate with the global, faction and squad chat if they belong to one. If the player dies a gravestone will be created. Also the game can contain a mission

If any of the web requests fail. For example, user queries for a single game with an ID (queried as a parameter in the URL) that is not present in the database return correct status codes, in this case 404, as well as verbose error messages.

## Table of Contents

- [Background](#background)
- [Install](#install)
- [Dependencies](#dependencies)
- [Usage](#usage)
- [API](#api)
- [Maintainers](#maintainers)
- [License](#license)

## Background
The background for this project was a mandatory assignment at Noroff Accelerate.

## Install
To install the project, simply clone the repository

#### Dependencies
.NET6.0
AutoMapper 11.0.1
AutoMapper Extensions Microsoft Dependency Injection
Microsoft Entity Framework
Swashbuckle
EntityFramework.SqlServer
EntityFramework.Tools
EntityFramework.Design

## Usage
In order to run the local version of the project, you have to first edit the appsettings.json file and paste this code: "<dbname>": "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = <dbname>; Integrated Security = True;". Then you have to edit in program.cs to use the <dbname> connection string. Lastly update the database by using the command "update-datbase" in package manager console. Then run the HvZ_API project.

## API

The api specification can be found by running the .Net core web api. Swagger spec will then open up in your default browser.

## Maintainers

[@Xerethars](https://github.com/Xerethars)

[@Denizx11](https://gitlab.com/Denizx11)

[@moadav](https://gitlab.com/moadav)

[@chonlawit.wp](https://gitlab.com/chonlawit.wp)

[@Filste98](https://gitlab.com/Filste98)

[@op.vibeke](https://gitlab.com/op.vibeke)

## License

MIT © 2022 Michal & Deniz & Mohammed Ali & Chonlawit & Filip & Vibeke
