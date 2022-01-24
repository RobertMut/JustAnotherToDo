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
### Controllers
![TodoController](https://i.imgur.com/comDN37.png)
![ProfileController](https://i.imgur.com/AiZWgvy.png)
![CategoryController](https://i.imgur.com/5X4drfU.png)
### UseCase
![UseCase](https://i.imgur.com/486QaZF.png)
### Mediator with Generalization
![Mediator](https://i.imgur.com/CAMRGIB.png)