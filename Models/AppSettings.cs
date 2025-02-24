namespace CalendarWebApiService.Models
{
    public class AppSettings
    {
        public string DatabaseConnectionString { get; set; }
        public int Period { get; set; }
        public string Delimeter { get; set; }
        public string CsvFileName { get; set; }
        public string CsvHeaderType { get; set; }
        public string HeaderMessage { get; set; }
        public int QueryPeriodForAlarm { get; set; }
    }
}
