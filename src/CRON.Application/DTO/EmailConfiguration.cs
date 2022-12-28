namespace CRON.Application.DTO;
public class EmailConfiguration 
{
    public string From { get; set; }
    public string To { get; set; }
    public string Host { get; set; } 
    public int Port { get; set; } 
    public string User { get; set; }
    public string Pass { get; set; }
}