using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LAB03_WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            TitlePage();
        }
        /// <summary>
        /// This method creates the menu and uses a switch statement.
        /// </summary>
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
                        string path = "../../../Words.txt";
                        NewGame(path);
                        break;
                    case 2:
                        Console.Clear();
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
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Im sorry, we did not understand your selection. Please try again.");
                TitlePage();
            }
        }
        /// <summary>
        /// This method begins a new game by calling subsequent methods.
        /// </summary>
        /// <param name="path">This is the path for the word list text file.</param>
        static void NewGame(string path)
        {
            RemoveAllGuesses();
            string[] wordList = ReadFromList(path);
            string randomWord = ChooseAWord(wordList);
            GameSetup(randomWord);
        }
        /// <summary>
        /// This method overwrites the previous gesses placed in the guess text file.
        /// </summary>
        static void RemoveAllGuesses()
        {
            string path = "../../../Guesses.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine();
            }
        }
        /// <summary>
        /// This reads all the words that are in the txt file and returns them in an array.
        /// </summary>
        /// <param name="path">this is the location of the text file</param>
        /// <returns>this method returns the words from the text file in an array</returns>
        static string[] ReadFromList(string path)
        {

            using (StreamReader sr = File.OpenText(path))
            {
                string[] words = File.ReadAllLines(path);
                return words;
            }
        }
        /// <summary>
        /// This chooses a word out of the array
        /// </summary>
        /// <param name="wordList">this is the array created in the previous method, ReadFromList();</param>
        /// <returns>returns the word as all uppercase</returns>
        public static string ChooseAWord(string[] wordList)
        {
            Random rnd = new Random();
            int wordIndex = rnd.Next(wordList.Length);
            string randomWord = wordList[wordIndex];
            string upperRandomWord = randomWord.ToUpper(new CultureInfo("en-US", false));
            return upperRandomWord;
        }
        /// <summary>
        /// This starts up the game, chreates a new array with only blanks the same length as the word.
        /// </summary>
        /// <param name="randomWord">this is the touppercased random word</param>
        static void GameSetup(string randomWord)
        {
            char[] letterArray = randomWord.ToCharArray();
            char[] blankArray = new char[letterArray.Length];
            for (int i = 0; i < blankArray.Length; i++)
            {
                char blank = Convert.ToChar("_");
                blankArray[i] = blank;
            }
            Play(letterArray, blankArray);
        }
        /// <summary>
        /// This displays the game to the user, shows the guessed letters, the blanks, and tells you how long the word is.
        /// </summary>
        /// <param name="letterArray"></param>
        /// <param name="blankArray"></param>
        public static void Play(char[] letterArray, char[] blankArray)
        {
            Console.Clear();
            Console.WriteLine("Lets Play!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"Your Word is {blankArray.Length} letters long.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Gussed:");
            string path = "../../../guesses.txt";
            using (StreamReader sr = File.OpenText(path))
            {
                string[] words = File.ReadAllLines(path);
                for (int i = 0; i < words.Length; i++)
                {
                    Console.Write(words[i]+" ");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[{0}]", string.Join(" ", blankArray));
            char guess = YourGuesses(letterArray, blankArray);
            YourWord(letterArray, blankArray, guess);
        }
        /// <summary>
        /// Writes the blank spaces in the array, checks guesses to see if there are letters in the original, and replaces them in the blank array. This also determines a winner.
        /// </summary>
        /// <param name="letterArray">this is the charArray of the word</param>
        /// <param name="blankArray">this is the charArray of the blank spaces</param>
        /// <returns></returns>
        public static bool YourWord(char[] letterArray, char[] blankArray, char guess)
        {
            if (letterArray.Contains(guess))
            {
                for (int i = 0; i < letterArray.Length; i++)
                {
                    if (letterArray[i] == guess)
                    {
                        blankArray[i] = guess;
                    }
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
                    Play(letterArray, blankArray);
                }
                return true;
            }
            else
            {
                Console.WriteLine($"Your word did not include {guess}, please try again.");
                Play(letterArray, blankArray);
                return false;
            }

        }
        /// <summary>
        /// This checks to see if there is a winner
        /// </summary>
        /// <param name="blankArray">brings in the initially blank array and checks to see if there are still blanks</param>
        /// <returns>boolian of the win</returns>
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
        /// <summary>
        /// this requests you guess a letter and makes sure you can only guess one at a time.
        /// </summary>
        /// <param name="letterArray">this is the charArray of the word</param>
        /// <param name="blankArray">this is the charArray of the blank spaces</param>
        /// <returns>returns your guess</returns>
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
            catch (FormatException)
            {
                Console.WriteLine("Please try again, your guess must be one character long.");
                Play(letterArray, blankArray);
                string thisOne = "%";
                char notInAWord = Convert.ToChar(thisOne);
                return notInAWord;
            }
        }
        /// <summary>
        /// this reads from the list of guessed letters
        /// </summary>
        /// <returns>returns the guessed letter</returns>
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
        /// <summary>
        /// this adds the guessed letter to the txt
        /// </summary>
        /// <param name="letter">this is the guessed letter</param>
        /// <returns>returns the guessed letter</returns>
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
        /// <summary>
        /// this checks to see if the guessed letter is in the txt file
        /// </summary>
        /// <param name="letter">the guessed letter</param>
        /// <returns>true if there or false if not</returns>
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
        /// <summary>
        /// this displays the admin menu and makes a selection with a switch statement.
        /// </summary>
        static void Admin()
        {
            try
            {
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
                        string path = "../../../Words.txt";
                        ViewWordList(path);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("What word would you like to add?");
                        string addWord = Console.ReadLine();
                        path = "../../../Words.txt";
                        AddAWord(addWord, path);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Here are the current words in the list.");
                        PrintNumberedArray();
                        Console.WriteLine("Please select the number of the word you would like to remove?");
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
        /// <summary>
        /// this allows you to see the word list
        /// </summary>
        /// <param name="path">path to the txt file</param>
        static void ViewWordList(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string[] words = File.ReadAllLines(path);
                string[] numbers = new string[words.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = (Convert.ToString(i + 1) + ". ");
                }
                for (int i = 0; i < words.Length; i++)
                {
                    Console.Write(numbers[i]);
                    Console.WriteLine(words[i]);
                }
            }
            Admin();
        }
        /// <summary>
        /// this allows you to add a word to the txt file
        /// </summary>
        /// <param name="word">the word you want to add</param>
        /// <param name="path">path to the txt file</param>
        /// <returns>true if it was added false if it failed</returns>
        public static bool AddAWord(string word, string path)
        {
            var truth = IsItThere(word, path);
            
            if (truth == false)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(word);
                }

            }

            truth = IsItThere(word, path);
            Admin();
            return truth;
        }
        /// <summary>
        /// checks the word list for the word
        /// </summary>
        /// <param name="word">selected word</param>
        /// <param name="path">path to the txt file</param>
        /// <returns>boolian true if it is there or false if not</returns>
        static bool IsItThere(string word, string path)
        {
            bool isIt = false;
            string[] list = ReadFromList(path);
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
        /// <summary>
        /// allows you to remove a word from the list
        /// </summary>
        /// <param name="selected">the selected word to delete</param>
        static void RemoveAWord(int selected)
        {
            string path = "../../../Words.txt";
            string[] list = ReadFromList(path);
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
            path = "../../../Words.txt";
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
        /// <summary>
        /// deletes the list file and replaces it with a new one. I placed one word in it to not have a blank line.
        /// </summary>
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
        /// <summary>
        /// this writes the word list and gives them all a number (used for easier word removal)
        /// </summary>
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
