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
            var responseApi = JsonConvert.DeserializeObject<List<HolidayInfoApiResponse>>(apiResponse.Result)?.First();
            var response = new HolidayInfoResponse()
            {
                //Date = new RepeatedField<HolidayInfoResponse.Types.dateInfo>(),
                HolidayType = responseApi.HolidayType,

            };
            response.Date.Add(new HolidayInfoResponse.Types.dateInfo()
            {
                Day = responseApi.Date.Day,
                Month = responseApi.Date.Month,
                DayOfWeek = responseApi.Date.DayOfWeek,
                Year = responseApi.Date.Year,

            });

            return Task.FromResult(response) ;

        }
    }
}
