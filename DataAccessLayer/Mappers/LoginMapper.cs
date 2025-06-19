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

        public async Task<List<AspTestUser>> GetAllAspTestUser()
        {
            try
            {
                List<AspTestUser> list = new List<AspTestUser>();
                using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();
                    MySqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandText = "SELECT * FROM ASP_TEST_USER";
                    // await 사용하여 결과 기다릴 뿐만 아니라 예외도 받을 수 있음
                    MySqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AspTestUser readUser = new AspTestUser
                            {
                                // 엔티티의 속성명을 정확하게 입력해야 함(대소문자 구분)
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Userid = reader.GetString(reader.GetOrdinal("userid")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Point = reader.GetInt32(reader.GetOrdinal("point"))
                            };
                            list.Add(readUser);
                        }
                    }
                    reader.Close();
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}