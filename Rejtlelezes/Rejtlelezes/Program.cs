namespace Rejtlelezes
{
    internal class Program
    {
        static Dictionary<char, int> code = new Dictionary<char, int>();
        
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

        static string GenerateKey(string message)
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

            Console.WriteLine(encryptedNumbers);
            Console.WriteLine(key);
            Console.WriteLine(keyNumbers);
            return encryptedMessage;
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
                string message = Console.ReadLine();
                Console.WriteLine(GenerateKey(message));
            }

            

        }
    }
}
