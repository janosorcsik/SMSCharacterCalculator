using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SMSCharacterCalculator.Test
{
    [TestClass]
    public class SMSCharacterCalculatorTest
    {
        [TestMethod]
        public void CalculateLength_With_Null()
        {
            string text = null;

            int length = SMSCharacterCalculator.CalculateLength(text);

            Assert.IsTrue(length == 0);
        }

        [TestMethod]
        public void CalculateLength_With_Empty()
        {
            string text = string.Empty;

            int length = SMSCharacterCalculator.CalculateLength(text);

            Assert.IsTrue(length == 0);
        }

        [TestMethod]
        public void CalculateLength_With_One_Character()
        {
            string text = "a";

            int length = SMSCharacterCalculator.CalculateLength(text);

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
                int length = SMSCharacterCalculator.CalculateLength(s);

                Assert.IsTrue(length == 2);
            }
        }

        [TestMethod]
        public void OptimizeText_With_Null()
        {
            string text = null;

            string actual = SMSCharacterCalculator.OptimizeText(text);

            Assert.IsTrue(text == actual);
        }

        [TestMethod]
        public void OptimizeText_With_Empty()
        {
            string text = string.Empty;

            string actual = SMSCharacterCalculator.OptimizeText(text);

            Assert.IsTrue(text == actual);
        }

        [TestMethod]
        public void OptimizeText_With_One_Character()
        {
            string text = "a";

            string actual = SMSCharacterCalculator.OptimizeText(text);

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
                string actual = SMSCharacterCalculator.OptimizeText(unicodeCharacters[i]);

                Assert.IsTrue(actual == gsmCharacters[i]);
            }
        }

        [TestMethod]
        public void GetSMSValidation_With_One_SMS_With_Unicode_AND_Special_Character()
        {
            string text = "{Í}";
            string expected = "{I}";

            SMSValidation actual = SMSCharacterCalculator.GetSMSValidation(text);

            Assert.IsFalse(actual.IsLonger);
            Assert.IsTrue(actual.TextLength == 5);
            Assert.IsTrue(actual.SMSCount == 1);
            Assert.IsTrue(actual.OptimizedText == expected);
        }

        [TestMethod]
        public void GetSMSValidation_With_Two_SMS()
        {
            string text =
                "{abvgfhtrururufhdfjhdjfhjdjfhjdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}";

            SMSValidation actual = SMSCharacterCalculator.GetSMSValidation(text);

            Assert.IsFalse(actual.IsLonger);
            Assert.IsTrue(actual.TextLength == 164);
            Assert.IsTrue(actual.SMSCount == 2);
            Assert.IsTrue(actual.OptimizedText == text);
        }

        [TestMethod]
        public void GetSMSValidation_With_Three_SMS()
        {
            string text =
                "{abvgfhtrururufhdfjhdjhfghfggfhfhiiiourzoruozuiorutiozuouuzuiortuziouriozuriozuiourzirtuziuituziouriotuzurzuriotuziutruziorioutruziutiuriturtzotioziortuzioiozutzutruzfhjdjfhjdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}";

            SMSValidation actual = SMSCharacterCalculator.GetSMSValidation(text);

            Assert.IsFalse(actual.IsLonger);
            Assert.IsTrue(actual.TextLength == 308);
            Assert.IsTrue(actual.SMSCount == 3);
            Assert.IsTrue(actual.OptimizedText == text);
        }

        [TestMethod]
        public void GetSMSValidation_With_Long_SMS()
        {
            string text =
                "{abvgfhtrururufhdfjhdjhfghfggfhfhiiiourzoruozuiorutiozuouuzuiortuziouriozuriozuiourzirtuziuituziouriotuzurzuriotuziutruziorioutruziutiuriturtzotioziortuzioiozutzutruzfhjdjfhhjhjjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhhjhjjhjjjjhjhjhjhhjhhhhuuuuuuuuuuuuuuuuuuuujdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}";

            SMSValidation actual = SMSCharacterCalculator.GetSMSValidation(text);

            Assert.IsTrue(actual.IsLonger);
            Assert.IsTrue(actual.TextLength == 461);
            Assert.IsTrue(actual.SMSCount == 3);
            Assert.IsTrue(actual.OptimizedText == text);
        }
    }
}
