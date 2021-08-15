using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Sharepoint.Consumer
{
    public class TokenHelper
    {
        public static string GetAPIResponse(string url)
        {
            try
            {
                string result = string.Empty;
                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;odata=verbose";
                endpointRequest.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                endpointRequest.UseDefaultCredentials = true;
                endpointRequest.PreAuthenticate = true;
                endpointRequest.Credentials = new NetworkCredential("UserName", "Password");
                endpointRequest.Headers.Add("X-RequestDigest", GetSharePointAccessToken());
                HttpWebResponse endpointResponse =(HttpWebResponse)endpointRequest.GetResponse();
                using (StreamReader sr = new StreamReader(endpointResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }

        public static string GetSharePointAccessToken()
        {

            Uri site = new Uri("http://<site url>/_api/contextinfo");
            string user = "UserName";
            string pwd = "Password";
            string result;
            using (var authenticationManager = new AuthManager())
            {
                string accessTokenSP = authenticationManager.AcquireToken(site, user, pwd).Result;
                result = accessTokenSP;
            }
            return result;
        }

    }
}
