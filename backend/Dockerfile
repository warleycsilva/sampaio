FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build_env

WORKDIR build

# copy sln solution
COPY ./*.sln ./

# copy all .csproj and restore
COPY ./src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file ./src/${file%.*}/; done

COPY ./tests/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p tests/${file%.*}/ && mv $file ./tests/${file%.*}/; done

RUN dotnet restore /p:RestoreUseStaticGraphEvaluation=true

COPY . ./ 

# run tests
#WORKDIR /build/tests/PayRentee.Tests
#RUN dotnet build PayRentee.Tests.csproj -c Release
#RUN dotnet test PayRentee.Tests.csproj

# restore and build app
WORKDIR /build/src/PayRentee.Api
RUN dotnet publish -c Release -o /build/publish

#run app
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /build
COPY --from=build_env /build/publish .
EXPOSE 5050
ENTRYPOINT ["dotnet", "PayRentee.Api.dll"]