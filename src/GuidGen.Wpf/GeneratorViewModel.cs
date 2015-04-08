using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GuidGen
{
    //TODO: SOLID principles dictate that this could probably be decoupled a bit further, data from view model, command patterns, and so on, but this will get the job done for now...
    public class GeneratorViewModel : ViewModelBase
    {
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
                RaisePropertyChanged(@"Formats");
                RaisePropertyChanged("SelectedResult");
            }
        }

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
                var selected = Formats.FirstOrDefault(x => x.IsSelected);
                //And preclude any inadvertent accelerator keys from appearing in the formatted result.
                return selected == null
                    ? string.Empty
                    : selected.Perform(Current, _textCase).Replace("_", "__");
            }
        }

        /// <summary>
        /// TextCase backing field.
        /// </summary>
        private TextCase _textCase;

        /// <summary>
        /// Gets the ResultHeader based on the <see cref="_textCase"/>.
        /// </summary>
        public string ResultHeader
        {
            get { return string.Format("Result ({0})", _textCase.GetResultHeaderText()); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="formats"></param>
        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public GeneratorViewModel(IEnumerable<FormatViewModel> formats)
        {
            New();

            _textCase = TextCase.Lower;
            _formats = formats.ToList();

            var number = 0;

            _formats.ToList()
                .ForEach(x =>
                {
                    x.Number = ++number;
                    x.PropertyChanged += (s, e) =>
                    {
                        if (!ReferenceEquals(s, x)) return;
                        RaisePropertyChanged("SelectedResult");
                    };
                });
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
            get { return string.Format("To _{0} Case", _textCase.GetNextTextCase()); }
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void ToggleCase()
        {
            _textCase = _textCase.GetNextTextCase();
            RaisePropertyChanged("CaseCommandContent");
            RaisePropertyChanged("ResultHeader");
            RaisePropertyChanged("SelectedResult");
        }

        public void Copy()
        {
            var selected = Formats.FirstOrDefault(x => x.IsSelected);
            if (selected == null) return;
            Clipboard.SetText(selected.Perform(Current, _textCase));
        }
    }
}
