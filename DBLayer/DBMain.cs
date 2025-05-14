using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.DBLayer
{
    internal class DBMain
    {
        string ConnnectionString = "Data Source=(local);Initial Catalog=QLNS;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da = null;


        public DBMain()
        {
            conn = new SqlConnection(ConnnectionString);
            cmd = conn.CreateCommand();
        }

        // Dùng để truy vấn lấy bảng dữ liệu
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        //Dùng để thực hiện các chức năng CRUD
        public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string error)
        {
            bool res = false;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            try
            {
                cmd.ExecuteNonQuery();
                res = true;
            }
            catch (SqlException ex)
            {
                res = false;
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return res;
        }


        //Dùng để truy vấn giá trị đơn
        public object MyExecuteScalar(string strSQL, CommandType ct, SqlParameter[] param = null)
        {
            object result = null;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();

                cmd.CommandText = strSQL;
                cmd.CommandType = ct;

                cmd.Parameters.Clear();
                if (param != null)
                    cmd.Parameters.AddRange(param);

                result = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

    }
}
