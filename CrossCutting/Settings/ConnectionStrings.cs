namespace CrossCutting.Settings
{
    public class ConnectionStrings
    {
        public ConnectionString St { get; set; }
    }

    public class ConnectionString
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FullString => $"Server={Server};Database={Database};User Id={Username};Password={Password};";
    }
}
