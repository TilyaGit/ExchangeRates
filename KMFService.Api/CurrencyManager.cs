using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            int i = 0;
            var connectionString = _configuration.SqlConnection;
            try
            {
                i++;
                string query = @"INSERT INTO dbo.R_Currency
               (TITLE,CODE,VALUE, A_DATE)Values
               ('" + currencies[i].Title + @"'
               ,'" + currencies[i].Code + @"'
               ,'" + currencies[i].Value + @"'
               ,'" + currencies[i].Date + @"')";

                using (var con = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                }

            }
            catch (Exception)
            {
                // ignored
            }
        }

        public Currency Get(in DateTime dateOn, string code)
        {
            throw new NotImplementedException();
        }
    }
}
