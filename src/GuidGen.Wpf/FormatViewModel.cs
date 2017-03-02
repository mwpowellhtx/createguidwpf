using System;

namespace GuidGen
{
    //TODO: format view model could be more of a 'command pattern' extension than a true 'view model' per se...
    public abstract class FormatViewModel : ViewModelBase, IFormat
    {
        /// <summary>
        /// IsSelected backing field.
        /// </summary>
        private bool _selected;

        /// <summary>
        /// Gets or sets whether IsSelected.
        /// </summary>
        public bool IsSelected
        {
            get { return _selected; }
            set
            {
                if (_selected == value) return;

                _selected = value;

                if (_selected)
                    _options.SelectedFormat = this;

                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Number of the format for use with the rest of the view model.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The base <see cref="DisplayName"/>.
        /// </summary>
        private readonly string _baseDisplayName;

        /// <summary>
        /// Gets the DisplayName for use with the rest of the view model.
        /// </summary>
        /// <remarks>Remember to format with gesture keys equipped and ready for action.</remarks>
        public string DisplayName => $"_{Number}: {_baseDisplayName}";

        /// <summary>
        /// Options backing field.
        /// </summary>
        private readonly IGeneratorOptions _options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="baseDisplayName"></param>
        /// <param name="formatter"></param>
        public FormatViewModel(IGeneratorOptions options, string baseDisplayName, Func<Guid, TextCase, string> formatter)
        {
            _baseDisplayName = baseDisplayName;
            _formatter = formatter;

            _options = options;

            //Starting out from the nominal default.
            FormattedText = _formatter(options.Current, options.Case);

            const StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;

            _options.PropertyChanged += (s, e) =>
            {
                var propertyName = e.PropertyName;

                if (string.Equals(propertyName, "Current", comparisonType)
                    || string.Equals(propertyName, "Case", comparisonType))
                    FormattedText = _formatter(options.Current, options.Case);
            };
        }

        /// <summary>
        /// Formatted backing field.
        /// </summary>
        private readonly Func<Guid, TextCase, string> _formatter;
        
        /// <summary>
        /// FormattedText backing field.
        /// </summary>
        private string _formattedText;

        //TODO: does this even need to be a getter/setter? seems like probably not, as long as there are connection with the underlying options...
        public string FormattedText
        {
            get { return _formattedText; }
            set
            {
                if (string.Equals(_formattedText, value)) return;
                _formattedText = value;
                RaisePropertyChanged();
            }
        }
    }
}
