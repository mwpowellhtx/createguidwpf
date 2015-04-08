using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CreateGuid
{
    //TODO: 'TextCase' could well be captured in an 'options' class
    public enum TextCase
    {
        Upper,
        Lower,
    }

    internal static class CaseExtensionMethods
    {
        private readonly static IDictionary<TextCase, Func<string, string>> TextCaseHandlers;

        private static readonly IDictionary<TextCase, string> TextCaseHeaders;

        private static readonly IDictionary<TextCase, TextCase> NextTextCase;

        static CaseExtensionMethods()
        {
            TextCaseHandlers = new Dictionary<TextCase, Func<string, string>>
            {
                {TextCase.Lower, x => x.ToLower()},
                {TextCase.Upper, x => x.ToUpper()}
            };

            TextCaseHeaders = new Dictionary<TextCase, string>
            {
                {TextCase.Lower, "Lower Case"},
                {TextCase.Upper, "Upper Case"},
            };

            NextTextCase = new Dictionary<TextCase, TextCase>
            {
                {TextCase.Lower, TextCase.Upper},
                {TextCase.Upper, TextCase.Lower},
            };
        }

        internal static string ToTextCase(this string text, TextCase textCase)
        {
            Debug.Assert(TextCaseHandlers.ContainsKey(textCase));
            var handler = TextCaseHandlers[textCase];
            return handler(text);
        }

        internal static string GetResultHeaderText(this TextCase textCase)
        {
            Debug.Assert(TextCaseHeaders.ContainsKey(textCase));
            return TextCaseHeaders[textCase];
        }

        internal static TextCase GetNextTextCase(this TextCase textCase)
        {
            Debug.Assert(NextTextCase.ContainsKey(textCase));
            return NextTextCase[textCase];
        }
    }
}
