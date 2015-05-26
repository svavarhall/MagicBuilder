
/* 
The follwowing code shows an example of these rules.
Comment conventions, camelCasing, predefined type names, PascalCasing
and aligned curly brackets.
*/

public class UserLog
{
    public void Add(LogEvent logEvent)
    {
        int itemCount = logEvent.Items.Count;
        var currentYear = 2015;
        // ...
    }
}

// Declare all member variables at the top of a class,
// with static variables at the very top.

public class Account
{
    public static string BankName;
    public static decimal Reserves;
 
    public string Number {get; set;}
    public DateTime DateOpened {get; set;}
    public DateTime DateClosed {get; set;}
    public decimal Balance {get; set;}
 
    // Constructor
    public Account()
    {
        // TODO
    }
}


