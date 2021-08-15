using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace Sharepoint.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Data> getdata = GetListData();
            foreach (Data data in getdata)
            {
                Console.WriteLine("Employee Name" + data.Title);
            }
            Console.ReadLine();
        }

        public static List<Data> GetListData()
        {
            const string DataColumn = "Account%5Fx0020%5Fnumber,Status,Fund%5Fx0020%5FName";
            const string DataAPIAllData = "{0}/_api/lists/getbytitle('{1}')/items?$top=10&$select=" + DataColumn + "&$orderby=Modified desc";
           
            try
            {
                var results = new List<Data>();
                string sharepointSiteUrl = Convert.ToString("https://mittal1201.sharepoint.com/sites/CommSiteHub");
                if (!string.IsNullOrEmpty(sharepointSiteUrl))
                {
                    string listname = "Employee";
                    string api = string.Format(DataAPIAllData, sharepointSiteUrl, listname); string url = api;
                    if (!string.IsNullOrEmpty(listname))
                    {
                        //Invoke REST Call  
                        string response = TokenHelper.GetAPIResponse(api);
                        if (!String.IsNullOrEmpty(response))
                        {
                          
                            dynamic jobj = JsonConvert.DeserializeObject(response);
                            JArray jarr = (JArray)jobj["d"]["results"];

                            //Write Response to Output  
                            foreach (JObject j in jarr)
                            {
                                Data data = new Data();
                                data.Title = Convert.ToString(j["Title"]);

                                results.Add(data);
                            }
                        }
                        return results;
                    }
                    else
                    {
                        throw new Exception("Custom Message");
                    }
                }
                else
                {
                    throw new Exception("Custom Message");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Custom Message");
            }
        }
    }
}
