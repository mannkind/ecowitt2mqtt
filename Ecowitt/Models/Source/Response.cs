using System;
using System.Linq;
using Newtonsoft.Json;

namespace Ecowitt.Models.Source
{
    /// <summary>
    /// The response from the source
    /// </summary>
    public record Response
    {
        /* Gateway */

        [JsonProperty("PASSKEY")]
        public string PASSKEY { get; set; } = string.Empty;

        [JsonProperty("stationtype")]
        public string StationType { get; set; } = string.Empty;

        [JsonProperty("freq")]
        public string Freq { get; set; } = string.Empty;

        [JsonProperty("model")]
        public string Model { get; set; } = string.Empty;

        [JsonProperty("dateutc")]
        public string DateUTC { get; set; } = string.Empty;

        /* Batteries */

        [JsonProperty("wh68batt")]
        public string Wh68Batt { get; set; } = string.Empty;

        [JsonProperty("wh26batt")]
        public string Wh26Batt { get; set; } = string.Empty;

        [JsonProperty("batt1")]
        public string Batt1 { get; set; } = string.Empty;

        [JsonProperty("pm25batt1")]
        public string Pm25Batt1 { get; set; } = string.Empty;


        /* Sensor Data */

        [JsonProperty("baromabsin")]
        public decimal? BarometerAbsolute { get; set; }

        [JsonProperty("baromrelin")]
        public decimal? BarometerRelative { get; set; }

        [JsonProperty("co2")]
        public decimal? Co2 { get; set; }

        [JsonProperty("dailyrainin")]
        public decimal? DailyRain { get; set; }

        [JsonProperty("dewpoint")]
        public decimal? Dewpoint 
        {
            get 
            {
                if (this.TempOudoor == null || this.HumidityOutdoor == null)
                {
                    return null;
                }

                return this.TempOudoor - ((100.0M - this.HumidityOutdoor) / 5.0M);
            }
        }

        [JsonProperty("eventrainin")]
        public decimal? EventRain { get; set; }

        [JsonProperty("feelslike")]
        public decimal? Feelslike
        {
            get 
            {
                if (this.TempOudoor == null || this.WindSpeed == null)
                {
                    return null;
                }

                return this.TempOudoor <= 50 && this.WindSpeed > 3 ? this.Windchill :
                    this.TempOudoor >= 80 ? this.HeatIndex :
                    this.TempOudoor;
            }
        }

        [JsonProperty("heatindex")]
        public decimal? HeatIndex 
        {
            get 
            {
                if (this.TempOudoor == null || this.HumidityOutdoor == null) 
                {
                    return null;
                }

                var res = 0.5M * (this.TempOudoor.Value + 61.0M + (this.TempOudoor.Value - 68.0M) * 1.2M + this.HumidityOutdoor.Value * 0.094M);

                return Math.Round(res, 3, MidpointRounding.AwayFromZero);
            }
        }

        [JsonProperty("hourlyrainin")]
        public decimal? HourlyRain { get; set; }

        [JsonProperty("humidity")]
        public decimal? HumidityOutdoor { get; set; }

        [JsonProperty("humidity1")]
        public decimal? Humidity1 { get; set; }

        [JsonProperty("humidity2")]
        public decimal? Humidity2 { get; set; }

        [JsonProperty("humidity3")]
        public decimal? Humidity3 { get; set; }

        [JsonProperty("humidity4")]
        public decimal? Humidity4 { get; set; }

        [JsonProperty("humidity5")]
        public decimal? Humidity5 { get; set; }

        [JsonProperty("humidity6")]
        public decimal? Humidity6 { get; set; }

        [JsonProperty("humidity7")]
        public decimal? Humidity7 { get; set; }

        [JsonProperty("humidity8")]
        public decimal? Humidity8 { get; set; }

        [JsonProperty("humidityin")]
        public decimal? HumidityIndoor { get; set; }

        [JsonProperty("lastrain")]
        public decimal? Lastrain { get; set; }

        [JsonProperty("maxdailygust")]
        public decimal? MaxDailyWindGust { get; set; }

        [JsonProperty("monthlyrainin")]
        public decimal? MonthlyRain { get; set; }

        [JsonProperty("pm25_ch1")]
        public decimal? Pm251 { get; set; }

        [JsonProperty("pm25_ch2")]
        public decimal? Pm252 { get; set; }

        [JsonProperty("pm25_ch3")]
        public decimal? Pm253 { get; set; }

        [JsonProperty("pm25_ch4")]
        public decimal? Pm254 { get; set; }

        [JsonProperty("pm25_avg_24h_ch1")]
        public decimal? Pm2524h1 { get; set; }

        [JsonProperty("pm25_avg_24h_ch2")]
        public decimal? Pm2524h2 { get; set; }

        [JsonProperty("pm25_avg_24h_ch3")]
        public decimal? Pm2524h3 { get; set; }

        [JsonProperty("pm25_avg_24h_ch4")]
        public decimal? Pm2524h4 { get; set; }

        [JsonProperty("rainratein")]
        public decimal? RainRate { get; set; }

        [JsonProperty("24hourrainin")]
        public decimal? Rolling24HourRain { get; set; }

        [JsonProperty("soilmoisture1")]
        public decimal? SoilMoisture1 { get; set; }

        [JsonProperty("soilmoisture2")]
        public decimal? SoilMoisture2 { get; set; }

        [JsonProperty("soilmoisture3")]
        public decimal? Soilmoisture3 { get; set; }

        [JsonProperty("soilmoisture4")]
        public decimal? Soilmoisture4 { get; set; }

        [JsonProperty("soilmoisture5")]
        public decimal? Soilmoisture5 { get; set; }

        [JsonProperty("soilmoisture6")]
        public decimal? Soilmoisture6 { get; set; }

        [JsonProperty("soilmoisture7")]
        public decimal? Soilmoisture7 { get; set; }

        [JsonProperty("soilmoisture8")]
        public decimal? Soilmoisture8 { get; set; }

        [JsonProperty("soiltemp1f")]
        public decimal? SoilTemp1 { get; set; }

        [JsonProperty("soiltemp2f")]
        public decimal? SoilTemp2 { get; set; }

        [JsonProperty("soiltemp3f")]
        public decimal? SoilTemp3 { get; set; }

        [JsonProperty("soiltemp4f")]
        public decimal? SoilTemp { get; set; }

        [JsonProperty("soiltemp5f")]
        public decimal? SoilTemp5 { get; set; }

        [JsonProperty("soiltemp6f")]
        public decimal? SoilTemp6 { get; set; }

        [JsonProperty("soiltemp7f")]
        public decimal? SoilTemp7 { get; set; }

        [JsonProperty("soiltemp8f")]
        public decimal? SoilTemp8 { get; set; }

        [JsonProperty("solarradiation")]
        public decimal? SolarRadiation { get; set; }

        [JsonProperty("temp1f")]
        public decimal? Temp1 { get; set; }

        [JsonProperty("temp2f")]
        public decimal? Temp2 { get; set; }

        [JsonProperty("temp3f")]
        public decimal? Temp3 { get; set; }

        [JsonProperty("temp4f")]
        public decimal? Temp4 { get; set; }

        [JsonProperty("temp5f")]
        public decimal? Temp5f { get; set; }

        [JsonProperty("temp6f")]
        public decimal? Temp6 { get; set; }

        [JsonProperty("temp7f")]
        public decimal? Temp7 { get; set; }

        [JsonProperty("temp8f")]
        public decimal? Temp8 { get; set; }

        [JsonProperty("tempf")]
        public decimal? TempOudoor { get; set; }

        [JsonProperty("tempinf")]
        public decimal? TempIndoor { get; set; }

        [JsonProperty("totalrainin")]
        public decimal? TotalRain { get; set; }

        [JsonProperty("uv")]
        public decimal? Uv { get; set; }

        [JsonProperty("weeklyrainin")]
        public decimal? WeeklyRain { get; set; }

        [JsonProperty("windchill")]
        public decimal? Windchill 
        { 
            get
            {
                if (this.TempOudoor == null || this.WindSpeed == null)
                {
                    return null;
                }

                var windSpeed = Convert.ToDecimal(Math.Pow(Decimal.ToDouble(this.WindSpeed.Value), 0.16));
                var res = 35.74M + (0.6215M * this.TempOudoor.Value) - 35.75M * windSpeed + 0.4275M * this.TempOudoor.Value * windSpeed;

                return Math.Round(res, 3, MidpointRounding.AwayFromZero);
            }
        }

        [JsonProperty("winddir")]
        public decimal? WindDirection { get; set; }

        [JsonProperty("windgustmph")]
        public decimal? WindGust { get; set; }

        [JsonProperty("windspeedmph")]
        public decimal? WindSpeed { get; set; }

        [JsonProperty("yearlyrainin")]
        public decimal? YearlyRain { get; set; }
    }
}