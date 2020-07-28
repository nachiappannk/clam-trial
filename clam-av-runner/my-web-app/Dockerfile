FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out



FROM debian:buster-slim

# Debian Base to use
ENV DEBIAN_VERSION buster

# initial install of av daemon
RUN echo "deb http://http.debian.net/debian/ $DEBIAN_VERSION main contrib non-free" > /etc/apt/sources.list && \
    echo "deb http://http.debian.net/debian/ $DEBIAN_VERSION-updates main contrib non-free" >> /etc/apt/sources.list && \
    echo "deb http://security.debian.org/ $DEBIAN_VERSION/updates main contrib non-free" >> /etc/apt/sources.list && \
    apt-get update && \
    DEBIAN_FRONTEND=noninteractive apt-get install --no-install-recommends -y -qq \
        clamav-daemon \
        clamav-freshclam \
        libclamunrar9 \
        ca-certificates \
        netcat-openbsd \
        wget && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# initial update of av databases
RUN wget -O /var/lib/clamav/main.cvd http://database.clamav.net/main.cvd && \
    wget -O /var/lib/clamav/daily.cvd http://database.clamav.net/daily.cvd && \
    wget -O /var/lib/clamav/bytecode.cvd http://database.clamav.net/bytecode.cvd && \
    chown clamav:clamav /var/lib/clamav/*.cvd

# permission juggling
RUN mkdir /var/run/clamav && \
    chown clamav:clamav /var/run/clamav && \
    chmod 750 /var/run/clamav

# av configuration update
RUN sed -i 's/^Foreground .*$/Foreground true/g' /etc/clamav/clamd.conf && \
    echo "TCPSocket 3310" >> /etc/clamav/clamd.conf && \
    if [ -n "$HTTPProxyServer" ]; then echo "HTTPProxyServer $HTTPProxyServer" >> /etc/clamav/freshclam.conf; fi && \
    if [ -n "$HTTPProxyPort"   ]; then echo "HTTPProxyPort $HTTPProxyPort" >> /etc/clamav/freshclam.conf; fi && \
    sed -i 's/^Foreground .*$/Foreground true/g' /etc/clamav/freshclam.conf

# env based configs - will be called by bootstrap.sh
COPY envconfig.sh /

COPY check.sh /

COPY dotnet-install.sh .
RUN chmod +x ./dotnet-install.sh
RUN ./dotnet-install.sh


# volume provision
VOLUME ["/var/lib/clamav"]


COPY --from=build-env /app/out /

ENV ASPNETCORE_URLS=http://+:5050
# port provision
EXPOSE 3310
EXPOSE 5050



# av daemon bootstrapping
COPY bootstrap.sh /bootstrap
COPY start.sh /start
COPY main.sh /
CMD ["/main.sh"]

HEALTHCHECK --start-period=500s CMD /check.sh
