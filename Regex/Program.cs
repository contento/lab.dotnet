using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        string pattern = @"\b[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\b";
        string input = "Here is an email address: example@test.com. Let's find it!";
        FindEmailAddresses(pattern, input);
    }

    public static void FindEmailAddresses(string pattern, string input)
    {
        Regex r = new(pattern, RegexOptions.IgnoreCase);
        Match m = r.Match(input);
        while (m.Success)
        {
            Console.WriteLine("Found '{0}' at position {1}.", m.Value, m.Index);
            m = m.NextMatch();
        }
    }
}
