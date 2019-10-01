using Newtonsoft.Json.Converters;

namespace VirtualLibraryApi.Converters
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
        }
    }
}