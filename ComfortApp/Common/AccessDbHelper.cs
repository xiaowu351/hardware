using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace ComfortApp.Common
{
    //本DBHelper适用于ACCESS
    //如果每次执行完后关闭conn,会导致程序卡顿，建议在程序使用期间不要关闭conn
    //每次使用都会执行try{} catch{},容错能力比较强
    class AccessDbHelper
    {
        public static OleDbConnection conn = null;
        
        /// <summary>  
        /// 连接字符串  
        /// </summary>  
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["AccessDb"]?.ConnectionString;
        private static Dictionary<int, OleDbConnection> _cacheConn;

        static AccessDbHelper()
        {
            _cacheConn = new Dictionary<int, OleDbConnection>();
        }

 
        private static void InitConnection(int index)
        {
            try
            { 
                if(_cacheConn.TryGetValue(index, out OleDbConnection oledbcConn))
                {
                    conn = oledbcConn;
                }
                else
                {
                    _cacheConn.Add(index, new OleDbConnection(string.Format(connectionString, index)));
                    conn = _cacheConn[index];
                }
                

                if (conn.State == ConnectionState.Broken)
                {
                    conn.Close();
                    conn.Open();
                }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (OleDbException Ex)
            {
                MessageBox.Show(Ex.Message);
                conn.Close();
            }
        }

        //获取DataReader
        public static OleDbDataReader GetOleDbDataReader(int dbIndex,string str)
        {
            InitConnection(dbIndex);
            try
            {
                OleDbCommand cmd = new OleDbCommand(str, conn);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (OleDbException Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
            }

        }

        //查询，获取DataSet
        public static DataSet GetDataSet(int dbIndex, string sqlStr)
        {
            InitConnection(dbIndex);

            try
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter dap = new OleDbDataAdapter(sqlStr, conn);
                dap.Fill(ds);
                return ds;
            }
            catch (OleDbException Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
            }
            //conn.Close();
        }

        //查询，获取DataTable
        public static DataTable GetDataTable(int dbIndex, string sqlStr)
        {
            try
            {
                return GetDataSet(dbIndex,sqlStr)?.Tables[0];
            }
            catch (OleDbException Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
            }
        }

        //增删改
        public static int ExecuteNonQuery(int dbIndex, string sqlStr)
        {
            InitConnection(dbIndex);

            try
            {
                OleDbCommand cmd = new OleDbCommand(sqlStr, conn); 
                int result = cmd.ExecuteNonQuery();
                //conn.Close();
                return result;
            }
            catch (OleDbException Ex)
            {
                MessageBox.Show(Ex.Message);
                return 0;
            }
        }

        //执行集合函数
        public static object ExecuteScalar(int dbIndex, string sqlStr)
        {
            InitConnection(dbIndex);

            try
            {
                OleDbCommand cmd = new OleDbCommand(sqlStr, conn);
                object result = cmd.ExecuteScalar();
                //conn.Close();
                return result;
            }
            catch (OleDbException Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
            }
        }
    }
 
}
