using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace XFApp2.ViewModels
{
    public class Content3ViewModel : BindableBase
    {
        public DelegateCommand ToContent1Command { get; private set; }
        public Content3ViewModel(INavigationService navigationService)
        {
            ToContent1Command = DelegateCommand.FromAsyncHandler(() => navigationService.NavigateAsync("RootPage/Content1"));
        }
    }
}
