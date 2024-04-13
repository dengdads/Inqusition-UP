namespace MasaBlazorApp1.Data
{
    public class UserAccountInfo
    {
        public int? code { get; set; }
        public string? msg { get; set; }
        public Data data { get; set; } = new Data();
    }

    public class Data
    {
        public int? id { get; set; } = 0;
        public string? name { get; set; }
        public string? account { get; set; }
        public string? password { get; set; }
        public int? freeze { get; set; }
        public int? server { get; set; }
        public string? taskType { get; set; }
        public Config config { get; set; } = new Config();
        public Active active { get; set; } = new Active();
        public Notice notice { get; set; } = new Notice();
        public int? refresh { get; set; }
        public int? agent { get; set; }
        public DateTime createTime { get; set; } = new DateTime();
        public DateTime updateTime { get; set; } = new DateTime();
        public DateTime expireTime { get; set; } = new DateTime();
        public int? delete { get; set; }
        public string[]? blimitDevice { get; set; }
    }

    public class Config
    {
        public Daily daily { get; set; } = new Daily();
        public Rogue rogue { get; set; } = new Rogue();
    }

    public class Daily
    {
        public List<Fight> fight { get; set; } = new();
        public Sanity sanity { get; set; } = new Sanity();
        public bool? mail { get; set; }
        public Offer offer { get; set; } = new Offer();
        public bool? friend { get; set; }
        public Infrastructure infrastructure { get; set; } = new Infrastructure();
        public bool? credit { get; set; }
        public bool? task { get; set; }
        public bool activity { get; set; }
    }

    public class Sanity
    {
        public int? drug { get; set; }
        public int? stone { get; set; }
    }

    public class Offer
    {
        public bool? enable { get; set; }
        public bool? car { get; set; }
        public bool? star4 { get; set; }
        public bool? star5 { get; set; }
        public bool? star6 { get; set; }
        public bool? other { get; set; }
    }

    public class Infrastructure
    {
        public bool? harvest { get; set; }
        public bool? shift { get; set; }
        public bool? acceleration { get; set; }
        public bool? communication { get; set; }
        public bool? deputy { get; set; }
    }

    public class Fight
    {
        public string? level { get; set; }
        public int? num { get; set; }
    }

    public class Rogue
    {
        public Operator _operator { get; set; } = new Operator();
        public int? level { get; set; }
        public int? coin { get; set; }
        public Skip skip { get; set; } = new Skip();
    }

    public class Operator
    {
        public int? index { get; set; }
        public int? num { get; set; }
        public int? skill { get; set; }
    }

    public class Skip
    {
        public bool? coin { get; set; }
        public bool? beast { get; set; }
        public bool? daily { get; set; }
        public bool? sensitive { get; set; }
        public bool? illusion { get; set; }
        public bool? survive { get; set; }
    }

    public class Active
    {
        public Monday monday { get; set; } = new Monday();
        public Tuesday tuesday { get; set; } = new Tuesday();
        public Wednesday wednesday { get; set; } = new Wednesday();
        public Thursday thursday { get; set; } = new Thursday();
        public Friday friday { get; set; } = new Friday();
        public Saturday saturday { get; set; } = new Saturday();
        public Sunday sunday { get; set; } = new Sunday();
    }

    public class Monday
    {
        public bool? enable { get; set; }
        public string[]? detail { get; set; }
    }

    public class Tuesday
    {
        public bool? enable { get; set; }
        public string[]? detail { get; set; }
    }

    public class Wednesday
    {
        public bool? enable { get; set; }
        public string[]? detail { get; set; }
    }

    public class Thursday
    {
        public bool? enable { get; set; }
        public string[]? detail { get; set; }
    }

    public class Friday
    {
        public bool? enable { get; set; }
        public string[]? detail { get; set; }
    }

    public class Saturday
    {
        public bool? enable { get; set; }
        public string[]? detail { get; set; }
    }

    public class Sunday
    {
        public bool? enable { get; set; }
        public string[]? detail { get; set; }
    }

    public class Notice
    {
        public Wxuid wxUID { get; set; } = new Wxuid();
        public Qq qq { get; set; } = new Qq();
        public Mail mail { get; set; } = new Mail();
    }

    public class Wxuid
    {
        public string? text { get; set; }
        public bool? enable { get; set; }
    }

    public class Qq
    {
        public string? text { get; set; }
        public bool? enable { get; set; }
    }

    public class Mail
    {
        public string? text { get; set; }
        public bool? enable { get; set; }
    }
}