using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NavySpade.Modules.Extensions.CsharpTypes
{
    /// <summary>
    /// <see cref="Dictionary{TKey,TValue}"/> extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns value for key if exists or default{<typeparamref name="TValue"/>}.
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            return dictionary.GetValueOrDefault(key, default(TValue));
        }

        /// <summary>
        /// Returns value for key if exists or <paramref name="defaultValue"/>.
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// Returns the <paramref name="dictionary"/> as read-only.
        /// </summary>
        public static IDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}