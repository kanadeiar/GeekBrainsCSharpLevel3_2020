using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfMailSender.ViewModels;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider _services;
        /// <summary> Сервис-провайдер </summary>
        public static IServiceProvider Services => _services ?? (_services = GetServices().BuildServiceProvider());
        private static IServiceCollection GetServices()
        {
            var services = new ServiceCollection();
            InitServices(services);
            return services;
        }
        private static void InitServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
        }
    }
}
