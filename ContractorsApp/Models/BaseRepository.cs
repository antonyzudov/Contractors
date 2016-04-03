using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ContractorsApp.Models
{
    public class BaseRepository
    {
        private readonly string _ConnectionString;

        protected BaseRepository(string connectionString)
        //запоминание строки подключения
        {
            _ConnectionString = connectionString;
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        //выполнение наследниками действий с данными в базе
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    await connection.OpenAsync();//асинхронно открыть подключение
                    return await getData(connection);//асинхронно выполнить действие
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() тайм-аут операции SQL вышел", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() ", GetType().FullName), ex);
            }
        }
    }
}