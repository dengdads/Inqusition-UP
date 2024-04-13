using Newtonsoft.Json.Linq;
using System.Runtime.Intrinsics.X86;
using MasaBlazorApp1.Data;

namespace MasaBlazorApp1.Pages;

public partial class Overview
{
    public string san = "";
    public string status = "";
    public string refresh = "";
    public string expireTime = "";

    [Inject]
    private IPopupService PopupService { get; set; } = null;

    protected override async Task<Task> OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetUserSan();
            await GetUserStatus();
            await GetUserRefresh();
            await GetUserAccount();
        }
        StateHasChanged();
        return base.OnAfterRenderAsync(firstRender);
    }

    public async Task RefreshOverview()
    {
        await GetUserSan();
        await GetUserStatus();
        await GetUserRefresh();
        await GetUserAccount();
    }

    public async Task<string> GetUserSan()
    {
        var request = new HttpRequestMessage(new HttpMethod("GET"), Http.BaseAddress.OriginalString + "showMySan");
        var token = await localStorage.GetItemAsync<string>("token");
        request.Headers.TryAddWithoutValidation("Authorization", token);
        var response = await Http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<UserSanResponse>();
            if (content.data == "出现严重错误，请立即使用立即作战以校准")
            {
                san = "你的账户可能正在冻结";
            }
            else
            {
                san = content.data;
            }
            return san;
        }
        else
        {
            san = "失败";
            return san;
        }
    }

    public async Task<string> GetUserStatus()
    {
        var request = new HttpRequestMessage(new HttpMethod("GET"), Http.BaseAddress.OriginalString + "showMyStatus");
        var token = await localStorage.GetItemAsync<string>("token");
        request.Headers.TryAddWithoutValidation("Authorization", token);
        var response = await Http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<UserStatusResponse>();
            status = content.data.status;
            return status;
        }
        else
        {
            status = "失败";
            return status;
        }
    }

    public async Task<string> FreezeUserAccount()
    {
        var request = new HttpRequestMessage(new HttpMethod("POST"), Http.BaseAddress.OriginalString + "freezeMyAccount");
        var token = await localStorage.GetItemAsync<string>("token");
        request.Headers.TryAddWithoutValidation("Authorization", token);
        var response = await Http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<FreezeUserResponse>();
            await RefreshOverview();
            await PopupService.ConfirmAsync("冻结账号", content.msg, AlertTypes.Warning);
            return status;
        }
        else
        {
            status = "失败";
            return status;
        }
    }

    public async Task<string> UnfreezeUserAccount()
    {
        var request = new HttpRequestMessage(new HttpMethod("POST"), Http.BaseAddress.OriginalString + "unfreezeMyAccount");
        var token = await localStorage.GetItemAsync<string>("token");
        request.Headers.TryAddWithoutValidation("Authorization", token);
        var response = await Http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<UnfreezeUserResponse>();
            await RefreshOverview();
            await PopupService.ConfirmAsync("解冻账号", content.msg, AlertTypes.Success);
            return status;
        }
        else
        {
            status = "失败";
            return status;
        }
    }

    public async Task<string> GetUserRefresh()
    {
        var request = new HttpRequestMessage(new HttpMethod("GET"), Http.BaseAddress.OriginalString + "getRefresh");
        var token = await localStorage.GetItemAsync<string>("token");
        request.Headers.TryAddWithoutValidation("Authorization", token);
        var response = await Http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<UserRefreshResponse>();
            refresh = content.data.ToString();
            return refresh;
        }
        else
        {
            refresh = "失败";
            return refresh;
        }
    }

    public async Task<string> UserStartNow()
    {
        var request = new HttpRequestMessage(new HttpMethod("POST"), Http.BaseAddress.OriginalString + "startNow");
        var token = await localStorage.GetItemAsync<string>("token");
        request.Headers.TryAddWithoutValidation("Authorization", token);
        var response = await Http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<UserStartNowResponse>();
            await RefreshOverview();
            await PopupService.ConfirmAsync("立刻开始作战", content.msg, AlertTypes.Success);
            return refresh;
        }
        else
        {
            refresh = "失败";
            return refresh;
        }
    }

    public async Task<string> UserForceHalt()
    {
        var request = new HttpRequestMessage(new HttpMethod("POST"), Http.BaseAddress.OriginalString + "forceHalt");
        var token = await localStorage.GetItemAsync<string>("token");
        request.Headers.TryAddWithoutValidation("Authorization", token);
        var response = await Http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<UserStartNowResponse>();
            await RefreshOverview();
            await PopupService.ConfirmAsync("强制停止作战", content.msg, AlertTypes.Success);
            return refresh;
        }
        else
        {
            refresh = "失败";
            return refresh;
        }
    }

    public async Task<string> GetUserAccount()
    {
        var request = new HttpRequestMessage(new HttpMethod("GET"), Http.BaseAddress.OriginalString + "showMyAccount");
        var token = await localStorage.GetItemAsync<string>("token");
        request.Headers.TryAddWithoutValidation("Authorization", token);
        var response = await Http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            JObject jobject = JObject.Parse(content);
            expireTime = jobject["data"]["expireTime"].ToString();
            return expireTime;
        }
        else
        {
            expireTime = "失败";
            return expireTime;
        }
    }
}