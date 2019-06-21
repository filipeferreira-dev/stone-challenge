namespace Monitor.Models
{
    public class Settings
    {
        public int Interval { get; set; }

        public ApiSettings ApiSettings { get; set; }
    }

    public class ApiSettings
    {
        public string Uri { get; set; }
    }
}
