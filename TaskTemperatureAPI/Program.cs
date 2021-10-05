using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskTemperatureAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Defining uri and making api Call with predefined parameters
                    client.BaseAddress = new Uri("https://api.openweathermap.org/");
                    var response = await client
                        .GetAsync($"/data/2.5/onecall?lat={SD.LAT}&lon={SD.LON}&exclude={SD.EXCLUDE}&units=metric&appid={SD.API_KEY}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();

                    // Parsing result as a dynamic object
                    dynamic data = JObject.Parse(stringResult);


                    // creating instance of our specific type
                    List<WeaklyWeather> weaklyWeather = new List<WeaklyWeather>();
                    var dailyData = data.daily;

                    // iterating through daily data from api response and adding it to list
                    for (int i = 1; i < dailyData.Count; i++)
                    {
                        var singleDayWeather = new WeaklyWeather
                        {
                            Pressure = dailyData[i].pressure,
                            Humidity = dailyData[i].humidity,
                            Temperature = new Temperature
                            {
                                Morning = dailyData[i].temp.morn,
                                Day = dailyData[i].temp.day,
                                Evening = dailyData[i].temp.eve,
                                Night = dailyData[i].temp.night,
                                Min = dailyData[i].temp.min,
                                Max = dailyData[i].temp.max
                            }
                        };
                        weaklyWeather.Add(singleDayWeather);
                    }

                    // printing result in console
                    int day = 1;
                    foreach (var dailyTemperature in weaklyWeather)
                    {
                        Console.WriteLine($"Day #{day++}:");
                        Console.WriteLine($" -Pressure: {dailyTemperature.Pressure}");
                        Console.WriteLine($" -Humidity: {dailyTemperature.Humidity}");
                        Console.WriteLine($" -Temperature:");
                        Console.WriteLine($"  -Morning: {dailyTemperature.Temperature.Morning}°C");
                        Console.WriteLine($"  -Day: {dailyTemperature.Temperature.Day}°C");
                        Console.WriteLine($"  -Evening: {dailyTemperature.Temperature.Evening}°C");
                        Console.WriteLine($"  -Night: {dailyTemperature.Temperature.Night}°C");
                        Console.WriteLine($"  -Minimum: {dailyTemperature.Temperature.Min}°C");
                        Console.WriteLine($"  -Maximum: {dailyTemperature.Temperature.Max}°C");
                    }

                    Console.ReadKey();
                }
                catch (HttpRequestException httpRequestException)
                {
                    Console.WriteLine($"Something went wrong: {httpRequestException.Message}");
                }
            }
        }
    }
}
