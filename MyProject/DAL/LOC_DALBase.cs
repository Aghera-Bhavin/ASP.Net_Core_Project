using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace MyProject.DAL
{
    public class LOC_DALBase
    {
        #region GetAllData
        public DataTable GetAllData(string con, string procedureName)
        {
            DataTable dt = new DataTable();
            SqlDatabase database = new SqlDatabase(con);
            DbCommand command = database.GetStoredProcCommand(procedureName);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                dt.Load(reader);
            }
            return dt;
        }
        #endregion

        #region DeleteData
        public bool DeleteData(string con, string procedureName, string column, int id)
        {
            SqlDatabase database = new SqlDatabase(con);
            DbCommand command = database.GetStoredProcCommand(procedureName);
            database.AddInParameter(command, column, (SqlDbType.Int), id);
            return Convert.ToBoolean(database.ExecuteNonQuery(command));
        }
        #endregion

        #region InsertOrUpdateData
        public bool InsertOrUpdateData(string con, string procedureName, Dictionary<string, object?> keyValuePairs)
        {
            SqlDatabase database = new SqlDatabase(con);
            DbCommand command = database.GetStoredProcCommand(procedureName);
            foreach (var item in keyValuePairs)
            {
                if (item.Value == null)
                {
                    continue;
                }
                SqlDbType type = ConvertTypeToSqlDbType(item.Value.GetType());
                database.AddInParameter(command, item.Key, type, item.Value);
            }
            return Convert.ToBoolean(database.ExecuteNonQuery(command));
            
        }
        #endregion

        private static readonly Dictionary<Type, SqlDbType> typeToSqlDbTypeMap = new Dictionary<Type, SqlDbType>
        {
            { typeof(int), SqlDbType.Int },
            { typeof(string), SqlDbType.NVarChar },
            { typeof(double), SqlDbType.Decimal },
            { typeof(bool), SqlDbType.Bit },
            { typeof(DateTime), SqlDbType.DateTime },
            // Add more mappings as needed
        };

        public static SqlDbType ConvertTypeToSqlDbType(Type type)
        {
            if (typeToSqlDbTypeMap.TryGetValue(type, out SqlDbType sqlDbType))
            {
                return sqlDbType;
            }

            // Handle unsupported types or return a default value
            throw new NotSupportedException($"Type {type.FullName} is not supported.");
        }
    }
}
