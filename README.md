# Ntype
Neo Type To Control At 100% The Variables

### Example
```C#
using System;
using System.Collections.Generic;

namespace test_types
{
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

    class Program
    {
        static void Main(string[] args)
        {
            Ntype<int> nint_1 = new Ntype<int>(0);
            Ntype<int> nint_2 = new Ntype<int>(10, false);
            // Doesn't clear history when reset variable

            Console.WriteLine($"current nint_1 value: {nint_1.get()}");
            nint_1.set(nint_1.get() + 1);
            Console.WriteLine($"current nint_1 value: {nint_1.get()}");
            Console.WriteLine($"current nint_1 value: {nint_1.add(5)}");
            Console.WriteLine($"current nint_1 value: {nint_1.remove(1)}");
            Console.WriteLine($"current nint_1 history count: {nint_1.get_history().Count}");
            Console.WriteLine($"current nint_1 value: {nint_1.reset()}");
            Console.WriteLine($"current nint_1 history count: {nint_1.get_history().Count}\n");

            Console.WriteLine($"current nint_2 value: {nint_2.get()}");
            nint_2.set(nint_2.get() + 1);
            Console.WriteLine($"current nint_2 value: {nint_2.get()}");
            Console.WriteLine($"current nint_2 value: {nint_2.add(5)}");
            Console.WriteLine($"current nint_2 value: {nint_2.remove(1)}");
            Console.WriteLine($"current nint_2 history count: {nint_2.get_history().Count}");
            Console.WriteLine($"current nint_2 value: {nint_2.reset()}");
            Console.WriteLine($"current nint_2 history count: {nint_2.get_history().Count}\n");

            Console.ReadLine();
        }
    }
}

```

##### Result
```
current nint_1 value: 0
current nint_1 value: 1
current nint_1 value: 6
current nint_1 value: 5
current nint_1 history count: 4
current nint_1 value: 0
current nint_1 history count: 1

current nint_2 value: 10
current nint_2 value: 11
current nint_2 value: 16
current nint_2 value: 15
current nint_2 history count: 4
current nint_2 value: 10
current nint_2 history count: 5
```
