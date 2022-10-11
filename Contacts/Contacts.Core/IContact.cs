namespace Contacts.Core
{
    public interface IContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Number { get; set; }
    }
}