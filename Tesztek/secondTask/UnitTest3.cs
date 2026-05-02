namespace Tesztek;

public class UnitTest2
{
    static Dictionary<char, int> code = new Dictionary<char, int>();
    static string abc = "abcdefghijklmnopqrstuvwxyz ";

    static HashSet<string> words = new HashSet<string>();
    static List<string> words_backwards = new List<string>();

    static void dataUpload()
    {
        StreamReader sr = new StreamReader("data.txt");
        int counter = 0;
        while (!sr.EndOfStream)
        {
            code.Add(char.Parse(sr.ReadLine()), counter);
            counter++;
        }
    }

    static void wordsUpload()
    {
        StreamReader sr = new StreamReader("words.txt");
        while (!sr.EndOfStream)
        {
            words.Add(sr.ReadLine().Trim().ToLower());
        }
        words_backwards = words.OrderByDescending(x => x.Length).ToList();
    }

    static string Encrypt(string message, string key)
    {
        string encryptedMessage = "";
        int currentNumber = 0;

        for (int i = 0; i < message.Length; i++)
        {
            currentNumber = (code.GetValueOrDefault(message[i]) + code.GetValueOrDefault(key[i])) % 27;
            encryptedMessage += abc[currentNumber];
        }

        return encryptedMessage;
    }

    static string Decrypt(string message, string key)
    {
        string decryptedMessage = "";
        int currentNumber = 0;
        if (key.Length >= message.Length)
        {
            for (int i = 0; i < message.Length; i++)
            {
                currentNumber = (code.GetValueOrDefault(message[i]) - code.GetValueOrDefault(key[i]) + 27) % 27;
                decryptedMessage += abc[currentNumber];
            }
        }


        return decryptedMessage;
    }

    static string KeyBroker(string message, string encrypted)
    {
        string key = "";
        int currentNumber = 0;

        for (int i = 0; i < message.Length; i++)
        {
            currentNumber = (code.GetValueOrDefault(encrypted[i]) - code.GetValueOrDefault(message[i]) + 27) % 27;
            key += abc[currentNumber];
        }

        return key;
    }

    static string Solver(string e1, string e2, int index, string fullKey)
    {
        if (index == e1.Length)
        {
            return fullKey;
        }

        foreach (var item in words_backwards)
        {
            string currentWord = item;
            if (index + currentWord.Length > e1.Length)
            {
                continue;
            }
            if (index + currentWord.Length < e1.Length)
            {
                currentWord += " ";
            }

            string part_e1 = e1.Substring(index, currentWord.Length);
            string possibleKey = KeyBroker(currentWord, part_e1);
            string part_e2 = e2.Substring(index, currentWord.Length);
            string possible_m2 = Decrypt(e2.Substring(0, index + currentWord.Length), fullKey + possibleKey);

            if (IsMessageValid(possible_m2, e2.Length))
            {
                string result = Solver(e1, e2, index + currentWord.Length, fullKey + possibleKey);
                if (result != null)
                {
                    return result;
                }
            }
        }
        return null;
    }

    static bool IsMessageValid(string message, int length)
    {
        string[] wrds = message.Split(' ');

        for (int i = 0; i < wrds.Length; i++)
        {
            string wrd = wrds[i];
            if (string.IsNullOrEmpty(wrd))
            {
                continue;
            }

            if (i < wrds.Length - 1 || message.Length == length)
            {
                if (!words.Contains(wrd))
                {
                    return false;
                }
            }
            else
            {
                if (!words.Any(x => x.StartsWith(wrd)))
                {
                    return false;
                }
            }
        }
        return true;
    }


    [Fact]
    public void Upload()
    {
        dataUpload();
        wordsUpload();
    }
    [Fact]
    public void Good()
    {
        string m1 = "curiosity killed the cat";
        string m2 = "early bird catches the worm";
        int diff = Math.Abs(m1.Length - m2.Length);
        string key = "";
        for (int i = -diff; i < m1.Length; i++)
        {
            Random rnd = new Random();
            int num = rnd.Next(0, 27);
            key += abc[num];
        }
        string e1 = Encrypt(m1, key);
        string e2 = Encrypt(m2, key);
        Console.WriteLine(e1);
        Console.WriteLine(e2);
        Console.WriteLine(Decrypt(e1, key));
        Console.WriteLine(Decrypt(e2, key));
        string foundKey = Solver(e1, e2, 0, "");
        Console.WriteLine(foundKey);
    }
    [Fact]
    public void Bad()
    {
        string m1 = "curiosity killed the cat";
        string m2 = "early bird catches the worm";
        string key = "";
        string e1 = Encrypt(m1, key);
        string e2 = Encrypt(m2, key);
        Console.WriteLine(e1);
        Console.WriteLine(e2);
        Console.WriteLine(Decrypt(e1, key));
        Console.WriteLine(Decrypt(e2, key));
        string foundKey = Solver(e1, e2, 0, "");
        Console.WriteLine(foundKey);

    }
}

