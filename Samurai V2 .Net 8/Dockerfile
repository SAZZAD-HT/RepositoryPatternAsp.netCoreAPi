#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug 
WORKDIR /src
COPY ["Samurai V2 .Net 8/SamuraiV2.Net 8.csproj", "Samurai V2 .Net 8/"]
RUN dotnet restore "./Samurai V2 .Net 8/./SamuraiV2.Net 8.csproj"
COPY . .
WORKDIR "/src/Samurai V2 .Net 8"
RUN dotnet build "./SamuraiV2.Net 8.csproj" -c $BUILD_CONFIGURATION -o /app/build


#ENV ASPNETCORE_URLS=http://+:8080;http://+:8081
#ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5000



FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./SamuraiV2.Net 8.csproj" -c $BUILD_CONFIGURATION -o /app/publish 



FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SamuraiV2.Net 8.dll"]