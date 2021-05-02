using Microsoft.Extensions.Logging;
using TwoMQTT.Interfaces;
using TwoMQTT.Liasons;
using Ecowitt.DataAccess;
using Ecowitt.Models.Shared;
using Ecowitt.Models.Source;
using Microsoft.Extensions.Options;
using Ecowitt.Models.Options;

namespace Ecowitt.Liasons
{
    /// <summary>
    /// Untested, subject to change.
    /// </summary>
    /// <typeparam name="TData">The type representing the mapped data from the source system.</typeparam>
    public class SourceLiason : PushSourceLiasonBase<Resource, Response, ISourceDAO>, ISourceLiason<Resource, object>
    {
        public SourceLiason(ILogger<SourceLiason> logger, ISourceDAO sourceDAO, IOptions<SourceOpts> opts,
            IOptions<SharedOpts> sharedOpts) : base(logger, sourceDAO)
        {
            this.Logger.LogInformation(
                "Port: {port}\n" +
                "UnitSystem: {unitsystem}\n" +
                "Resources: {@resources}\n" +
                "",
                opts.Value.Port,
                sharedOpts.Value.UnitSystem,
                sharedOpts.Value.Resources
            );
        }

        protected override Resource? Map(Response? response) =>
            response switch
            {
                Response => new Resource
                {
                    PASSKEY = response.PASSKEY,
                    StationType = response.StationType,
                    Freq = response.Freq,
                    Model = response.Model,
                    DateUTC = response.DateUTC,

                    Rolling24HourRain = response.Rolling24HourRain,
                    BarometerAbsolute = response.BarometerAbsolute,
                    BarometerRelative = response.BarometerRelative,
                    Co2 = response.Co2,
                    DailyRain = response.DailyRain,
                    Dewpoint = response.Dewpoint,
                    EventRain = response.EventRain,
                    FeelsLike = response.Feelslike,
                    HeatIndex = response.HeatIndex,
                    HourlyRain = response.HourlyRain,
                    HumidityOutdoor = response.HumidityOutdoor,
                    Humidity1 = response.Humidity1,
                    Humidity2 = response.Humidity2,
                    Humidity3 = response.Humidity3,
                    Humidity4 = response.Humidity4,
                    Humidity5 = response.Humidity5,
                    Humidity6 = response.Humidity6,
                    Humidity7 = response.Humidity7,
                    Humidity8 = response.Humidity8,
                    HumidityIndoor = response.HumidityIndoor,
                    LastRain = response.Lastrain,
                    MaxDailyWindGust = response.MaxDailyWindGust,
                    MonthlyRain = response.MonthlyRain,
                    Pm251 = response.Pm251,
                    Pm252 = response.Pm252,
                    Pm253 = response.Pm253,
                    Pm254 = response.Pm254,
                    Pm2524h1 = response.Pm2524h1,
                    Pm2524h2 = response.Pm2524h2,
                    Pm2524h3 = response.Pm2524h3,
                    Pm2524h4 = response.Pm2524h4,
                    RainRate = response.RainRate,
                    SoilMoisture1 = response.SoilMoisture1,
                    SoilMoisture2 = response.SoilMoisture2,
                    SoilMoisture3 = response.Soilmoisture3,
                    SoilMoisture4 = response.Soilmoisture4,
                    SoilMoisture5 = response.Soilmoisture5,
                    SoilMoisture6 = response.Soilmoisture6,
                    SoilMoisture7 = response.Soilmoisture7,
                    SoilMoisture8 = response.Soilmoisture8,
                    SoilTemp1 = response.SoilTemp1,
                    Soiltemp2 = response.SoilTemp2,
                    Soiltemp3 = response.SoilTemp3,
                    SoilTemp4 = response.SoilTemp,
                    SoilTemp5 = response.SoilTemp5,
                    SoilTemp6 = response.SoilTemp6,
                    SoilTemp7 = response.SoilTemp7,
                    Soiltemp8 = response.SoilTemp8,
                    SolarRadiation = response.SolarRadiation,
                    Temp1 = response.Temp1,
                    Temp2 = response.Temp2,
                    Temp3 = response.Temp3,
                    Temp4 = response.Temp4,
                    Temp5 = response.Temp5f,
                    Temp6 = response.Temp6,
                    Temp7 = response.Temp7,
                    Temp8 = response.Temp8,
                    TempOutdoor = response.TempOudoor,
                    TempIndoor = response.TempIndoor,
                    TotalRain = response.TotalRain,
                    Uv = response.Uv,
                    WeeklyRain = response.WeeklyRain,
                    WindChill = response.Windchill,
                    WindDirection = response.WindDirection,
                    WindGust = response.WindGust,
                    WindSpeed = response.WindSpeed,
                    YearlyRain = response.YearlyRain,
                },
                _ => null,
            };
    }
}
