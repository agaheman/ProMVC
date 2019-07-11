namespace ProMVC.WebFramework
{
    public static class YeKe
    {
        public const char ArabickYeWithOneDotBelow = (char)1568;
        public const char ArabickYeWithHighHamze = (char)1574;
        public const char ArabickYeWithInvertedV = (char)1597;
        public const char ArabickYeWithTowDotsAbove = (char)1598;
        public const char ArabickYeWithThreeDotsAbove = (char)1599;
        public const char ArabickYeWithTowDotsBelow = (char)1610;
        public const char ArabickYeWithHighHamzeYeh = (char)1656;
        public const char ArabickYeWithFinalFrom = (char)1744;
        public const char ArabickYeWithThreeDotsBelow = (char)1745;
        public const char ArabickYeWithTail = (char)1741;
        public const char ArabickYeSmallV = (char)1742;

        public const char PersianYeChar = (char)1740;

        public const char ArabickKeChar = (char)1603;
        public const char PersianKeChar = (char)1705;

        public static string ApplyPersianYeKe(this string data)
        {
            return data == null
                ? null
                : (string.IsNullOrWhiteSpace(data)
                    ? string.Empty
                    : data.Replace(ArabickYeWithOneDotBelow, PersianYeChar)
                        .Replace(ArabickYeWithHighHamze, PersianYeChar)
                        .Replace(ArabickYeWithInvertedV, PersianYeChar)
                        .Replace(ArabickYeWithTowDotsAbove, PersianYeChar)
                        .Replace(ArabickYeWithThreeDotsAbove, PersianYeChar)
                        .Replace(ArabickYeWithTowDotsBelow, PersianYeChar)
                        .Replace(ArabickYeWithHighHamzeYeh, PersianYeChar)
                        .Replace(ArabickYeWithFinalFrom, PersianYeChar)
                        .Replace(ArabickYeWithThreeDotsBelow, PersianYeChar)
                        .Replace(ArabickYeWithTail, PersianYeChar)
                        .Replace(ArabickYeSmallV, PersianYeChar)

                        .Replace(ArabickKeChar, PersianKeChar).Trim());
        }

        public static string ApplyArabickYeKe(this string data)
        {
            return data == null
                ? null
                : (string.IsNullOrWhiteSpace(data)
                    ? string.Empty
                    : data.Replace(PersianYeChar, ArabickYeWithOneDotBelow).Replace(PersianKeChar, ArabickKeChar).Trim());
        }
    }

}
