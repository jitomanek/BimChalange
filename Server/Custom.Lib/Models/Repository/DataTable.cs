using Newtonsoft.Json;

namespace Custom.Lib.Models.Repository
{
    public class DataTable : DataTablePagination
    {
        [JsonIgnore]
        public DataTableFilter?[] FilterArray => GetType().GetProperties()
            .Where(x => x.PropertyType == typeof(DataTableFilter))
            .Select(x =>
            {
                var obj = (DataTableFilter)x.GetValue(this);
                if (obj?.PropValue != null && !string.IsNullOrWhiteSpace(x.Name) && PropertyNames.TryGetValue(x.Name, out string propName))
                    obj.PropName = propName;
                else
                    return null;

                return obj;
            })
            .Where(x => x != null)
            .ToArray();

        [JsonIgnore]
        protected IDictionary<string, string>? PropertyNames { get; set; }
        protected virtual IDictionary<string, string>? GetPropertyNames() { return PropertyNames; }
    }
}
