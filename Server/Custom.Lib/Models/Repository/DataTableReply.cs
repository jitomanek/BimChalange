namespace Custom.Lib.Models.Repository
{
    public class DataTableReply<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
    }
}
