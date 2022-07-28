using Google.Protobuf.Collections;
using Grpc.Core;
using GrpcDemo.Model.Response;
using Newtonsoft.Json;

namespace GrpcDemo.Services
{
    public class HolidayInfoService : HolidayInfo.HolidayInfoBase
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _apiUrl;

        public HolidayInfoService(IHttpClientService httpClientService, IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            _apiUrl = configuration.GetValue<string>("ApiUrl");
        }

        public override Task<HolidayInfoResponse> GetHoliday(HolidayInfoRequest request, ServerCallContext context)
        {
            var url = $"{_apiUrl}action=getHolidaysForYear&year={request.Year}&country={request.Country}&holidayType=all";
            var apiResponse =  _httpClientService.Get(url);
            var responseApi = JsonConvert.DeserializeObject<List<HolidayInfoApiResponse>>(apiResponse.Result);
            var response = new HolidayInfoResponse();
            foreach (var holiday in responseApi)
            {
                var data = new HolidayInfoResponse.Types.HolidayInfoData();
                data.HolidayType = holiday.HolidayType;
                data.Date.Add(new HolidayInfoResponse.Types.dateInfo()
                {
                    Day = holiday.Date.Day,
                    Month = holiday.Date.Month,
                    DayOfWeek = holiday.Date.DayOfWeek,
                    Year = holiday.Date.Year,

                });
                foreach (var name in holiday.Name)
                {
                    data.Name.Add(new HolidayInfoResponse.Types.nameInfo()
                    {
                        Lang = name.Language,
                        Text = name.Text

                    });
                };
                response.HolidayInfo.Add(data);
            }
           

            return Task.FromResult(response) ;

        }
    }
}
