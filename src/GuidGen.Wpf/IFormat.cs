using System.ComponentModel;

namespace GuidGen
{
    public interface IFormat : INotifyPropertyChanged
    {
        bool IsSelected { get; set; }

        int Number { get; set; }

        string DisplayName { get; }

        string FormattedText { get; set; }
    }
}
