using System;

namespace GuidGen.Formats
{
    [DisplayOrder(5)]
    public class CSharpGuidAttributeFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase textCase)
        {
            return string.Format(@"[Guid(""{0}"")]", value.ToString("D").ToTextCase(textCase));
        }

        public CSharpGuidAttributeFormatViewModel(IGeneratorOptions options)
            : base(options, @"[Guid(""xxxxxxxx-xxxx ... xxxx"")]", Formatter)
        {
        }
    }
}
