using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Permissions;
public static class BookPermissions
{
    public const string GroupName = "Book";
    public static class Genres
    {
        public const string Default = $"{GroupName}.Genres";
        public const string Create = $"{Default}.Create";
        public const string Edit = $"{Default}.Edit";
        public const string Delete = $"{Default}.Delete";
    }

    public static class Authors
    {
        public const string Default = $"{GroupName}.Authors";
        public const string Create = $"{Default}.Create";
        public const string Edit = $"{Default}.Edit";
        public const string Delete = $"{Default}.Delete";
    }
}