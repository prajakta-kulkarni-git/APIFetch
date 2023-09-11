using Newtonsoft.Json;
using OireachtasAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OireachtasAPI
{
    public class Program
    {
        static List<Member> listOfMembers = new List<Member>();
        static List<LegislationResponse> listOfLegislation = new List<LegislationResponse>();

        public const string LEGISLATION_DATASET = Constants.LEGISLATION_DATASET;
        public const string MEMBERS_DATASET = Constants.MEMBERS_DATASET;
        static void Main(string[] args)
        {
            bool continueInnerLoop, continueOuterLoop = true;
            do
            {
                #region Fetch Member Data
                //Console.WriteLine("How to want to fetch Member data? By FileName(1) or API(2)... Select numbers as input.");
                Console.WriteLine("Choose an option to fetch Member data:");
                Console.WriteLine("(1) Fetch data from files");
                Console.WriteLine("(2) Fetch data from API");
                Console.WriteLine("(3) Exit");

                string memberFetchWay = Console.ReadLine();
                string path = string.Empty;

                switch (memberFetchWay)
                {
                    case "1":
                        Console.WriteLine("Selected Option for fetching Members - FileName");
                        break;
                    case "2":
                        Console.WriteLine("Selected Option for fetching Members - API");
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid Option Selected. Exiting Program...");
                        Thread.Sleep(3000);
                        break;
                }
                if (memberFetchWay.Equals("2", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter API URL with endpoint or just press enter for https://api.oireachtas.ie/v1/members");
                    path = Console.ReadLine();
                }

                Console.WriteLine("Fetching member data...");
                listOfMembers = GetMembersAsync(memberFetchWay, path).Result;
                if (listOfMembers.Count == 0)
                {
                    continue;
                }
                #endregion

                #region Fetch Lesislation Data              
                //Console.WriteLine("How to want to fetch Legislation data? By FileName(1) or API(2)... Select numbers as input.");
                Console.WriteLine("Choose an option to fetch Legislation data:");
                Console.WriteLine("(1) Fetch data from files");
                Console.WriteLine("(2) Fetch data from API");
                Console.WriteLine("(3) Exit");
                string legislationFetchWay = Console.ReadLine();

                path = string.Empty;

                switch (legislationFetchWay)
                {
                    case "1":
                        Console.WriteLine("Selected Option for fetching Legislation - FileName");
                        break;
                    case "2":
                        Console.WriteLine("Selected Option for fetching Legislation - API");
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid Option Selected. Exiting Program...");
                        Thread.Sleep(3000);
                        break;
                }
                if (legislationFetchWay.Equals("2", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter API URL with endpoint or jusr press enter for https://api.oireachtas.ie/v1/legislation");
                    path = Console.ReadLine();
                }
                Console.WriteLine("Fetching Legislation data...");
                listOfLegislation = GetLegislationResultAsync(legislationFetchWay, path).Result;
                if (listOfLegislation.Count == 0)
                {
                    continue;
                }
                #endregion
                do
                {
                    //Console.WriteLine("What operation you want to perform?\n(1) Fetch bills sponsored by a given member.\n(2) Fetch bills those were last updated within a specified time period.");
                    Console.WriteLine("What operation do you want to perform?");
                    Console.WriteLine("(1) Fetch bills sponsored by a given member.");
                    Console.WriteLine("(2) Fetch bills that were last updated within a specified time period.");
                    Console.WriteLine("(3) Back to main menu");
                    string operationChoice = Console.ReadLine();
                    continueInnerLoop = true;
                    switch (operationChoice)
                    {
                        case "1":
                            string pId;
                            do
                            {
                                Console.WriteLine("Enter PID:");
                                pId = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(pId))
                                {
                                    Console.WriteLine("PID cannot be null or empty. Please try again.");
                                }
                            } while (string.IsNullOrWhiteSpace(pId));
                            var result = FilterBillsSponsoredBy(pId, listOfLegislation, listOfMembers);

                            Console.WriteLine($"Total count of Bills Sponsored by {pId} are - {result.Count()}");
                            Console.WriteLine("Bill Details -");
                            foreach (var item in result)
                            {
                                Console.WriteLine(JsonConvert.SerializeObject(item));
                            }
                            break;

                        case "2":
                            Console.WriteLine("Enter Start Date");
                            string startDateString = Console.ReadLine();

                            Console.WriteLine("Enter End date");
                            string endDateString = Console.ReadLine();

                            if (DateTime.TryParse(startDateString, out DateTime startDate) && DateTime.TryParse(endDateString, out DateTime endDate))
                            {
                                List<Bill> filteredBills = FilterBillsByLastUpdated(startDate, endDate, listOfLegislation);
                                if (filteredBills.Any())
                                {
                                    Console.WriteLine($"Found {filteredBills.Count} bills updated within the specified date range.");
                                }
                                else
                                {
                                    Console.WriteLine("No bills found within the specified date range.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("invalid date");
                            }
                            break;
                        case "3":
                            continueInnerLoop = false;
                            break;
                        default:
                            break;
                    }
                } while (continueInnerLoop);
            } while (continueOuterLoop);
        }

        public static Func<string, dynamic> load = jfname => JsonConvert.DeserializeObject((new System.IO.StreamReader(jfname)).ReadToEnd());

        /// <summary>
        /// Return Member data 
        /// </summary>
        /// <param name="memberFetchWay">selecting way to fetch data either API or dataset</param>
        /// <param name="path">API Path if provided by user</param>
        /// <returns>List of member records</returns>
        public static async Task<List<Member>> GetMembersAsync(string memberFetchWay, string path = "")
        {
            try
            {
                if (memberFetchWay.Equals("2", StringComparison.OrdinalIgnoreCase))
                {
                    string apiUrl = "https://api.oireachtas.ie/v1/members";
                    string url = string.IsNullOrWhiteSpace(path) ? apiUrl : path;


                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(data))
                        {
                            var membersResponse = JsonConvert.DeserializeObject<MemberResponse>(data);
                            if (membersResponse != null && membersResponse.Results != null)
                            {
                                // Map MemberItems to Members here
                                List<Member> members = membersResponse.Results.Select(memberItem => memberItem.Member).ToList();
                                return members;
                            }
                            else
                            {
                                Console.WriteLine("Invalid API response: Missing or null data.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("API response is empty.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code {response.StatusCode}");
                    }
                }
                else if (memberFetchWay.Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    var data = load(MEMBERS_DATASET);

                    if (data != null)
                    {
                        MemberResponse membersResponse = JsonConvert.DeserializeObject<MemberResponse>(Convert.ToString(data));
                        if (membersResponse != null)
                        {
                            // Map MemberItems to Members here
                            List<Member> members = membersResponse.Results.Select(memberItem => memberItem.Member).ToList();
                            return members;
                        }
                        else
                        {
                            Console.WriteLine("Invalid File Content");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return new List<Member>();
        }

        /// <summary>
        /// Return Legislation data 
        /// </summary>
        /// <param name="legislationFetchWay">selecting way to fetch data either API or dataset</param>
        /// <param name="path">API Path if provided by user</param>
        /// <returns>List of legislation records</returns>
        public static async Task<List<LegislationResponse>> GetLegislationResultAsync(string legislationFetchWay, string path = "")
        {
            string baseUrl = "https://api.oireachtas.ie/v1/legislation";

            try
            {
                if (legislationFetchWay.Equals("2", StringComparison.OrdinalIgnoreCase))
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(baseUrl);
                    List<LegislationResponse> legislationList = new List<LegislationResponse>();
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(data))
                        {
                            var legislationResponse = JsonConvert.DeserializeObject<LegislationResponse>(data);
                            if (legislationResponse != null && legislationResponse.results != null)
                            {

                                legislationList.Add(legislationResponse);
                                return legislationList;
                            }
                            else
                            {
                                Console.WriteLine("Invalid API response: Missing or null data.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("API response is empty.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code {response.StatusCode}");
                    }
                }
                else if (legislationFetchWay.Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    var data = load(LEGISLATION_DATASET);

                    if (data != null)
                    {
                        LegislationResponse legislationResponse = JsonConvert.DeserializeObject<LegislationResponse>(Convert.ToString(data));
                        if (legislationResponse != null)
                        {
                            // Map MemberItems to Members here
                            List<LegislationResponse> legislations = new List<LegislationResponse>
                            {
                                legislationResponse
                            };
                            return legislations;
                        }
                        else
                        {
                            Console.WriteLine("Invalid File Content");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return new List<LegislationResponse>();
        }

        /// <summary>
        /// Return bills updated within the specified date range
        /// </summary>
        /// <param name="since">The lastUpdated value for the bill should be greater than or equal to this date</param>
        /// <param name="until">The lastUpdated value for the bill should be less than or equal to this date.If unspecified, until will default to today's date</param>
        /// <returns>List of bill records</returns>
        public static List<Bill> FilterBillsByLastUpdated(DateTime since, DateTime until, List<LegislationResponse> listOfLegislation)
        {
            List<Bill> listOfBills = new List<Bill>();
            try
            {

                foreach (var itemlegislation in listOfLegislation)
                {
                    foreach (var item in itemlegislation.results)
                    {
                        if (item != null)
                        {
                            if (item.bill.lastUpdated >= since && item.bill.lastUpdated <= until)
                            {
                                listOfBills.Add(item.bill);
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return listOfBills;
        }

        /// <summary>
        /// Return bills sponsored by the member with the specified pId
        /// </summary>
        /// <param name="pId">The pId value for the member</param>
        /// <returns>List of bill records</returns>
        public static List<Bill> FilterBillsSponsoredBy(string pId, List<LegislationResponse> leg, List<Member> mem)
        {
            //dynamic leg = load(LEGISLATION_DATASET);
            //dynamic mem = load(MEMBERS_DATASET);
            try
            {
                List<Bill> ret = new List<Bill>();

                foreach (LegislationResponse res in leg)
                {
                    foreach (var result in res.results)
                    {
                        List<Sponsor> sponsors = result.bill.sponsors; // res["bill"]["sponsors"];
                        foreach (Sponsor sponser in sponsors)
                        {
                            string name = sponser.sponsor.by.showAs; // i["sponsor"]["by"]["showAs"];
                            if (name != null)
                            {
                                foreach (Member member in mem)
                                {
                                    if (member != null)
                                    {
                                        string fname = member.FullName; // result["member"]["fullName"];
                                        string rpId = member.PId; // result["member"]["pId"];
                                        if (fname == name && rpId == pId)
                                        {
                                            ret.Add(result.bill);// res["bill"]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return new List<Bill>();

        }
    }

}


