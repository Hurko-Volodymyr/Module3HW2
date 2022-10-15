using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Contacts.Core
{
    public class ContactCollection<T> : ICollection<T>
        where T : IContact
    {
        private const int _defaultSize = 10;
        private readonly IReadOnlyDictionary<CollectionType, List<T>> _allCollections;
        private CultureInfo _culture;
        private List<T> _contacsEnglish;
        private List<T> _contacsUkrainian;
        private List<T> _contacsSharp;
        private List<T> _contacsNumbers;

        public ContactCollection()
            : this(culture: new CultureInfo("EN"))
        {
        }

        public ContactCollection(CultureInfo culture)
            : this(culture, _defaultSize)
        {
        }

        public ContactCollection(CultureInfo culture, int size)
        {
            _culture = culture;
            _contacsEnglish = new List<T>(size);
            _contacsUkrainian = new List<T>(size);
            _contacsSharp = new List<T>(size);
            _contacsNumbers = new List<T>(size);
            _allCollections = new Dictionary<CollectionType, List<T>>()
            {
                { CollectionType.English, _contacsEnglish },
                { CollectionType.Ukrainian, _contacsUkrainian },
                { CollectionType.Numbers, _contacsNumbers },
                { CollectionType.Sharp, _contacsSharp }
            };
        }

        public int Count => _contacsNumbers.Count + _contacsEnglish.Count + _contacsSharp.Count + _contacsUkrainian.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var collectionType = GetCollectionTypeFromContact(item);
            var collection = _allCollections.TryGetValue(collectionType, out var result)
                ? result : throw new ArgumentException(nameof(collectionType));
            collection.Add(item);
        }

        public void Clear()
        {
            foreach (var colection in _allCollections)
            {
                colection.Value.Clear();
            }
        }

        public bool Contains(T item)
        {
            return _allCollections.SelectMany(a => a.Value).Any(contact => contact.Number == item.Number);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            var collectionType = GetCollectionTypeFromContact(item);
            var collection = _allCollections.TryGetValue(collectionType, out var result)
                ? result : throw new ArgumentException(nameof(collectionType));
            return collection.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var all = _allCollections.SelectMany(item => item.Value);
            foreach (var item in all)
            {
                yield return item;
            }
        }

        public List<T> GroupByABC()
        {
            return _allCollections
            .SelectMany(item => item.Value)
            .OrderBy(item => item.FirstName)
            .ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_allCollections).GetEnumerator();
        }

        private CollectionType GetCollectionTypeFromContact(T contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            var charContacts = contact.FirstName.ToUpper().ToCharArray();
            if (char.IsNumber(charContacts[0]))
            {
                return CollectionType.Numbers;
            }
            else if (charContacts[0] >= 65 && charContacts[0] < 90)
            {
                return CollectionType.English;
            }
            else if (charContacts[0] >= 224 && charContacts[0] < 255)
            {
                return CollectionType.Ukrainian;
            }
            else
            {
                return CollectionType.Sharp;
            }
        }
    }
}
