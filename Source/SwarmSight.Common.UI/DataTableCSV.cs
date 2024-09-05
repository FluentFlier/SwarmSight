using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace SwarmSight.Common.UI;

public class CSV
{
    public static DataTable ToDataTable(string csvPath)
    {
        using var sr = new StreamReader(csvPath);
        var headers = sr.ReadLine().Split(',');
        var dt = new DataTable();

        foreach (var header in headers)
        {
            dt.Columns.Add(header.Trim());
        }

        while (!sr.EndOfStream)
        {
            var rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            var dr = dt.NewRow();
            for (int i = 0; i < headers.Length; i++)
            {
                dr[i] = rows[i];
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
}
