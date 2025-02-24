using System.Text;

namespace CalendarWebApiService.Helpers
{
    public static class CsvHelper<T> where T: class
    {
        public static byte[] CreateCsv<T>(IEnumerable<T> list, string delimeter)
        {
            var csv = new StringBuilder();
            string header = string.Empty;
            var props = list.First()?.GetType().GetProperties();
            foreach (var item in props)
            {
                header=string.Concat(header, item.Name.ToString(), delimeter);
            }
            csv.AppendLine(header.Substring(0,header.Length-1));
            string line = string.Empty;
            foreach (var item in list)
            {
                foreach (var property in props)
                {
                    line = string.Concat(line, item?.GetType()?.GetProperty(property.Name)?.GetValue(item), delimeter);
                }
                csv.AppendLine(line.Substring(0, line.Length - 1));
                line = string.Empty;
            }
            
            return new UTF8Encoding().GetBytes(csv.ToString());
        }
    }
}
