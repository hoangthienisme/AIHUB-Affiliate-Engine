# =============================
# STAGE 1: BUILD
# =============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file csproj
COPY AIHUB_Affiliate_Engine/AIHUB_Affiliate_Engine.csproj AIHUB_Affiliate_Engine/
RUN dotnet restore "AIHUB_Affiliate_Engine/AIHUB_Affiliate_Engine.csproj"



# =============================
# STAGE 2: RUNTIME
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AIHUB_Affiliate_Engine.dll"]
