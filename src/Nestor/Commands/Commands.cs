namespace Nestor.Commands;

public class Commands
{
    const char CommandPrefix = '!';
    const char SuperUserCommandPrefix = '$';
    enum CommandTypes
    {
        Ping,
        Time
    }
    
    public string Command { get; set; }
}