using System;

namespace GuidGen
{
    //TODO: format view model could be more of a 'command pattern' extension than a true 'view model' per se...
    public class FormatViewModel : ViewModelBase
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
        public string DisplayName
        {
            get
            {
                //Remember to format with gesture keys equipped and ready for action.
                return string.Format("_{0}: {1}", Number, _baseDisplayName);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseDisplayName"></param>
        /// <param name="formatter"></param>
        public FormatViewModel(string baseDisplayName, Func<Guid, TextCase, string> formatter)
        {
            _baseDisplayName = baseDisplayName;
            _formatter = formatter;
        }

        /// <summary>
        /// Formatted backing field.
        /// </summary>
        private readonly Func<Guid, TextCase, string> _formatter;

        /// <summary>
        /// Performs the <see cref="guid"/> formatting itself.
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="textCase"></param>
        /// <returns></returns>
        public string Perform(Guid guid, TextCase textCase)
        {
            return _formatter(guid, textCase);
        }
    }
}
