using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OireachtasAPI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestOireachtasAPI
{
    namespace OireachtasAPI.Tests
    {
        [TestClass]

        public class LoadDatasetTest
        {
            dynamic memberExpected, legislationExpected;
            [TestInitialize]
            public void SetUpData()
            {
                using (StreamReader r = new StreamReader(Program.MEMBERS_DATASET))
                {
                    string json = r.ReadToEnd();
                    memberExpected = JsonConvert.DeserializeObject(json);
                }
                using (StreamReader r = new StreamReader(Program.LEGISLATION_DATASET))
                {
                    string json = r.ReadToEnd();
                    legislationExpected = JsonConvert.DeserializeObject(json);
                }
            }

            [TestMethod]
            public void TestLoadFromFile()
            {
                dynamic loaded = Program.load(Program.MEMBERS_DATASET);
                Assert.AreEqual(loaded["results"].Count, memberExpected["results"].Count);
            }



            [TestMethod]
            public async Task TestLoadFromUrlAsync_withPath()
            {
                // Make an asynchronous API request
                dynamic loaded = await Program.GetMembersAsync("2", "https://api.oireachtas.ie/v1/members?limit=50");

                // Ensure that the API response is not null
                Assert.IsNotNull(loaded);

                // Ensure that the "PId" property is present in the response

                var loadedItems = (IEnumerable<dynamic>)loaded;
                bool pidExists = loadedItems.Any(item => item.PId != null);
                Assert.IsTrue(pidExists);

            }

            [TestMethod]
            public async Task TestLoadFromUrlAsync_WithoutPath()
            {
                // Make an asynchronous API request
                dynamic loaded = await Program.GetMembersAsync("2", "");

                // Ensure that the API response is not null
                Assert.IsNotNull(loaded);

                // Ensure that the "PId" property is present in the response

                var loadedItems = (IEnumerable<dynamic>)loaded;
                bool pidExists = loadedItems.Any(item => item.PId != null);
                Assert.IsTrue(pidExists);

            }

            [TestMethod]
            public void TestLoadLegislationFromFile()
            {
                dynamic loaded = Program.load(Program.LEGISLATION_DATASET);
                Assert.AreEqual(loaded["results"].Count, legislationExpected["results"].Count);
            }



            [TestMethod]
            public async Task TestLoadLegislationFromUrlAsync_WithPath()
            {
                // Make an asynchronous API request
                dynamic loaded = await Program.GetLegislationResultAsync("2", "https://api.oireachtas.ie/v1/legislations");

                // Ensure that the API response is not null
                Assert.IsNotNull(loaded);

                // Ensure that the "PId" property is present in the response
                bool resultsExists = false;
                int count = 0;
                foreach (var item in loaded)
                {
                    if (item.results != null)
                    {
                        resultsExists = true; count++;
                        break; // Exit the loop if "PId" is found in at least one item
                    }
                }
                Assert.IsTrue(resultsExists);


            }

            [TestMethod]
            public async Task TestLoadLegislationFromUrlAsync_WithoutPath()
            {
                // Make an asynchronous API request
                dynamic loaded = await Program.GetLegislationResultAsync("2", "");

                // Ensure that the API response is not null
                Assert.IsNotNull(loaded);

                // Ensure that the "PId" property is present in the response
                bool resultsExists = false;
                int count = 0;
                foreach (var item in loaded)
                {
                    if (item.results != null)
                    {
                        resultsExists = true; count++;
                        break; // Exit the loop if "PId" is found in at least one item
                    }
                }
                Assert.IsTrue(resultsExists);


            }
        }
    }
}
