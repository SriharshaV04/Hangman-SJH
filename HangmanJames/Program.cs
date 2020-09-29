using System;
using System.Collections.Generic;
using System.IO;

namespace HangmanJames
{
    class Program
    {
        private static string RandomWord()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "words.txt");
            try
            {
                // Open the text file using a stream reader.
                using (var words = new StreamReader(path))
                {
                    Random rnd = new Random();
                    int randomNumberGen = rnd.Next(1, 10001);
                    for (int i = 1; i < randomNumberGen; i++)
                    {
                        words.ReadLine();
                    }
                    string magic = words.ReadLine(); //magic is generated word
                    return magic;

                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
        
        static void print_word(char[] splitted)
        {
            Console.Write("Word: ");
            foreach (char thing in splitted)
            {
                Console.Write(thing + " ");
            }

            Console.WriteLine("");
        }

        static void print_used(List<char> used)
        {
            Console.Write("Letters used: ");
            foreach (char thing in used)
            {
                Console.Write(thing + " ");
            }

            Console.WriteLine("");
        }
        static char[] createUser(int len)
        {
            char[] user = new char[len];
            for (int i = 0; i < len; i++)
            {
                user[i] = '_';
            }
 
            return user;
        }

        static char[] update(char letter, char[] word, char[] user)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    user[i] = letter;
                }
            }
            if (word == user)
            {
                Console.WriteLine("Game Complete");
                print_word(user);
            }

            return user;
        }

        static List<char> validate(char letter, char[] word, List<char> used)
        {
            for (int i = 0; i < used.Count; i++)
            {
                if (letter == used[i])
                    return null;
            }
            
            bool ifin = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    ifin = true;
                }
            }
            if (ifin == false) 
                used.Add(letter);
            return used;
        }

        static bool game(char[] user)
        {
            foreach (char letter in user)
            {
                if (letter == '_')
                    return false;
            }

            return true;
        }
        static bool draw(List<char> used, char[] word)
        {
            var one = " +---+ \n" +
                      " |   |\n" +
                      "     |\n" +
                      "     |\n" +
                      "     |\n" +
                      "     |\n" +
                      "========\n";
            var two = " +---+ \n" +
                      " |   |\n" +
                      " O   |\n" +
                      "     |\n" +
                      "     |\n" +
                      "     |\n" +
                      "========\n";
            var three = " +---+ \n" +
                      " |   |\n" +
                      " O   |\n" +
                      " |   |\n" +
                      "     |\n" +
                      "     |\n" +
                      "========\n";
            var four = " +---+ \n" +
                      " |   |\n" +
                      " O   |\n" +
                      "/|   |\n" +
                      "     |\n" +
                      "     |\n" +
                      "========\n";
            var five = " +---+ \n" +
                       " |   |\n" +
                       " O   |\n" +
                       "/|   |\n" +
                       "/    |\n" +
                       "     |\n" +
                       "========\n";
            var six = " +---+ \n" +
                      " |   |\n" +
                      " O   |\n" +
                      "/|\\  |\n" +
                      "/    |\n" +
                      "     |\n" +
                      "========\n";
            var seven = " +---+ \n" +
                      " |   |\n" +
                      " O   |\n" +
                      "/|\\  |\n" +
                      "/ \\  |\n" +
                      "     |\n" +
                      "========\n";

            if (used.Count == 1)
                Console.Write(one);
            else if (used.Count == 2)
                Console.Write(two);
            else if (used.Count == 3)
                Console.Write(three);
            else if (used.Count == 4)
                Console.Write(four);
            else if (used.Count == 5)
                Console.Write(five);
            else if (used.Count == 6)
                Console.Write(six);
            else if (used.Count == 7)
            {
                Console.Write(seven);
                Console.WriteLine("");
                Console.WriteLine("=====GAME OVER!======");
                print_word(word);
                return false;
            }

            return true;
        }
        static int use(string a, List<char> b)
        {
            int d = b.Count;
            for (int i=0;i < d; i++) 
            {
                if (a == b[i].ToString())
                {
                    return 1;
                }
            }

            return 0;
        }
        static void Main(string[] args)
        {
            char[] model = RandomWord().ToCharArray();
            // char[] model = "harsha".ToCharArray(); // testing
            var used = new List<char>();
            char[] user = createUser(model.Length);
            
            Console.WriteLine("=====Welcome=====");
            Console.WriteLine("You have 7 attempts attempts to guess the word");
            Console.WriteLine("-------------------");
            Console.WriteLine("");
            
            while (used.Count <= 7)
            {
                bool f = draw(used,model);
                if (f == false)
                    break;
                Console.WriteLine("");
                Console.WriteLine("");
                print_word(user);
                print_used(used);
                Console.WriteLine($"Lives:{7-used.Count}");
                Console.WriteLine("Choose a letter:");
                string guess = Console.ReadLine();
                while (guess.Length != 1 || Char.IsLetter(guess[0]) == false || use (guess[0].ToString(), used) == 1 )
                {
                    Console.WriteLine("the input is above 2 letters long or is not a letter, please input again:");
                    guess = Console.ReadLine();
                }
                char letter = Char.Parse(guess);
                used = validate(letter, model, used);
                user = update(letter, model, user);
                bool winner = game(user);
                if (winner == true)
                {
                    print_word(model);
                    Console.WriteLine("---------You win!----------");
                    break;
                }
            }
        }
    }
}


