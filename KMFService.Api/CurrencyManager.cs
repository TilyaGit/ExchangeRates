using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using JetBrains.Annotations;
using KMFService.Core;

namespace KMFService.Api
{
    public class CurrencyManager : ICurrencyManager
    {
        private readonly AppConfiguration _configuration;

        public CurrencyManager([NotNull] AppConfiguration configuration)
        {
            _configuration = configuration ?? 
                throw new ArgumentNullException(nameof(configuration));
        }

        public void SaveList(IList<Currency> currencies)
        {
            var connectionString = _configuration.SqlConnection;

            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var sqlCommand = new SqlCommand("INSERT INTO R_CURRENCY (TITLE, CODE, VALUE, A_DATE)" +
                                                    "VALUES (@TITLE, @CODE, @VALUE, @A_DATE)");
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Parameters.Add("@TITLE", SqlDbType.Text);
                    sqlCommand.Parameters.Add("@CODE", SqlDbType.Text);
                    sqlCommand.Parameters.Add("@VALUE", SqlDbType.Decimal);
                    sqlCommand.Parameters.Add("@A_DATE", SqlDbType.Date);
                    foreach (var item in currencies)
                    {
                        sqlCommand.Parameters[0].Value = item.Title;
                        sqlCommand.Parameters[1].Value = item.Code;
                        sqlCommand.Parameters[2].Value = item.Value;
                        sqlCommand.Parameters[3].Value = item.Date;

                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public IList<Currency> GetList(in DateTime dateOn, string code)
        { 
            var connectionString = _configuration.SqlConnection;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var currencies =
                    sqlConnection.Query<Currency>("dbo.sp_GetRates", new { date = dateOn, code = code }, commandType: CommandType.StoredProcedure).ToList();

                sqlConnection.Close();
                return currencies;
            }

        }
    }
}
