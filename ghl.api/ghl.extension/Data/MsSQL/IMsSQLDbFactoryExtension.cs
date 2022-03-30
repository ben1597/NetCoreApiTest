using System;
using System.Data;
using System.Threading.Tasks;

namespace ghl.extension.Data.MsSQL
{
    public static class IMsSQLDbFactoryExtension
    {
        public static async Task OperateAsync(this IMsSQLDbFactory msSqlDbFactory, Func<IDbConnection, Task> function , AccessMode mode = AccessMode.ReadWrite)
        {
            using (IDbConnection conn = await msSqlDbFactory.OpenConnectionAsync(mode))
            {
                await function(conn);
            }
        }

        public static async Task<T> OperateAsync<T>(this IMsSQLDbFactory msSqlDbFactory, Func<IDbConnection, Task<T>> function, AccessMode mode = AccessMode.ReadWrite )
        {
            using (IDbConnection conn = await msSqlDbFactory.OpenConnectionAsync(mode))
            {
                return await function(conn);
            }
        }

        public static async Task OperateAsync(this IMsSQLDbFactory msSqlDbFactory, Func<IDbConnection, Task> function, string connection)
        {
            using (IDbConnection conn = await msSqlDbFactory.OpenConnectionAsync(connection))
            {
                await function(conn);
            }
        }

        public static async Task<T> OperateAsync<T>(this IMsSQLDbFactory msSqlDbFactory, Func<IDbConnection, Task<T>> function, string connection)
        {
            using (IDbConnection conn = await msSqlDbFactory.OpenConnectionAsync(connection))
            {
                return await function(conn);
            }
        }
    }
}
