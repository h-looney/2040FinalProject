using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string,User> users = new Dictionary<string,User>();
            string[] contents = File.ReadAllLines("player_log.csv");
            foreach(string line in contents)
            {
                string [] userInfo = line.Split(",");
                int userWins = int.Parse(userInfo[1]);
                int userLosses = int.Parse(userInfo[2]);
                int userDraws = int.Parse(userInfo[3]);
                User u = new User(userInfo[0], userWins, userLosses, userDraws);
                users.Add(userInfo[0], u);
            }
            Console.WriteLine("Welcome to Rock, Paper, Scissors!\n1. Start New Game\n2. Load Game\n 3. Quit\nEnter your choice: ");
            string menuChoice = Console.ReadLine();
            User currentUser;
            int roundCount = 1;
            bool keepPlaying = true;
            while(keepPlaying)
            {
                if(menuChoice == "1")
                {
                    string userName;
                    while(true)
                    {
                        Console.WriteLine("What is your name?");
                        userName = Console.ReadLine();
                        bool exists = users.ContainsKey(userName);
                        if(exists)
                        {
                            Console.WriteLine("That name has been claimed by another user.");
                        }
                        else
                        {
                            break;
                        }
                    }
                    User u = new User(userName);
                    users.Add(userName, u);
                    currentUser = u;
                    Console.WriteLine("Hello {0}. Let's play!", userName);
                    RPS(currentUser, roundCount++);
                    while(true)
                    {
                        Console.WriteLine("What would you like to do?\n1.Play Again\n2.View Player Statistics\n3.View Leaderboard\n4.Quit");
                        string userResponse = Console.ReadLine();
                        if(userResponse == "1")
                        {
                            RPS(currentUser, roundCount++);
                        }
                        else if(userResponse == "2")
                        {
                            Console.WriteLine(userStats(currentUser));
                        }
                        else if(userResponse == "3")
                        {
                            viewLeaderboard(users);
                        }
                        else
                        {
                            keepPlaying = false;
                            break;
                        }
                    }
                }
                else if(menuChoice == "2")
                {
                    string userName;
                    while(true)
                    {
                        Console.WriteLine("What is your name?");
                        userName = Console.ReadLine();
                        bool exists = users.ContainsKey(userName);
                        if(!exists)
                        {
                            Console.WriteLine("{0}, your game could not be found.", userName);
                        }
                        else
                        {
                            break;
                        }
                    }
                    currentUser = users[userName];
                    Console.WriteLine("Welcome back {0}! Let's play!", userName);
                    RPS(currentUser, roundCount++);
                    while(true)
                    {
                        Console.WriteLine("What would you like to do?\n1.Play Again\n2.View Player Statistics\n3.View Leaderboard\n4.Quit");
                        string userResponse = Console.ReadLine();
                        if(userResponse == "1")
                        {
                            RPS(currentUser, roundCount++);
                        }
                        else if(userResponse == "2")
                        {
                            Console.WriteLine(userStats(currentUser));
                        }
                        else if(userResponse == "3")
                        {
                            viewLeaderboard(users);
                        }
                        else
                        {
                            keepPlaying = false;
                            break;
                        }
                    }
                }
                else if(menuChoice == "3")
                {
                    break;
                }
                else if(menuChoice == "4" && keepPlaying)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Option not valid. Enter number 1-3, stinker.");
                }
            }
            using(StreamWriter file = new StreamWriter("player_log.csv"))
                        foreach(var entry in users)
                            file.WriteLine("{0},{1},{2},{3}", entry.Key, entry.Value.getWins(), entry.Value.getLosses(), entry.Value.getDraws());
        }
        static void RPS(User user, int roundCount)
        {
            Random r = new Random();
            Console.WriteLine("Round {0}:", roundCount);
            Console.WriteLine("1.Rock");
            Console.WriteLine("2.Paper");
            Console.WriteLine("3.Scissors\n");
            Console.WriteLine("What is your choice?");
            int userInt;
            while(true)
            {
                string userChoice = Console.ReadLine();
                try
                {
                    userInt = int.Parse(userChoice);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect input, please enter an integer.");
                    continue;
                } 
                if (userInt != 1 && userInt != 2 && userInt != 3)
                {
                    Console.WriteLine("You need to enter an integer valued 1-3.");
                    continue;
                }
                break;
            }
            int compChoice = r.Next(1, 4);
            string winner = checkGame(userInt, compChoice);
            Console.WriteLine("\nYou chose {0}. The computer chose {1}. You {2}!", choiceName(userInt), choiceName(compChoice), winner);
            switch(winner)
            {
                case "won":
                    user.addWins();
                    break;
                case "draw":
                    user.addDraws();
                    break;
                case "lost":
                    user.addLosses();
                    break;
            }
        }
        static string choiceName(int choice)
        {
            switch(choice)
            {
                case 1:
                    return "Rock";
                case 2:
                    return "Paper";
                case 3:
                    return "Scissors";
                default:
                    return "";

            }
        }
        static string checkGame(int userInt, int compChoice)
        {
            switch(userInt)
            {
                case 1:
                    if(compChoice == 2)
                    {
                        return "lost";
                    }
                    else if(compChoice == 1)
                    {
                        return "draw";
                    }
                    return "won";
                case 2:
                    if(compChoice == 3)
                    {
                        return "lost";
                    }
                    else if(compChoice == 2)
                    {
                        return "draw";
                    }
                    return "won";
                case 3:
                    if(compChoice == 1)
                    {
                        return "lost";
                    }
                    else if(compChoice == 3)
                    {
                        return "draw";
                    }
                    return "won";
                default:
                    return "";
            }
        }
        static void viewLeaderboard(Dictionary<string, User> users)
        {
            Console.WriteLine("Top 10 Winning Players:");
            var topWins = users.OrderByDescending(u => u.Value.wins).Take(10);
            for(int i = 0; i < topWins.Count(); i++)
            {
                Console.WriteLine("{0}: {1} wins", topWins.ElementAt(i).Key, topWins.ElementAt(i).Value.getWins());
            }
            Console.WriteLine("Most Games Played:");
            var mostGames = users.OrderByDescending(u => u.Value.total).Take(5);
            for(int i = 0; i < mostGames.Count(); i++)
            {
                Console.WriteLine("{0}: {1} games played", mostGames.ElementAt(i).Key, mostGames.ElementAt(i).Value.total);
            }
            var totalWL = users.Sum(u => u.Value.winloss);
            Console.WriteLine("Win/Loss Ratio: {0}", totalWL);
            var totalGames = users.Sum(u => u.Value.total);
            Console.WriteLine("Total Games Played: {0}", totalGames);
        }

        static string userStats(User user)
        {
            return String.Format("{0}, here are your gameplay statistics...\nWins: {1}\nLosses: {2}\nDraws: {3}\nW/L Ratio: {4}", user.getuserName(), user.getWins(), user.getLosses(), user.getDraws(), user.winLoss());  
        }
    }
}