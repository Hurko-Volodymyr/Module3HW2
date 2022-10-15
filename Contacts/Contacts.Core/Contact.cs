using System;

namespace Contacts.Core
{
    public class Contact : IContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Number { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(FirstName)}: {FirstName}{Environment.NewLine}" +
                $"{nameof(LastName)}: {LastName}{Environment.NewLine}" +
                $"{nameof(Number)}: {Number}";
        }
    }
}
