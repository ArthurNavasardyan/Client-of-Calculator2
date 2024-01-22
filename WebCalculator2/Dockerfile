#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebCalculator2/WebCalculator2.csproj", "WebCalculator2/"]
COPY ["SignalR_Common/SignalR_Common.csproj", "SignalR_Common/"]
RUN dotnet restore "WebCalculator2/WebCalculator2.csproj"
COPY . .
WORKDIR "/src/WebCalculator2"
RUN dotnet build "WebCalculator2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebCalculator2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebCalculator2.dll"]