using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace evidence_osob_Wpf
{
    public class database
    {
        // SQLite connection
        private SQLiteAsyncConnection Database;

        public database(string dbPath)
        {
            Database = new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<Person>().Wait();
       }
        // Query
        public Task<List<Person>> GetItemsAsync()
        {
            return Database.Table<Person>().ToListAsync();
        }
        // Query using SQL query string
        public Task<List<Person>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<Person>("SELECT * FROM [Person] ");
        }

        
        public Task<int> SaveItemAsync(Person item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<List<Person>> DeleteItemAsync(int item)
        {
            return Database.QueryAsync<Person>("DELETE FROM [Person] WHERE [ID] = " + item );
        }
    }
}
