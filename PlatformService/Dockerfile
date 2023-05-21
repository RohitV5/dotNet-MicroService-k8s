# The first line specifies the base image to use for the build stage. 
# It uses the official Microsoft .NET SDK version 7.0 as the starting point for the build process. 
# The "AS build" tag creates an intermediate container with the alias "build" to separate it 
# from the subsequent stages.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# The WORKDIR command sets the working directory inside the container to "/app". 
# This is where all the subsequent commands will be executed.
WORKDIR /app

# The next line, COPY *.csproj ./, copies the project files with the extension ".csproj" 
# from the host directory into the current working directory inside the container.
COPY *.csproj ./

# The RUN command executes the "dotnet restore" command, which restores the NuGet packages required
# by the application. This command reads the project file and downloads the necessary dependencies.
RUN dotnet restore

# The following line, COPY . ./, copies all the files and directories from the host's current directory 
# (where the Dockerfile is located) into the current working directory of the container.
COPY . ./

# The next command, RUN dotnet publish -c Release -o out, builds and publishes the .NET 
#application in Release configuration. The "-c Release" flag specifies the build configuration, 
# and the "-o out" flag indicates that the output files should be placed in the "out" directory.
RUN dotnet publish -c Release -o out

# The second part of the Dockerfile starts with the "FROM" command, which specifies the base 
# image for the final runtime stage. In this case, it uses the official Microsoft 
# .NET ASP.NET runtime version 7.0 as the base image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# The WORKDIR command sets the working directory inside the container to "/app".
WORKDIR /app

# The COPY --from=build /app/out . command copies the published output files from the build
# stage (from the previous stage with the alias "build") 
#into the current working directory of the final runtime stage.
COPY --from=build /app/out .

#The ENTRYPOINT [ "dotnet", "PlatformService.dll" ] command sets the default command to run 
# when the container starts. It specifies that the entry point for the container should be running the "dotnet" command with "PlatformService.dll" as the main application to start.

ENTRYPOINT [ "dotnet", "PlatformService.dll" ]




#########################DEVELOPMENT DOCKER####################################
# Development Dockerfile

#
## FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
## WORKDIR /app


# Copy only the project file to restore dependencies
## COPY *.csproj ./
## RUN dotnet restore

# Copy the entire project to build
##COPY . ./

# Add additional development dependencies if needed
##RUN dotnet tool restore
##RUN dotnet ef database update --project YourProject.csproj --context YourDbContext

##FROM mcr.microsoft.com/dotnet/aspnet:7.0
##WORKDIR /app

# Copy the published output from the build stage
##COPY --from=build /app/out .

# Add additional development configurations if needed
## ENV ASPNETCORE_ENVIRONMENT=Development

## ENTRYPOINT [ "dotnet", "PlatformService.dll" ]