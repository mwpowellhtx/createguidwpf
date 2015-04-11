using System;
using System.ComponentModel;

namespace GuidGen
{
    public interface IGeneratorOptions : INotifyPropertyChanged
    {
        Guid Current { get; }

        TextCase Case { get; }

        FormatViewModel SelectedFormat { get; set; }
    }
}
