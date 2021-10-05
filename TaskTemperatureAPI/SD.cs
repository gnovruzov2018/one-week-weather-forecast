using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTemperatureAPI
{
    public static class SD
    {
        // Api key
        public static string API_KEY = "df45a36741fff515381f56f686cb2976";

        // Cyprus Coordinates 
        public static string LAT = "35.1264";
        public static string LON = "33.4299";

        // Comma seperated string which excludes current ,minutely ,hourly, and alerts weather data 
        public static string EXCLUDE = "current,minutely,hourly,alerts";
    }
}
