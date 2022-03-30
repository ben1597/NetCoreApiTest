using System;
using System.Data;
using System.Threading.Tasks;

namespace ghl.extension.Data.MySql
{
    public static class IDbFactoryExtension
    {
        public static async Task OperateAsync(this IMySQLDbFactory mySqlDbFactory, Func<IDbConnection, Task> function , AccessMode mode = AccessMode.ReadWrite)
        {
            using (IDbConnection conn = await mySqlDbFactory.OpenConnectionAsync(mode))
            {
                await function(conn);
            }
        }

        public static async Task<T> OperateAsync<T>(this IMySQLDbFactory mySqlDbFactory, Func<IDbConnection, Task<T>> function, AccessMode mode = AccessMode.ReadWrite )
        {
            using (IDbConnection conn = await mySqlDbFactory.OpenConnectionAsync(mode))
            {
                return await function(conn);
            }
        }

        public static async Task OperateAsync(this IMySQLDbFactory mySqlDbFactory, Func<IDbConnection, Task> function, string connection)
        {
            using (IDbConnection conn = await mySqlDbFactory.OpenConnectionAsync(connection))
            {
                await function(conn);
            }
        }

        public static async Task<T> OperateAsync<T>(this IMySQLDbFactory mySqlDbFactory, Func<IDbConnection, Task<T>> function, string connection)
        {
            using (IDbConnection conn = await mySqlDbFactory.OpenConnectionAsync(connection))
            {
                return await function(conn);
            }
        }
    }
}
