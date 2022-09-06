using System;
using Newtonsoft.Json;

namespace Framework.Core.Base.ModelEntity
{
    [Serializable]
    public class PaginationParameters
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("companyId")]
        public int? CompanyId { get; set; }

        [JsonProperty("searchJson")]
        public string SearchJson { get; set; }

        public dynamic SearchObject { get; set; }
    }
}
