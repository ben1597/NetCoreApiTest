using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ghl.extension.Data;

namespace ghl.extension.Data.MsSQL
{
    /// <summary>
    /// MsSQL 資料庫連線工廠
    /// </summary>
    public class MsSqlDbFactory : IMsSQLDbFactory
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 讀寫用資料庫
        /// </summary>
        private DbSetting _dbWriteSetting => new DbSetting
        {
            Connection = _configuration.GetValue<string>("storage:mssql:readWrite:connection"),
            DbProvider = _configuration.GetValue<string>("storage:mssql:readWrite:provider")
        };

        /// <summary>
        /// 唯讀資料庫
        /// </summary>
        private DbSetting _dbReadSetting => new DbSetting
        {
            Connection = _configuration.GetValue<string>("storage:mssql:readOnly:connection"),
            DbProvider = _configuration.GetValue<string>("storage:mssql:readOnly:provider")
        };

        public MsSqlDbFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public Task<SqlConnection> CreateConnectionAsync(AccessMode policy = AccessMode.ReadOnly)
        {
            return Task.FromResult(CreateConnection(policy));
        }

        /// <inheritdoc />
        public SqlConnection OpenConnection(AccessMode policy = AccessMode.ReadOnly)
        {
            var connection = CreateConnection(policy);
            connection.Open();
            return connection;
        }

        /// <inheritdoc />
        public async Task<SqlConnection> OpenConnectionAsync(AccessMode policy = AccessMode.ReadOnly)
        {
            SqlConnection connection = CreateConnection(policy);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<SqlConnection> OpenConnectionAsync(string setting)
        {
            if (string.IsNullOrEmpty(setting))
                throw new InvalidOperationException();

            var connection = new SqlConnection(setting);
            await connection.OpenAsync();
            return connection;
        }

        /// <inheritdoc />
        public SqlConnection CreateConnection(AccessMode policy = AccessMode.ReadOnly)
        {
            return policy switch
            {
                AccessMode.ReadOnly => new SqlConnection(_dbReadSetting.Connection),
                AccessMode.ReadWrite => new SqlConnection(_dbWriteSetting.Connection),
                _ => throw new InvalidOperationException()
            };
        }
 
    }
}