using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailSender.Models.Base
{
    /// <summary> Модель </summary>
    public abstract class Model : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }
    }
    /// <summary> Именованная модель </summary>
    public abstract class NamedModel : Model
    {
        public virtual string Name { get; set; }
    }
    /// <summary> Именованная модель с адресом </summary>
    public abstract class PersonModel : NamedModel
    {
        public virtual string Address { get; set; }
    }
}
