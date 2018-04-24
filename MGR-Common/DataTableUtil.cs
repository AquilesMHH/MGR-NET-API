using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MGR_Common
{
    public static class DataTableUtil
    {
        public static List<T> ConvertToList<T>(this DataTable dt) where T : new()
        {
            List<T> resList = new List<T>();
            T item = default(T);
            string column;

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (DataRow row in dt.Rows)
            {
                item = new T();

                foreach (var prop in properties)
                {
                    if (!prop.CanWrite)
                        continue;

                    var attributes = prop.GetCustomAttributes(typeof(TableColumn), false);
                    if (attributes.Length > 0)
                    {
                        column = ((TableColumn)attributes[0]).Name;
                    }
                    else
                    {
                        column = prop.Name;
                    }

                    if (dt.Columns.Contains(column) && row[column] != DBNull.Value)
                        prop.SetValue(item, row[column], null);
                }

                resList.Add(item);
            }

            return resList;
        }
    }
}
