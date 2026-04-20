namespace shared.Configurations;

public class AppSettings
{
    public ConnectionStringsSettings ConnectionStrings { get; set; } = new();
}

public class ConnectionStringsSettings
{
    public string PgConnection { get; set; } = string.Empty;
}
