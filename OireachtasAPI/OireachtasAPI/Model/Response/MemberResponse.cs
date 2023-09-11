using System;
using System.Collections.Generic;

namespace OireachtasAPI
{
    public class MemberResponse
    {
        public Head Head { get; set; }
        public List<MemberItem> Results { get; set; }
    }



    public class MemberItem
    {
        public Member Member { get; set; }
    }

    public class Member
    {
        public string ShowAs { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public List<MembershipItem> Memberships { get; set; }
        public string Uri { get; set; }
        public string WikiTitle { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string MemberCode { get; set; }
        public bool Image { get; set; }
        public string PId { get; set; }
    }

    public class MembershipItem
    {
        public Membership Membership { get; set; }
    }

    public class Membership
    {
        public List<PartyItem> Parties { get; set; }
        public House House { get; set; }
        public List<OfficeItem> Offices { get; set; }
        public List<RepresentItem> Represents { get; set; }
        public DateRange DateRange { get; set; }
        public string Uri { get; set; }
    }

    public class PartyItem
    {
        public Party Party { get; set; }
    }

    public class Party
    {
        public string PartyCode { get; set; }
        public string Uri { get; set; }
        public string ShowAs { get; set; }
        public DateRange DateRange { get; set; }
    }

    public class House
    {
        public string HouseCode { get; set; }
        public string Uri { get; set; }
        public string HouseNo { get; set; }
        public string ShowAs { get; set; }
        public string ChamberType { get; set; }
    }

    public class OfficeItem
    {
        public Office Office { get; set; }
    }

    public class Office
    {
        public OfficeName OfficeName { get; set; }
        public DateRange DateRange { get; set; }
    }

    public class OfficeName
    {
        public string ShowAs { get; set; }
        public string Uri { get; set; }
    }

    public class RepresentItem
    {
        public Represent Represent { get; set; }
    }

    public class Represent
    {
        public string RepresentCode { get; set; }
        public string Uri { get; set; }
        public string ShowAs { get; set; }
        public string RepresentType { get; set; }
    }

}
