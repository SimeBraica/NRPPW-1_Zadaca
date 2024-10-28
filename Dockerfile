FROM node:20 AS angular-build
WORKDIR /app/frontend/ui

COPY frontend/UI/package.json frontend/UI/package-lock.json ./
RUN npm install

COPY frontend/UI/ .
RUN npm run build -- --configuration production --project UI

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build
WORKDIR /app/backend

COPY backend/API/*.csproj ./API/
COPY backend/BAL/BAL.csproj ./BAL/
COPY backend/DAL/DAL.csproj ./DAL/
COPY backend/DTO/DTO.csproj ./DTO/

RUN dotnet restore ./API/API.csproj

COPY backend/ .

RUN dotnet publish ./API/API.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=dotnet-build /out .

COPY --from=angular-build /app/frontend/ui/dist/UI ./wwwroot

EXPOSE 80

ENTRYPOINT ["dotnet", "API.dll"]
