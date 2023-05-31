using System;
using System.Collections.Generic;
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
                length += CharactersWithTwoLength.Contains(text[i]) ? 2 : 1;
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
            return UnicodeToGsm.TryGetValue(character, out char temp) ? temp : character;
        }

        private static readonly IReadOnlyDictionary<char, char> UnicodeToGsm = new Dictionary<char, char>
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

        private static readonly IReadOnlyCollection<char> CharactersWithTwoLength =
            Array.AsReadOnly(new[] { '|', '^', '€', '{', '}', '[', '~', ']', '\\' });
    }
}
