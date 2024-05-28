set CURRENT_DIR=%cd%

docker run --name microservice.gateway -p 33331:80 -d -v %cd%/Config/gateway.appsettings.json:/app/appsettings.json microservice.gateway
docker run --name microservice.notifyservice -d -v %cd%/Config/notifyservice.appsettings.json:/app/appsettings.json microservice.notifyservice
docker run --name microservice.userapi-1 -p 33333:80 -d -v %cd%/Config/userapi.appsettings.json:/app/appsettings.json microservice.userapi 
docker run --name microservice.userapi-2 -p 33334:80 -d -v %cd%/Config/userapi.appsettings.json:/app/appsettings.json microservice.userapi
docker run --name microservice.itemapi -p 33335:80 -d -v %cd%/Config/itemapi.appsettings.json:/app/appsettings.json microservice.itemapi
