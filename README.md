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

