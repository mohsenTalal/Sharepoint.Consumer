using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharepoint.Consumer
{

    public class Metadata
    {
        [JsonProperty("odata.metadata")]
        public string OdataMetadata { get; set; }
        public int FormDigestTimeoutSeconds { get; set; }
        public string FormDigestValue { get; set; }
        public string LibraryVersion { get; set; }
        public string SiteFullUrl { get; set; }
        public List<string> SupportedSchemaVersions { get; set; }
        public string WebFullUrl { get; set; }

        public static explicit operator Dictionary<object, object>(Metadata v)
        {
            throw new NotImplementedException();
        }
    }
}
