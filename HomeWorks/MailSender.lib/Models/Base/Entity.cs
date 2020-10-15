namespace MailSender.Models.Base
{
    /// <summary> Сущность </summary>
    public abstract class Entity
    {
        public int Id { get; set; }
    }
    /// <summary> Именованная сущность </summary>
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
    /// <summary> Именованная сущность с адресом </summary>
    public abstract class Person : NamedEntity
    {
        public string Address { get; set; }
    }
}
