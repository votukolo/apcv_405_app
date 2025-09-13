# Use the official .NET SDK image for build and test
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ClassProjectApp/ClassProjectApp.csproj ClassProjectApp/
RUN dotnet restore ClassProjectApp/ClassProjectApp.csproj

# Copy the rest of the source code
COPY . .

# Build and test the application
WORKDIR /src/ClassProjectApp
RUN dotnet build --no-restore -c Release

# Publish the application
RUN dotnet publish --no-restore -c Release -o /app/publish

# Use the official ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "ClassProjectApp.dll"]