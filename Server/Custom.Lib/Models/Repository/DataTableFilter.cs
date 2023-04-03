using Newtonsoft.Json;

namespace Custom.Lib.Models.Repository
{
    public class DataTableFilter
    {

        [JsonIgnore]
        public string? PropName { get; set; }
        public string? PropValue { get; set; }
        public FilterType? FilterType { get; set; }
    }
}
