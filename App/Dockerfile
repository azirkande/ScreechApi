FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Screechr.Api/Screechr.Api.csproj", "Screechr.Api/"]
COPY ["Screechr.Core/Screechr.Core.csproj", "Screechr.Core/"]

RUN dotnet restore "Screechr.Api/Screechr.Api.csproj"
COPY . .

WORKDIR "/src/Screechr.Api"
RUN dotnet build "Screechr.Api.csproj" -c Release -o /app

FROM build as publish
RUN dotnet publish "Screechr.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Screechr.Api.dll"]
