FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["PhoneBook.Presentation.WebApi/PhoneBook.Presentation.WebApi.csproj", "PhoneBook.Presentation.WebApi/"]
RUN dotnet restore "PhoneBook.Presentation.WebApi/PhoneBook.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/PhoneBook.Presentation.WebApi"
RUN dotnet build "PhoneBook.Presentation.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PhoneBook.Presentation.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PhoneBook.Presentation.WebApi.dll"]