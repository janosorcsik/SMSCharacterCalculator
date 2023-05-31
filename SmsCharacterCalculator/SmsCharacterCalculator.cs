using System;
using System.Linq;

namespace SmsCharacterCalculator
{
    public static class SmsCharacterCalculator
    {
        public static SmsValidation GetSmsValidation(string text, bool optimize = true)
        {
            if (optimize)
            {
                text = OptimizeText(text);
            }

            return new SmsValidation
            {
                OptimizedText = text,
                TextLength = CalculateLength(text)
            };
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
                length += Consts.CharactersWithTwoLength.Contains(text[i]) ? 2 : 1;
            }

            return length;
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
                for (int i = length; i >= 0; i--)
                {
                    buffer[i] = OptimizeCharacter(text[i]);
                }
            });
        }

        private static char OptimizeCharacter(char character)
        {
            return Consts.UnicodeToGsm.TryGetValue(character, out char temp) ? temp : character;
        }
    }
}
