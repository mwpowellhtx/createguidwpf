using System;

namespace GuidGen.Formats
{
    [DisplayOrder(6)]
    public class VisualBasicDotNetGuidAttributeFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase textCase)
            => $@"<Guid(""{$"{value:D}".ToTextCase(textCase)}"")>";

        public VisualBasicDotNetGuidAttributeFormatViewModel(IGeneratorOptions options)
            : base(options, @"<Guid(""xxxxxxxx-xxxx ... xxxx"")>", Formatter)
        {
        }
    }
}
