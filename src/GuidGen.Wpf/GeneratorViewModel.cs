using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GuidGen
{
    //TODO: SOLID principles dictate that this could probably be decoupled a bit further, data from view model, command patterns, and so on, but this will get the job done for now...
    public class GeneratorViewModel : ViewModelBase, IGeneratorOptions
    {
        #region GeneratorOptions Members

        //TODO: arguably, if we really wanted to, Current should be captured in a separate class, provider, or even 'options', per se, just apart from the view model...
        private Guid _current;

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public Guid Current
        {
            get { return _current; }
            private set
            {
                if (_current == value) return;
                _current = value;
                RaisePropertyChanged();
                RaisePropertyChanged("Formats");
                RaisePropertyChanged("SelectedResult");
            }
        }

        /// <summary>
        /// Case backing field.
        /// </summary>
        private TextCase _textCase;

        /// <summary>
        /// Gets the Case.
        /// </summary>
        public TextCase Case
        {
            get { return _textCase; }
            private set
            {
                if (value == _textCase) return;
                _textCase = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// SelectedFormat backing field.
        /// </summary>
        private FormatViewModel _selectedFormat;

        /// <summary>
        /// Gets or sets the SelectedFormat
        /// </summary>
        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public FormatViewModel SelectedFormat
        {
            get { return _selectedFormat; }
            set
            {
                if (ReferenceEquals(value, _selectedFormat)) return;
                _selectedFormat = value;
                RaisePropertyChanged();
                RaisePropertyChanged("SelectedResult");
            }
        }

        #endregion

        /// <summary>
        /// Formats backing field.
        /// </summary>
        private readonly IEnumerable<FormatViewModel> _formats;

        /// <summary>
        /// Gets the Formats for use by the generator. This is the rest of the view model.
        /// </summary>
        public IEnumerable<FormatViewModel> Formats
        {
            get { return _formats; }
        }

        /// <summary>
        /// Gets the SelectedResult for display purposes.
        /// This illustrates the power and flexibility of a loosely coupled MVVM.
        /// </summary>
        public string SelectedResult
        {
            get
            {
                /* And preclude any inadvertent accelerator keys from appearing in the formatted result.
                 * As well as any need whatsoever to call methods on the view model. */
                return SelectedFormat == null ? string.Empty : SelectedFormat.FormattedText.Replace("_", "__");
            }
        }

        /// <summary>
        /// Gets the ResultHeader based on the <see cref="Case"/>.
        /// </summary>
        public string ResultHeader
        {
            get { return string.Format("Result ({0})", Case.GetResultHeaderText()); }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public GeneratorViewModel()
        {
            New();

            Case = TextCase.Lower;

            //No need to evaluate the listed formats twice.
            _formats = GetFormats(this).ToList();

            var number = 0;

            //The good 'old' foreach loop still has its place.
            foreach (var f in _formats)
            {
                f.Number = ++number;
                //TODO: TBD: could potentially get fancier with the PC event, but does not seem to be necessary
                f.PropertyChanged += (s, e) => RaisePropertyChanged("SelectedResult");
            }
        }


        private ICommand _newCommand;

        public ICommand NewCommand
        {
            get
            {
                return _newCommand
                       ?? (_newCommand = new DelegateCommand(x => New(), x => true));
            }
        }

        private ICommand _copyCommand;

        public ICommand CopyCommand
        {
            get
            {
                return _copyCommand
                       ?? (_copyCommand = new DelegateCommand(x => Copy(), x => true));
            }
        }

        private ICommand _caseCommand;

        public ICommand CaseCommand
        {
            get
            {
                return _caseCommand
                       ?? (_caseCommand = new DelegateCommand(x => ToggleCase(), x => true));
            }
        }

        public void New()
        {
            Current = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the CaseCommandContent content.
        /// </summary>
        public string CaseCommandContent
        {
            get { return string.Format("To _{0} Case", Case.GetNextTextCase()); }
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void ToggleCase()
        {
            Case = Case.GetNextTextCase();
            RaisePropertyChanged("CaseCommandContent");
            RaisePropertyChanged("ResultHeader");
            RaisePropertyChanged("SelectedResult");
        }

        public void Copy()
        {
            //TODO: which would then also potentially be allowing the view model to present itself for copy to the clipboard without needing to call any methods
            if (SelectedFormat == null) return;
            Clipboard.SetText(SelectedFormat.FormattedText);
        }

        private static IEnumerable<FormatViewModel> GetFormats(GeneratorViewModel generatorViewModel)
        {
            var __this = generatorViewModel;

            //TODO: TBD: from here's it's not far to drop it in as a VERY loosely coupled resource in App, MainWindow, etc

            yield return new FormatViewModel(__this, "IMPLEMENT_OLECREATE(...)",
                (x, c) =>
                    string.Format(@"// {0}
IMPLEMENT_OLECREATE(<<class>>, <<external_name>>, 
0x{1}, 0x{2}, 0x{3}, 0x{4}, 0x{5}, 0x{6}, 0x{7}, 0x{8}, 0x{9}, 0x{10}, 0x{11});",
                        x.GetEnumeratedParts(c)));

            yield return new FormatViewModel(__this, "DEFINE_GUID(...)",
                (x, c) =>
                    string.Format(@"// {0}
DEFINE_GUID(<<name>>, 
0x{1}, 0x{2}, 0x{3}, 0x{4}, 0x{5}, 0x{6}, 0x{7}, 0x{8}, 0x{9}, 0x{10}, 0x{11});",
                        x.GetEnumeratedParts(c)));

            yield return new FormatViewModel(__this, "static const struct GUID = { ... }",
                (x, c) =>
                    string.Format(@"// {0}
static const GUID <<name>> = 
{{ 0x{1}, 0x{2}, 0x{3}, {{ 0x{4}, 0x{5}, 0x{6}, 0x{7}, 0x{8}, 0x{9}, 0x{10}, 0x{11} }} }};",
                        x.GetEnumeratedParts(c)));

            yield return new FormatViewModel(__this, @"Registry Format (ie. {{xxxxxxxx-xxxx ... xxxx }})",
                (x, c) => x.ToString("B").ToTextCase(c));

            yield return new FormatViewModel(__this, @"[Guid(""xxxxxxxx-xxxx ... xxxx"")]",
                (x, c) => string.Format(@"[Guid(""{0}"")]", x.ToString("D").ToTextCase(c)));

            yield return new FormatViewModel(__this, @"<Guid(""xxxxxxxx-xxxx ... xxxx"")>",
                (x, c) => string.Format(@"<Guid(""{0}"")>", x.ToString("D").ToTextCase(c)));

            yield return new FormatViewModel(__this, @"Digits", (x, c) => x.ToString("N").ToTextCase(c));

            yield return new FormatViewModel(__this, @"Hypens", (x, c) => x.ToString("D").ToTextCase(c));

            yield return new FormatViewModel(__this, @"Braces", (x, c) => x.ToString("B").ToTextCase(c));

            yield return new FormatViewModel(__this, @"Parentheses", (x, c) => x.ToString("P").ToTextCase(c));
        }
    }
}
