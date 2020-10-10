using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfMailSenderCore.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowVModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
