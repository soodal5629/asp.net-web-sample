using DataAccessLayer.Models;
using MySqlConnector;
using System.Data;
// ADO.NET + 저장 프로시저 이용해보기
namespace DataAccessLayer.Mappers
{
    public class ProcCall
    {
        private string connstr = "Server=localhost;Database=test;Uid=root;Pwd=ldcc!2626;";

        public async Task<DataTable> RequestProcedure(string procedurename, Dictionary<string, object> dc)
        {

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                await conn.OpenAsync();

                MySqlCommand command = new MySqlCommand(procedurename, conn);
                command.CommandType = CommandType.StoredProcedure;

                //고정적이지않게 Dictionary
                foreach (var aw in dc)
                {
                    // MySqlParameter p1 = new MySqlParameter();
                    // p1.ParameterName = aw.Key;
                    // p1.DbType = GetDbType(aw.Value.GetType());
                    // p1.Direction = ParameterDirection.Input;
                    // p1.Value = aw.Value;
                    string paramName = "p_" + aw.Key; // 고유 파라미터 이름 생성
                    
                    MySqlParameter p1 = new MySqlParameter {
                        ParameterName = paramName,
                        MySqlDbType = MySqlDbType.VarChar,
                        Value = aw.Value
                    };
                    command.Parameters.Add(p1);
                    //Console.WriteLine("키는" + aw.Key + "," + aw.Value);
                }
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds);

                //조회결과가 없을 경우
                if (ds.Tables.Count > 0)
                {
                    //조회된 행의수가 없을 경우
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("DB fail");
                    }
                    //DB Catch에서 Error가 Select되서 넘어온경우.
                    if (ds.Tables[0].Rows[0][0].Equals("Error"))
                    {
                        throw new Exception("DB fail");
                    }
                    return ds.Tables[0];
                }
                else
                {
                    throw new Exception("DB fail");
                }

            }

        }
        public static DbType GetDbType(Type runtimeType)
        {
            var nonNullableType = Nullable.GetUnderlyingType(runtimeType);
            if (nonNullableType != null)
            {
                runtimeType = nonNullableType;
            }

            var templateValue = (Object)null;
            //object? templateValue;
            if (runtimeType.IsClass == false)
            {
                templateValue = Activator.CreateInstance(runtimeType);
            }

            var sqlParamter = new MySqlParameter(String.Empty, value: templateValue);
            //var sqlParamter = new MySqlParameter("@Userid", MySqlDbType.VarChar);
            return sqlParamter.DbType;
        }

    }

}