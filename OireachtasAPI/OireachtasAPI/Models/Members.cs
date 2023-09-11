
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
    public int memberCount { get; set; }
    public int resultCount { get; set; }
}

public class Daterange
{
    public string start { get; set; }
    public string end { get; set; }
}

public class Result
{
    public Member member { get; set; }
}

public class Member
{
    public string showAs { get; set; }
    public string lastName { get; set; }
    public string firstName { get; set; }
    public string gender { get; set; }
    public Membership[] memberships { get; set; }
    public string uri { get; set; }
    public string wikiTitle { get; set; }
    public string fullName { get; set; }
    public string dateOfDeath { get; set; }
    public string memberCode { get; set; }
    public bool image { get; set; }
    public string pId { get; set; }
}

public class Membership
{
    public Membership1 membership { get; set; }
}

public class Membership1
{
    public Party[] parties { get; set; }
    public House house { get; set; }
    public Office[] offices { get; set; }
    public string uri { get; set; }
    public Represent[] represents { get; set; }
    public Daterange1 dateRange { get; set; }
}

public class House
{
    public string houseCode { get; set; }
    public string uri { get; set; }
    public string houseNo { get; set; }
    public string showAs { get; set; }
    public string chamberType { get; set; }
}

public class Daterange1
{
    public string end { get; set; }
    public string start { get; set; }
}

public class Party
{
    public Party1 party { get; set; }
}

public class Party1
{
    public string partyCode { get; set; }
    public string uri { get; set; }
    public string showAs { get; set; }
    public Daterange2 dateRange { get; set; }
}

public class Daterange2
{
    public string end { get; set; }
    public string start { get; set; }
}

public class Office
{
    public Office1 office { get; set; }
}

public class Office1
{
    public Officename officeName { get; set; }
    public Daterange3 dateRange { get; set; }
}

public class Officename
{
    public string showAs { get; set; }
    public string uri { get; set; }
}

public class Daterange3
{
    public string end { get; set; }
    public string start { get; set; }
}

public class Represent
{
    public Represent1 represent { get; set; }
}

public class Represent1
{
    public string representCode { get; set; }
    public string uri { get; set; }
    public string showAs { get; set; }
    public string representType { get; set; }
}
