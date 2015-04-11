using System;

namespace GuidGen.Formats
{
    [DisplayOrder(3)]
    public class CPlusPlusStaticConstStructFormatViewModel : FormatViewModel
    {
        private static string Formatter(Guid value, TextCase textCase)
        {
            return string.Format(@"// {0}
static const GUID <<name>> = 
{{ 0x{1}, 0x{2}, 0x{3}, {{ 0x{4}, 0x{5}, 0x{6}, 0x{7}, 0x{8}, 0x{9}, 0x{10}, 0x{11} }} }};",
                value.GetEnumeratedParts(textCase));
        }

        public CPlusPlusStaticConstStructFormatViewModel(IGeneratorOptions options)
            : base(options, "static const struct GUID = { ... }", Formatter)
        {
        }
    }
}
