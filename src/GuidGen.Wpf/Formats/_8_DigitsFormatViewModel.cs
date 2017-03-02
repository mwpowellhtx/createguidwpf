using System;

namespace GuidGen.Formats
{
    [DisplayOrder(7)]
    public class DigitsFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase testCase)
            => $"{value:N}".ToTextCase(testCase);

        public DigitsFormatViewModel(IGeneratorOptions options)
            : base(options, "Digits", Formatter)
        {
        }
    }
}
