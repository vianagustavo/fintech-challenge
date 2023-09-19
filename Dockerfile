FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

WORKDIR /source

COPY . .

RUN dotnet restore fintech-challenge/fintech-challenge.csproj

RUN dotnet publish "./fintech-challenge/fintech-challenge.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal

WORKDIR /app

COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "fintech-challenge.dll"]