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
            get
            {
                return _exitCommand
                       ?? (_exitCommand = new DelegateCommand(x => Close(), x => true));
            }
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
            //Spec out the parts that should be enumerated.
            var pairs = new[]
            {
                new {SkipCount = 0, TakeCount = 4},
                new {SkipCount = 4, TakeCount = 2},
                new {SkipCount = 6, TakeCount = 2},
                new {SkipCount = 8, TakeCount = 1},
                new {SkipCount = 9, TakeCount = 1},
                new {SkipCount = 10, TakeCount = 1},
                new {SkipCount = 11, TakeCount = 1},
                new {SkipCount = 12, TakeCount = 1},
                new {SkipCount = 13, TakeCount = 1},
                new {SkipCount = 14, TakeCount = 1},
                new {SkipCount = 15, TakeCount = 1},
            };

            var bytes = aGuid.ToByteArray();

            //By virtue of the fact we are working with Guid this should be the case but check anyway.
            Debug.Assert(bytes.Length == 16);

            var theParts = pairs.Select(x => bytes.Skip(x.SkipCount)
                .Take(x.TakeCount).ToHexString().ToTextCase(testCase));

            return new object[] {aGuid.ToString("D").ToTextCase(testCase)}
                .Concat(theParts).ToArray();
        }
    }
}
