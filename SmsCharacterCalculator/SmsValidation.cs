namespace SmsCharacterCalculator
{
    public class SmsValidation
    {
        public bool IsLonger => TextLength > Consts.ThirdSmsLengthMax;

        public int TextLength { get; set; }

        public int SmsCount =>
            TextLength <= Consts.FirstSmsLengthMax ? 1
            : TextLength <= Consts.SecondSmsLengthMax ? 2
            : 3;

        public string OptimizedText { get; set; }
    }
}
