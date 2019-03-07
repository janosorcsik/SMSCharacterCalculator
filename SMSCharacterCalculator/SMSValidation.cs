namespace SmsCharacterCalculator
{
    public class SmsValidation
    {
        private const int FirstSmsLengthMax = 160;

        private const int SecondSmsLengthMax = 306;

        private const int ThirdSmsLengthMax = 459;

        public bool IsLonger => TextLength > ThirdSmsLengthMax;

        public int TextLength { get; set; }

        public int SmsCount =>
            TextLength <= FirstSmsLengthMax ? 1
            : TextLength <= SecondSmsLengthMax ? 2
            : 3;

        public string OptimizedText { get; set; }
    }
}
