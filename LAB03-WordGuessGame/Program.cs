using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LAB03_WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            TitlePage();
        }
        static void TitlePage()
        {
            try
            {
                Console.WriteLine("Lets play the word game!");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("----------");
                Console.WriteLine("1. New game");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Exit");
                Console.WriteLine("----------");
                Console.WriteLine("Please make a selection");
                string selection = Console.ReadLine();
                int option = Convert.ToInt32(selection);
                switch (option)
                {
                    case 1:
                        NewGame();
                        break;
                    case 2:
                        Admin();
                        break;
                    case 3:
                        Console.WriteLine("Thank you for playing Word Game!");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("I didnt understand your input, please make a selection.");
                        TitlePage();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.Clear();
                Console.WriteLine("Im sorry, we did not understand your selection. Please try again.");
                TitlePage();
            }
        }
        static void NewGame()
        {
            RemoveAllGuesses();
            string[] wordList = ReadFromList();
            string randomWord = ChooseAWord(wordList);
            GameSetup(randomWord);
        }
        static void RemoveAllGuesses()
        {
            string path = "../../../Guesses.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine();
            }
        }
        static string[] ReadFromList()
        {
            string path = "../../../Words.txt";

            using (StreamReader sr = File.OpenText(path))
            {
                string[] words = File.ReadAllLines(path);
                return words;
            }
        }
        static string ChooseAWord(string[] wordList)
        {
            Random rnd = new Random();
            int wordIndex = rnd.Next(wordList.Length);
            string randomWord = wordList[wordIndex];
            string upperRandomWord = randomWord.ToUpper(new CultureInfo("en-US", false));
            return upperRandomWord;
        }
        static void GameSetup(string randomWord)
        {
            Console.Clear();
            Console.WriteLine("Lets Play!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            char[] letterArray = randomWord.ToCharArray();
            char[] blankArray = new char[letterArray.Length];
            for (int i = 0; i < blankArray.Length; i++)
            {
                char blank = Convert.ToChar("_");
                blankArray[i] = blank;
            }
            Console.WriteLine($"Your Word is {blankArray.Length} letters long.");
            YourWord(letterArray, blankArray);
        }
        static void YourWord(char[] letterArray, char[] blankArray)
        {
            Console.WriteLine("[{0}]", string.Join(" ", blankArray));
            char guess = YourGuesses(letterArray, blankArray);
            if (letterArray.Contains(guess))
            {
                for (int i = 0; i < letterArray.Length; i++)
                {
                    if (letterArray[i] == guess)
                    {
                        blankArray[i] = guess;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Your word did not include {guess}, please try again.");
            }
            if (WinCheck(blankArray))
            {
                RemoveAllGuesses();
                Console.WriteLine(blankArray);
                Console.WriteLine("");
                Console.WriteLine("You WON!!!");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                TitlePage();
            }
            else
            {
                YourWord(letterArray, blankArray);
            }
        }
        static bool WinCheck(char[] blankArray)
        {
            char blank = '_';
            for (int i = 0; i < blankArray.Length; i++)
            {
                if (blankArray[i] == blank)
                {
                    return false;
                }
            }
            return true;
        }
        static char YourGuesses(char[] letterArray, char[] blankArray)
        {
            try
            {
                Console.WriteLine("Please guess a letter.");
                string guess = Console.ReadLine();
                string upperGuess = guess.ToUpper(new CultureInfo("en-US", false));
                char charGuess = Convert.ToChar(upperGuess);
                char vettedGuess = AddALetter(charGuess);
                return charGuess;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Please try again, your guess must be one character long.");
                YourWord(letterArray, blankArray);
                string thisOne = "%";
                char notInAWord = Convert.ToChar(thisOne);
                return notInAWord;
            }
        }
        static char[] ReadFromLetterList()
        {
            string path = "../../../guesses.txt";

            using (StreamReader sr = File.OpenText(path))
            {
                string[] stringLetters = File.ReadAllLines(path);
                string wordOfLetters = string.Join("", stringLetters);
                char[] letterArray = wordOfLetters.ToCharArray();
                return letterArray;
            }
        }
        static char AddALetter(char letter)
        {
            bool truth = IsTheLetterThere(letter);

            if (truth == false)
            {
                string path = "../../../Guesses.txt";

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(letter);
                }
                return letter;
            }
            string thisOne = "%";
            char notInAWord = Convert.ToChar(thisOne);
            return notInAWord;
        }
        static bool IsTheLetterThere(char letter)
        {
            bool isIt = false;
            char[] list = ReadFromLetterList();
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == letter)
                {
                    Console.WriteLine("");
                    Console.WriteLine("You have already guessed this letter.");
                    Console.WriteLine("");
                    isIt = true;
                    return isIt;
                }
            }
            return isIt;
        }
        static void Admin()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Admin Menu");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("----------");
                Console.WriteLine("1. View Words");
                Console.WriteLine("2. Add A Word");
                Console.WriteLine("3. Remove A Word");
                Console.WriteLine("4. Delete the list");
                Console.WriteLine("5. Return To The Previous Menu");
                Console.WriteLine("----------");
                Console.WriteLine("Please make a selection");
                string selection = Console.ReadLine();
                int option = Convert.ToInt32(selection);
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        PrintNumberedArray();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("What word would you like to add?");
                        string addWord = Console.ReadLine();
                        AddAWord(addWord);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Here are the current words in the list.");
                        PrintNumberedArray();
                        Console.WriteLine("Which word would you like to remove?");
                        string removeWord = Console.ReadLine();
                        int whichWord = Convert.ToInt32(removeWord);
                        RemoveAWord(whichWord) ;
                        break;
                    case 4:
                        Console.Clear();
                        Delete();
                        break;
                    case 5:
                        Console.Clear();
                        TitlePage();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("I didnt understand your input, please make a selection.");
                        Admin();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        static void ViewWordList()
        {
            string path = "../../../Words.txt";

            using (StreamReader sr = File.OpenText(path))
            {
                string[] words = File.ReadAllLines(path);
                for (int i = 0; i < words.Length; i++)
                {
                    Console.WriteLine(words[i]);
                }
            }
            Admin();
        }
        static void AddAWord(string word)
        {
            var truth = IsItThere(word);
            
            if (truth == false)
            {
                string path = "../../../Words.txt";

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(word);
                }

            }
            Admin();
        }
        static bool IsItThere(string word)
        {
            bool isIt = false;
            string[] list = ReadFromList();
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == word)
                {
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("The list already contains this word.");
                    Console.WriteLine("");
                    isIt = true;
                    return isIt;
                }
            }
            return isIt;
        }
        static void RemoveAWord(int selected)
        {
            string[] list = ReadFromList();
            string word = list[selected-1];
            string[] newList = new string[list.Length - 1];
            int j = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != word)
                {
                    newList[j] = list[i];
                    j++;
                }
            }
            string path = "../../../Words.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < newList.Length; i++)
                {
                    string addWord = newList[i];
                    sw.WriteLine(addWord);
                }
            }
            Console.Clear();
            Admin();
        }
        static void Delete()
        {
            string path = "../../../Words.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                    sw.WriteLine("Always");
            }
            Console.Clear();
            Admin();
        }
        static void PrintNumberedArray()
        {
            string path = "../../../Words.txt";

            using (StreamReader sr = File.OpenText(path))
            {
                string[] words = File.ReadAllLines(path);
                string[] numbers = new string[words.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = (Convert.ToString(i+1)+ ". ");
                }
                for (int i = 0; i < words.Length; i++)
                {
                    Console.Write(numbers[i]);
                    Console.WriteLine(words[i]);
                }
            }
        }
    }
}
