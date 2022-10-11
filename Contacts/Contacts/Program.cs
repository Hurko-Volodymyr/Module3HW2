using Contacts.Core;

namespace Contacts
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var collection = new ContactCollection<Contact>
            {
                new Contact { FirstName = "Сулейман", LastName = "Фамилия2", Number = 380123123122 },
                new Contact { FirstName = "89462-386=263", LastName = "7543747", Number = 387430123123122 },
                new Contact { FirstName = "Зита", LastName = "Фамилия4", Number = 380123123124 },
                new Contact { FirstName = "Sara", LastName = "Фамилия3", Number = 380123123123 },
                new Contact { FirstName = "Abdula", LastName = "Фамилия1", Number = 380123123121 }
            };

            var sortedCollection = collection.GroupByABC();

            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("After sorting");

            foreach (var contact in sortedCollection)
            {
                Console.WriteLine(contact);
            }
        }
    }
}