# 1. Obraz do budowania aplikacji (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiujemy pliki projektów (dzięki temu Docker wykorzysta cache przy budowaniu)
COPY ["TailorWebsite.Web/TailorWebsite.Web.csproj", "TailorWebsite.Web/"]
COPY ["TailorWebsite.DAL/TailorWebsite.DAL.csproj", "TailorWebsite.DAL/"]
COPY ["TailorWebsite.Model/TailorWebsite.Model.csproj", "TailorWebsite.Model/"]
COPY ["TailorWebsite.Services/TailorWebsite.Services.csproj", "TailorWebsite.Services/"]
COPY ["TailorWebsite.ViewModels/TailorWebsite.ViewModels.csproj", "TailorWebsite.ViewModels/"]

# Pobieramy biblioteki (Restore)
RUN dotnet restore "TailorWebsite.Web/TailorWebsite.Web.csproj"

# Kopiujemy całą resztę kodu
COPY . .

# Budujemy aplikację
WORKDIR "/src/TailorWebsite.Web"
RUN dotnet build "TailorWebsite.Web.csproj" -c Release -o /app/build

# Publikujemy gotową aplikację (Publish)
FROM build AS publish
RUN dotnet publish "TailorWebsite.Web.csproj" -c Release -o /app/publish

# 2. Obraz finalny (uruchomieniowy) - lekki
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TailorWebsite.Web.dll"]