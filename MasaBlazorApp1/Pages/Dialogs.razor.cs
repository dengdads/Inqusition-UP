using MasaBlazorApp1.Data;
using System;
using System.Linq;

namespace MasaBlazorApp1.Pages
{
    public partial class Dialogs
    {
        public int current = 1;
        public int size = 6;
        public string detail = "";
        public GetUserlogResponse GetUserlogResponse = new GetUserlogResponse();
        public List<UserLogRecords> Records;

        protected override async Task<Task> OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetUserLog();
            }
            StateHasChanged();
            return base.OnAfterRenderAsync(firstRender);
        }

        public async Task<string> GetUserLog()
        {
            Console.WriteLine(Http.BaseAddress.OriginalString + $"showMyLog?current={current}&size={size}");
            var request = new HttpRequestMessage(new HttpMethod("GET"), Http.BaseAddress.OriginalString + $"showMyLog?current={current}&size={size}");
            var token = await localStorage.GetItemAsync<string>("token");
            request.Headers.TryAddWithoutValidation("Authorization", token);
            var response = await Http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                GetUserlogResponse = await response.Content.ReadFromJsonAsync<GetUserlogResponse>();
                Records = GetUserlogResponse.data.records;
                detail = GetUserlogResponse.data.records[0].detail;
            }
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return response.StatusCode.ToString();
        }
    }
}