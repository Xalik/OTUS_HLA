FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OtusSocNet/OtusSocNet/OtusSocNet.csproj", "OtusSocNet/OtusSocNet/"]
COPY ["OtusSocNet/Utils/Utils.csproj", "OtusSocNet/Utils/"]
RUN dotnet restore "OtusSocNet/OtusSocNet/OtusSocNet.csproj"
COPY . .
WORKDIR "/src/OtusSocNet/OtusSocNet"
RUN dotnet build "OtusSocNet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OtusSocNet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OtusSocNet.dll"]