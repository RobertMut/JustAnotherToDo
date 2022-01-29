Small To-Do application built using ASP.NET Core.

## Technologies
- ASP.Net Core
- Entity Framework Core
- IdentityServer4
- MediatR
- AutoMapper
- Angular
- Moq
- NUnit
- Shouldly
## Run
1. Clone the repository
2. At the root directory, restore required packages by running:
```
dotnet restore
```
3. Modify connection string under `appsettings.json` under `\Src\Presentation\WebUI` directory
4. Build the solution, by running:
```
dotnet build
```
5. Next, within the `\Src\Presentation\WebUI\ClientApp` directory, launch front end by running:
```
npm start
```
6. Once the front end has started, within the `\Src\Presentation\WebUI` directory, launch back end by running:
```
dotnet run
```
Default user: Administrator / 1234\
Launch https://localhost:44447 to view Web UI\
Launch https://localhost:7143/swagger to access Swagger


## Documentation
The documentation of this solution can be found [here](./docs/documentation.md).

## Screenshots
![Login](https://i.imgur.com/Wwl9rKF.png)
![Home](https://i.imgur.com/62gRFoe.png)
![Menu](https://i.imgur.com/ebhkqFV.png)
![Profile](https://i.imgur.com/Vi77PYT.png)
![List](https://i.imgur.com/dsXqIkd.png)