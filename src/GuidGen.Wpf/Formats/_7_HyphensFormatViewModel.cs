using System;

namespace GuidGen.Formats
{
    [DisplayOrder(8)]
    public class HyphensFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase textCase)
        {
            return value.ToString("D").ToTextCase(textCase);
        }

        public HyphensFormatViewModel(IGeneratorOptions options)
            : base(options, @"Hypens", Formatter)
        {
        }
    }
}
