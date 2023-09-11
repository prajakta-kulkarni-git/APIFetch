
public class Rootobject
{
    public Head head { get; set; }
    public Result[] results { get; set; }
}

public class Head
{
    public Counts counts { get; set; }
    public Daterange dateRange { get; set; }
    public string lang { get; set; }
}

public class Counts
{
}

public class Daterange
{
    public DateTime start { get; set; }
    public DateTime end { get; set; }
}

public class Result
{
    public Bill bill { get; set; }
}

public class Bill
{
    public Debate[] debates { get; set; }
    public Sponsor[] sponsors { get; set; }
    public DateTime lastUpdated { get; set; }
    public Originhouse originHouse { get; set; }
    public string shortTitleEn { get; set; }
    public string shortTitleGa { get; set; }
    public string status { get; set; }
    public string billType { get; set; }
    public object[] events { get; set; }
    public string uri { get; set; }
    public Amendmentlist[] amendmentLists { get; set; }
    public string billYear { get; set; }
    public object[] relatedDocs { get; set; }
    public string billNo { get; set; }
    public Stage1[] stages { get; set; }
    public string method { get; set; }
    public string source { get; set; }
    public Version[] versions { get; set; }
}

public class Originhouse
{
    public string showAs { get; set; }
    public string uri { get; set; }
}

public class Debate
{
    public string debateSectionId { get; set; }
    public string date { get; set; }
    public string showAs { get; set; }
    public string uri { get; set; }
    public Chamber chamber { get; set; }
}

public class Chamber
{
    public string showAs { get; set; }
    public string uri { get; set; }
}

public class Sponsor
{
    public Sponsor1 sponsor { get; set; }
}

public class Sponsor1
{
    public By by { get; set; }
    public As _as { get; set; }
}

public class By
{
}

public class As
{
}

public class Amendmentlist
{
    public Amendmentlist1 amendmentList { get; set; }
}

public class Amendmentlist1
{
    public Stage stage { get; set; }
    public string date { get; set; }
    public string showAs { get; set; }
    public string stageNo { get; set; }
    public Formats formats { get; set; }
    public Chamber1 chamber { get; set; }
}

public class Stage
{
    public string showAs { get; set; }
    public string uri { get; set; }
}

public class Formats
{
}

public class Chamber1
{
    public string showAs { get; set; }
    public string uri { get; set; }
}

public class Stage1
{
    public Event _event { get; set; }
}

public class Event
{
    public string showAs { get; set; }
    public int progressStage { get; set; }
    public Date[] dates { get; set; }
    public string uri { get; set; }
}

public class Date
{
    public string date { get; set; }
}

public class Version
{
    public Version1 version { get; set; }
}

public class Version1
{
    public string date { get; set; }
    public Formats1 formats { get; set; }
    public string text { get; set; }
    public string lang { get; set; }
    public string showAs { get; set; }
    public string uri { get; set; }
}

public class Formats1
{
}
