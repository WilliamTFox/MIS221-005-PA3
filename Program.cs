using System;
using System.IO;
using System.Threading;

namespace StarWars
 {
     class Program
     {
         static void Main(string[] args)
         {
             int credits = 300;
             int totalLoss = 0;
             int totalWin = 0;
             Menu(credits, totalLoss, totalWin);
         }
         static void Menu(int credits, int totalLoss, int totalWin)
         {

             Console.Clear();
             DisplayMenu(ref credits);
             GetMenuChoice(ref credits, ref totalLoss, ref totalWin);
         }

        static void DisplayMenu(ref int credits)
        {
            Console.WriteLine("                                     Credits: "+ credits);
            Console.WriteLine("Welcome to Star Wars Games!");
            Console.WriteLine("Please select one of the options below to start: ");
            Console.WriteLine("1. The Force");
            Console.WriteLine("2. Blasters");
            Console.WriteLine("3. Stats Board");
            Console.WriteLine("4. Exit Program");
            Console.WriteLine();
            Console.WriteLine("Please enter you selection below then hit enter: ");
        }
        static void GetMenuChoice(ref int credits, ref int totalLoss, ref int totalWin)
        {
            int userChoice = int.Parse(Console.ReadLine());
            if (userChoice == 1)
            {
                Force(ref credits, ref totalWin, ref totalLoss);
            }
            else if (userChoice == 2)
            {
                Blasters(ref credits, ref totalLoss, ref totalWin);
            }
            else if (userChoice == 3)
            {
                StatBoard(credits, totalLoss, totalWin);
            }
            else if (userChoice == 4)
            {

            }
            else
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid option, please enter an available option, press enter to continue... ");
                Console.ReadKey();
                Menu(credits, totalLoss, totalWin);
            }
        }










        static void Force(ref int credits, ref int totalWin, ref int totalLoss)
        {
            int card = 0;
            Console.Clear();
            ForceInstructions();
            Console.ReadKey();
            Console.Clear();
            int bet = betting(ref credits);
            KeepGoing(ref card, ref credits, ref bet, ref totalWin, ref totalLoss);
            
        }
        static void ForceInstructions()
        {
            Console.WriteLine("Welcome to The Force, the game of higher or lower. \n In this game, you are required to place bet to start, the bet can be as many credits as you want so long as it's more than 0. \n The further you make it in the game, the more credits you'll make! \n The game will start with a card displayed to you, and it is your job to guess whether the next card is  higher or lower than that card. \n If you guess right, you move on to the next card, if you guess wrong, you lose! \n Make it to 5 cards correct to break even, 7 cards correct to  double your credits you bet, and all 10 cards to earn the jackpot of triple your bet!");
            Console.WriteLine();
            Console.WriteLine("Hit enter to begin playing!");

        }
        static int betting(ref int credits)
        {
            Console.WriteLine("Please enter the amount you would like to bet on this round");
            int bet = int.Parse(Console.ReadLine());
            while (bet > credits)
            {
                Console.WriteLine("You do not have that manhy credits, please enter a value you can spend");
                bet = int.Parse(Console.ReadLine());
            }
            credits = credits - bet;
            return bet;
        }

        static void KeepGoing(ref int card, ref int credits, ref int bet, ref int totalWin, ref int totalLoss)
        {
            GetCard();
            DisplayCard();
            userForce(ref card, ref credits, ref bet, ref totalWin, ref totalLoss);
        }

        static void GameEnd(ref int card, ref int credits, ref int bet, ref int totalWin, ref int totalLoss)
        {
            Console.WriteLine("Game has ended");
            Console.WriteLine("You guessed " + card + " cards correct");
            if (card == 5 || card == 6)
            {
                Console.WriteLine("You cut even");
                credits = credits;
                totalWin++;
            }
            else if (card == 7 || card == 8 || card == 9)
            {
                Console.WriteLine("You made your betting amount");
                credits = credits + bet;
                totalWin++;
            }
            else if (card == 10)
            {
                Console.WriteLine("You guessed 10 correctly! You tripled your betting amount");
                credits = credits + 2 * bet;
                totalWin++;
            }
            else
            {
                Console.WriteLine("You did not guess enough correct card, you lost your bet");
                credits = credits - bet;
            }
            Console.WriteLine("");
            Console.WriteLine("Please select one of the choices down below");
            Console.WriteLine("1. Play again");
            Console.WriteLine("2. Return to main menu");
            int userChoice = int.Parse(Console.ReadLine());

            while (userChoice != 1 || userChoice != 2)
            {
                Console.WriteLine("You entered an invalid option, please enter an available option, press enter to continue... ");
                userChoice = int.Parse(Console.ReadLine());
            }

            if (userChoice == 1)
            {
                Force(ref credits, ref totalWin, ref totalLoss);
            }
            else if (userChoice == 2)
            {
                Menu(credits, totalLoss, totalWin);
            }   


        }
        static void userForce(ref int card, ref int credits, ref int bet, ref int totalWin, ref int totalLoss)
        {
            int userInput = int.Parse(Console.ReadLine());
            if (userInput == 1 && int.Parse(DisplayCard()) > int.Parse(GetCard()))
            {
                 card = card + 1;
                KeepGoing(ref card, ref credits, ref bet, ref totalWin, ref totalLoss);
            }
            else if (userInput == 2 && int.Parse(DisplayCard()) < int.Parse(GetCard()))
            {
                 card = card + 1;
                KeepGoing(ref card, ref credits, ref bet, ref totalWin, ref totalLoss);
                
            }
            else
            {
                GameEnd(ref card, ref credits, ref bet, ref totalWin, ref totalLoss);
            }

        }

        static string GetCard()
        {

            //GetCards();

            StreamReader inFile2 = new StreamReader("deck.txt");

            Random rnd = new Random();
            int RNDnumber = rnd.Next(1,4);
            Random rnd2 = new Random();
            int RNDnumber2 = rnd.Next(1,13);

            string[] suit = {"Hearts" , "Diamonds" , "Clubs" , "Spades"};
            string[] number = {"Ace" , "2" , "3" , "4" , "5" , "6" , "7" , "8" , "9" , "10" , "Jack" , "Queen" , "King"};
            string card = number[RNDnumber2] + " of " + suit[RNDnumber];

            return card;
        }

        static void GetCards()
        {
            StreamWriter inFile = new StreamWriter("deck.txt");

            string[] suit = {"Hearts" , "Diamonds" , "Clubs" , "Spades"};
            string[] number = {"Ace" , "2" , "3" , "4" , "5" , "6" , "7" , "8" , "9" , "10" , "Jack" , "Queen" , "King"};
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    int k = 0;
                    string[] card = {number[j] + " of " + suit[i]+ "\n"};
                    inFile.Write(card[k]);
                    k++;
                    
                }
            }
            inFile.Close();
            
        }
        static string DisplayCard()
        {
            string card = GetCard();
            Console.WriteLine("");
            Console.WriteLine("                    Your Card");
            Console.WriteLine("                   _____________");
            Console.WriteLine("                  |             |");
            Console.WriteLine("                  |             |");
            Console.WriteLine("                  |             |");

            Console.WriteLine("                  |             |");
            Console.WriteLine("                  | " + card + " |" );
            Console.WriteLine("                  |             |");
            
            Console.WriteLine("                  |             |");
            Console.WriteLine("                  |             |");
            Console.WriteLine("                  |_____________|");
            Console.WriteLine("");
            Console.WriteLine("Your choice's are listed below, please select one:");
            Console.WriteLine("1. The Next Card is Higher");
            Console.WriteLine("2. The Next Card is Lower");
            string previousCard = card;

            return previousCard;
        }











        static void Blasters(ref int credits, ref int totalLoss, ref int totalWin)
        {
            Console.Clear();
            int points = 20;
            BlastersInstructions(ref credits);
            Console.ReadKey();
            Console.Clear();
            while (points < 40 && points > 0)
            {
                Console.Clear();
                Nuetral(ref points);
                int option = UserMove();
                Animation();
                Result(ref points, option);
            }
            Console.Clear();
            if (points >= 40)
            {
                BlastersWinDisplay();
                credits = credits + 20;
                totalWin = totalWin + 1;
            }
            else if (points <= 0)
            {
                BlastersLossDisplay();
                credits = credits - 20;
                totalLoss = totalLoss + 1;
            }
            BlastersFinish(ref credits, ref totalLoss, ref totalWin);


        }

        static void BlastersFinish(ref int credits, ref int totalLoss, ref int totalWin)
        {
            int userInput = int.Parse(Console.ReadLine()!);
            while (userInput != 1 || userInput != 2)
            {
                Console.WriteLine("You entered an invalid option, please enter an available option, press enter to continue... ");
                userInput = int.Parse(Console.ReadLine());
            }
            if (userInput == 1)
            {
                Blasters(ref credits, ref totalLoss, ref totalWin);
            }
            else if (userInput == 2)
            {

                Menu(credits, totalLoss, totalWin);
            }
        }
        static void BlastersWinDisplay()
        {
            Console.WriteLine("Congrats!! You have won Blasters!");
            Console.WriteLine("You have won a prize of 40 credits!!");
            Console.WriteLine("");
            Console.WriteLine("To Continue, please select one of the options down below: ");
            Console.WriteLine("1. Play again");
            Console.WriteLine("2. Quit to main menu");
        }
        static void BlastersLossDisplay()
        {
            Console.WriteLine("Oh No!! You lost!");
            Console.WriteLine("The storm trooper stole your 20 credits");
            Console.WriteLine("");
            Console.WriteLine("To Continue, please select one of the options down below: ");
            Console.WriteLine("1. Play again");
            Console.WriteLine("2. Quit to main menu");
        }

        static int Result(ref int points, int option)
        {
            int number = RandomNumber();
            //int option = UserMove();
            if (number < 7 && option == 1)
            {
                points = points + 5;
                Console.WriteLine("Great Dodge");
                Console.WriteLine("Hit enter to continue playing:");
                Console.ReadKey();
            }
            else if (number >= 7 && option == 1)
            {
                points = points - 10;
                Console.WriteLine("You got hit!");
                Console.WriteLine("Hit enter to continue playing:");
                Console.ReadKey();
            }
            else if (number < 5 && option == 2)
            {
                points = points + 10;
                Console.WriteLine("Awesome deflection!");
                Console.WriteLine("Hit enter to continue playing:");
                Console.ReadKey();
            }
            else if (number >= 5 && option == 2)
            {
                points = points - 10;
                Console.WriteLine("You got hit!");
                Console.WriteLine("Hit enter to continue playing:");
                Console.ReadKey();
            }
            return points;
        }

        static int UserMove()
        {
            
            int option = int.Parse(Console.ReadLine());
            // if (option != 1 || option != 2)
            // {
            //     Console.WriteLine("You entered an invalid option, please enter an available option, press enter to continue... ");
            //     option = int.Parse(Console.ReadLine());
            // }

            return option;
        }

        static int RandomNumber()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 11);

            return number;
        }
        static void BlastersInstructions(ref int Credits)
        {
            Console.WriteLine("                                          Credits: " + Credits);
            Console.WriteLine("Welcome to Blasters, the game of chance. \n In this game, you are required to have at least 20 credits to play. \n A storm trooper will be shooting at you from the right of the screen. \n It is your choice if you want to dodge or deflect the lazers coming at you. \n But beware, dodging has a higher chance at working than deflecting but blocking gives you more points if you succeed!");
            Console.WriteLine();
            Console.WriteLine("Hit enter to begin playing!");

        }



        static void Nuetral(ref int points)
        {
            Console.WriteLine("                               Points: "+ points);
            Console.WriteLine("                                 ___        ");
            Console.WriteLine("    ___                        __|_|__       ");
            Console.WriteLine("   |   |                        |   |     ");
            Console.WriteLine("   |___|                        |___|     ");
            Console.WriteLine("  |  |                            |       ");
            Console.WriteLine("  | /|___|                   ==>__|        ");
            Console.WriteLine("  TV |                            |        ");
            Console.WriteLine("     |                            |        ");
            Console.WriteLine("     ^                            ^         ");
            Console.WriteLine("    | |                          | |      ");
            Console.WriteLine("    L L                          J J        ");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Your choice's are listed below, please select one:");
            Console.WriteLine("1. Dodge ( + 5 point if successful )");
            Console.WriteLine("2. Deflect ( +10 points if successful )");

        }
        static void Animation()
        {
            Console.Clear();

            Console.WriteLine("                                 ___        ");
            Console.WriteLine("    ___                        __|_|__       ");
            Console.WriteLine("   |   |                        |   |     ");
            Console.WriteLine("   |___|                        |___|     ");
            Console.WriteLine("  |  |                            |       ");
            Console.WriteLine("  | /|___|                -- ==>__|        ");
            Console.WriteLine("  TV |                            |        ");
            Console.WriteLine("     |                            |        ");
            Console.WriteLine("     ^                            ^         ");
            Console.WriteLine("    | |                          | |      ");
            Console.WriteLine("    L L                          J J        ");
            Console.WriteLine("------------------------------------------");

            Thread.Sleep(1000);
            Console.Clear();
            
            Console.WriteLine("                                 ___        ");
            Console.WriteLine("    ___                        __|_|__       ");
            Console.WriteLine("   |   |                        |   |     ");
            Console.WriteLine("   |___|                        |___|     ");
            Console.WriteLine("  |  |                            |       ");
            Console.WriteLine("  | /|___|               --  ==>__|        ");
            Console.WriteLine("  TV |                            |        ");
            Console.WriteLine("     |                            |        ");
            Console.WriteLine("     ^                            ^         ");
            Console.WriteLine("    | |                          | |      ");
            Console.WriteLine("    L L                          J J        ");
            Console.WriteLine("------------------------------------------");

            Thread.Sleep(1000);
            Console.Clear();
            
            Console.WriteLine("                                 ___        ");
            Console.WriteLine("    ___                        __|_|__       ");
            Console.WriteLine("   |   |                        |   |     ");
            Console.WriteLine("   |___|                        |___|     ");
            Console.WriteLine("  |  |                            |       ");
            Console.WriteLine("  | /|___|            --     ==>__|        ");
            Console.WriteLine("  TV |                            |        ");
            Console.WriteLine("     |                            |        ");
            Console.WriteLine("     ^                            ^         ");
            Console.WriteLine("    | |                          | |      ");
            Console.WriteLine("    L L                          J J        ");
            Console.WriteLine("------------------------------------------");

            Thread.Sleep(1000);
            Console.Clear();
           
            Console.WriteLine("                                 ___        ");
            Console.WriteLine("    ___                        __|_|__       ");
            Console.WriteLine("   |   |                        |   |     ");
            Console.WriteLine("   |___|                        |___|     ");
            Console.WriteLine("  |  |                            |       ");
            Console.WriteLine("  | /|___|       --          ==>__|        ");
            Console.WriteLine("  TV |                            |        ");
            Console.WriteLine("     |                            |        ");
            Console.WriteLine("     ^                            ^         ");
            Console.WriteLine("    | |                          | |      ");
            Console.WriteLine("    L L                          J J        ");
            Console.WriteLine("------------------------------------------");

            Thread.Sleep(1000);
            Console.Clear();
            
            Console.WriteLine("                                 ___        ");
            Console.WriteLine("    ___                        __|_|__       ");
            Console.WriteLine("   |   |                        |   |     ");
            Console.WriteLine("   |___|                        |___|     ");
            Console.WriteLine("  |  |                            |       ");
            Console.WriteLine("  | /|___|   --              ==>__|        ");
            Console.WriteLine("  TV |                            |        ");
            Console.WriteLine("     |                            |        ");
            Console.WriteLine("     ^                            ^         ");
            Console.WriteLine("    | |                          | |      ");
            Console.WriteLine("    L L                          J J        ");
            Console.WriteLine("------------------------------------------");

            Thread.Sleep(1000);
            Console.Clear();
            
            Console.WriteLine("                                 ___        ");
            Console.WriteLine("    ___                        __|_|__       ");
            Console.WriteLine("   |   |                        |   |     ");
            Console.WriteLine("   |___|                        |___|     ");
            Console.WriteLine("  |  |                            |       ");
            Console.WriteLine("  | /|___| --                ==>__|        ");
            Console.WriteLine("  TV |                            |        ");
            Console.WriteLine("     |                            |        ");
            Console.WriteLine("     ^                            ^         ");
            Console.WriteLine("    | |                          | |      ");
            Console.WriteLine("    L L                          J J        ");
            Console.WriteLine("------------------------------------------");

            Thread.Sleep(1000);
            Console.Clear();

        }










        static void StatBoard(int credits, int totalLoss, int totalWin)
        {
            Console.Clear();
            Console.WriteLine("Statistics board");
            Console.WriteLine("");
            Console.WriteLine("Total wins: "+totalWin);
            Console.WriteLine("Total loses: "+totalLoss);
            Console.WriteLine("");
            Console.WriteLine("Please hit enter to return to main menu:");
            Console.ReadKey();
            Menu(credits, totalLoss, totalWin);
        }












     }
 }
