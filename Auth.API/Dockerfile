FROM microsoft/dotnet:2.2-aspnetcore-runtime


ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS http://+:27015;
EXPOSE 27015

COPY ./publish /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "Auth.API.dll"]