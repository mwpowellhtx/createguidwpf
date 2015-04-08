//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace CreateGuid
//{
//    public static class FormatHelper
//    {
//        private static Guid Current { get;set; }

//        static FormatHelper()
//        {
//            Current = Guid.NewGuid();
//            _instances = new List<FormatViewModel>();
//        }

//        public static string ToLabelContent(this FormatViewModel viewModel)
//        {
//            return viewModel.DisplayName;
//        }

//        private static readonly List<FormatViewModel> _instances;

//        public static List<KeyValuePair<string, string>> GetFormatKeyValuePairs()
//        {
//            return _instances.Select(x => new KeyValuePair<string, string>(x.DisplayName, x.Perform(Current)));
//        }
//    }
//}
