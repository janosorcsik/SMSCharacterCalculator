public static class SmsCharacterCalculator
{
    public static SmsValidation GetSmsValidation(string text, bool optimize = true)
    {
        if (optimize)
        {
            text = OptimizeText(text);
        }

        return new SmsValidation(text);
    }

    internal static string OptimizeText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var length = text.Length - 1;

        return string.Create(text.Length, 0, (buffer, _) =>
        {
            int i = length;
            while (i >= 0)
            {
                buffer[i] = OptimizeCharacter(text[i]);
                i--;
            }
        });
    }

    private static char OptimizeCharacter(char character)
        => Consts.UnicodeToGsm.TryGetValue(character, out char temp) ? temp : character;
}
