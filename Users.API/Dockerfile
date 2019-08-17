FROM microsoft/dotnet:2.2-aspnetcore-runtime

# RUN dotnet dev-certs https

# COPY --from=build /root/.dotnet/corefx/cryptography /root/.dotnet/corefx/cryptography

ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS http://+:27017;
EXPOSE 27017

COPY ./publish /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "Users.API.dll"]