using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.Constants;
public static class CacheKey
{
    public static class Author
    {
        public const string GetAll = $"{nameof(Author)}.{nameof(GetAll)}";
        public const string Get = $"{nameof(Author)}.{nameof(Get)}";
    }
    public static class Book
    {
        public const string GetAll = $"{nameof(Book)}.{nameof(GetAll)}";
        public const string Get = $"{nameof(Book)}.{nameof(Get)}";
    }
}
