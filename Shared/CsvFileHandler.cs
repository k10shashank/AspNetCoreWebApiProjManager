using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AspNetCoreWebApiProjManager.Shared
{
    public static class CsvFileHandler
    {
        private static readonly string CsvExtension = ".csv";
        private static readonly IEnumerable<char> DelimiterList = new List<char>() { ',', ';' };
        private static readonly char QuoteChar = '"';

        public static DataTable ConvertCsvToDatatable(string filePath, Stream streeam)
        {
            if (!filePath.EndsWith(CsvExtension))
                throw new AppException("Not a CSV File.", HttpStatusCode.NotAcceptable);

            using StreamReader reader = new(streeam);
            string firstLine = reader.ReadLine();

            Dictionary<char, int> delimiterDict = new();
            foreach (char item in DelimiterList)
                delimiterDict.Add(item, firstLine.Count(x => x.Equals(item)));
            if (delimiterDict.Values.Count(x => x.Equals(0)) != delimiterDict.Count - 1)
                throw new AppException("Error Occurred while choosing CSV Delimiter.", HttpStatusCode.NotAcceptable);

            char delimiter = delimiterDict.FirstOrDefault(x => !x.Value.Equals(0)).Key;
            if (firstLine.EndsWith(delimiter))
                throw new AppException("Header Line ends with Delimiter.", HttpStatusCode.NotAcceptable);

            DataTable resultData = new();
            foreach (string item in firstLine.Split(delimiter))
            {
                if (string.IsNullOrWhiteSpace(item))
                    throw new AppException("Blank Header Name.", HttpStatusCode.NotAcceptable);
                resultData.Columns.Add(item);
            }

            int numberOfColumns = resultData.Columns.Count;
            int rowIdx = 0;
            while (!reader.EndOfStream)
            {
                rowIdx++;
                string line = reader.ReadLine();
                IList<string> lineValues = new List<string>();

                if (line.Contains(QuoteChar))
                {
                    foreach (string item in line.Split(QuoteChar))
                    {
                        if (item.First() == delimiter && item.Last() == delimiter)
                            foreach (string jtem in item[1..^1].Split(delimiter))
                                lineValues.Add(jtem);
                        else if (item.First() == delimiter)
                            foreach (string jtem in item[1..].Split(delimiter))
                                lineValues.Add(jtem);
                        else if (item.Last() == delimiter)
                            foreach (string jtem in item[..^1].Split(delimiter))
                                lineValues.Add(jtem);
                        else
                            lineValues.Add(item);
                    }
                }
                else
                {
                    lineValues = line.Split(delimiter);
                }


                if (lineValues.Count != numberOfColumns)
                    throw new AppException($"Cannot map Data from Line {rowIdx + 1}.", HttpStatusCode.NotAcceptable);

                DataRow row = resultData.NewRow();
                for (int i = 0; i < lineValues.Count; i++)
                    row[i] = lineValues[i];
                resultData.Rows.Add(row);
            }

            return resultData;
        }

        public static FileStreamResult DownloadCsv(DataTable dataTable, string fileName, Func<string, string> headerNameChange, string delimiter = ",", bool includeHeader = true)
        {
            Stream stream = new MemoryStream(ConvertDatatableToCsv(dataTable, headerNameChange, delimiter, includeHeader));
            return new FileStreamResult(stream, "text/csv") { FileDownloadName = $"{fileName}{CsvExtension}" };
        }

        public static void ExportCsv(DataTable dataTable, string filePath, Func<string, string> headerNameChange, string delimiter = ",", bool includeHeader = true)
        {
            using StreamWriter writer = new(filePath);
            byte[] byteArray = ConvertDatatableToCsv(dataTable, headerNameChange, delimiter, includeHeader);
            writer.Write(Encoding.ASCII.GetString(byteArray, 0, byteArray.Length));
        }

        private static byte[] ConvertDatatableToCsv(DataTable dataTable, Func<string, string> headerNameChange, string delimiter, bool includeHeader)
        {
            StringBuilder builder = new();
            
            if (includeHeader)
                builder.AppendLine(ListToCsvline(dataTable.Columns.Cast<DataColumn>().Select(x => headerNameChange.Invoke(x.ColumnName)), delimiter));

            for (int i = 0; i < dataTable.Rows.Count; i++)
                builder.AppendLine(ListToCsvline(dataTable.Rows[i].ItemArray.Select(x => x.ToString()), delimiter));

            return Encoding.ASCII.GetBytes(builder.ToString());
        }

        private static string ListToCsvline(IEnumerable<string> list, string delimiter)
        {
            IList<string> line = new List<string>();
            foreach (string item in list)
            {
                if (item.Contains(delimiter))
                    line.Add($"{QuoteChar}{item}{QuoteChar}");
                else
                    line.Add(item);
            }
            return string.Join(delimiter, line);
        }
    }
}
