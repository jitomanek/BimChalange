namespace Custom.Lib.Models.Repository
{
    public class DataTablePagination
    {
        public int CountOnPage { get; set; }
        public int PageIndex { get; set; }
        public int NumberOfPages { get; set; }

        private int _from => CountOnPage == 0 ? Count * PageIndex : CountOnPage * (PageIndex - 1);
        private int _count => NumberOfPages * CountOnPage;
        internal int From => _from;
        internal int Count => _count == 0 ? 30 : _count;
    }
}
