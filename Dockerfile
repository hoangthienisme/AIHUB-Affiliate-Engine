# =============================
# STAGE 1: BUILD
# =============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file csproj riêng để cache restore layer
COPY AIHUB_Affiliate_Engine/AIHUB_Affiliate_Engine.csproj AIHUB_Affiliate_Engine/

# Restore dependencies
RUN dotnet restore "AIHUB_Affiliate_Engine/AIHUB_Affiliate_Engine.csproj"

# Copy toàn bộ source
COPY . .

# Build và publish ra thư mục /app/publish
WORKDIR /src/AIHUB_Affiliate_Engine
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# =============================
# STAGE 2: RUNTIME
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy từ stage build
COPY --from=build /app/publish .

# Expose port (tùy project)
EXPOSE 8080

ENTRYPOINT ["dotnet", "AIHUB_Affiliate_Engine.dll"]
