using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    //class EqualityComparer : IEqualityComparer<Stackholder>
    //{
    //    public bool Equals(Stackholder x, Stackholder y)
    //    {
    //        return x.Id == y.Id;
    //    }

    //    public int GetHashCode(Stackholder obj)
    //    {
    //        return obj.Id.GetHashCode();
    //    }
    //}

    //public class GenericEqualityComparer<T> : IEqualityComparer<T> where T : BaseEntity
    //{
    //    public bool Equals(T x, T y)
    //    {
    //        return x.Id == y.Id;
    //    }

    //    public int GetHashCode(T obj)
    //    {
    //        return obj.Id.GetHashCode();
    //    }
         
    //}

    public static class StaticClassHelper
    {
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
                                                                                Func<T, TKey> getKey)
        {
            return from item in items
                   join otherItem in other on getKey(item)
                   equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;

        }

    }
}