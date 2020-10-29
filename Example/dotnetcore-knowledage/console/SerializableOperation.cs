using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BDS.DotNetCoreKnowledage
{
    public class SerializableOperation: IProcess
    {
        public void InvokeMain()
        {
            List<WeatherForecastWithPOCOs> weatherForecastWithPOCOs = new List<WeatherForecastWithPOCOs>()
            {
                new WeatherForecastWithPOCOs()
                {
                    TemperatureCelsius = 1,
                    Summary = "new_1",
                    highLowTemps = new List<HighLowTemps>()
                    {
                        new HighLowTemps(){ High = "1-H1", Low = "1-L1"},
                        new HighLowTemps(){ High = "1-H2", Low = "1-L2"},
                        new HighLowTemps(){ High = "1-H3", Low = "1-L3"}
                    }
                },
                new WeatherForecastWithPOCOs()
                {
                    TemperatureCelsius = 2,
                    Summary = "new_2",
                    highLowTemps = new List<HighLowTemps>()
                    {
                        new HighLowTemps(){ High = "2-H1", Low = "2-L1"},
                        new HighLowTemps(){ High = "2-H2", Low = "2-L2"},
                        new HighLowTemps(){ High = "2-H3", Low = "2-L3"}
                    }
                },
                new WeatherForecastWithPOCOs()
                {
                    TemperatureCelsius = 3,
                    Summary = "new_3",
                    highLowTemps = new List<HighLowTemps>()
                    {
                        new HighLowTemps(){ High = "3-H1", Low = "3-L1"},
                        new HighLowTemps(){ High = "3-H2", Low = "3-L2"},
                        new HighLowTemps(){ High = "3-H3", Low = "3-L3"}
                    }
                }
            };
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonStr = JsonSerializer.Serialize(weatherForecastWithPOCOs, typeof(List<WeatherForecastWithPOCOs>), options);
            Console.WriteLine(jsonStr);
            var weatherForecast = JsonSerializer.Deserialize<List<WeatherForecastWithPOCOs>>(jsonStr);
            foreach(var weather in weatherForecast)
            {
                Console.WriteLine(weather.Summary);
            }
        }
    }

    [Serializable]
    public class WeatherForecastWithPOCOs
    {
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
        public List<HighLowTemps> highLowTemps { get; set; }
    }

    [Serializable]
    public class HighLowTemps
    {
        public string High { get; set; }
        public string Low { get; set; }
    }
}