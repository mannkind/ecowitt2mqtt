namespace Ecowitt.Models.Shared;

/// <summary>
/// The shared resource across the application
/// </summary>
public record Resource
{
    public string PASSKEY { get; set; } = string.Empty;
    public string StationType { get; set; } = string.Empty;
    public string Freq { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string DateUTC { get; set; } = string.Empty;

    /* Sensor Data */
    public decimal? BarometerAbsolute { get; set; }
    public decimal? BarometerRelative { get; set; }
    public decimal? Co2 { get; set; }
    public decimal? DailyRain { get; set; }
    public decimal? Dewpoint { get; set; }
    public decimal? EventRain { get; set; }
    public decimal? FeelsLike { get; set; }
    public decimal? HeatIndex { get; set; }
    public decimal? HourlyRain { get; set; }
    public decimal? HumidityOutdoor { get; set; }
    public decimal? Humidity1 { get; set; }
    public decimal? Humidity2 { get; set; }
    public decimal? Humidity3 { get; set; }
    public decimal? Humidity4 { get; set; }
    public decimal? Humidity5 { get; set; }
    public decimal? Humidity6 { get; set; }
    public decimal? Humidity7 { get; set; }
    public decimal? Humidity8 { get; set; }
    public decimal? HumidityIndoor { get; set; }
    public decimal? MaxDailyWindGust { get; set; }
    public decimal? MonthlyRain { get; set; }
    public decimal? Pm251 { get; set; }
    public decimal? Pm252 { get; set; }
    public decimal? Pm253 { get; set; }
    public decimal? Pm254 { get; set; }
    public decimal? Pm2524h1 { get; set; }
    public decimal? Pm2524h2 { get; set; }
    public decimal? Pm2524h3 { get; set; }
    public decimal? Pm2524h4 { get; set; }
    public decimal? RainRate { get; set; }
    public decimal? Rolling24HourRain { get; set; }
    public decimal? SoilMoisture1 { get; set; }
    public decimal? SoilMoisture2 { get; set; }
    public decimal? SoilMoisture3 { get; set; }
    public decimal? SoilMoisture4 { get; set; }
    public decimal? SoilMoisture5 { get; set; }
    public decimal? SoilMoisture6 { get; set; }
    public decimal? SoilMoisture7 { get; set; }
    public decimal? SoilMoisture8 { get; set; }
    public decimal? SoilTemp1 { get; set; }
    public decimal? Soiltemp2 { get; set; }
    public decimal? Soiltemp3 { get; set; }
    public decimal? SoilTemp4 { get; set; }
    public decimal? SoilTemp5 { get; set; }
    public decimal? SoilTemp6 { get; set; }
    public decimal? SoilTemp7 { get; set; }
    public decimal? Soiltemp8 { get; set; }
    public decimal? SolarRadiation { get; set; }
    public decimal? Temp { get; set; }
    public decimal? Temp1 { get; set; }
    public decimal? Temp2 { get; set; }
    public decimal? Temp3 { get; set; }
    public decimal? Temp4 { get; set; }
    public decimal? Temp5 { get; set; }
    public decimal? Temp6 { get; set; }
    public decimal? Temp7 { get; set; }
    public decimal? Temp8 { get; set; }
    public decimal? TempOutdoor { get; set; }
    public decimal? TempIndoor { get; set; }
    public decimal? TotalRain { get; set; }
    public decimal? Uv { get; set; }
    public decimal? WeeklyRain { get; set; }
    public decimal? WindChill { get; set; }
    public decimal? WindDirection { get; set; }
    public decimal? WindGust { get; set; }
    public decimal? WindSpeed { get; set; }
    public decimal? YearlyRain { get; set; }
}
