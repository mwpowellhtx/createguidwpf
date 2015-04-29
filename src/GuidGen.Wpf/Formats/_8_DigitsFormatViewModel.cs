using System;

namespace GuidGen.Formats
{
    [DisplayOrder(8)]
    public class DigitsFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase testCase)
        {
            return value.ToString("N").ToTextCase(testCase);
        }

        public DigitsFormatViewModel(IGeneratorOptions options)
            : base(options, "Digits", Formatter)
        {
        }
    }
}
