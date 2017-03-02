using System;

namespace GuidGen.Formats
{
    [DisplayOrder(11)]
    public class CSharpAssemblyGuidAttributeFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase textCase)
            => $@"[assembly: Guid(""{$"{value:D}".ToTextCase(textCase)}"")]";

        public CSharpAssemblyGuidAttributeFormatViewModel(IGeneratorOptions options)
            : base(options, @"[assembly: Guid(""xxxxxxxx-xxxx ... xxxx"")]", Formatter)
        {
        }
    }
}
