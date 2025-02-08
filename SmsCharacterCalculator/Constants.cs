namespace SmsCharacterCalculator;

internal static class Constants
{
    public const int FirstSmsLengthMax = 160;

    public const int SecondSmsLengthMax = 306;

    public const int ThirdSmsLengthMax = 459;

    public static readonly IReadOnlyDictionary<char, char> UnicodeToGsm = new Dictionary<char, char>
    {
        { 'á', 'à' },
        { 'í', 'ì' },
        { 'ó', 'ò' },
        { 'ú', 'ù' },
        { 'ő', 'ö' },
        { 'ű', 'ü' },
        { 'Á', 'Å' },
        { 'Í', 'I' },
        { 'Ú', 'U' },
        { 'Ó', 'O' },
        { 'Ő', 'Ö' },
        { 'Ű', 'Ü' }
    };

    public static readonly IReadOnlyCollection<char> CharactersWithTwoLength =
        Array.AsReadOnly(['|', '^', '€', '{', '}', '[', '~', ']', '\\']);
}
