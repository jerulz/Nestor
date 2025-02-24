# Use the official .NET SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the project files
COPY src/Nestor/Nestor.csproj ./
RUN dotnet restore

# Copy the rest of the files
COPY src/Nestor/. ./

# Build the project
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image as a runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory
WORKDIR /app

# Copy the build output from the build environment
COPY --from=build-env /app/out .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Nestor.dll"]