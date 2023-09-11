using System;
using System.Collections.Generic;

namespace OireachtasAPI
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AmendmentList
    {
        public AmendmentList amendmentList { get; set; }
    }

    public class AmendmentList2
    {
        public Stage stage { get; set; }
        public string date { get; set; }
        public string showAs { get; set; }
        public string stageNo { get; set; }
        public Formats formats { get; set; }
        public Chamber chamber { get; set; }
    }

    public class As
    {
        public string name { get; set; } = string.Empty;
    }

    public class Bill
    {
        public List<Debate> debates { get; set; }
        public List<Sponsor> sponsors { get; set; }
        public DateTime lastUpdated { get; set; }
        public OriginHouse originHouse { get; set; }
        public string shortTitleEn { get; set; }
        public string shortTitleGa { get; set; }
        public string status { get; set; }
        public string billType { get; set; }
        public List<object> events { get; set; }
        public string uri { get; set; }
        public List<AmendmentList> amendmentLists { get; set; }
        public string billYear { get; set; }
        public List<object> relatedDocs { get; set; }
        public string billNo { get; set; }
        public List<Stage> stages { get; set; }
        public string method { get; set; }
        public string source { get; set; }
        public List<Version> versions { get; set; }
    }

    public class By
    {
        public string name { get; set; } = string.Empty;
        public string showAs { get; set; }
    }

    public class Chamber
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

    public class Event
    {
        public string showAs { get; set; }
        public int progressStage { get; set; }
        public List<Date> dates { get; set; }
        public string uri { get; set; }
    }

    public class Formats
    {
    }
    public class Date
    {
        public string date { get; set; }
    }


    public class OriginHouse
    {
        public string showAs { get; set; }
        public string uri { get; set; }
    }

    public class Result
    {
        public Bill bill { get; set; }
    }

    public class LegislationResponse
    {
        public Head head { get; set; }
        public List<Result> results { get; set; }
    }

    public class Sponsor
    {
        public Sponsordetails sponsor { get; set; }
    }

    public class Sponsordetails
    {
        public By by { get; set; }
        public As @as { get; set; }
    }

    public class Stage
    {
        public string showAs { get; set; }
        public string uri { get; set; }
    }

    public class Stage2
    {
        public Event @event { get; set; }
    }

    public class Version
    {
        public Version version { get; set; }
    }

    public class Version2
    {
        public string date { get; set; }
        public Formats formats { get; set; }
        public string text { get; set; }
        public string lang { get; set; }
        public string showAs { get; set; }
        public string uri { get; set; }
    }


}
