# Use the official .NET 8.0 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the project files
COPY BackgroundWorker/*.csproj ./BackgroundWorker/
COPY CoreLib/*.csproj ./CoreLib/
RUN dotnet restore BackgroundWorker/BackgroundWorker.csproj

# Copy the rest of the files and build the project
COPY BackgroundWorker/* ./BackgroundWorker/
COPY CoreLib/* ./CoreLib/
RUN dotnet publish ./BackgroundWorker/BackgroundWorker.csproj -c Release -o out

# Use the official .NET 8.0 runtime image as the base runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "BackgroundWorker.dll"]
