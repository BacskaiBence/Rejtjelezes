using System.Security;

namespace Rejtlelezes
{
    internal class Program
    {
        static Dictionary<char, int> code = new Dictionary<char, int>();
        static string abc = "abcdefghijklmnopqrstuvwxyz ";


        static Dictionary<string,string> encryptedMessages = new Dictionary<string, string>();
        static void dataUpload()
        {
            StreamReader sr = new StreamReader("data.txt");
            int counter = 0;
            while (!sr.EndOfStream)
            {
                code.Add(char.Parse(sr.ReadLine()),counter);
                counter++;
            }
        }

        static string Encrypt(string message, string key)
        {
            string encryptedMessage ="";
            int currentNumber = 0;

            for (int i = 0; i < message.Length; i++)
            {
                currentNumber = (code.GetValueOrDefault(message[i]) + code.GetValueOrDefault(key[i])) %27;
                encryptedMessage += abc[currentNumber];
            }

            return encryptedMessage;
        }

        static string Decrypt(string message, string key)
        {
            string decryptedMessage = "";
            int currentNumber = 0;

            for (int i = 0; i < message.Length; i++)
            {
                currentNumber = (code.GetValueOrDefault(message[i]) - code.GetValueOrDefault(key[i])+27)%27;
                decryptedMessage += abc[currentNumber];
            }
                
            return decryptedMessage;
        }


        static void Main(string[] args)
        {
            dataUpload();

            //string message = "almaz"; -> 0 11 12 0 25
            //string key = "fgdsn"; -> 5 6 3 18 13
            // 5 17 15 18 38%27-> 11
            //string encrypted = "frpsl";

            while (true)
            {
                Console.WriteLine("Irj egy uzenetet: ");
                string message = Console.ReadLine().ToLower();
                Console.WriteLine("Ird be a kulcsot: ");
                string key = Console.ReadLine().ToLower();
                encryptedMessages.Add(key, Encrypt(message, key));
                Console.WriteLine(Encrypt(message,key));
                string dmessage = Encrypt(message, key);
                Console.WriteLine("Ird be a megoldo kulcsot: ");
                string dkey = Console.ReadLine().ToLower();
                Console.WriteLine(Decrypt(dmessage,dkey));
            }

            

        }
    }
}
