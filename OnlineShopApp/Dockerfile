#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /src
COPY ["OnlineShopApp/OnlineShopApp.csproj", "OnlineShopApp/"]
RUN dotnet restore "OnlineShopApp/OnlineShopApp.csproj"
COPY . .
WORKDIR "/src/OnlineShopApp"
RUN dotnet build "OnlineShopApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OnlineShopApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineShopApp.dll"]