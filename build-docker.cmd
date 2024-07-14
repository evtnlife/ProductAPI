@echo off
setlocal

REM Set environment variables
set ASPNETCORE_ENVIRONMENT=Testing

REM Build and deploy the application and test services
docker-compose -f docker-compose\docker-compose.yml -f docker-compose\docker-compose.override.yml up --build -d

echo Waiting for the application to start...
timeout /t 20

REM Run the tests
docker-compose -f docker-compose\docker-compose.yml -f docker-compose\docker-compose.override.yml run productapi.tests
set TEST_EXIT_CODE=%ERRORLEVEL%

REM Clean up
docker-compose -f docker-compose\docker-compose.yml -f docker-compose\docker-compose.override.yml down

set ASPNETCORE_ENVIRONMENT=Development
REM Check if tests passed
if %TEST_EXIT_CODE% EQU 0 (
    echo Tests passed. Deploying the container...
    REM Publish the container
	docker-compose -f docker-compose/docker-compose.yml -f docker-compose/docker-compose.override.yml up --build -d productapi.database
	docker-compose -f docker-compose\docker-compose.yml -f docker-compose\docker-compose.override.yml up --build -d productapi.application 
) else (
    echo Tests failed. Not deploying the container.
)

REM Keep the command prompt window open
pause

endlocal