using System;
using System.IO;

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
        static void NewGame()
        {
            RemoveAllGuesses();
            string[] wordList = ReadFromList();
            string randomWord = ChooseAWord(wordList);
            PlayGame(randomWord);
        }
        static void RemoveAllGuesses()
        {
            Console.WriteLine("remove");
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
        static string ChooseAWord(string[] wordlist)
        {
            string randomWord = wordlist[0];
            return randomWord;
        }
        static void PlayGame(string randomWord)
        {
            Console.WriteLine(randomWord);
        }
        static void Admin()
        {

            Console.WriteLine("Admin Menu");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("----------");
            Console.WriteLine("1. View Words");
            Console.WriteLine("2. Add A Word");
            Console.WriteLine("3. Remove A Word");
            Console.WriteLine("4. Return To The Previous Menu");
            Console.WriteLine("----------");
            Console.WriteLine("Please make a selection");
            string selection = Console.ReadLine();
            int option = Convert.ToInt32(selection);
            switch (option)
            {
                case 1:
                    ViewWordList();
                    break;
                case 2:
                    Console.WriteLine("What word would you like to add?");
                    string addWord = Console.ReadLine();
                    AddAWord(addWord);
                    break;
                case 3:
                    Console.WriteLine("Here are the current words in the list.");
                    ViewWordList();
                    Console.WriteLine("Which word would you like to remove?");
                    string removeWord = Console.ReadLine();
                    RemoveAWord(removeWord);
                    break;
                case 4:
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
        }
        static void AddAWord(string word)
        {
            string path = "../../../Words.txt";

            using (StreamWriter sw = File.AppendText(path)) 
            {
                sw.WriteLine(word);
            }
        }
        static void RemoveAWord(string word)
        {
            string[] list = ReadFromList();
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
            
        }
    }
}
