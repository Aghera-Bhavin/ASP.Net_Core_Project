using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace MyProject.DAL
{
    public class LOC_DAL : LOC_DALBase
    {
        public DataTable GetSelectedData(string con, string procedureName, Dictionary<string, dynamic> keyValuePairs)
        {
            DataTable dt = new DataTable();
            SqlDatabase database = new SqlDatabase(con);
            DbCommand command = database.GetStoredProcCommand(procedureName);
            foreach (var item in keyValuePairs)
            {
                database.AddInParameter(command, item.Key, SqlDbType.VarChar, item.Value);

            }
            using (IDataReader reader = database.ExecuteReader(command))
            {
                dt.Load(reader);
            }   
            return dt;
        }
    }
}
