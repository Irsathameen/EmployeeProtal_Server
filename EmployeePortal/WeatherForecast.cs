namespace EmployeePortal
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary1 { get; set; }
        public string? Summary { get; set; }
    }
}
