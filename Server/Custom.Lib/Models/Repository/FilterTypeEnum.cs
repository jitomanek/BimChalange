namespace Custom.Lib.Models.Repository
{
    public enum FilterType : byte
    {
        Equals = 1,
        NotEquals = 2,
        StartsWith = 3,
        EndsWith = 4,
        Contains = 5,
        Flags = 6,
        MultiEqual = 7,
    }
}
