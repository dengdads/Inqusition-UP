namespace MasaBlazorApp1.Data
{
    public class UserStatusData
    {
        public string status { get; set; }
    }

    public class UserSanResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }

    public class FreezeUserResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }

    public class UnfreezeUserResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }

    public class UserRefreshResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int data { get; set; }
    }

    public class UserStartNowResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }

    public class UserForceHaltResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }

    public class UserStatusResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public UserStatusData data { get; set; }
    }

    public class UserLogRecords
    {
        public int id { get; set; }

        public string level { get; set; }

        public string taskType { get; set; }

        public string title { get; set; }

        public string detail { get; set; }

        public string imageUrl { get; set; }

        public string @from { get; set; }

        public int server { get; set; }

        public string name { get; set; }

        public string account { get; set; }

        public string password { get; set; }

        public string time { get; set; }

        public int delete { get; set; }
    }

    public class UserLogData
    {
        public int current { get; set; }

        public int page { get; set; }

        public int total { get; set; }

        public List<UserLogRecords> records { get; set; }
    }

    public class GetUserlogResponse
    {
        public int code { get; set; }

        public string msg { get; set; }

        public UserLogData data { get; set; }
    }
}