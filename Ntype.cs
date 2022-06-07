public class Ntype<T>
{
    private bool auto_clear = true;
    private List<T> values { get; set; }
    private T init { get; set; }
    private T value { get; set; }

    public Ntype(T variable)
    {
        initialise(variable);
    }

    public Ntype(T variable, bool set_auto_clear)
    {
        auto_clear = set_auto_clear;
        initialise(variable);
    }

    private void initialise(T variable)
    {
        init = variable;
        values = new List<T>();
        add_value(variable);
    }

    private void add_value(T new_value)
    {
        values.Add(new_value);
        value = values[values.Count - 1];
    }

    public T reset()
    {
        if (auto_clear == true)
        {
            clear();
        }
        add_value(init);

        return (init);
    }

    public void clear()
    {
        values.Clear();
    }

    public T get()
    {
        return (value);
    }

    public T set(T new_value)
    {
        add_value(new_value);

        return (value);
    }

    public T add(T increment)
    {
        return (set(value + (dynamic)increment));
    }

    public T remove(T decrement)
    {
        return (set(value - (dynamic)decrement));
    }

    public List<T> get_history()
    {
        return (values);
    }
}
