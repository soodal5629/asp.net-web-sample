using DataAccessLayer.Models;
using MySqlConnector;
// ADO.NET 이용해보기
namespace DataAccessLayer.Mappers
{
    public class LoginMapper : ILoginMapper
    {
        private string connectionString;
        public LoginMapper(string conn)
        {
            connectionString = conn;
        }
        public async Task<AspTestUser> Create(AspTestUser user)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();
                    MySqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandText = "INSERT INTO ASP_TEST_USER(userid, username, point) VALUES (@userid, @username, @point)";
                    sqlCommand.Parameters.AddWithValue("@userid", user.Userid);
                    sqlCommand.Parameters.AddWithValue("@username", user.Username);
                    sqlCommand.Parameters.AddWithValue("@point", user.Point);

                    // 비동기로 DB에 쿼리 요청
                    sqlCommand.ExecuteNonQuery();

                    // 마지막으로 삽입된 ID 조회
                    string getLastIdQuery = "SELECT LAST_INSERT_ID()";
                    using (MySqlCommand cmd = new MySqlCommand(getLastIdQuery, sqlConnection))
                    {
                        int insertedId = Convert.ToInt32(cmd.ExecuteScalar());
                        Console.WriteLine("새로운 사용자 ID: " + insertedId);
                        user.Id = insertedId;
                    }
                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}