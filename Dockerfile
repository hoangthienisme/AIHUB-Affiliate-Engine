# =============================
# STAGE 1: BUILD
# =============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file csproj (chú ý dấu ngoặc kép do có khoảng trắng)
COPY "AIHUB Affiliate Engine.csproj" ./
RUN dotnet restore "AIHUB Affiliate Engine.csproj"

# Copy toàn bộ source code
COPY . .
RUN dotnet publish -c Release -o /app/publish

# =============================
# STAGE 2: RUNTIME
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AIHUB Affiliate Engine.dll"]
