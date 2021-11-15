using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ecowitt.Models;
using Ecowitt.Models.Options;
using Ecowitt.Models.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwoMQTT;
using TwoMQTT.Interfaces;
using TwoMQTT.Liasons;
using TwoMQTT.Models;
using TwoMQTT.Utils;

namespace Ecowitt.Liasons;

/// <inheritdoc />
public class MQTTLiason : MQTTLiasonBase<Resource, object, SlugMapping, SharedOpts>, IMQTTLiason<Resource, object>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="generator"></param>
    /// <param name="sharedOpts"></param>
    public MQTTLiason(ILogger<MQTTLiason> logger, IMQTTGenerator generator, IOptions<SharedOpts> sharedOpts) :
        base(logger, generator, sharedOpts)
    {
        this.UnitSystem = sharedOpts.Value.UnitSystem;
    }

    /// <inheritdoc />
    public IEnumerable<(string topic, string payload)> MapData(Resource input)
    {
        var results = new List<(string, string)>();
        var slug = this.Questions
            .Select(x => x.Slug)
            .FirstOrDefault() ?? string.Empty;

        if (string.IsNullOrEmpty(slug))
        {
            this.Logger.LogDebug("Unable to find slug for {mac}", input.PASSKEY);
            return results;
        }

        this.Logger.LogDebug("Found slug {slug} for incoming data for {mac}", slug, input.PASSKEY);
        var us = this.UnitSystem;
        results.AddRange(new[]
            {
                    (this.Generator.StateTopic(slug, nameof(Resource.PASSKEY)), input.PASSKEY),
                    (this.Generator.StateTopic(slug, nameof(Resource.StationType)), input.StationType),
                    (this.Generator.StateTopic(slug, nameof(Resource.Freq)), input.Freq),
                    (this.Generator.StateTopic(slug, nameof(Resource.Model)), input.Model),
                    (this.Generator.StateTopic(slug, nameof(Resource.DateUTC)), input.DateUTC),
                
                    /* Sensor Data */
                    (this.Generator.StateTopic(slug, nameof(Resource.BarometerAbsolute)), BaromUnits(input.BarometerAbsolute, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.BarometerRelative)), BaromUnits(input.BarometerRelative, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Co2)), input.Co2.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.DailyRain)), RainUnits(input.DailyRain, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Dewpoint)), TempUnits(input.Dewpoint, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.EventRain)), RainUnits(input.EventRain, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.FeelsLike)), TempUnits(input.FeelsLike, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.HeatIndex)), TempUnits(input.HeatIndex, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.HourlyRain)), RainUnits(input.HourlyRain, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.HumidityOutdoor)), input.HumidityOutdoor.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Humidity1)), input.Humidity1.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Humidity2)), input.Humidity2.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Humidity3)), input.Humidity3.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Humidity4)), input.Humidity4.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Humidity5)), input.Humidity5.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Humidity6)), input.Humidity6.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Humidity7)), input.Humidity7.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Humidity8)), input.Humidity8.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.HumidityIndoor)), input.HumidityIndoor.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.MaxDailyWindGust)), WindUnits(input.MaxDailyWindGust, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.MonthlyRain)), RainUnits(input.MonthlyRain, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Pm251)), input.Pm251.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Pm252)), input.Pm252.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Pm253)), input.Pm253.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Pm254)), input.Pm254.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Pm2524h1)), input.Pm2524h1.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Pm2524h2)), input.Pm2524h2.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Pm2524h3)), input.Pm2524h3.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Pm2524h4)), input.Pm2524h4.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.RainRate)), RainUnits(input.RainRate, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Rolling24HourRain)), RainUnits(input.Rolling24HourRain, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilMoisture1)), input.SoilMoisture1.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilMoisture2)), input.SoilMoisture2.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilMoisture3)), input.SoilMoisture3.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilMoisture4)), input.SoilMoisture4.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilMoisture5)), input.SoilMoisture5.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilMoisture6)), input.SoilMoisture6.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilMoisture7)), input.SoilMoisture7.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilMoisture8)), input.SoilMoisture8.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilTemp1)), TempUnits(input.SoilTemp1, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Soiltemp2)), TempUnits(input.Soiltemp2, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Soiltemp3)), TempUnits(input.Soiltemp3, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilTemp4)), TempUnits(input.SoilTemp4, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilTemp5)), TempUnits(input.SoilTemp5, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilTemp6)), TempUnits(input.SoilTemp6, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SoilTemp7)), TempUnits(input.SoilTemp7, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Soiltemp8)), TempUnits(input.Soiltemp8, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.SolarRadiation)), input.SolarRadiation.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp)), TempUnits(input.Temp, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp1)), TempUnits(input.Temp1, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp2)), TempUnits(input.Temp2, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp3)), TempUnits(input.Temp3, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp4)), TempUnits(input.Temp4, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp5)), TempUnits(input.Temp5, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp6)), TempUnits(input.Temp6, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp7)), TempUnits(input.Temp7, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Temp8)), TempUnits(input.Temp8, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.TempOutdoor)), TempUnits(input.TempOutdoor, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.TempIndoor)), TempUnits(input.TempIndoor, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.TotalRain)), RainUnits(input.TotalRain, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.Uv)), input.Uv.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.WeeklyRain)), RainUnits(input.WeeklyRain, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.WindChill)), TempUnits(input.WindChill, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.WindDirection)), input.WindDirection.ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.WindGust)), WindUnits(input.WindGust, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.WindSpeed)), WindUnits(input.WindSpeed, us).ToStringOrEmpty()),
                    (this.Generator.StateTopic(slug, nameof(Resource.YearlyRain)), RainUnits(input.YearlyRain, us).ToStringOrEmpty()),
                }
        );

        return results;

        static decimal? BaromUnits(decimal? input, UnitSystem us = UnitSystem.Metric) =>
            input.HasValue ? us switch
            {
                UnitSystem.Metric => Math.Round(input.Value * 33.8639M, 3, MidpointRounding.AwayFromZero),
                _ => input,
            } : null;

        static decimal? RainUnits(decimal? input, UnitSystem us) =>
            input.HasValue ? us switch
            {
                UnitSystem.Metric => Math.Round(input.Value * 25.4M, 3, MidpointRounding.AwayFromZero),
                _ => input,
            } : null;

        static decimal? TempUnits(decimal? input, UnitSystem us) =>
            input.HasValue ? us switch
            {
                UnitSystem.Metric => Math.Round(((input.Value - 32.0M) * (5.0M / 9.0M)), 3, MidpointRounding.AwayFromZero),
                _ => input,
            } : null;

        static decimal? WindUnits(decimal? input, UnitSystem us) =>
            input.HasValue ? us switch
            {
                UnitSystem.Metric => Math.Round(input.Value * 1.60934M, 3, MidpointRounding.AwayFromZero),
                _ => input,
            } : null;
    }

    /// <inheritdoc />
    public IEnumerable<(string slug, string sensor, string type, MQTTDiscovery discovery)> Discoveries()
    {
        var discoveries = new List<(string, string, string, MQTTDiscovery)>();
        var assembly = Assembly.GetAssembly(typeof(Program))?.GetName() ?? new AssemblyName();
        var mapping = new[]
        {
                new { Sensor = nameof(Resource.PASSKEY), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.StationType), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Freq), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Model), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.DateUTC), Type = Const.SENSOR },
                
                /* Sensor Data */
                new { Sensor = nameof(Resource.BarometerAbsolute), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.BarometerRelative), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Co2), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.DailyRain), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Dewpoint), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.EventRain), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.FeelsLike), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.HeatIndex), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.HourlyRain), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.HumidityOutdoor), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Humidity1), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Humidity2), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Humidity3), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Humidity4), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Humidity5), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Humidity6), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Humidity7), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Humidity8), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.HumidityIndoor), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.MaxDailyWindGust), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.MonthlyRain), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Pm251), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Pm2524h1), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.RainRate), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Rolling24HourRain), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilMoisture1), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilMoisture2), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilMoisture3), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilMoisture4), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilMoisture5), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilMoisture6), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilMoisture7), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilMoisture8), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilTemp1), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Soiltemp2), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Soiltemp3), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilTemp4), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilTemp5), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilTemp6), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SoilTemp7), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Soiltemp8), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.SolarRadiation), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Temp1), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Temp2), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Temp3), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Temp4), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Temp5), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Temp6), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Temp7), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Temp8), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.TempOutdoor), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.TempIndoor), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.TotalRain), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.Uv), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.WeeklyRain), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.WindChill), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.WindDirection), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.WindGust), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.WindSpeed), Type = Const.SENSOR },
                new { Sensor = nameof(Resource.YearlyRain), Type = Const.SENSOR },
            };

        foreach (var input in this.Questions)
        {
            foreach (var map in mapping)
            {
                this.Logger.LogDebug("Generating discovery for {sensor}", map.Sensor);
                var (icon, uom) = UnitOfMeasure(map.Sensor, this.UnitSystem);
                var discovery = this.Generator.BuildDiscovery(input.Slug, map.Sensor, assembly, false);
                discovery = discovery with
                {
                    Icon = icon,
                    UnitOfMeasurement = uom,
                };

                discoveries.Add((input.Slug, map.Sensor, map.Type, discovery));
            }
        }

        return discoveries;

        static (string icon, string uom) UnitOfMeasure(string sensor, UnitSystem us = UnitSystem.Metric)
        {
            var barom = sensor.Contains("barom", System.StringComparison.OrdinalIgnoreCase);
            var battery = sensor.Contains("batt", System.StringComparison.OrdinalIgnoreCase);
            var humidity = sensor.Contains("humidity", System.StringComparison.OrdinalIgnoreCase);
            var rain = sensor.Contains("rain", System.StringComparison.OrdinalIgnoreCase);
            var soilMoisture = sensor.Contains("soilmois", System.StringComparison.OrdinalIgnoreCase);
            var soilTemp = sensor.Contains("soiltemp", System.StringComparison.OrdinalIgnoreCase);
            var temp = sensor.Contains("temp", System.StringComparison.OrdinalIgnoreCase);
            var wind = sensor.Contains("wind", System.StringComparison.OrdinalIgnoreCase);

            var icon = sensor switch
            {
                nameof(Resource.Co2) => string.Empty,
                nameof(Resource.Dewpoint) => "mdi:thermometer",
                nameof(Resource.FeelsLike) => "mdi:thermometer",
                nameof(Resource.HeatIndex) => "mdi:thermometer",
                nameof(Resource.MaxDailyWindGust) => "mdi:weather-windy",
                nameof(Resource.Pm251) => "mdi:biohazard",
                nameof(Resource.Pm2524h1) => "mdi:biohazard",
                nameof(Resource.SolarRadiation) => "mdi:weather-sunny",
                nameof(Resource.Uv) => "mdi:weather-sunny",
                nameof(Resource.WindChill) => "mdi:thermometer",
                string when barom => "mdi:cloud",
                string when battery => "mdi:battery",
                string when humidity => "mdi:water-percent",
                string when rain => "mdi:weather-pouring",
                string when soilMoisture => "mdi:water-percent",
                string when soilTemp => "mdi:thermometer",
                string when temp => "mdi:thermometer",
                string when wind => "mdi:weather-windy",
                _ => "mid:eye",
            };

            var unitOfMeasure = sensor switch
            {
                nameof(Resource.Co2) => "ppm",
                nameof(Resource.Dewpoint) => TemperatureUOM(us),
                nameof(Resource.FeelsLike) => TemperatureUOM(us),
                nameof(Resource.HeatIndex) => TemperatureUOM(us),
                nameof(Resource.MaxDailyWindGust) => WindUOM(us),
                nameof(Resource.Pm251) => "µg/m^3",
                nameof(Resource.Pm2524h1) => "µg/m^3",
                nameof(Resource.SolarRadiation) => "w/m^2",
                nameof(Resource.Uv) => "UV index",
                nameof(Resource.WindDirection) => "°",
                nameof(Resource.WindChill) => TemperatureUOM(us),
                string when barom => BaromUOM(us),
                string when rain => RainUOM(us),
                string when soilMoisture => "%",
                string when soilTemp => TemperatureUOM(us),
                string when temp => TemperatureUOM(us),
                string when wind => WindUOM(us),
                _ => string.Empty,
            };


            return (icon, unitOfMeasure);
        }

        static string BaromUOM(UnitSystem us) =>
            us switch
            {
                UnitSystem.Imperial => "inHg",
                UnitSystem.Metric => "hPa",
                _ => string.Empty,
            };

        static string RainUOM(UnitSystem us) =>
            us switch
            {
                UnitSystem.Imperial => "in",
                UnitSystem.Metric => "mm",
                _ => string.Empty,
            };

        static string TemperatureUOM(UnitSystem us) =>
            us switch
            {
                UnitSystem.Imperial => "°F",
                UnitSystem.Metric => "°C",
                _ => string.Empty,
            };

        static string WindUOM(UnitSystem us) =>
            us switch
            {
                UnitSystem.Imperial => "mph",
                UnitSystem.Metric => "km/h",
                _ => string.Empty,
            };
    }

    private readonly UnitSystem UnitSystem;
}

public static class NullableDecimalExt
{
    public static string ToStringOrEmpty(this decimal? d) =>
        d switch
        {
            null => string.Empty,
            _ => d.ToString() ?? string.Empty
        };
}
