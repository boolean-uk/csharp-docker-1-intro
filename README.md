# C# Docker Introduction

Note: This exercise requires a few things to be installed.  For a details please examine the [Prerequisites Readme](PREREQUISITES.md)

## Learning Objectives
- Create and publish a simple .Net app
- Create and configure a Dockerfile for .Net  
- Build a Docker image
- Create and run a Docker container

## Hello from Docker!

From Git Bash run the command:  
```
docker run hello-world  
```
This should produce something simlar to the following:  
```
Unable to find image 'hello-world:latest' locally
latest: Pulling from library/hello-world
719385e32844: Pulling fs layer
719385e32844: Verifying Checksum
719385e32844: Download complete
719385e32844: Pull complete
Digest: sha256:88ec0acaa3ec199d3b7eaf73588f4518c25f9d34f58ce9a0df68429c5af48e8d
Status: Downloaded newer image for hello-world:latest

Hello from Docker!
This message shows that your installation appears to be working correctly.

To generate this message, Docker took the following steps:
 1. The Docker client contacted the Docker daemon.
 2. The Docker daemon pulled the "hello-world" image from the Docker Hub.
    (amd64)
 3. The Docker daemon created a new container from that image which runs the
    executable that produces the output you are currently reading.
 4. The Docker daemon streamed that output to the Docker client, which sent it
    to your terminal.

To try something more ambitious, you can run an Ubuntu container with:
 $ docker run -it ubuntu bash

Share images, automate workflows, and more with a free Docker ID:
 https://hub.docker.com/

For more examples and ideas, visit:
 https://docs.docker.com/get-started/


 ```
If this doesn't work then you will have to go and rethink the installation.  

If it worked then congratulations, that was your first docker container!  

## Simple .Net App


Examine the Program.cs file.  You can see that this is a simple loop which runs for 10 seconds.  

Lets go through the steps to **Containerize** this application.  

- From within the exercise.main directory (where the exercise.main.csproj resides) run the 
command:  

```
dotnet publish -c Release 
```

- This should create a Release folder inside the exercise.main\bin\Release\net7.0 with a Release build of the application.  
- Now from within the exercise.main directory (where the exercise.main.csproj resides) create a Dockerfile:
```
touch Docker
```
- Now open the Dockerfile and add the following contents to it:  
```
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /exercise.main

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /exercise.main
COPY --from=build-env /exercise.main/out .
ENTRYPOINT ["dotnet", "exercise.main.dll"]
```
- All being well you should be able to run the following command: 
```
docker build -t my-clock-image -f Dockerfile .  
```
- This should build and create your docker image.  You should be able to see your image in Docker Desktop now.  

## Create a container
Now you have an image you need to create a container by running:
```
docker create --name my-clock-container my-clock-image 
```
## Listing all containers 
```
docker ps -a
```
## Starting a container
```
docker start my-clock-container
```

### Stopping a container
```
docker stop my-clock-container
```

## Deleting a container
```
docker rm my-clock-container
```


