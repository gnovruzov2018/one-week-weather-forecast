using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTemperatureAPI
{
    public class WeaklyWeather
    {
        public Temperature Temperature { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class Temperature
    {
        public double Morning { get; set; }
        public double Day { get; set; }
        public double Evening { get; set; }
        public double Night { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }

    }
}
