using System;
using System.Data;
using System.Data.SqlClient;

namespace EasyZ.Models
{
    public class DB_Final
    {
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataSet ds;
        DataTable dt;

        public DB_Final()
        {
            ds = new DataSet();
            dt = new DataTable();
            conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            adapter = new SqlDataAdapter();

        }

        public DataSet Dataset(ref SqlCommand cmd)
        {
            
            try
            {
                cmd.Connection = conn;
                adapter.SelectCommand = cmd;
                if (conn.State == ConnectionState.Closed) conn.Open();
                adapter.Fill(ds);

            }catch(Exception e)
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                throw e;

            }
            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
              
            }

            return ds;
        }

        public DataTable Datatable(ref SqlCommand cmd)
        {
            try
            {
                cmd.Connection = conn;
                adapter.SelectCommand = cmd;
                if (conn.State == ConnectionState.Closed) conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception e)
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                throw e;

            }
            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
            }

            return dt;
        }

        public void ExecuteNonquery(ref SqlCommand cmd)
        {
            try
            {
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                throw e;

            }
            finally
            {
                conn.Close();
                cmd.Parameters.Clear();
            }
        }






    }
}