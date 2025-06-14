# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos csproj y restauramos dependencias
COPY ["Video90s.API.csproj", "./"]
RUN dotnet restore

# Copiamos el resto y publicamos
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expondremos el puerto 80 dentro del contenedor
EXPOSE 80

# Enty point
ENTRYPOINT ["dotnet", "Video90s.API.dll"]
