FROM microsoft/dotnet:2.2-aspnetcore-runtime


ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS http://+:4444;
EXPOSE 4444

COPY ./publish /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "ApiGateway.Web.dll"]