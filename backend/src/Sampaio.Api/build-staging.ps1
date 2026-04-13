New-Item -ItemType Directory -Force -Path "./dist";

Remove-Item -Recurse -Force -ErrorAction SilentlyContinue "./dist/*";

dotnet publish -c Release /p:EnvironmentName=Staging -o "./dist/dc-api-staging";

Compress-Archive -Path "./dist/dc-api-staging/*" -CompressionLevel Fastest -DestinationPath "./dist/dc-api-staging-$((Get-Date).ToString('ddMMyyyyHHmmssfff')).zip";