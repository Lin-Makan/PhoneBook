using System;
using System.Data.SQLite;

namespace PhoneBook
{
    internal class databaseobject
    {
        public static SQLiteConnection myConnection { get; internal set; }

        internal static void openDB()
        {
            throw new NotImplementedException();
        }

        internal static void closeDB()
        {
            throw new NotImplementedException();
        }
    }
}