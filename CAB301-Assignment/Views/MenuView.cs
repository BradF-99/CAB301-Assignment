using System;
using System.Collections.Generic;
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
                        break;
                    case 2:
                        break;
                    case 0:
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

        public void StaffLogin()
        {

        }

        public void MemberLogin()
        {

        }
    }
}
