using Microsoft.VisualStudio.TestTools.UnitTesting;
using OireachtasAPI;
using System;
using System.Collections.Generic;

namespace TestOireachtasAPI
{
    namespace OireachtasAPI.Tests
    {
        [TestClass]
        public class ProgramTests
        {
            // Mock data for testing
            private static List<LegislationResponse> mockLegislation;
            private static List<Member> mockMembers;

            [ClassInitialize]
            public static void ClassInitialize(TestContext context)
            {
                // Initialize mock data here (e.g., create mock members and legislation responses)
                mockMembers = new List<Member>
        {
            new Member { FullName = "Prajakta Kulkarni", PId = "PrajaktaKulkarni" , Memberships = new List<MembershipItem>
                {
                    new MembershipItem
                    {
                        Membership = new Membership
                        {
                            Parties = new List<PartyItem>
                            {
                                new PartyItem
                                {
                                    Party = new Party
                                    {
                                        ShowAs = "Party A"
                                    }
                                }
                            }
                        }
                    }
                }
            },
            new Member { FullName = "Sush Kulkarni", PId = "SushKulkarni", Memberships = new List<MembershipItem>
                {
                    new MembershipItem
                    {
                        Membership = new Membership
                        {
                            Parties = new List<PartyItem>
                            {
                                new PartyItem
                                {
                                    Party = new Party
                                    {
                                        ShowAs = "Party B"
                                    }
                                }
                            }
                        }
                    }
                } },
        };

                mockLegislation = new List<LegislationResponse>
        {
            new LegislationResponse
            {
                results = new List<Result>
                {
                    new Result
                    {
                        bill = new Bill
                        {
                            sponsors = new List<Sponsor>
                            {
                                new Sponsor { sponsor = new Sponsordetails { by = new By{showAs= "Prajakta Kulkarni" } } },
                            },
                             lastUpdated=new DateTime(2023, 3, 2)
                        }
                    },
                    new Result
                    {
                        bill = new Bill
                        {
                            sponsors = new List<Sponsor>
                            {
                                new Sponsor { sponsor = new Sponsordetails { by = new By{showAs= "Sush Kulkarni" } } },
                            },
                            lastUpdated=new DateTime(2023, 2, 2)
                        }
                    },
                }
            }
        };
            }

            [TestMethod]
            public void FilterBillsSponsoredBy_ShouldReturnBillsForValidSponsor()
            {
                // Arrange
                string pidSponsorName = "PrajaktaKulkarni";
                string fullName = "Prajakta Kulkarni";
                // Act
                List<Bill> sponsoredBills = Program.FilterBillsSponsoredBy(pidSponsorName, mockLegislation, mockMembers);

                // Assert
                Assert.IsNotNull(sponsoredBills);
                Assert.AreEqual(1, sponsoredBills.Count);
                Assert.AreEqual(fullName, sponsoredBills[0].sponsors[0].sponsor.by.showAs);
            }


            [TestMethod]
            public void FilterBillsSponsoredBy_ShouldReturnEmptyListForInvalidSponsor()
            {
                // Arrange
                string sponsorName = "Invalid Sponsor";

                // Act
                List<Bill> sponsoredBills = Program.FilterBillsSponsoredBy(sponsorName, mockLegislation, mockMembers);

                // Assert
                Assert.IsNotNull(sponsoredBills);
                Assert.AreEqual(0, sponsoredBills.Count);
            }

            [TestMethod]
            public void FilterBillsByLastUpdated_valid()
            {

                DateTime sinceDate = new DateTime(2023, 1, 1); // Set your desired date
                DateTime untilDate = new DateTime(2023, 9, 1); // Set your desired date
                List<Bill> Bill = Program.FilterBillsByLastUpdated(sinceDate, untilDate, mockLegislation);

                Assert.IsNotNull(Bill);
                Assert.IsTrue(Bill.Count > 0);
                Assert.IsTrue(Bill.Count == 2);

            }

            [TestMethod]
            public void FilterBillsByLastUpdated_Invalid()
            {

                DateTime sinceDate = new DateTime(1000, 1, 1); // Set your desired date
                DateTime untilDate = new DateTime(1200, 9, 1); // Set your desired date
                List<Bill> Bill = Program.FilterBillsByLastUpdated(sinceDate, untilDate, mockLegislation);

                Assert.IsNotNull(Bill);
                Assert.IsTrue(Bill.Count == 0);

            }

            [TestMethod]
            public void FilterBillsByLastUpdated_nullUntil()
            {
                _ = new List<Bill>();
                DateTime sinceDate = new DateTime(1000, 1, 1); // Set your desired date
                DateTime untilDate = new DateTime(1200, 9, 1); // Set your desired date
                List<Bill> Bill = Program.FilterBillsByLastUpdated(sinceDate, untilDate, mockLegislation);

                Assert.IsNotNull(Bill);
                Assert.IsTrue(Bill.Count == 0);

            }
        }
    }
}


