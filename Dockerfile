# Step 1: Build the Angular app
FROM node:20 AS angular-build
WORKDIR /app/frontend/ui

# Copy package.json and package-lock.json for installing dependencies
COPY frontend/UI/package.json frontend/UI/package-lock.json ./
RUN npm install

# Copy the entire UI frontend folder and build the Angular application
COPY frontend/UI/ .
RUN npm run build -- --configuration production --project UI

# Step 2: Build the .NET API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build
WORKDIR /app/backend

# Copy the .csproj files for all projects
COPY backend/API/*.csproj ./API/
COPY backend/BAL/BAL.csproj ./BAL/
COPY backend/DAL/DAL.csproj ./DAL/
COPY backend/DTO/DTO.csproj ./DTO/

# Restore dependencies for the .NET application
RUN dotnet restore ./API/API.csproj

# Copy the entire backend directory to the image
COPY backend/ .

# Publish the .NET application
RUN dotnet publish ./API/API.csproj -c Release -o /out

# Step 3: Prepare the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the built .NET application files
COPY --from=dotnet-build /out .

# Copy the built Angular application files into the correct location
COPY --from=angular-build /app/frontend/ui/dist/UI ./wwwroot

# Expose the port the .NET app runs on
EXPOSE 80

# Start the .NET application with the correct DLL path
ENTRYPOINT ["dotnet", "API.dll"]
