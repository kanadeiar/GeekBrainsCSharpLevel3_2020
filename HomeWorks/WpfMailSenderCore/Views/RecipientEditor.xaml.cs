using System.Windows.Controls;

namespace WpfMailSenderCore.Views
{
    /// <summary>
    /// Логика взаимодействия для RecipientEditor.xaml
    /// </summary>
    public partial class RecipientEditor : UserControl
    {
        public RecipientEditor() => InitializeComponent();

        private void Validation_OnError(object? sender, ValidationErrorEventArgs e)
        {
            var control = (Control) sender;
            if (control != null && e.Action == ValidationErrorEventAction.Added)
                control.ToolTip = e.Error.ErrorContent.ToString();
            else
                control?.ClearValue(ToolTipProperty);
        }
    }
}
