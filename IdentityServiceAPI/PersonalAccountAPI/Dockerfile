#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PersonalAccountAPI/PersonalAccountAPI.csproj", "PersonalAccountAPI/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "PersonalAccountAPI/PersonalAccountAPI.csproj"
COPY . .
WORKDIR "/src/PersonalAccountAPI"
RUN dotnet build "PersonalAccountAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalAccountAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalAccountAPI.dll"]