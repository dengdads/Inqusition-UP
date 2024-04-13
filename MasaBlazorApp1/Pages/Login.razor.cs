using MasaBlazorApp1.Data;

namespace MasaBlazorApp1.Pages
{
    public partial class Login
    {
        private bool _show;
        public UserLoginData UserLoginData = new UserLoginData("", "");
        public NavigationManager Navigation { get; set; } = default!;

        [Parameter]
        public bool HideLogo { get; set; }

        [Parameter]
        public double Width { get; set; } = 410;

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public string CreateAccountRoute { get; set; } = $"pages/authentication/register-v1";

        [Parameter]
        public string ForgotPasswordRoute { get; set; } = $"pages/authentication/forgot-password-v1";

        public async Task OnLogin()
        {
            var response = await Http.PostAsJsonAsync("/userLogin", UserLoginData);
            var content = await response.Content.ReadFromJsonAsync<UserLoginResponse>();
            if (content != null && content.data != null)
            {
                await localStorage.SetItemAsync("token", $"Bearer {content.data.token}");
                NavigationManager.NavigateTo("Overview");
                Http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {content.data.token}");
            }
            else
            {
                UserLoginData.account = "登陆失败";
            }
            return;
        }

        public async Task onGetUserStatus()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), Http.BaseAddress.OriginalString + "showMyStatus");
            var token = await localStorage.GetItemAsync<string>("token");
            request.Headers.TryAddWithoutValidation("Authorization", token);
            var response = await Http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<UserStatusResponse>();
                UserLoginData.account = content.data.status;
            }
            else
            {
                UserLoginData.account = "失败";
            }
        }
    }

    public class LoginData
    {
        /// <summary>
        ///
        /// </summary>
        public string token { get; set; }
    }

    public class UserLoginResponse
    {
        /// <summary>
        ///
        /// </summary>
        public int code { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        ///
        /// </summary>
        public LoginData data { get; set; }
    }

    public class UserStatusResponse
    {
        /// <summary>
        ///
        /// </summary>
        public int code { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        ///
        /// </summary>
        public UserStatusData data { get; set; }
    }

    public class UserStatusData
    {
        public string status { get; set; }
    }
}