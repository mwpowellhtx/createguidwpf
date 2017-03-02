using System;

namespace GuidGen.Formats
{
    [DisplayOrder(4)]
    public class RegistryFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase textCase)
            => $"{value:B}".ToTextCase(textCase);

        public RegistryFormatViewModel(IGeneratorOptions options)
            : base(options, @"Registry Format (ie. {{xxxxxxxx-xxxx ... xxxx }})", Formatter)
        {
        }
    }
}
