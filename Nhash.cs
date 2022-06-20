public class Nhash
{
    public class Settings
    {
        public static Random random = new Random();
        public static List<byte> salt = new List<byte>()
        {
            0x7b, 0x6e, 0x63, 0x6f, 0x72, 0x65, 0x7d
        };
        public static int size = 256;
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
            for (int i = 0; i < 512 - Settings.salt.Count(); i++)
            {
                Settings.salt.Add((byte)Settings.random.Next(33, 126));
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

    public Nhash()
    {
        Tools.generate_salt();
    }

    public string encode(string data)
    {
        List<byte> result = new List<byte>();
        byte[] bytes = Encoding.ASCII.GetBytes(data);

        Tools.add(result, Settings.salt);
        Tools.add(result, compress(Tools.rotate(Tools.dealign(Tools.rotate(bytes.ToList())))));

        return (Tools.bts(result));
    }

    private List<byte> compress(List<byte> bytes)
    {
        for (int i = (Settings.salt.Count); i < bytes.Count; i++)
        {
            if ((i + 5) <= bytes.Count)
            {
                bytes[i] = (byte)((bytes[i] >> i) | (bytes[i] << 0x6e));
                bytes[i + 1] = (byte)((bytes[i] >> i - (i + 1)) | (bytes[i] << 0x63));
                bytes[i + 2] = (byte)((bytes[i] >> i - (i + 2)) | (bytes[i] << 0x6f));
                bytes[i + 3] = (byte)((bytes[i] >> i - (i + 3)) | (bytes[i] << 0x72));
                bytes[i + 4] = (byte)((bytes[i] >> i - (i + 4)) | (bytes[i] << 0x65));
            } else
            {
                bytes[i] = (byte)(bytes[i] ^ bytes[i % bytes.Count]);
            }
        }

        return (bytes);
    }
}