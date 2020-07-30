FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out



FROM mk0x/docker-clamav


COPY script-dotnet-install.sh /
RUN chmod +x ./script-dotnet-install.sh
RUN ./script-dotnet-install.sh

RUN mkdir files-to-be-scaned

COPY --from=build-env /app/out /
ENV ASPNETCORE_URLS=http://+:5050

COPY script-bootstrap-asp.sh /
COPY script-asp-and-clam.sh /

EXPOSE 5050

CMD ["/script-asp-and-clam.sh"]