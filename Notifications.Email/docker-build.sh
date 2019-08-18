rm -r ./publish
dotnet publish --output ./publish
docker build -t email-notifications .