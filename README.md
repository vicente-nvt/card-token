# card-token

This is a microservice responsible for register and validate user cards.

### Tech Stack
.NET Core 2.2

### How to start it

You will need .NET Core 2.2 SDK in your machine;

After cloning the project, you should run the command below

`dotnet run --project CardToken.WebAPI/CardToken.WebAPI.csproj`

### How to use it

Once yout have been started the service, you will be able to find more information about APIs [here](https://localhost:5001/swagger/)

To make requests, a token (JWT) is necessary.
Bellow is a token for your tests:

`Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkNhcmRTZXJ2aWNlIiwibmJmIjoxNTc5ODMyMTQ1LCJleHAiOjE4OTU0NTEzNDUsImlhdCI6MTU3OTgzMjE0NX0.S6-csM6d6C0Xp3yKNQ1MjtB0Zft92NzjBYZmcJ3hSDU`
