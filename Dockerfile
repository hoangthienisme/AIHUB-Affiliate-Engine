# ======================================
# 🚧 STAGE 1: Build and Publish App
# ======================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all source code
COPY . ./

# Build & publish app to /app/publish
RUN dotnet publish -c Release -o /app/publish

# ======================================
# 🚀 STAGE 2: Run App
# ======================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose the default port
EXPOSE 8080

# Optional: environment variable for production
ENV ASPNETCORE_URLS=http://+:8080

# Start the app
ENTRYPOINT ["dotnet", "AIHUB_Affiliate_Engine.dll"]
