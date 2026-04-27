namespace Rejtlelezes
{
    internal class Program
    {
        static Dictionary<char, int> code = new Dictionary<char, int>();
        
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

        static string Encrypt(string message)
        {
            Random random = new Random();
            
            string key= "";
            int counter = 0;
            string encryptedMessage ="";
            int currentNumber = 0;
            string encryptedNumbers = "";
            string keyNumbers = "";
            foreach (var item in message)
            {
                string temp = "";
                if (item==Convert.ToChar(" "))
                {
                    key+=" ";
                    temp = " ";
                }
                else
                {
                    temp = random.Next(0, 26).ToString();
                    keyNumbers+= temp+" ";
                    key += code.FirstOrDefault(x => x.Value == int.Parse(temp.ToString())).Key;
                    counter++;
                }
                currentNumber=code.GetValueOrDefault(item)+int.Parse(temp);
                if (currentNumber>26)
                {
                    currentNumber = (code.GetValueOrDefault(item) + int.Parse(temp)) % 27;
                }
                encryptedNumbers += currentNumber+" ";
                encryptedMessage += code.FirstOrDefault(x => x.Value == int.Parse(currentNumber.ToString())).Key;

            }
            encryptedMessages.Add(key, encryptedMessage);

            /*Console.WriteLine(encryptedNumbers);
            Console.WriteLine(key);
            Console.WriteLine(keyNumbers);*/
            return encryptedMessage;
        }

        static string Decrypt(string message)
        {
            Random random = new Random();

            string decryptedMessage = "";
            int currentNumber = 0;

            foreach (var item in encryptedMessages)
            {
                if (item.Value==message)
                {
                    foreach (var item1 in message)
                    {
                        currentNumber= code.GetValueOrDefault(item1)-code.GetValueOrDefault(item.Key[message.IndexOf(item1)]);
                        if (currentNumber<0)
                        {
                            currentNumber = code.GetValueOrDefault(item1) - code.GetValueOrDefault(item.Key[message.IndexOf(item1)]) + 27;
                        }
                        decryptedMessage += code.FirstOrDefault(x => x.Value == int.Parse(currentNumber.ToString())).Key;
                    }
                }
               /* Console.WriteLine(item.Value);
                Console.WriteLine("adsdasd");
                Console.WriteLine(item.Key);*/
                
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
                Console.WriteLine(Encrypt(message));
                string dmessage = Encrypt(message);
                Console.WriteLine(Decrypt(dmessage));
            }

            

        }
    }
}
