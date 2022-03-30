using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ghl.extension.Data;

namespace ghl.extension.Data.MySql
{
    /// <summary>
    /// MySQL 資料庫連線工廠
    /// </summary>
    public class MySqlDbFactory : IMySQLDbFactory
    {
        private readonly IConfiguration _configuration;
        //private IMySQLDbFactory _mySqlDbFactoryImplementation;

        /// <summary>
        /// 讀寫用資料庫
        /// </summary>
        private DbSetting _dbWriteSetting => new DbSetting
        {
            Connection = _configuration.GetValue<string>("storage:mysql:readWrite:connection"),
            DbProvider = _configuration.GetValue<string>("storage:mysql:readWrite:provider")
        };

        /// <summary>
        /// 唯讀資料庫
        /// </summary>
        private DbSetting _dbReadSetting => new DbSetting
        {
            Connection = _configuration.GetValue<string>("storage:mysql:readOnly:connection"),
            DbProvider = _configuration.GetValue<string>("storage:mysql:readOnly:provider")
        };

        public MySqlDbFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public Task<MySqlConnection> CreateConnectionAsync(AccessMode policy = AccessMode.ReadOnly)
        {
            return Task.FromResult(CreateConnection(policy));
        }

        /// <inheritdoc />
        public MySqlConnection OpenConnection(AccessMode policy = AccessMode.ReadOnly)
        {
            var connection = CreateConnection(policy);
            connection.Open();
            return connection;
        }

        /// <inheritdoc />
        public async Task<MySqlConnection> OpenConnectionAsync(AccessMode policy = AccessMode.ReadOnly)
        {
            MySqlConnection connection = CreateConnection(policy);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<MySqlConnection> OpenConnectionAsync(string setting)
        {
            if (string.IsNullOrEmpty(setting))
                throw new InvalidOperationException();

            var connection = new MySqlConnection(setting);
            await connection.OpenAsync();
            return connection;
        }

        /// <inheritdoc />
        public MySqlConnection CreateConnection(AccessMode policy = AccessMode.ReadOnly)
        {
            return policy switch
            {
                AccessMode.ReadOnly => new MySqlConnection(_dbReadSetting.Connection),
                AccessMode.ReadWrite => new MySqlConnection(_dbWriteSetting.Connection),
                _ => throw new InvalidOperationException()
            };
        }
 
    }
}