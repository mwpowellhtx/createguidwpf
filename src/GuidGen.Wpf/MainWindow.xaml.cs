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

            //TODO: short of seeing this in an honest to goodness resource, this is getting better...
            DataContext = new GeneratorViewModel();
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
