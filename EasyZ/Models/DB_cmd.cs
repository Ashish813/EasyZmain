using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EasyZ.Models
{
    public class DB_cmd
    {
        SqlCommand cmd; 
        DB_Final objfinal;

        public DB_cmd()
        {
            cmd = new SqlCommand();
            objfinal = new DB_Final();
        }


        public SqlCommand getcmd()
        {
            return cmd;
        }

        public void setcmd(SqlCommand c)
        {
            cmd = c;
        }

        public void addParameter(string ParameterName, string value) {

            cmd.Parameters.AddWithValue(ParameterName, value);

        }






        public dynamic returnDataset(string ProcName)  
        {
           // cmd.CommandText = "";// part of explanation
           //int check= cmd.CommandText!=""?1:0;
            cmd.CommandText = ProcName;
            cmd.CommandType = CommandType.StoredProcedure;

            return objfinal.Dataset(ref cmd);


        }

        public dynamic returnDatatable(string ProcName)
        {

            cmd.CommandText = ProcName;
            cmd.CommandType = CommandType.StoredProcedure;

            return objfinal.Datatable(ref cmd);


        }

        public void  NonQuery(string ProcName)
        {

            cmd.CommandText = ProcName;
            cmd.CommandType = CommandType.StoredProcedure;

            objfinal.ExecuteNonquery(ref cmd);


        }


    }
}