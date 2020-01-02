# caesar-cipher
A tool for encrypting, decrypting and hacking the Caesar cipher 🦝

### Contents
1.  [Problem](#PROBLEM)
2.  [Development](#DEVELOPMENT)
3.  [Algorithm](#ALGORITHM)
4.  [Documentation](#DOCUMENTATION)
5.  [Testing](#TESTING)
6.  [Reflection](#REFLECTION)

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

Encrypting the message is rather simple but first requires the message to be cleaned of non-letter
characters and converted to capital letters. After the shift method is called, the encryption function uses a
for-loop to first take each letter in the message and find its index in the original alphabet. Next, the letter in
the shifted alphabet of corresponding index is appended to the encrypted message. Before returning the
finished encrypted message, the chunking method is applied.





