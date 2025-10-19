# Stage 1: Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Stage 2: Build image with SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project file and restore dependencies
COPY ["cbtBackend/csbBackend.csproj", "cbtBackend/"]
RUN dotnet restore "csbBackend/csbBackend.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/cbtBackend"
RUN dotnet build "cbtBackend.csproj" -c Release -o /app/build

# Stage 3: Publish the app
FROM build AS publish
RUN dotnet publish "cbtBackend.csproj" -c Release -o /app/publish

# Stage 4: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cbtBackend.dll"]