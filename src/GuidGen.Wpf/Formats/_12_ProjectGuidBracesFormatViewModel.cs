using System;

namespace GuidGen.Formats
{
    // ReSharper disable once UnusedMember.Global
    [DisplayOrder(12)]
    public class ProjectGuidBracesFormatViewModel : FormatViewModel
    {
        private const string XmlTag = "ProjectGuid";

        private static string GetXmlWithContent(string tag, string content)
            => $"<{tag}>{content}</{tag}>";

        private static string Formatter(Guid value, TextCase textCase)
            => GetXmlWithContent(XmlTag, $@"{{{$"{value:D}".ToTextCase(textCase)}}}");

        // ReSharper disable once UnusedMember.Global
        public ProjectGuidBracesFormatViewModel(IGeneratorOptions options)
            : base(options, GetXmlWithContent(XmlTag, $"{{xxxxxxxx-xxxx ... xxxx}}"), Formatter)
        {
        }
    }
}
