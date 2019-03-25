using System;
using Xunit;
using LAB03_WordGuessGame;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void ICanAddAWord()
        {
            string test = "test";
            string path = "../LAB03_WordGuessGame/Words.txt";
            bool result = Program.AddAWord(test, path);

            Assert.True(result);
        }
        [Fact]
        public void ICanChooseARandomWord()
        {
            string[] mockList = new string[] { "PINK", "PURPLE", "OCTOBER", "RUG" };

            string result = Program.ChooseAWord(mockList);      

            Assert.True(Array.Exists(mockList, s => s.Contains(result)));
        }
        [Fact]
        public void MyWordContainsALetter()
        {
            char[] word = new char[] { 'A', 'P', 'P', 'L', 'E' };
            char[] blanks = new char[] { '_', '_', '_', '_', '_' };
            char guess = 'A';

            bool result = Program.YourWord(word, blanks, guess);

            Assert.True(result);
        }
    }   
}
