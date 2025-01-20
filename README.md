My Reference
with https://dev.to/leandroveiga/building-restful-apis-with-net-9-a-comprehensive-guide-for-modern-developers-47hc 

# Microservice-RESTAPI
## Step 1
>> dotnet new webapi -o helloworld --no-https
>> cd helloworld
>> dotnet run
if you can check your setup by go to your "localhost/weatherforecast"

## Step 2
add swagger
>> dotnet add package Swashbuckle.AspNetCore
>> dotnet build
>> dotnet run
Check at "localhost/swagger"
