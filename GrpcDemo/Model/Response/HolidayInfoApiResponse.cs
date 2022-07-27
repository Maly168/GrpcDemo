using Newtonsoft.Json;

namespace GrpcDemo.Model.Response
{
    public class HolidayInfoApiResponse
    {
        [JsonProperty("date")]
        public HolidayDateInfo Date { get; set; }

        [JsonProperty("name")]
        public List<HolidayNameInfo> Name { get; set; }

        [JsonProperty("holidayType")]
        public string HolidayType { get; set; }
    }

    public class HolidayNameInfo
    {
        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class HolidayDateInfo
    {
        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("dayOfWeek")]
        public int DayOfWeek { get; set; }
    }
}
