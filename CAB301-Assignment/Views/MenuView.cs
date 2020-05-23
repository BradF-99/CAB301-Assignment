using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CAB301_Assignment.Views
{
    class MenuView
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.Write("Welcome to the Community Library\n" +
                          "============Main Menu===========\n" +
                          " 1. Staff Login\n" +
                          " 2. Member Login\n" +
                          " 0. Exit\n" +
                          "================================\n\n" +
                          "Please make a selection (1-2, or 0 to exit): ");
            string menuChoice = Console.ReadLine();

            try
            {
                int result = Int32.Parse(menuChoice);
                switch (result)
                {
                    case 1:
                        StaffLogin();
                        break;
                    case 2:
                        MemberLogin();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        ErrorView.Error("Invalid selection. Please choose 1, 2 or 0.");
                        MainMenu();
                        break;
                }
            }
            catch (Exception e)
            {
                if(e is FormatException)
                {
                    ErrorView.Error("Invalid selection. Please choose 1, 2 or 0.");
                }
                else
                {
                    ErrorView.Error(e.Message);
                }
                MainMenu();
            }
        }

        public static void StaffLogin()
        {
            Console.Clear();
            Console.Write("==========Staff Login==========\n" +
                          "Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            if(userName == "staff" && password == "today123")
            {
                StaffMainMenu();
            } 
            else
            {
                ErrorView.Error("Invalid username and/or password. " +
                    "Please check your credentials and try again.");
                MainMenu();
            }
        }

        public static void MemberLogin()
        {
            Console.Clear();
            Console.Write("==========Member Login==========\n" +
                          "Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            if (userName == "staff" && password == "today123") // fix
            {
                MemberMainMenu();
            }
            else
            {
                ErrorView.Error("Invalid username and/or password. " +
                    "Please check your credentials and try again.");
                MemberLogin();
            }
        }

        public static void StaffMainMenu()
        {
            Console.Clear();
            Console.Write("==========Staff Menu==========\n" +
                          " 1. Add a new movie DVD\n" +
                          " 2. Remove a movie DVD\n" +
                          " 3. Register a new Member\n" +
                          " 4. Find a registered member's phone number\n" +
                          " 0. Return to main menu\n" +
                          "==============================\n\n" +
                          "Please make a selection (1-4, or 0 to return to main menu): ");
            
            string menuChoice = Console.ReadLine();

            try
            {
                int result = Int32.Parse(menuChoice);
                switch (result)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 0:
                        MainMenu();
                        break;
                    default:
                        ErrorView.Error("Invalid selection. Please choose 1, 2, 3, 4 or 0.");
                        StaffMainMenu();
                        break;
                }
            }
            catch (Exception e)
            {
                if (e is FormatException)
                {
                    ErrorView.Error("Invalid selection. Please choose 1, 2, 3, 4 or 0.");
                }
                else
                {
                    ErrorView.Error(e.Message);
                }
                StaffMainMenu();
            }
        }

        public static void MemberMainMenu()
        {
            Console.Clear();
            Console.Write("==========Member Menu==========\n" +
                          " 1. Display all movies\n" +
                          " 2. Borrow a movie DVD\n" +
                          " 3. Return a movie DVD\n" +
                          " 4. List current borrowed movie DVDs\n" +
                          " 5. Display top 10 most popular movies\n" +
                          " 0. Return to main menu\n" +
                          "==============================\n\n" +
                          "Please make a selection (1-5, or 0 to return to main menu): ");
            
            string menuChoice = Console.ReadLine();

            try
            {
                int result = Int32.Parse(menuChoice);
                switch (result)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 0:
                        MainMenu();
                        break;
                    default:
                        ErrorView.Error("Invalid selection. Please choose 1, 2, 3, 4, 5 or 0.");
                        MemberMainMenu();
                        break;
                }
            }
            catch (Exception e)
            {
                if (e is FormatException)
                {
                    ErrorView.Error("Invalid selection. Please choose 1, 2, 3, 4, 5 or 0.");
                }
                else
                {
                    ErrorView.Error(e.Message);
                }
                MemberMainMenu();
            }
        }
    }
}
