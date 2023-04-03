namespace Custom.Lib.Util
{
    public static class StringExtension
    {
        public static string FirstToUpper(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input.Trim();

            return $"{input[0].ToString().ToUpper()}{string.Join("", input.Skip(1))}";
        }
    }
}
