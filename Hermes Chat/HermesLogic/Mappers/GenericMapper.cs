using System;
using System.Collections.Generic;

namespace HermesLogic.Mappers
{
    public static class GenericMapper
    {
        public static List<T> ToGenericDtoList<T, Tdb>(this IList<Tdb> dboObjects, Func<Tdb, T> transformAction) // Should not be called directly
        {
            if (transformAction == null)
            {
                throw new ArgumentNullException(nameof(transformAction));
            }

            var listToReturn = new List<T>();
            if (dboObjects == null)
            {
                return listToReturn;
            }

            int objectCount = dboObjects.Count;
            for (int i = 0; i < objectCount; i++)
            {
                listToReturn.Add(transformAction(dboObjects[i]));
            }

            return listToReturn;
        }
    }
}
