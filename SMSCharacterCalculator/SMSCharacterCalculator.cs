using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("SMSCharacterCalculator.Test")]
namespace SMSCharacterCalculator
{
    public static class SMSCharacterCalculator
    {
        public static SMSValidation GetSMSValidation(string text, bool optimize = true)
        {
            SMSValidation smsValidation = new SMSValidation();

            if (optimize)
            {
                text = OptimizeText(text);
            }

            smsValidation.OptimizedText = text;

            smsValidation.TextLength = CalculateLength(text);

            const int firstSMSLengthMax = 160;
            const int secondSMSLengthMax = 306;
            const int thirdSMSLengthMax = 459;

            smsValidation.IsLonger = smsValidation.TextLength > thirdSMSLengthMax;

            if (smsValidation.TextLength <= firstSMSLengthMax)
            {
                smsValidation.SMSCount = 1;
            }
            else
            {
                if (smsValidation.TextLength <= secondSMSLengthMax)
                {
                    smsValidation.SMSCount = 2;
                }
                else
                {
                    smsValidation.SMSCount = 3;
                }
            }

            return smsValidation;
        }

        internal static int CalculateLength(string text)
        {
            int length = 0;

            if (string.IsNullOrEmpty(text))
            {
                return length;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (CharactersWithTwoLength.Contains(text[i]))
                {
                    length += 2;
                }
                else
                {
                    length++;
                }
            }

            return length;
        }

        internal static string OptimizeText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            StringBuilder sb = new StringBuilder(text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                sb.Append(OptimizeCharacter(text[i]));
            }

            return sb.ToString();
        }

        private static char OptimizeCharacter(char character)
        {
            if (!UnicodeToGSM.TryGetValue(character, out char temp))
            {
                temp = character;
            }

            return temp;
        }

        private static readonly Dictionary<char, char> UnicodeToGSM = new Dictionary<char, char>
        {
            {'á', 'à'},
            {'í', 'ì'},
            {'ó', 'ò'},
            {'ú', 'ù'},
            {'ő', 'ö'},
            {'ű', 'ü'},
            {'Á', 'Å'},
            {'Í', 'I'},
            {'Ú', 'U'},
            {'Ó', 'O'},
            {'Ő', 'Ö'},
            {'Ű', 'Ü'}
        };

        private static readonly char[] CharactersWithTwoLength = {'|', '^', '€', '{', '}', '[', '~', ']', '\\'};
    }
}
