using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using JetBrains.Annotations;
using KMFService.Core;
using Newtonsoft.Json.Linq;

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
            catch (Exception e)
            {
                // ignored
            }
        }

        public IList<Currency> GetList(in DateTime dateOn, string code)
        { 
            var connectionString = _configuration.SqlConnection;
            var sqlExpression = "sp_GetRates";

            try
            {
                DataTable dataTable = new DataTable();

                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var sqlCommand = new SqlCommand(sqlExpression, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    SqlParameter dateParam = new SqlParameter
                    {
                        ParameterName = "@date",
                        Value = "dateOn"
                    };
                    sqlCommand.Parameters.Add(dateParam);

                    SqlParameter codeParam = new SqlParameter
                    {
                        ParameterName = "@code",
                        Value = "code"
                    };
                    sqlCommand.Parameters.Add(codeParam);

                    var result = sqlCommand.ExecuteNonQuery();

                    //var reader = sqlCommand.ExecuteReader();

                    

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                // ignored
            }

            return null;
        }
    }
}
