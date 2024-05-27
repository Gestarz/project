set CURRENT_DIR=%cd%
cd ..
set PARENT_DIR=%cd%
cd %CURRENT_DIR%

docker build -f "%PARENT_DIR%\MicroService.Gateway\Dockerfile" --force-rm -t microservice.gateway  --build-arg "BUILD_CONFIGURATION=Debug" --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=MicroService.Gateway" "%PARENT_DIR%"
docker build -f "%PARENT_DIR%\MicroService.ItemApi\Dockerfile" --force-rm -t microservice.itemapi  --build-arg "BUILD_CONFIGURATION=Debug" --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=MicroService.ItemApi" "%PARENT_DIR%"
docker build -f "%PARENT_DIR%\MicroService.NotifyService\Dockerfile" --force-rm -t microservice.notifyservice --build-arg "BUILD_CONFIGURATION=Debug" --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=MicroService.NotifyService" "%PARENT_DIR%"
docker build -f "%PARENT_DIR%\MicroService.UserApi\Dockerfile" --force-rm -t microservice.userapi  --build-arg "BUILD_CONFIGURATION=Debug" --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=MicroService.UserApi" "%PARENT_DIR%"