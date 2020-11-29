using System.Collections.Generic;

namespace HermesLogic.Extensions
{
    /// <summary>
    /// Extensions for type.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Converts one object with type to List with same object (used for list queries).
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">Object to convert.</param>
        /// <returns>List that contains passed as parameter object.</returns>
        public static List<T> ToListType<T>(this T obj)
        {
            return new List<T>() { obj };
        }
    }
}
