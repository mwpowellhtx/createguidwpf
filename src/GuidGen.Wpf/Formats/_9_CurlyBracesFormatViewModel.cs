using System;

namespace GuidGen.Formats
{
    [DisplayOrder(9)]
    public class CurlyBracesFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase textCase)
        {
            return value.ToString("B").ToTextCase(textCase);
        }

        public CurlyBracesFormatViewModel(IGeneratorOptions options)
            : base(options, @"Braces", Formatter)
        {
        }
    }
}
