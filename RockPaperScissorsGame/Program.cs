using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsGame
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            bool lps = true;
            while(lps)
            {
                Console.WriteLine("Welcome to Rock, Paper and Scissors Game!");
                Console.WriteLine("To Start, please press \"Enter\" ");
                Console.ReadLine();
                Random rnd = new Random();
                AccountInfo account = new AccountInfo();
                GameMechanics gameMechanics = new GameMechanics();
                string computer;
                bool playGame = true;
                Console.WriteLine("How much money do you want to deposit?");
                account.balance = Convert.ToDouble(Console.ReadLine());
                while (playGame)
                {
                    string player = "";
                    account.betAmount = 0;
                    string deposit = "";
                    while (player != "ROCK" && player != "PAPER" && player != "SCISSORS")
                    {
                        if (account.totalGames > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Total Games Played:" + account.totalGames);
                            Console.WriteLine("Wins: " + account.win);
                            Console.WriteLine("Loses: " + account.lose);
                            Console.WriteLine("Draws: " + account.draws);
                            Console.WriteLine("Total Money: $" + account.balance);
                            Console.WriteLine("Total Earnings: $" + account.totalEarnings);
                        }

                        while (account.betAmount > account.balance || account.betAmount == 0)
                        {
                            Console.WriteLine("Please, enter your bet");
                            account.betAmount = Convert.ToDouble(Console.ReadLine());
                            if (account.betAmount > account.balance)
                            {
                                Console.WriteLine("You don't have enough money!");
                                Console.WriteLine("Would you like to deposit money? Y/N");
                                deposit = Console.ReadLine().ToUpper();
                                switch (deposit)
                                {
                                    case "Y":
                                        Console.WriteLine("How much money do you want to deposit?");
                                        gameMechanics.depositMoney(account, Convert.ToDouble(Console.ReadLine()));

                                        break;
                                    case "N":
                                        if (account.balance > 0)
                                        {
                                            break;
                                        }
                                        Console.WriteLine("Have a good day!");
                                        return;

                                }
                            }
                            else if (account.betAmount <= account.balance)
                            {
                                Console.WriteLine("You have successfully entered your bet");
                                Console.WriteLine("Your bet is: $" + account.betAmount);

                            }
                            else if (account.betAmount == 0)
                            {
                                Console.WriteLine("You can't enter 0 as bet!");
                            }
                        }
                        account.balance = account.balance - account.betAmount;
                        Console.WriteLine("Current Balance After Betting: " + account.balance);

                        Console.WriteLine("Choose one of them - ROCK / PAPER / SCISSORS");
                        player = Console.ReadLine().ToUpper();


                    }
                    switch (rnd.Next(1, 4))
                    {

                        case 1:
                            computer = "ROCK";
                            Console.Clear();
                            Console.WriteLine("Your choice: " + player);
                            Console.WriteLine("Computer choice: " + computer);
                            if (player == computer)
                            {
                                gameMechanics.draw(account,account.betAmount);
                            }
                            else if (player == "PAPER")
                            {
                                gameMechanics.won(account, account.balance, account.betAmount);
                            }
                            else if (player == "SCISSORS")
                            {
                                gameMechanics.lost(account, account.balance, account.betAmount);
                            }
                            break;
                        case 2:
                            computer = "PAPER";
                            Console.Clear();
                            Console.WriteLine("Your choice: " + player);
                            Console.WriteLine("Computer choice: " + computer);
                            if (player == computer)
                            {
                                gameMechanics.draw(account, account.betAmount);
                            }
                            else if (player == "SCISSORS")
                            {
                                gameMechanics.won(account, account.balance, account.betAmount);
                            }
                            else if (player == "ROCK")
                            {
                                gameMechanics.lost(account, account.balance, account.betAmount);
                            }
                            break;
                        case 3:
                            computer = "SCISSORS";
                            Console.Clear();
                            Console.WriteLine("Your choice: " + player);
                            Console.WriteLine("Computer choice: " + computer);
                            if (player == computer)
                            {
                                gameMechanics.draw(account, account.betAmount);
                            }
                            else if (player == "ROCK")
                            {
                                gameMechanics.won(account, account.balance, account.betAmount);
                            }
                            else if (player == "PAPER")
                            {
                                gameMechanics.lost(account, account.balance, account.betAmount);
                            }
                            break;
                    }
                    bool carryOn = true;
                    while (carryOn)
                    {

                        account.totalGames++;
                        Console.WriteLine("Total Games Played:" + account.totalGames);
                        Console.WriteLine("Wins: " + account.win);
                        Console.WriteLine("Loses: " + account.lose);
                        Console.WriteLine("Draws: " + account.draws);
                        Console.WriteLine("Total Money: $" + account.balance);
                        Console.WriteLine("Total Earnings: $" + account.totalEarnings);
                        Console.WriteLine("Would you like to continue playing? Y/N or RESET");
                        string choice = Console.ReadLine().ToUpper();
                        if (choice == "Y")
                        {

                            Console.Clear();
                            carryOn = false;

                        }
                        else if (choice == "N")
                        {
                            Console.WriteLine("Thanks for playing");
                            carryOn = false;
                            playGame = false;
                            lps = false;
                        }
                        else if (choice == "RESET")
                        {
                            gameMechanics.resetGame(account);
                            carryOn = false;
                            playGame = false;
                        }
                        else
                        {
                            Console.WriteLine("Only use Y/N, please!");
                        }

                    }

                }
            }
            
            
        }
    }
    public class AccountInfo
    {
        public int ID { get; set; }
        public double balance { get; set; }
        public double betAmount { get; set; }
        public double totalEarnings { get; set; }
        public int totalGames { get;set; }
        public int win { get; set; }
        public int lose {  get; set; }
        public int draws { get; set; }
    }
    public class GameMechanics
    {
        public void won(AccountInfo account,double totalMoney, double betAmount)
        {
            account.balance = totalMoney + (betAmount * 2);
            account.totalEarnings = account.totalEarnings + (betAmount);
            account.win++;


            Console.WriteLine("You have won $" + betAmount);
            betAmount = 0;
        }
        public void lost(AccountInfo account, double totalMoney, double betAmount)
        {

            account.totalEarnings = account.totalEarnings - betAmount;
            account.lose++;
            Console.WriteLine("You have lost $" + betAmount);
            betAmount = 0;
        }
        public void draw(AccountInfo account, double betAmount)
        {
            account.balance = account.balance + betAmount;
            account.draws++;
            Console.WriteLine("It's draw so you got your $" + betAmount + " back.");
            betAmount = 0;
        }
        public void depositMoney(AccountInfo account, double money)
        {
            account.balance = account.balance + money;
            Console.WriteLine("You have successfully deposited $" + money + " into your account!");
        }
        public void resetBetAmount(AccountInfo account)
        {
            account.betAmount = 0;
        }
        public static int lastID = 0;
        public void resetGame(AccountInfo account)
        {
            account.ID = ++lastID;
        }
    }
}
