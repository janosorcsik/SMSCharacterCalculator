using Xunit;

public class SmsCharacterCalculatorTests
{
    [Theory]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData("a", 1)]
    [InlineData("|", 2)]
    [InlineData("^", 2)]
    [InlineData("€", 2)]
    [InlineData("{", 2)]
    [InlineData("}", 2)]
    [InlineData("[", 2)]
    [InlineData("~", 2)]
    [InlineData("]", 2)]
    [InlineData(@"\", 2)]
    public void CalculateLength(string text, int expectedLength)
    {
        int length = new SmsValidation(text).TextLength;

        Assert.Equal(expectedLength, length);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("á", "à")]
    [InlineData("í", "ì")]
    [InlineData("ó", "ò")]
    [InlineData("ú", "ù")]
    [InlineData("ő", "ö")]
    [InlineData("ű", "ü")]
    [InlineData("Á", "Å")]
    [InlineData("Í", "I")]
    [InlineData("Ú", "U")]
    [InlineData("Ó", "O")]
    [InlineData("Ő", "Ö")]
    [InlineData("Ű", "Ü")]
    public void OptimizeText(string text, string expectedOptimizedText)
    {
        string optimizedText = SmsCharacterCalculator.OptimizeText(text);

        Assert.Equal(expectedOptimizedText, optimizedText);
    }

    [Theory]
    [InlineData("{Í}", false, 5, 1, "{I}")]
    [InlineData("{abvgfhtrururufhdfjhdjfhjdjfhjdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}", false, 164, 2, "{abvgfhtrururufhdfjhdjfhjdjfhjdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}")]
    [InlineData("{abvgfhtrururufhdfjhdjhfghfggfhfhiiiourzoruozuiorutiozuouuzuiortuziouriozuriozuiourzirtuziuituziouriotuzurzuriotuziutruziorioutruziutiuriturtzotioziortuzioiozutzutruzfhjdjfhjdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}", false, 308, 3, "{abvgfhtrururufhdfjhdjhfghfggfhfhiiiourzoruozuiorutiozuouuzuiortuziouriozuriozuiourzirtuziuituziouriotuzurzuriotuziutruziorioutruziutiuriturtzotioziortuzioiozutzutruzfhjdjfhjdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}")]
    [InlineData("{abvgfhtrururufhdfjhdjhfghfggfhfhiiiourzoruozuiorutiozuouuzuiortuziouriozuriozuiourzirtuziuituziouriotuzurzuriotuziutruziorioutruziutiuriturtzotioziortuzioiozutzutruzfhjdjfhhjhjjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhhjhjjhjjjjhjhjhjhhjhhhhuuuuuuuuuuuuuuuuuuuujdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}", true, 461, 3, "{abvgfhtrururufhdfjhdjhfghfggfhfhiiiourzoruozuiorutiozuouuzuiortuziouriozuriozuiourzirtuziuituziouriotuzurzuriotuziutruziorioutruziutiuriturtzotioziortuzioiozutzutruzfhjdjfhhjhjjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhjhhjhjjhjjjjhjhjhjhhjhhhhuuuuuuuuuuuuuuuuuuuujdfhjkfhjksuizueuruiewrziwuezrizweiurzwuiezruiweuirweuizruiwezruizweuirwzeurzweuiriuwzeuirrwezruizweuiriuwzruizwiuezriuzweuirziuzzzz}")]
    public void GetSmsValidation(string text, bool expectedIsLonger, int expectedTextLength, int expectedSmsCount, string expectedOptimizedText)
    {
        SmsValidation actual = SmsCharacterCalculator.GetSmsValidation(text);

        Assert.Equal(expectedIsLonger, actual.IsLonger);
        Assert.Equal(expectedTextLength, actual.TextLength);
        Assert.Equal(expectedSmsCount, actual.SmsCount);
        Assert.Equal(expectedOptimizedText, actual.Text);
    }
}
