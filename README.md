# caesar-cipher
A tool for encrypting, decrypting and hacking the Caesar cipher 🦝

### Contents
1.  [Problem](#PROBLEM)
2.  [Development](#DEVELOPMENT)
3.  [Algorithm](#ALGORITHM)
4.  [Testing](#TESTING)
5.  [Reflection](#REFLECTION)

### Problem <a name = "PROBLEM"></a>
The Caesar Cipher is one of the earliest forms of encryption named after Julius Caesar who
used the method to disguise military messages. A string of text is encrypted by shifting the
alphabet by a number of letters, the key, and substituting the letters in the message for the
corresponding letters of the transformed alphabet. Decrypting the message involves a reversal
of this process but hacking it without knowing the key becomes rather complex and ambiguous
as words may exist in all possible cases.

**Goal:** Create a program that allows the user to encrypt and decrypt a message in addition to
hacking an encrypted message of an unknown key. Allow for testing of the hacking algorithm and export data to an excel file.

### Development <a name = "DEVELOPMENT"></a>
**Timeline:** 

| Task | Description | Hours |
|--------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------|
| Setup outline | -made notes and pseudo code for program<br>-declared required methods and return values<br>-prompted for user input and output | 0.5 |
| Programmed shift + encrypt/decrypt methods | -used loops and array to shift elements<br>-took user input and returned encrypted string<br>-handling for negative keys and large keys | 2 |
| Hacking | -read from file to comb through list of English words to<br>detect matches for all possible cases<br>-assessment of case with greatest probability<br>-found ways to use less loops, yet same outcome | 6 |
| Testing/Debugging | -conducted timed efficiency test and effectiveness for<br>hacking method using strings of varying length on text<br>file and outputted data into Excel file<br>-removed logic errors that were always assessing<br>hacked cases containing small words as correct | 12 |
| Error Handling/User Interface | -used try-catch and occasional conditional statements to<br>determine if input was valid or error was thrown<br>-worked on bettering user experience in console | 10 |
| Documentation/Report | -comments, API, and explanations to code<br>-graphed data, formatted report and reflected on project | 6 |

**Analysis:**  
The problem can be broken down into three parts, encryption/decryption, hacking and testing.

*Encryption/Decryption*
- How can I use a loops and arrays to shift the alphabet by a key?
- What is the easiest way to compare letters in the message to corresponding alphabet?
- How do I avoid IndexOutOfBounds errors when checking each letter?
- Is decryption method the same as encryption but switching alphabet?
- Is it possible to add spaces between the words in the decrypted message?

*Hacking*
- How do I check if a string contains a word?
- Is it necessary to check all possible cases?
- How do I assess the correct solution if multiple cases contain words?

*Testing*
- How do I read from a .txt file?
- What library allows to format and export data into an Excel file?
- How to check if hacking successfully decrypted the unknown message?  

### Algorithm <a name = "ALGORITHM"></a>  
**Shift:**  
The shift function involves first taking the key parameter and ensuring it is within range of 0-26. A
negative shift is equivalent to a positive shift by adding 26 whereas a large number shift can be simplified
by dividing by 26 and finding the remainder. The shift algorithm uses a for-loop to assign values to the
global array holding the shifted alphabet; the key is used as a marker for the current position in the
original alphabet and when it reaches the end, the marker is reset to 0, ‘A’ and continues.  

```C#
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
```  
**Encryption/Decryption:**  
Encrypting the message is rather simple but first requires the message to be cleaned of non-letter
characters and converted to capital letters. After the shift method is called, the encryption function uses a
for-loop to first take each letter in the message and find its index in the original alphabet. Next, the letter in
the shifted alphabet of corresponding index is appended to the encrypted message.  

```C#  
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
```  
The decryption method follows the same routine except the roles of the original alphabet array and the
shifted alphabet array are swapped. The loop takes a letter in the message and finds its index in the
shifted alphabet then appends the corresponding letter from the original alphabet to the decrypted
message.  

**Hacking:**  
Hacking an encrypted message involves checking each of the 26 possible decrypted cases and
assessing the most probable message. The hacking method applies a nested loop to first decrypt each
case and then comb through the a text file containing a word list of 58,000 + words and see if the message contains each
word and award points based on the length of the containing word.  

```C#  
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
```  
The message with the most matches is returned as the most probable message. Each of the 26 cases
performs over 58 000 evaluations resulting in a total over 1.5 million evaluations making the software
somewhat slow.  

### Testing <a name = "TESTING"></a>
**Shifitng Accuracy:**  
To test the shift function, the key was taken as input and outputted the transformed alphabet. Also tested
extreme cases of negative keys and large keys and was still effective.  

```  
Input: 2
Output: [C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, A, B]

Input: -4
Output: [W, X, Y, Z, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V]

Input: 30
Output: [E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, A, B, C, D]  
```  
**Encryption/Decryption Accuracy:**  
To test the encryption and decryption functions, a message and key were taken as input and outputted the encrypted message followed by the decrypted message. The testing cases used were made to sure to experiment with all letters and punctuation to be cleaned. The shifted alphabet was also outputted as a reference.  
```  
Input: 
Message: The quick brown fox jumps over the lazy dog
Key: 4

Output: 
[E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, A, B, C, D]
Encrypted message:XLIUYMGOFVSARJSBNYQTWSZIVXLIPEDCHSK
Decrypted message:THEQUICKBROWNFOXJUMPSOVERTHELAZYDOG

Input: 
Message: THe ROMANS ARE INVADING! SEND 100 MEN!
Key: 100

Output: 
[W, X, Y, Z, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V]
Encrypted message:PDANKIWJOWNAEJRWZEJCOAJZIAJ
Decrypted message:THEROMANSAREINVADINGSENDMEN

Input: 
Message: C1L$E0O?P&A%T8R#A
Key: -1

Output: 
[Z, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y]
Encrypted message:BKDNOZSQZ
Decrypted message:CLEOPATRA  
```  
**Effectiveness of Hacking:**  
Testing of the hacking method involves inputting an encrypted phrase and outputting the most probable decryption without knowing the key. We can first encrypt a phrase and then input it to ensure the function is checking all possible cases.  
```  
Message: Chicken and Waffles
Encrypted Message: MRSMUOXKXNGKPPVOC

Input: MRSM UOXKXNG KPPVOC
Output: CHICKENANDWAFFLES

Message: computer science
Encrypted Message: IUSVAZKXYIOKTIK

Input: IU SVAZK XYIOKTIK
Output: COMPUTERSCIENCE  
```  
Now we can stop encrypting input first and experiment with recognizing words in more complex strings. Surprisingly, the method detects unique proper nouns like names perhaps because they are composed of smaller, real words. If it outputs the same phrase, then we know it recognizes it as the most probable case.  
```  
Input: DWALIN BALIN KILI FILI DORI NORI ORI OIN GLOIN BIFUR BOFUR BOMBUR AND THORIN
Output: DWALINBALINKILIFILIDORINORIORIOINGLOINBIFURBOFURBOMBURANDTHORIN

Input: QUANTUM ENTANGLEMENT
Output: QUANTUMENTANGLEMENT  
```  
However, this hacking technique is occasionally flawed when it the true encrypted message is an unusual string or the odd chance that another case retrieves more points.The input is not recognized as the most probable case and instead outputs another case that we can reasonably understand being likely since it contains real words.  
```  
Input: CLEOPATRA
Output:VEXHITMKT

Input: SZECHUAN SAUCE
Output:YFKINAGTYGAIK  
```  
**Accuracy/Efficiency of Hacking**  
Testing the efficiency of the hacking method involved creating another Testing class that would encrypt phrases from a text file, keep record of the time it takes to hack it and then outputting the results in a tabular formatted Excel spreadsheet. The input is accepted as a list of words/phrases in a text file (.txt) and using the *EPPlus* library for C# to format and output testing results into Excel (.xlsx).  
```C#  
            List<String> testMessagesList = loadTestMessages(inputFilename); //store cleaned messages from given text file (.txt)
            String[,] results = new String[testMessagesList.Count,3]; // create multidimensional string array to hold results

            Random random = new Random();
            Stopwatch stopwatch = new Stopwatch();

            Console.Clear();
            Console.WriteLine("Please wait until testing is complete...");

            for (int index  = 0; index < testMessagesList.Count; index++) //for each message 
            {
                String message = testMessagesList[index]; //store current message 
                String length = message.Length.ToString(); //store length of current message

                int key = random.Next(1, 27); //generate random key
                String encrypted = Program.encrypt(message, key); //encrypt current message using key

                stopwatch.Start(); //start stopwatch
                String hacked = Program.hack(encrypted); //hack encrypted message
                stopwatch.Stop(); //stop stopwatch

                String time = Math.Round(stopwatch.Elapsed.TotalSeconds, 2).ToString(); //store time in seconds
                stopwatch.Reset(); //reset timer

                String success = (message == hacked).ToString(); //check if hack was succesful and store boolean

                results[index, 0] = length;
                results[index, 1] = time;
                results[index, 2] = success;
            }
            exportToExcel(results, outputFilename); //export results to excel
            Console.WriteLine($"Testing is complete. Results saved as: {outputFilename}");  
```  
This is the resulting generated excel file. Successful hacks are filled green while unsuccessful hacks are filled red.  

![Excel spreadsheet of results](https://github.com/tsaruggan/caesar-cipher/blob/master/assets/img1.png)  

It is notable that the longer the message, the more likely the hack to be successful in addition to having a linear relationship with time to hack and length of message. This is better illustrated with a graph.  

![Graph of results](https://github.com/tsaruggan/caesar-cipher/blob/master/assets/img2.png)  

The algorithm takes **linear time** therefore it has a time-complexity of **O(n)**. 

### Reflection <a name = "REFLECTION"></a>  

In conclusion, the project was a success and met, if not passed all requirements. Some of the features
that make my software unique from other programs is its robust design against throwing system errors,
user navigation using switch statements, ability to test hacking for time efficiency and export results to an organized Excel file.  

Some of the recent updates made were to create methods that would validate user input before passing
values into the functional methods. This allowed for a more robust user experience as error messages
specifically state what was wrong with the user input and directed the user to restart. After experimenting with shorter word lists I realized that my algorithm wasn't causing the long wait but rather the length of the word list itself. 

This project introduced me to some new concepts in C# and explore advanced programming
techniques. This was my first time producing a class strictly for testing purposes and allowed me to create
a template for data to be presented; this experience was invaluable as the technique proves to be used in
the software engineering industry for testing software. Although I have experience in reading from an
external file, I learned to create a method that would write to an Excel spreadsheet. In addition, this project was a
refresher on key C# principles like string regex, arrays and loop structures.  

Overall, the Caesar Cipher task was an excellent learning opportunity and I am excited to work on future
projects with string manipulation.



















