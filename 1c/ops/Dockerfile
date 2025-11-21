FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine as build

COPY ./SiloHost/SiloHost.csproj /SiloHost/SiloHost.csproj
COPY ./SiloHost/src/* /SiloHost/src/
COPY ./Grains/Grains.fsproj /Grains/Grains.fsproj
COPY ./Grains/src/* /Grains/src/
COPY ./ops/entrypoint.sh /SiloHost/

RUN dotnet publish --verbosity normal "/SiloHost/SiloHost.csproj" --configuration Release --output /SiloHost

EXPOSE 8080

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine

WORKDIR /SiloHost
COPY --from=build /SiloHost ./
EXPOSE 3000 8080

CMD ["dotnet", "SiloHost.dll"]
ENTRYPOINT ["./entrypoint.sh"]