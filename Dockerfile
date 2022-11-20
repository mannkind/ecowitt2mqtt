# $BUILDPLATFORM ensures the native build platform is utilized
ARG BUILDPLATFORM=linux/amd64
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
# Only fetch dependencies once
# Find the non-test csproj file, move it to the appropriate folder, and restore project deps
COPY Ecowitt/*.csproj ./Ecowitt/
RUN mkdir -p vendor && dotnet restore Ecowitt
COPY . ./
# Build the app
# Find the non-test csproj file, build that project
ARG BUILD_VERSION=0.0.0.0
RUN dotnet build -o output -c Release --no-restore -p:Version=$BUILD_VERSION Ecowitt

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime
COPY --from=build /src/output app
ENTRYPOINT ["dotnet", "./app/Ecowitt.dll"]
