public class Nhash
{
    public class Settings
    {
        public static List<byte> key { get; set; }
        public static List<byte> salt = new List<byte>()
        {
            0x7b, 0x6e, 0x63, 0x6f, 0x72, 0x65, 0x7d
        };
        public static List<byte> result { get; set; }
        public static int size = 128;
        public static int salt_size = 256;
    }

    public class Tools
    {
        public static List<byte> rotate(List<byte> bytes)
        {
            byte buffer = 0x00;

            for (int i = 0; i < bytes.Count() / 2; i++)
            {
                buffer = bytes[i];
                bytes[i] = bytes[bytes.Count() - i - 1];
                bytes[bytes.Count() - i - 1] = buffer;
            }

            return (bytes);
        }

        public static bool is_exception(byte[] exceptions, byte b)
        {
            foreach (byte ex in exceptions)
            {
                if (b == ex)
                    return (true);
            }

            return (false);
        }

        public static List<byte> dealign(List<byte> bytes)
        {
            for (int i = 0; i < bytes.Count() - 1; i++)
            {
                if (bytes[i] % 2 != 0)
                {
                    bytes[i]++;
                }
                bytes[i] = (byte)((bytes[i] >> bytes[bytes.Count() - 1]));
            }

            return (bytes);
        }

        public static List<byte> add(List<byte> main, List<byte> to_add)
        {
            foreach (byte b in to_add)
            {
                main.Add(b);
            }

            return (main);
        }

        public static void generate_salt()
        {
            for (int i = 0; i < Settings.salt_size - Settings.salt.Count(); i++)
            {
                Settings.salt.Add(0x00);
            }
        }

        public static string bts(List<byte> bytes)
        {
            string buffer = "";

            foreach (byte b in bytes)
            {
                buffer += (char)b;
            }

            return (buffer);
        }
    }

    public Nhash(string key)
    {
        Settings.key = Encoding.ASCII.GetBytes(key).ToList();
        Tools.generate_salt();
    }

    public string encode(string data)
    {
        List<byte> result = new List<byte>();
        byte[] bytes = Encoding.ASCII.GetBytes(data);

        Tools.add(result, Settings.salt);
        Tools.add(result, Tools.rotate(Tools.dealign(Tools.rotate(bytes.ToList()))));

        Settings.result = compress(result);

        return (Tools.bts(Settings.result));
    }

    private List<byte> compress(List<byte> bytes)
    {
        int start = 7;
        int index = start;
        int key_index = 0;
        int iterations = 0;

        for (int i = start; i < bytes.Count; i++, index++)
        {
            if (index >= Settings.size)
            {
                index = start;
            }
            if ((i + 6) <= bytes.Count && Settings.key.Count % 2 == 0)
            {
                bytes[index] = (byte)(
                    (bytes[index] >> Settings.salt[0]) | ((bytes[index] & bytes[index + 1])) |
                    (bytes[index + 1] >> Settings.salt[1]) | ((bytes[index] & bytes[index + 2])) |
                    (bytes[index + 2] >> Settings.salt[2]) | ((bytes[index] & bytes[index + 3])) |
                    (bytes[index + 3] >> Settings.salt[3]) | ((bytes[index] & bytes[index + 4])) |
                    (bytes[index + 4] >> Settings.salt[4]) | ((bytes[index] & bytes[index + 5]))
                );
            } else
            {
                if (key_index >= Settings.key.Count)
                {
                    key_index = 0;
                    iterations++;
                }
                bytes[index] = (byte)(
                    ((bytes[index] + Settings.key[key_index] / (iterations + 2)) ^ bytes[i % bytes.Count]) |
                    ((bytes[index] >> iterations) ^ (Settings.key[key_index] >> Settings.key[0]))
                );
                key_index++;
            }
        }
        bytes.RemoveRange(Settings.size - 1, bytes.Count() - Settings.size);

        return (bytes);
    }

    public void dump(string path)
    {
        File.WriteAllBytes(path, Settings.result.ToArray());
    }
}
