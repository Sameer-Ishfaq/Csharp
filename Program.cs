using System.Collections;
using System.Linq;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Text;
using System.Reflection.Metadata;

namespace spaceProbe
{
    /* A space probe going to jupiter ooo saucy. It needs to send messages back to planet earth in morse code. Not everyone understands morse code. Thats why this exists.
     * NOTE: WordOut function produces the string back to front. This was not intentional.
     * This progam implements a queue data structure that can store alphanumeric characters and their corresponding Morse code representations.
     * It provides methods to enqueue and dequeue characters, peek at the next character, view the entire queue, clear the queue, check its length, and generate a hash of the queue's contents.
     * The class also includes a user menu for interacting with the queue through console input.
     * 
     * A lot of blood sweat and tears went into this (minus the blood and sweat...Mostly tears)
     */

    internal class Queue
    {
        private Queue<string> queue;
        private Dictionary<string, string> morseCodeTranslator;
       // private Timer timer;
        

        public Queue()
        {
            queue = new Queue<string>();
            morseCodeTranslator = new Dictionary<string, string>() 
            {
                { "A", ".-" }, { "B", "-..." }, { "C", "-.-." }, { "D", "-.." }, { "E", "." }, { "F", "..-." }, { "G", "--." }, { "H", "...." }, { "I", ".." },                                                     
                { "J", ".---" }, { "K", "-.-" }, { "L", ".-.." }, { "M", "--" }, { "N", "-." }, { "O", "---" }, { "P", ".--." }, { "Q", "--.-" }, { "R", ".-." }, 
                { "S", "..." }, { "T", "-" }, { "U", "..-" }, { "V", "...-" }, { "W", ".--" }, { "X", "-..-" }, { "Y", "-.--" }, { "Z", "--.." }, { "1", ".----" }, 
                { "2", "..---" }, { "3", "...--" }, { "4", "....-" },  { "5", "....." }, { "6", "-...." }, { "7", "--..." }, { "8", "---.." }, 
                { "9", "----." }, { "0", "-----" } 
            };
        }
        //DEFINE FUNCTIONS
        public void MorseIn(string morseCode) //WORKING
        //add a character to the queue
        { 
            if (morseCodeTranslator.ContainsKey(morseCode) && morseCode.Length == 1) //error handling on the other side of &&. If not met it will go straight to else statement
            {
                queue.Enqueue(morseCode.ToUpper());
                string morseEquivalent = morseCodeTranslator[morseCode];
                Console.WriteLine("|---------------|");
                Console.WriteLine("The following has been enqueued:" + " Character: " + morseCode +  " || Morse Code: " + morseEquivalent);
                Console.WriteLine("|---------------|");
            }
            else
            {
                Console.WriteLine("Only one character can be entered at a time");
            }
        }
        public static bool IsCharAlphanumeric(string str)
        {
            return str.All(char.IsLetterOrDigit);
        }
        public void CharIn(string c) //WORKINHG
                                   //Add a char to the queue
        {
            if (IsCharAlphanumeric(c) && c.Length == 1) //check for a single letter or number
            {
             string charIn = c.ToString().ToUpper();
             queue.Enqueue(charIn);
             Console.WriteLine("|---------------|");
             Console.WriteLine("You have entered the alphanumeric character: " + charIn + " into the queue.");
             Console.WriteLine("|---------------|");
            }
            else
            {
            Console.WriteLine("Only one character can be entered at a time");
            }

            
        }
        public void MorseOut() //WORKING
            //step 1: Convert it to morse code
            //step 2: Remove morse code value form queue
        {
            try
            {
               
                string morseAsCharacter = queue.Dequeue();
                if (morseCodeTranslator.ContainsKey(morseAsCharacter))
                {
                    string morseAsMorseCode = morseCodeTranslator[morseAsCharacter];
                    Console.WriteLine("|---------------|");
                    Console.WriteLine("The Character as morse code: " + morseAsMorseCode);

                    Console.WriteLine("Dequeuing morse code: " + morseAsCharacter + " / " + morseAsMorseCode);
                    Console.WriteLine("|---------------|");
                }
            }
            catch (Exception ex)
            {
                IsEmpty();
                Console.WriteLine("|---------------|");
                Console.WriteLine("Queue is already empty.");
                Console.WriteLine("|---------------|");
            }

        }
        public void CharOut() //WORKING
            // remove only the char value from queue
        {
            try
            {

                string characterAsChar = queue.Dequeue();
                Console.WriteLine("|---------------|");
                Console.WriteLine("Dequeuing alphanumeric code: " + characterAsChar);
                Console.WriteLine("|---------------|");
            }
            catch(Exception ex)
            {
                IsEmpty();
                Console.WriteLine("|---------------|");
                Console.WriteLine("Queue is already empty.");
                Console.WriteLine("|---------------|");
            }

        }
        public void MorsePeek() //WORKING
            //view next available morse code character value
        {
            if (IsEmpty()) //function is used as a condition
            {
                Console.WriteLine("|---------------------------|");
                Console.WriteLine("the queue is currently empty");
                Console.WriteLine("|---------------------------|");
            }
            if (queue.Count > 0) //Condition that must be met to execute the code within its curly braces
            {
                string oldestChar = queue.Peek();
                string morsePeekequivalent = morseCodeTranslator[oldestChar]; //getting the morse code translation of the next letter in the queue
                if (morseCodeTranslator.ContainsKey(oldestChar)) //Condition that will be fulfilled if the next letter in the queue matches .Peek()
                {
                    //this code is executed upon the fulfilment of the above if statement
                    Console.WriteLine("|--------------|");
                    Console.WriteLine("The next available morse value is: " + oldestChar + " || Morse code translation: " + morsePeekequivalent);
                    Console.WriteLine("|--------------|");
                  
                }  
                else //when the other conditions are not met, else will be executed. Always the last option to be executed
                {
                    Console.WriteLine("|--------------|");
                    Console.WriteLine( "Morse code is not next in queue.");
                    Console.WriteLine("|--------------|");
                }  
            }
        }
        public void CharPeek() //WORKING
            //view next available char
        {
            if (IsEmpty())
            {
                Console.WriteLine("|---------------|");
                Console.WriteLine("the queue is currently empty");
                Console.WriteLine("|---------------|");
            }
            else
            {
                string charPeek = queue.Peek();
                Console.WriteLine("The next available alphanumeric value is: " + charPeek);
            }
        }
        public void PeekAll() //WORKING
            //views the entire queue
        {
            Console.WriteLine("\n Current Queue: ");
            Console.WriteLine("|---------------|");
            if (queue.Count > 0)
            {
                Console.WriteLine("Queue elements:");
                foreach (string item in queue)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("|---------------|");
                Console.WriteLine("Queue is currently empty");
                Console.WriteLine("|---------------|");
            }
            Console.WriteLine("|---------------|");
        }
        public void Flush() //WORKING
            //Removes all contents from the queue
        {
            Console.WriteLine("|-------------------------|");
            Console.WriteLine("The queue has been cleared");
            Console.WriteLine("|-------------------------|");
            queue.Clear();
            
        }
        public void Length() //WORKING
            //Returns the entire queue to be viewed by the user
        {
            int elements = queue.Count();
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("The number of elements in this queue: " + elements);
            Console.WriteLine("|--------------------------------------|");

        }
        public bool IsEmpty() //WORKING

        {
            return queue.Count == 0;
        }
        public static string Hash(string input) //WORKING
             //Cryptographic hash function that produces a 128bit hash value to ensure data has not been corrupted or altered.
        {
            StringBuilder hash = new StringBuilder();
            HMACMD5 md5Provider = new HMACMD5(); //will create a randomly generated key 
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++) //loops through the array and encodedes the string which will be in the queue.
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public string WordOut() //WORKING
            //return the oldest characters following the newest characters to make a word e.g., 'olleh' in the queue would return hello
        {
            if (IsEmpty())
            {
                Console.WriteLine("|---------------|");
                Console.WriteLine("The queue is empty");
                Console.WriteLine("|---------------|");
            }
            StringBuilder wordbuilder = new StringBuilder(); //build the word
            while (queue.Count > 0)
            {
                string currentcharacter = queue.Peek();
                if (currentcharacter == " ")
                {
                    queue.Dequeue();
                }
                wordbuilder.Append(queue.Dequeue());
            }
            string word = wordbuilder.ToString(); //convert string builder to string
            char[] wordArray = word.ToCharArray(); //Word is then converted to a char[]
            Array.Reverse(wordArray); //characters are reversed
            string reversedWord = new string(wordArray);//wordArray is converted back to a string

           /* char[] charArray = reversedWord.ToCharArray();
            Array.Reverse(charArray);
            string wordOut = new string(charArray);     IGNORE THIS COMMENT*/

            Console.WriteLine("|---------------|");
            Console.WriteLine("WordOut has produced: " +  reversedWord);
            Console.WriteLine("This queue has been cleared");
            Console.WriteLine("|---------------|");
            return reversedWord; //returned
                
        }
        public void Menu()
            //user selection menu
        {
            
            bool exit = false; //exit 

            while (!exit)
            {
                Console.WriteLine("Select an operation");
                Console.WriteLine("1. Add morse code");
                Console.WriteLine("2. Add alphanumeric character");
                Console.WriteLine("3. Remove morse code");
                Console.WriteLine("4. Remove alphanumeric character");
                Console.WriteLine("5. Find the next value as morse code");
                Console.WriteLine("6. Find the next value as an alphanumeric character");
                Console.WriteLine("7. View all values");
                Console.WriteLine("8. View current number of elements in the queue");
                Console.WriteLine("9. Clear entire queue");
                Console.WriteLine("10. Print oldest characters (This will also clear the queue)");
                Console.WriteLine("11. Print hash value");
                Console.WriteLine("12. Exit ");
                Console.WriteLine("Enter your choice");
                Console.WriteLine("|---------------------------------------------------|");
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": //MorseIn
                        Console.WriteLine("|--------------------------------|");
                        Console.WriteLine("Enter a Character into the Queue");
                        Console.WriteLine("|--------------------------------|");
                        string? morseEnqueue = Console.ReadLine();
                            MorseIn(morseEnqueue.ToUpper());
                        break;
                    case "2": //CharIn
                        Console.WriteLine("|--------------------------------------------|");
                        Console.WriteLine("Enter an alphanumeric character into the queue");
                        Console.WriteLine("|--------------------------------------------|");
                        string charEnqueue = Console.ReadLine();
                        CharIn(charEnqueue.ToUpper());
                        break;
                    case "3": //MorseOut
                        MorseOut();
                        break;
                    case "4": //CharOut
                        CharOut();
                        break;
                    case "5": //MorsePeek
                      MorsePeek();
                        break;
                    case "6": //CharPeek
                      CharPeek();
                        break;
                    case "7": //PeekAll
                        if (IsEmpty())
                        {
                            Console.WriteLine("Queue is already empty");
                        }
                        else
                        {
                            
                            Console.WriteLine("|---------------|");
                            Console.WriteLine("Peeking both values");
                            PeekAll();
                            
                        }
                        break;
                    case "8": //Length
                        Length();
                        break;
                    case "9": //Flush
                        Console.WriteLine("|---------------|");
                        Console.WriteLine("Clearing queue");
                        Console.WriteLine("|---------------|");
                        Flush();
                        break;
                    case "10": //WordOut
                        WordOut();
                        break;
                    case "11": //Hash
                        string? hashQueue = queue.ToString();
                        string hash = Hash(hashQueue);
                        Console.WriteLine("|--------------------------------------|");
                        Console.WriteLine("The MD5 hash of the queue is: " + hash);
                        Console.WriteLine("|--------------------------------------|");
                        break;
                    case "12": //Exit
                        Console.WriteLine("Exiting...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("|---------------|");
                        Console.WriteLine("Invalid option. Please try again");
                        Console.WriteLine("|---------------|");
                        break;
                }


            } 
        }
        static void Main(string[] args)
        {
            Queue queue = new Queue(); 
            Console.WriteLine("Hello. What's your name?");
            string? name = Console.ReadLine();

            Console.WriteLine("Hello " + name);
            Console.WriteLine("|---------------------------------------------------|");
            Console.WriteLine("Welcome to space probe. Select any letter or number to start.");
     

            
            queue.Menu();
            
           

            Console.WriteLine("END OF PROGRAM");
            Console.ReadKey();
        }
    }
}