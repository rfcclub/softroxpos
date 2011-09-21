namespace AppFrame.Utilities
{
    public static class StringStaticUtility
    {
        public static bool SafeEquals(this string source, string destination)
        {
            if (string.IsNullOrEmpty(source)) return false;
            return source.Equals(destination);
        }
    }
}
