public class Nscan
{
    private List<char> up { get; set; }
    private List<char> low { get; set; }
    private List<char> number { get; set; }
    private List<char> special { get; set; }

    private void initialise()
    {
        up = new List<char>();
        low = new List<char>();
        number = new List<char>();
        special = new List<char>();
    }

    public Nscan(string data)
    {
        initialise();
        parse(data);
    }

    private void parse(string data)
    {
        foreach (char c in data)
        {
            if (c >= 'A' && c <= 'Z')
                up.Add(c);
            else if (c >= 'a' && c <= 'z')
                low.Add(c);
            else if (c >= '0' && c <= '9')
                number.Add(c);
            else
                special.Add(c);
        }
    }

    public List<char> get_up()
    {
        return (up);
    }

    public List<char> get_low()
    {
        return (low);
    }

    public List<char> get_numbers()
    {
        return (number);
    }

    public List<char> get_specials()
    {
        return (special);
    }

    public int count_up()
    {
        return (up.Count());
    }

    public int count_low()
    {
        return (low.Count());
    }

    public int count_numbers()
    {
        return (number.Count());
    }

    public int count_specials()
    {
        return (special.Count());
    }
}
