public record SmsValidation(string Text)
{
    public bool IsLonger
        => TextLength > Consts.ThirdSmsLengthMax;

    public int SmsCount
        => TextLength switch
        {
            <= Consts.FirstSmsLengthMax => 1,
            <= Consts.SecondSmsLengthMax => 2,
            _ => 3
        };

    public int TextLength
        => (string.IsNullOrEmpty(Text)) ? 0
        : Text.Sum(c => Consts.CharactersWithTwoLength.Contains(c) ? 2 : 1);
}
