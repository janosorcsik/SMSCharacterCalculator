namespace SmsCharacterCalculator;

public record SmsValidation(string? Text)
{
    public bool IsLonger
        => TextLength > Constants.ThirdSmsLengthMax;

    public int SmsCount
        => TextLength switch
        {
            <= Constants.FirstSmsLengthMax => 1,
            <= Constants.SecondSmsLengthMax => 2,
            _ => 3
        };

    public int TextLength
        => (string.IsNullOrEmpty(Text))
            ? 0
            : Text.Sum(c => Constants.CharactersWithTwoLength.Contains(c) ? 2 : 1);
}
