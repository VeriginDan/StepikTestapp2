//Напишите программу целиком.
//Не забудте подключить необходимые
//пространства имён.
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Principal;

public class MainClass
{
    static void Main(string[] args)
    {
        List<string[]> arrayInput = readInput();
        for (int i = 0; i < arrayInput.Count; i++)
            if (NoobDb.SearchByEmail(arrayInput[i][2]) == null)
                NoobDb.Add(new Account(arrayInput[i][0], arrayInput[i][1], arrayInput[i][2]));

        NoobDb.PrintAll();
    }

    public static List<string[]> readInput()
    {
         string input;
         string[] arrayInput;
         List<string[]> arrayReturn = new List<string[]>();
         
        while((input = Console.ReadLine()) != "end")// cycle until input string is not end
        {
            if (isCorrect(input, 18)) 
                // check whole string on essential elements, min 4 chars for name, 6 for pass , 6 for email, 2 spaces
            arrayInput = input.Split(" ");
            //in ideal case it gives array of 3 elements - name pass email
            else continue;
            
            if ((arrayInput.Length) != 3) continue; 
            // if there are not 3 elements than there is odd spaces because of spaces in name || pass || email or input misses name || pass || email
            if (isCorrect(arrayInput[0], 4))
                if (isCorrect(arrayInput[1], 6) && isPasswordCorrect(arrayInput[1]))
                    if (isEmailCorrect(arrayInput[2]) && isCorrect(arrayInput[2], 6))
                        arrayReturn.Add(arrayInput);
                  
        }
        return arrayReturn;
    }

    public bool isCorrect(string input, int expectedLength)
    {
        if (String.IsNullOrWhiteSpace(input)) return false;// input should be not null or spaces
        if (input[0] == ' ') return false;
        if (input.Length < expectedLength) return false; // input must have specific minimum legth
        return true;
    }
    
    public bool isEmailCorrect(string email) => Regex.IsMatch(email,
                                                      @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                                                      RegexOptions.IgnoreCase);

    public bool isPasswordCorrect(string password) => Regex.IsMatch(password,
                                                      @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])).{6,}",
                                                      RegexOptions.IgnoreCase);
}