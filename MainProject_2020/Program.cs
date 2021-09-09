using System;
using System.Collections.Generic;
using System.Numerics;

namespace MainProject_2020
{     
    class RSA
    {
        public static BigInteger Pow(BigInteger value, BigInteger exponent)
        {
            // Terminal.Output("CALC", "Calculating " + value + " ^ " + exponent + "...");            
            BigInteger originalValue = value;
            while (exponent-- > 1)
            {
                // Terminal.Output("CALC", "Multiplying " + value);
                value = BigInteger.Multiply(value, originalValue);                
            }   
            return value;
        }

        public static void Crypt(BigInteger x, BigInteger y, BigInteger z, string menu)
        {
            BigInteger pow = Pow(x, z);           

            BigInteger c = BigInteger.Remainder(pow, y);

            if (menu == "3")
                Terminal.Output("RESULT", "Ciphertext: " + Convert.ToString(c));
            else
                Terminal.Output("RESULT", "Plaintext: " + Convert.ToString(c));
        }   
        
        public static void Encrypt(BigInteger p, BigInteger n, BigInteger e)
        {
            BigInteger pow = Pow(p, e);
            BigInteger c = BigInteger.Remainder(pow, n);

            Terminal.Output("RESULT", "Ciphertext: " + Convert.ToString(c));
        }
        
    }

    class Euclid
    {        
        private static int[] CalculateValues(int[] u, int[] v, int[] w)
        {            
            while(v[0] > 0)
            {                
                // SET W
                w[0] = u[0] % v[0];
                w[1] = u[1] - ((u[0] / v[0]) * v[1]);
                w[2] = u[2] - ((u[0] / v[0]) * v[2]);

                // SET U
                u[0] = v[0];
                u[1] = v[1];
                u[2] = v[2];

                // SET V
                v[0] = w[0];
                v[1] = w[1];
                v[2] = w[2];

                // RECURSIVE CALL WITH NEW VALUES
                CalculateValues(u, v, w);                                          
            }
            return u;
        }        

        public static void Calculate(int a, int b)
        {
            int[] u = { a, 1, 0 };
            int[] v = { b, 0, 1 };

            int[] w = { 0, 0, 0 };

            int[] result = CalculateValues(u, v, w);
            Terminal.Output("RESULT", "D = gcd(a, b) =" + result[0] + ", X = " + result[1] + ", Y = " + result[2]);
        }
    }

    class Primes
    {        
        public static BigInteger Sqrt(BigInteger n) // stackoverflow.com reference used for this part
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }
            throw new ArithmeticException("NaN"); // exception handling
        }

        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);
            return (n >= lowerBound && n < upperBound);
        }

        public static void Factors(BigInteger n)
        {
            string result = "";
            BigInteger original = n;

            while (n % 2 == 0) // check 2s
            {                
                result = result + 2 + ", ";
                n = n / 2;
            }            
                        
            for (int i = 3; i <= Sqrt(n); i += 2) // check odds
            {                
                while (n % i == 0)
                {                    
                    result = result + i + ", ";                  
                    n = n / i;
                }
            }

            if (n > 2) // handle n > 2
                result = result + n;

            try
            {
                if (BigInteger.Parse(result) == original)
                    Terminal.Output("RESULT", original + " is a prime number.");
            }
            catch
            {
                Terminal.Output("RESULT", "Prime factors for " + original + " are: " + result);
            }
            
                
        }
    }

    class Program
    {
        static void Main(string[] args)
        {                       
            Menu.Home();            

            string menuOption = "";
            while (menuOption != "exit")
            {
                string currentMenu = "Main Menu";
                menuOption = Terminal.Input(currentMenu);

                if (menuOption == "1")
                    Menu.Sub(menuOption, "Prime Factors");
                else if (menuOption == "2")
                    Menu.Sub(menuOption, "Extended Euclidean Algorithm");
                else if (menuOption == "3")
                    Menu.Sub(menuOption, "RSA Encryption");
                else if (menuOption == "4")
                    Menu.Sub(menuOption, "RSA Decryption");
                else if (menuOption == "0")
                {
                    Terminal.Output("INFO", "The application will now exit.");
                    break;
                }
                else
                {
                    Menu.Home();
                    Terminal.Output("ERROR", "Please enter a valid menu option.");
                }

                Console.WriteLine();
            }                
        }
    }

    class Menu
    {
        public static void Sub(string menuNumber, string currentMenu)
        {            
            string menuOption = "";
            bool stay = true;

            while(stay)
            {
                if (menuNumber == "1")
                {                
                    menuOption = Terminal.Input(currentMenu + ": Enter a natural number (n > 1):");
                    // int x = Convert.ToInt32(menuOption);                  

                    BigInteger x = BigInteger.Parse(menuOption);

                    Primes.Factors(x);                    
                }
                else if (menuNumber == "2")
                {
                    menuOption = Terminal.Input(currentMenu + ": Enter a positive int (a):");
                    int a = Convert.ToInt32(menuOption);

                    menuOption = Terminal.Input(currentMenu + ": Enter a positive int (b) such that b < a:");
                    int b = Convert.ToInt32(menuOption);

                    Euclid.Calculate(a, b);
                }
                else if (menuNumber == "3")
                {
                    menuOption = Terminal.Input(currentMenu + ": Enter the Plaintext (P):");
                    int p = Convert.ToInt32(menuOption);

                    menuOption = Terminal.Input(currentMenu + ": Enter the RSA Modulus (N):");
                    int n = Convert.ToInt32(menuOption);

                    menuOption = Terminal.Input(currentMenu + ": Enter the Encryption Exponent (E):");
                    int e = Convert.ToInt32(menuOption);


                    // Part ii test
                    /*
                    BigInteger testA = 153817;
                    BigInteger testB = 1542689;
                    BigInteger testN = (testA * testB);
                    BigInteger testE = 202404606;
                    BigInteger testP = 888999000;
                    RSA.Encrypt(testP, testE, testN);
                    */

                    RSA.Crypt(p, n, e, "3");
                }
                else if (menuNumber == "4")
                {
                    menuOption = Terminal.Input(currentMenu + ": Enter the Ciphertext (C):");
                    int c = Convert.ToInt32(menuOption);

                    menuOption = Terminal.Input(currentMenu + ": Enter the Decryption Exponent (D):");
                    int d = Convert.ToInt32(menuOption);

                    menuOption = Terminal.Input(currentMenu + ": Enter the RSA Modulus (N):");
                    int n = Convert.ToInt32(menuOption);

                    RSA.Crypt(c, n, d, "4");
                }

                menuOption = Terminal.Input(currentMenu + ": Would you like to return to the main menu? (y/n)");
                if (menuOption == "y" || menuOption == "Y")
                {
                    menuOption = "";
                    break;
                }
            }
            
        }

        public static void Home()
        {
            Terminal.MakeLine();
            Console.WriteLine("1 - Prime Factorization");
            Console.WriteLine("2 - Euclidean Algorithm");
            Console.WriteLine("3 - RSA Encryption");
            Console.WriteLine("4 - RSA Decryption");
            Console.WriteLine("\n0 - Exit");
            Terminal.MakeLine();
        }
    }

    class Terminal
    {
        public static string Input(string currentMenu)
        {
            Console.Write("  @ {0}\n  > ", currentMenu);
            string input = Console.ReadLine();
            return input;
        }

        public static void Output(string tag, string message)
        {
            Console.WriteLine("  [{0}]: {1}", tag, message);
            if (tag == "RESULT")
                Console.WriteLine();
        }

        public static void MakeLine()
        {
            for (short i = 0; i <= 25; i++)
                Console.Write("-");
            Console.WriteLine();
        }        
    }
}
