namespace NotisissimusSimpleSearch.Extension
{
    public static class RandomDataGenerator
    {
        private static readonly Random random = new Random();

        public static string GenerateRandomString(int length)
        {
            const string chars = "абвгд жзиклм нопрстивхчщьюя 0123456 789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
