#!/bin/bash
cd App
docker build . -f "Dockerfile" -t screech-api-demo-image
docker create --publish 8080:8080 --name screech-api-demo screech-api-demo-image
docker start screech-api-demo
docker image rm -f screech-api-demo-image