using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AspNetCoreWebApiProjManager.Shared
{
    public static class Functions
    {
        // String Functions
        public static string ToTitleCase(this string text) => text[..1].ToUpper() + text[1..].ToLower();
        public static string GetFileColumnName(this string dbColumnName) => string.Join(string.Empty, dbColumnName.Split('_').Select(x => x.ToTitleCase()));
        public static string GetDbColumnName(this string fileColumnName) => Regex.Replace(fileColumnName, "(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])", "_").ToUpper();

        
        // DataTable Functions
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            DataTable dataTable = new();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in properties)
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in list)
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public static DataTable ToDataTable<T>(this DataTable dataTable)
        {
            DataTable resultData = new();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in properties)
                resultData.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DataRow resultRow = resultData.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    string columnName = propertyInfo.Name;
                    resultRow[columnName] = dataTable.Columns.Contains(columnName) && !string.IsNullOrEmpty(dataRow[columnName].ToString())
                        ? resultRow[columnName] = Convert.ChangeType(dataRow[columnName], Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType)
                        : DBNull.Value;
                }
                resultData.Rows.Add(resultRow);
            }
            return resultData;
        }

        public static IList<T> ToList<T>(this DataTable dataTable)
        {
            IList<T> list = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T listItem = Activator.CreateInstance<T>();
                IList<PropertyInfo> properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Count; i++)
                {
                    if (!dataTable.Columns.Contains(properties[i].Name))
                        continue;
                    PropertyInfo property = typeof(T).GetProperty(properties[i].Name);
                    property.SetValue(listItem, Convert.ChangeType(row[properties[i].Name], property.PropertyType), null);
                }
                list.Add(listItem);
            }
            return list;
        }
    }
}