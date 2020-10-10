using System.Linq;
using System.Windows;
using WpfMailSenderCore.Infrastructure.Commands.Base;

namespace WpfMailSenderCore.Infrastructure.Commands
{
    class CloseWindowCommand : Command
    {
        protected override void Execute(object p)
        {
            var window = p as Window;
            if (window is null)
                window = App.Current.Windows
                    .Cast<Window>()
                    .FirstOrDefault(w => w.IsFocused);
            if (window is null)
                window = App.Current.Windows
                    .Cast<Window>()
                    .FirstOrDefault(w => w.IsActive);
            window?.Close();
        }
    }
}
