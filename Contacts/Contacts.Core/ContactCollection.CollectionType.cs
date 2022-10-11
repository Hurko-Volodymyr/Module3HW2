namespace Contacts.Core
{
    public partial class ContactCollection<T>
        where T : IContact
    {
        public enum CollectionType
        {
            English,
            Ukrainian,
            Sharp,
            Numbers,
        }
    }
}
