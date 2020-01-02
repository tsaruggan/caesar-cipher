using System;
using System.IO;
using System.Text.RegularExpressions;

namespace caesar_cipher
{
    class Program
    {
        public static String[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public static String[] shiftedAlphabet = new String[26];
        const String wordListFileName = "words.txt";

        private static void shift(int key)
        {
            // if key shifts alphabet one or more times, simplify to shift less than 26 char
            if (key > 26 || key < -26) { key = key % 26; }

            // if key is negative, simplify to positive
            if (key < 0) { key = 26 + key; }

            int current = key; // current is index of the starting char wanted for new alphabet
            for (int i = 0; i < alphabet.Length; i++)
            {
                shiftedAlphabet[i] = alphabet[current]; // set characters for new alphabet
                current++;
                if (current > 25) { current = 0; }// if end of alphabet is reached, reset index back to 'A'
            }
        }

        public static String encrypt(String message, int key)
        {
            String encryptedMsg = ""; // initialize encrypted message
            message = Regex.Replace(message, "[^a-zA-Z]", "").ToUpper();  // remove all non letter characters from string +  make upper case
            shift(key); // applies shift to alphabet by given key

            // for each letter in message, swap with corresponding letter in new alphabet
            for (int x = 0; x < (message.Length); x++)
            {
                int index = 0; // index value of original alphabet
                bool check = false;

                // loops until the index of the letter in original alpha is found for single letter in message
                while (!check)
                {
                    String currentLetter = message[x].ToString(); // hold current letter of message
                    if (string.Compare(currentLetter, alphabet[index]) == 0)
                    { // compare if letter in message matches letter in alpha of current index
                        check = true; // if found, break loop
                        encryptedMsg = encryptedMsg + shiftedAlphabet[index]; // add the letter from the corresponding index of new alpha
                    }
                    else
                    {
                        index++; // if not found, add to index and check again
                    }
                }
            }
            return encryptedMsg;
        }

        public static String decrypt(String message, int key)
        {
            String decryptedMsg = ""; // initialize decrypted message
            message = Regex.Replace(message, "[^a-zA-Z]", "").ToUpper();  // remove all non letter characters from string +  make upper case
            shift(key); // applies shift to alphabet by given key

            // for each letter in encrypted message, swap with corresponding letter in original alpha
            for (int x = 0; x < (message.Length); x++)
            {
                int index = 0; // index value of new alphabet
                bool check = false;

                // loops until the index of the letter in new alpha is found for single letter in encrypted message
                while (check == false)
                {
                    String currentLetter = message[x].ToString(); // hold current letter of encrypted message
                    if (string.Compare(currentLetter, shiftedAlphabet[index]) == 0)
                    {
                        check = true; // if found, break loop
                        decryptedMsg = decryptedMsg + alphabet[index]; // add the letter from corresponding index of original alpha
                    }
                    else
                    {
                        index++; // if not found, add to index and check again
                    }
                }
            }
            return decryptedMsg;
        }

        public static String hack(String encryptedMsg)
        {
            String hackedMsg = ""; // initialize hacked message
            int topMatches = 0; // the most words found within a decrypted message

            // for each possible message, check the number of real words exist it contains and compare
            for (int key = 0; key < 26; key++)
            {
                int matches = 0; // number of match points for current decrypted message
                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(Path.Combine(Environment.CurrentDirectory, wordListFileName));
                    String word; // read from file of over 58 000 english words

                    // loop for every word in .txt file
                    while ((word = file.ReadLine()) != null)
                    {
                        // check if decrypted msg contains word
                        if (decrypt(encryptedMsg, key).Contains(word)) { matches += word.Length; } // if word exists, award match points depending on length of matching word  
                    }

                    if (matches > topMatches)
                    { // if word has most match points, becomes the most probable message
                        topMatches = matches;
                        hackedMsg = decrypt(encryptedMsg, key); // set hacked message to word with top matche points
                    }

                }
                catch (System.IO.FileNotFoundException)
                {
                }
            }
            return hackedMsg;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("WELCOME TO CAESAR CIPHER\n\nEnter 1 for encrypt...\nEnter 2 for decrypt...\nEnter 3 for hack...\nEnter 4 for testing...\n\n");
            int input;
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (input)
                {
                    case 1:
                        Console.WriteLine("Input message to encrypt: ");
                        String message = Console.ReadLine();
                        Console.Clear();
                        // ensures message contains at least a single english letter
                        if (Regex.IsMatch(message, ".*[a-zA-Z]+.*") == true)
                        {
                            // prompts user for key
                            Console.WriteLine("Input key: ");
                            int keyE = 0;

                            try
                            { // checks if key is a valid integer
                                keyE = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                            }
                            catch (Exception e)
                            { // if key is invalid, make key equal 0 and show error message
                                Console.WriteLine("Error. key = 0");
                                keyE = 0;
                            }

                            // perform encryption and display on console
                            String encrypted = encrypt(message, keyE);
                            Console.WriteLine("Encrypted message: " + encrypted);
                        }
                        else
                        { // if message contains no real letters, display error message
                            Console.WriteLine("Error. Must enter letters from the alphabet.\n\nRestart");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Input message to decrypt: ");
                        String userInputEncryptedMsg = Console.ReadLine();
                        Console.Clear();
                        if (Regex.IsMatch(userInputEncryptedMsg, ".*[a-zA-Z]+.*") == true)
                        {
                            // prompts user for key
                            Console.WriteLine("Input key: ");
                            int keyD = 0;

                            try
                            { // checks if key is a valid integer
                                keyD = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                            }
                            catch (Exception e)
                            { // if key is invalid, make key equal 0 and show error message
                                Console.WriteLine("Error. key = 0");
                                keyD = 0;
                            }

                            // perform decryption and display on console
                            String decryptedMsg = decrypt(userInputEncryptedMsg, keyD);
                            Console.WriteLine("Decrypted message:" + decryptedMsg);

                        }
                        else
                        { // if userInputEncryptedMsg contains no real letters, display error message
                            Console.WriteLine("Error. Invalid encrypted message.\n\nRestart");
                        }
                        break;
                    case 3:
                        // prompt for encrypted message
                        Console.WriteLine("Input encrypted message: ");
                        String encryptedMsg = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("please wait one moment... \n\n"); // display wait message as hacking may take tim
                        String hackedMsg = hack(encryptedMsg); // call for hacking function
                        Console.WriteLine("Hacked message: " + hackedMsg); // output hacked message
                        break;
                    case 4:
                        //prompt for testing
                        Console.WriteLine("Enter file directory of testing messages (.txt):  ");
                        String inputFilename = Console.ReadLine();
                        Console.WriteLine("Enter filename to save test results as (.xlsx):  ");
                        String outputFilename = Console.ReadLine();
                        Testing.test(inputFilename, outputFilename);
                        break;
                    default:
                        throw new Exception("Not a valid input. Must enter 1, 2, 3 or 4. Please restart.");
                }

            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("There was an error handling your input. Please restart.");
            }
        }
    }
}
