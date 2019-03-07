using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmsCharacterCalculator.Test
{
    [TestClass]
    public class SmsCharacterCalculatorTest
    {
        [TestMethod]
        public void CalculateLength_With_Null()
        {
            int length = SmsCharacterCalculator.CalculateLength(null);

            Assert.IsTrue(length == 0);
        }

        [TestMethod]
        public void CalculateLength_With_Empty()
        {
            string text = string.Empty;

            int length = SmsCharacterCalculator.CalculateLength(text);

            Assert.IsTrue(length == 0);
        }

        [TestMethod]
        public void CalculateLength_With_One_Character()
        {
            const string text = "a";

            int length = SmsCharacterCalculator.CalculateLength(text);

            Assert.IsTrue(length == 1);
        }

        [TestMethod]
        public void CalculateLength_With_Special_Character()
        {
            string[] specialCharacters =
            {
                "|", "^", "€", "{", "}", "[", "~", "]", "\\"
            };

            foreach (string s in specialCharacters)
            {
                int length = SmsCharacterCalculator.CalculateLength(s);

                Assert.IsTrue(length == 2);
            }
        }

        [TestMethod]
        public void OptimizeText_With_Null()
        {
            string actual = SmsCharacterCalculator.OptimizeText(null);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void OptimizeText_With_Empty()
        {
            string text = string.Empty;

            string actual = SmsCharacterCalculator.OptimizeText(text);

            Assert.IsTrue(text == actual);
        }

        [TestMethod]
        public void OptimizeText_With_One_Character()
        {
            const string text = "a";

            string actual = SmsCharacterCalculator.OptimizeText(text);

            Assert.IsTrue(text == actual);
        }

        [TestMethod]
        public void OptimizeText_With_Unicode_Characters()
        {
            string[] unicodeCharacters =
            {
                "á",
                "í",
                "ó",
                "ú",
                "ő",
                "ű",
                "Á",
                "Í",
                "Ú",
                "Ó",
                "Ő",
                "Ű"
            };

            string[] gsmCharacters =
            {
                "à",
                "ì",
                "ò",
                "ù",
                "ö",
                "ü",
                "Å",
                "I",
                "U",
                "O",
                "Ö",
                "Ü"
            };

            for (int i = 0; i < unicodeCharacters.Length; i++)
            {
                string actual = SmsCharacterCalculator.OptimizeText(unicodeCharacters[i]);

                Assert.IsTrue(actual == gsmCharacters[i]);
            }
        }

        [TestMethod]
        public void GetSMSValidation_With_One_SMS_With_Unicode_AND_Special_Character()
        {
            const string text = "{Í}";
            const string expected = "{I}";

            SmsValidation actual = SmsCharacterCalculator.GetSmsValidation(text);

            Assert.IsFalse(actual.IsLonger);
            Assert.IsTrue(actual.TextLength == 5);
            Assert.IsTrue(actual.SmsCount == 1);
            Assert.IsTrue(actual.OptimizedText == expected);
        }

        [TestMethod]
        public void GetSMSValidation_With_Two_SMS()
        {
            const string text =
                "{abvgfhtrururufhdfjhdjfhjdjfhjdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}";

            SmsValidation actual = SmsCharacterCalculator.GetSmsValidation(text);

            Assert.IsFalse(actual.IsLonger);
            Assert.IsTrue(actual.TextLength == 164);
            Assert.IsTrue(actual.SmsCount == 2);
            Assert.IsTrue(actual.OptimizedText == text);
        }

        [TestMethod]
        public void GetSMSValidation_With_Three_SMS()
        {
            const string text =
                "{abvgfhtrururufhdfjhdjhfghfggfhfhiiiourzoruozuiorutiozuouuzuiortuziouriozuriozuiourzirtuziuituziouriotuzurzuriotuziutruziorioutruziutiuriturtzotioziortuzioiozutzutruzfhjdjfhjdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}";

            SmsValidation actual = SmsCharacterCalculator.GetSmsValidation(text);

            Assert.IsFalse(actual.IsLonger);
            Assert.IsTrue(actual.TextLength == 308);
            Assert.IsTrue(actual.SmsCount == 3);
            Assert.IsTrue(actual.OptimizedText == text);
        }

        [TestMethod]
        public void GetSMSValidation_With_Long_SMS()
        {
            const string text =
                "{abvgfhtrururufhdfjhdjhfghfggfhfhiiiourzoruozuiorutiozuouuzuiortuziouriozuriozuiourzirtuziuituziouriotuzurzuriotuziutruziorioutruziutiuriturtzotioziortuzioiozutzutruzfhjdjfhhjhjjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhhjhjjhjjjjhjhjhjhhjhhhhuuuuuuuuuuuuuuuuuuuujdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}";

            SmsValidation actual = SmsCharacterCalculator.GetSmsValidation(text);

            Assert.IsTrue(actual.IsLonger);
            Assert.IsTrue(actual.TextLength == 461);
            Assert.IsTrue(actual.SmsCount == 3);
            Assert.IsTrue(actual.OptimizedText == text);
        }
    }
}
