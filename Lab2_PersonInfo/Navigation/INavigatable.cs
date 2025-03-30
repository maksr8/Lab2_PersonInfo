using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2_PersonInfo.ViewModels;

namespace Lab2_PersonInfo.Navigation
{
    enum PersonInfoNavigationType
    {
        PersonInfoInput,
        PersonInfoOutput
    }
    interface INavigatable<TEnum> where TEnum : Enum
    {
        public TEnum ViewModelType { get; }
        public bool IsEnabled { get; set; }
    }
}
