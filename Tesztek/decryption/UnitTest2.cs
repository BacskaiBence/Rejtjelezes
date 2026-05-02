namespace Tesztek;

public class UnitTest2
{
    static Dictionary<char, int> code = new Dictionary<char, int>();
    static string abc = "abcdefghijklmnopqrstuvwxyz ";

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

    public static string Encrypt(string message, string key)
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

    public static string Decrypt(string message, string key)
    {
        string decryptedMessage = "";
        int currentNumber = 0;

        for (int i = 0; i < message.Length; i++)
        {
            currentNumber = (code.GetValueOrDefault(message[i]) - code.GetValueOrDefault(key[i]) + 27) % 27;
            decryptedMessage += abc[currentNumber];
        }

        return decryptedMessage;
    }

    [Fact]
    public void Upload()
    {
        dataUpload();
    }
    [Fact]
    public void Good()
    {
        string message = "alma fa";
        string key = "vbgdhtk";

        Console.WriteLine($"SUCCESS: {Decrypt(Encrypt(message, key), key)}");
    }
    [Fact]
    public void Bad()
    {
        string message = "alma fa";
        string key = "sdscv";

        Console.WriteLine($"FAIL: {Decrypt(Encrypt(message, key), key)}");
    }
}

