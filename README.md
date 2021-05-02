# ecowitt2mqtt

[![Software
License](https://img.shields.io/badge/License-MIT-orange.svg?style=flat-square)](https://github.com/mannkind/ecowitt2mqtt/blob/main/LICENSE.md)
[![Build Status](https://github.com/mannkind/ecowitt2mqtt/workflows/Main%20Workflow/badge.svg)](https://github.com/mannkind/ecowitt2mqtt/actions)
[![Coverage Status](https://img.shields.io/codecov/c/github/mannkind/ecowitt2mqtt/main.svg)](http://codecov.io/github/mannkind/ecowitt2mqtt?branch=main)

An experiment to publish Ecowitt to MQTT.

## Use

The application can be locally built using `dotnet build` or you can utilize the multi-architecture Docker image(s).

### Example

```bash
docker run \
-e ECOWITT__PORT="10000" \
-e ECOWITT__UNITSYSTEM="Imperial" \
-e ECOWITT__RESOURCES__0__MAC="11:22:33:44:55:66" \
-e ECOWITT__RESOURCES__0__Slug="main" \
-e ECOWITT__MQTT__BROKER="localhost" \
-e ECOWITT__MQTT__DISCOVERYENABLED="true" \
mannkind/ecowitt2mqtt:latest
```

OR

```bash
ECOWITT__PORT="10000" \
ECOWITT__UNITSYSTEM="Imperial" \
ECOWITT__RESOURCES__0__MAC="11:22:33:44:55:66" \
ECOWITT__RESOURCES__0__Slug="main" \
ECOWITT__MQTT__BROKER="localhost" \
ECOWITT__MQTT__DISCOVERYENABLED="true" \
./ecowitt2mqtt 
```


## Configuration

Configuration happens via environmental variables

```bash
ECOWITT__PORT                               - The port on which to run the http server, defaults to "10000"
ECOWITT__RESOURCES__0__MAC                  - The MAC to identify the specific Ecowitt Gateway
ECOWITT__RESOURCES__0__Slug                 - The slug to identify the specific Ecowitt Gateway
ECOWITT__MQTT__TOPICPREFIX                  - [OPTIONAL] The MQTT topic on which to publish the collection lookup results, defaults to "home/ecowitt"
ECOWITT__MQTT__DISCOVERYENABLED             - [OPTIONAL] The MQTT discovery flag for Home Assistant, defaults to false
ECOWITT__MQTT__DISCOVERYPREFIX              - [OPTIONAL] The MQTT discovery prefix for Home Assistant, defaults to "homeassistant"
ECOWITT__MQTT__DISCOVERYNAME                - [OPTIONAL] The MQTT discovery name for Home Assistant, defaults to "ecowitt"
ECOWITT__MQTT__BROKER                       - [OPTIONAL] The MQTT broker, defaults to "test.mosquitto.org"
ECOWITT__MQTT__USERNAME                     - [OPTIONAL] The MQTT username, default to ""
ECOWITT__MQTT__PASSWORD                     - [OPTIONAL] The MQTT password, default to ""
```