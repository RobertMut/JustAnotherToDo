### Introduction
Simple documentation of the solution made with `Visual Paradigm`.
This documentation consist of eight parts:
- Configuration files
- ER diagram
- Controllers
- Use Case
- Class model of commands with mediator
- Service Architecture - Boundary, Control, Entity class model
- Sequence model
- Application Architecture - Component model

### Configuration files
Project uses standard configuration files for ASP.NET and one additional (angular.json) stored at ClientApp/src/assets used to login using IdentityServer4
```json
{
    "Authority" : "https://localhost:7143",
    "ClientId": "webui",
    "ClientSecret": "2bb80d537b1da3e38bd30361aa855686bde0eacd7162fef6a25fe97bf527a25b",
    "GrantType": "implicit",
    "ClientName": "Web UI",
    "Scope": "api"
}
```
### ER diagram
![ER](https://i.imgur.com/gvmh11U.png)
## Project Independent Model (PIM)
### Controllers
![TodoController](https://i.imgur.com/YUMLIC7.png)
![ProfileController](https://i.imgur.com/Wmisi6l.png)
![CategoryController](https://i.imgur.com/QbZfAKq.png)
### UseCase
![UseCase](https://i.imgur.com/486QaZF.png)
### Mediator with Generalization
![Mediator](https://i.imgur.com/CAMRGIB.png)
### Service Architecture
![Todo](https://i.imgur.com/wzBclrB.png)
![Category](https://i.imgur.com/KludtKj.png)
### Sequence model
![TodoCreation](https://i.imgur.com/lCpaTn5.png)
![CategoryCreation](https://i.imgur.com/qroThOx.png)
### Component model
![Component](https://i.imgur.com/1axPyqp.png)