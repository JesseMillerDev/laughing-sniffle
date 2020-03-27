FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY PointingPokerPlus.Client.csproj .

RUN dotnet restore "PointingPokerPlus.Client.csproj"
RUN dotnet restore "PointingPokerPlus.Shared.csproj"
COPY . .
RUN dotnet build "PointingPokerPlus.Shared.csproj" -c Release -o /app/build
RUN dotnet build "PointingPokerPlus.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PointingPokerPlus.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/PointingPokerPlus.Client/dist .
ENTRYPOINT ["dotnet", "PointingPokerPlus.Client.dll"]