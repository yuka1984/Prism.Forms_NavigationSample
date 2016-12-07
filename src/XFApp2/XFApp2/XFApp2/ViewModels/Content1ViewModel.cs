using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace XFApp2.ViewModels
{
    public class Content1ViewModel : BindableBase
    {
        public DelegateCommand ToSubContent1Command { get; private set; }
        public Content1ViewModel(INavigationService navigationService)
        {
            ToSubContent1Command = DelegateCommand.FromAsyncHandler(()=> navigationService.NavigateAsync("SubContent1"));            
        }
    }
}
