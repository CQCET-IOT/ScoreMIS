using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ScoreMIS.App_Code
{
    public class ConnectionClass
    {
        SqlConnection myConnection;
        DataSet myDataSet;
        SqlDataReader myDataReader;
        SqlDataAdapter myAdapter;
        SqlCommand myCommand;
        public static string GetConStr
        {
            get
            {
                return "Data Source=.;Integrated Security=SSPI;database=ScoreDB";
            }
        }

        public DataSet GetDataSet(string sqlStr,string TableName)
        {
            myConnection = new SqlConnection(GetConStr);
            myAdapter = new SqlDataAdapter(sqlStr,myConnection);
            myDataSet = new DataSet();
            myConnection.Close();
            myConnection.Open();
            myAdapter.Fill(myDataSet, TableName);
            return myDataSet;
        }

        public SqlDataReader GetDataReader(string sqlStr)
        {
            myConnection = new SqlConnection(GetConStr);
            myCommand = new SqlCommand(sqlStr, myConnection);
            myConnection.Close();
            myConnection.Open();
            myDataReader = myCommand.ExecuteReader();
            return myDataReader;
        }

    }
}
