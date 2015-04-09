using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GuidGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICommand _exitCommand;

        public ICommand ExitCommand
        {
            get { return _exitCommand ?? (_exitCommand = new DelegateCommand(x => Close(), x => true)); }
        }

        public MainWindow()
        {
            InitializeComponent();

            //TODO: may store and retrieve the options settings from a registry value ...
            var options = new GeneratorViewModel(
                new[]
                {
                    new FormatViewModel("IMPLEMENT_OLECREATE(...)",
                        (x, c) =>
                            string.Format(@"// {0}
IMPLEMENT_OLECREATE(<<class>>, <<external_name>>, 
0x{1}, 0x{2}, 0x{3}, 0x{4}, 0x{5}, 0x{6}, 0x{7}, 0x{8}, 0x{9}, 0x{10}, 0x{11});",
                                x.GetEnumeratedParts(c))),
                    new FormatViewModel("DEFINE_GUID(...)",
                        (x, c) =>
                            string.Format(@"// {0}
DEFINE_GUID(<<name>>, 
0x{1}, 0x{2}, 0x{3}, 0x{4}, 0x{5}, 0x{6}, 0x{7}, 0x{8}, 0x{9}, 0x{10}, 0x{11});",
                                x.GetEnumeratedParts(c))),
                    new FormatViewModel("static const struct GUID = { ... }",
                        (x, c) =>
                            string.Format(@"// {0}
static const GUID <<name>> = 
{{ 0x{1}, 0x{2}, 0x{3}, {{ 0x{4}, 0x{5}, 0x{6}, 0x{7}, 0x{8}, 0x{9}, 0x{10}, 0x{11} }} }};",
                                x.GetEnumeratedParts(c))),
                    new FormatViewModel(@"Registry Format (ie. {{xxxxxxxx-xxxx ... xxxx }})",
                        (x, c) => x.ToString("B").ToTextCase(c)),
                    new FormatViewModel(@"[Guid(""xxxxxxxx-xxxx ... xxxx"")]",
                        (x, c) => string.Format(@"[Guid(""{0}"")]", x.ToString("D").ToUpper()).ToTextCase(c)),
                    new FormatViewModel(@"<Guid(""xxxxxxxx-xxxx ... xxxx"")>",
                        (x, c) => string.Format(@"<Guid(""{0}"")>", x.ToString("D").ToUpper()).ToTextCase(c)),
                    new FormatViewModel(@"Digits", (x, c) => x.ToString("N").ToTextCase(c)),
                    new FormatViewModel(@"Hypens", (x, c) => x.ToString("D").ToTextCase(c)),
                    new FormatViewModel(@"Braces", (x, c) => x.ToString("B").ToTextCase(c)),
                    new FormatViewModel(@"Parentheses", (x, c) => x.ToString("P").ToTextCase(c))
                });

            DataContext = options;
        }
    }

    internal static class GuidExtensionMethods
    {
        internal static object[] GetEnumeratedParts(this Guid aGuid, TextCase testCase)
        {
            var bytes = aGuid.ToByteArray();
            //By virtue of the fact we are working with Guid this should be the case but check anyway.
            Debug.Assert(bytes.Length == 16);
            return new object[]
            {
                aGuid.ToString("D").ToTextCase(testCase)
                , bytes.Take(4).ToHexString().ToTextCase(testCase)
                , bytes.Skip(4).Take(2).ToHexString().ToTextCase(testCase)
                , bytes.Skip(6).Take(2).ToHexString().ToTextCase(testCase)
                , bytes.Skip(8).Take(1).ToHexString().ToTextCase(testCase)
                , bytes.Skip(9).Take(1).ToHexString().ToTextCase(testCase)
                , bytes.Skip(10).Take(1).ToHexString().ToTextCase(testCase)
                , bytes.Skip(11).Take(1).ToHexString().ToTextCase(testCase)
                , bytes.Skip(12).Take(1).ToHexString().ToTextCase(testCase)
                , bytes.Skip(13).Take(1).ToHexString().ToTextCase(testCase)
                , bytes.Skip(14).Take(1).ToHexString().ToTextCase(testCase)
                , bytes.Skip(15).ToHexString().ToTextCase(testCase)
            };
        }
    }
}
