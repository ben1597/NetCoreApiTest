using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ghl.extension.Data.MsSQL
{
    /// <summary>
    /// 資料庫連線工廠
    /// </summary>
    public interface IMsSQLDbFactory
    {
        /// <summary>
        /// 建立資料庫連線
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        Task<SqlConnection> CreateConnectionAsync(AccessMode policy = AccessMode.ReadOnly);

        /// <summary>
        /// 開啟資料庫連線
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        Task<SqlConnection> OpenConnectionAsync(AccessMode policy = AccessMode.ReadOnly);

        /// <summary>
        /// 建立資料庫連線,由外部直接指定
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="connection">外部指定所使用的連線字串</param>
        /// <returns></returns>
        Task<SqlConnection> OpenConnectionAsync(string setting);

        /// <summary>
        /// 建立資料庫連線
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        SqlConnection CreateConnection(AccessMode policy = AccessMode.ReadOnly);

        /// <summary>
        /// 開啟資料庫連線
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        SqlConnection OpenConnection(AccessMode policy = AccessMode.ReadOnly);


        
    }
}