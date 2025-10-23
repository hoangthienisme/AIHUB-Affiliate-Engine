# =============================
# STAGE 1: BUILD
# =============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY AIHUB_Affiliate_Engine/AIHUB_Affiliate_Engine.csproj AIHUB_Affiliate_Engine/

# Restore dependencies
RUN dotnet restore "AIHUB_Affiliate_Engine/AIHUB_Affiliate_Engine.csproj"

COPY . .

# Build và publish ra thư mục /app/publish
WORKDIR /src/AIHUB_Affiliate_Engine
RUN dotnet publish -c Release -o /app/publish

# =============================
# STAGE 2: RUNTIME
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AIHUB_Affiliate_Engine.dll"]
