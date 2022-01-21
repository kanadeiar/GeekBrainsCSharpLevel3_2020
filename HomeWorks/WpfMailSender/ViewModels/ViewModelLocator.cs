using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WpfMailSender.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowView 
            => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
