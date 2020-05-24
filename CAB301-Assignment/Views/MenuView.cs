using CAB301_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            }
            MainMenu();
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
                        StaffAddMovie();
                        break;
                    case 2:
                        break;
                    case 3:
                        StaffAddMember();
                        break;
                    case 4:
                        break;
                    case 0:
                        MainMenu();
                        break;
                    default:
                        ErrorView.Error("Invalid selection. Please choose 1, 2, 3, 4 or 0.");
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
            }
            StaffMainMenu();
        }

        public static void StaffAddMovie()
        {
            Console.Clear();
            Console.Write("==========Add Movie==========\n");
            
            // Title
            Console.Write("Title: ");
            string title = Console.ReadLine();

            // Starring
            Console.Write("Starring (comma separated): ");
            string starring = Console.ReadLine();
            List<string> starringList = starring.Split(",").ToList();

            // Director
            Console.Write("Director: ");
            string director = Console.ReadLine();

            // Duration
            int duration = 0; // keep the compiler happy
            bool durationValidInput = false;
            while (!durationValidInput)
            {
                Console.Write("Duration (in minutes, no letters or colons): ");
                string durationInput = Console.ReadLine();
                durationValidInput = Int32.TryParse(durationInput, out duration);
            }

            // Genre
            bool genreValidInput = false;
            Genre genre = Genre.Other; // keep the compiler happy again

            Console.Write("\nAllowable genres are: ");
            int genreLength = Enum.GetValues(typeof(Genre)).Length;
            for (int i = 0; i < genreLength; i++)
            {
                Console.Write(Enum.GetName(typeof(Genre),i) + " ");
            }

            while (!genreValidInput)
            {
                Console.Write("\nGenre: ");
                string genreInput = Console.ReadLine();
                genreValidInput = Enum.TryParse(genreInput, out genre);
            }

            // Classification
            bool classificationValidInput = false;
            Classification classification = Classification.G; // keep the compiler happy yet again

            Console.Write("\nAllowable classifications are: ");
            int classificationLength = Enum.GetValues(typeof(Classification)).Length;
            for (int i = 0; i < classificationLength; i++)
            {
                Console.Write(Enum.GetName(typeof(Classification), i) + " ");
            }

            while (!classificationValidInput)
            {
                Console.Write("\nClassification: ");
                string classificationInput = Console.ReadLine();
                classificationValidInput = Enum.TryParse(classificationInput, out classification);
            }

            // Release Date
            bool dateValidInput = false;
            DateTime releaseDate = DateTime.Now; // once again I am suppressing CS0165

            while (!dateValidInput)
            {
                Console.Write("\nRelease Date (DD/MM/YYYY): ");
                string releaseDateInput = Console.ReadLine();
                dateValidInput = DateTime.TryParse(releaseDateInput, out releaseDate);
            }

            // Copies
            int copies = 0; // same as all the others
            bool copiesValidInput = false;
            while (!copiesValidInput)
            {
                Console.Write("Copies on hand: ");
                string copiesInput = Console.ReadLine();
                copiesValidInput = Int32.TryParse(copiesInput, out copies);
            }

            // Review
            bool reviewValidInput = false;
            while (!reviewValidInput)
            {
                Console.Clear();
                Console.Write("==========Add Movie==========\n" +
                    "Please review the below information to ensure everything is correct.\n" +
                    "\n Title: " + title +
                    "\n Starring: " + String.Join(",", starringList) +
                    "\n Director: " + director +
                    "\n Duration: " + duration + " mins" +
                    "\n Genre: " + genre.ToString() +
                    "\n Classification: " + classification.ToString() +
                    "\n Release Date: " + releaseDate.ToString("dd/MM/yyyy") +
                    "\n Copies: " + copies.ToString() +
                    "\n\nTo add this movie to the database, press Y. To start again, press N.");
                string reviewChoice = Console.ReadLine();

                switch (reviewChoice.ToLower())
                {
                    case "y":
                        // add db
                        StaffMainMenu();
                        break;
                    case "n":
                        StaffAddMovie();
                        break;
                }

                // no need for default as it will loop again if invalid response
                Console.Beep();
            }
        }

        public static void StaffRemoveMovie()
        {

        }

        public static void StaffAddMember()
        {
            Console.Clear();
            Console.Write("==========Add New Member==========\n");

            // First Name
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            // Last Name
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            // Phone Number
            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();

            // Address
            // Unit Number
            string unitNumber = "N/A"; // used later
            Console.Write("\nUnit Number (leave blank if inapplicable): ");
            // we parse this at the end to determine which model to use
            unitNumber = Console.ReadLine(); 

            // Property Number
            Console.Write("\nProperty / House Number: ");
            string propertyNumber = Console.ReadLine();

            // Street Name
            Console.Write("\nStreet Name: ");
            string streetName = Console.ReadLine();

            // Suburb
            Console.Write("\nSuburb: ");
            string suburb = Console.ReadLine();

            // State
            bool stateValidInput = false;
            State state = State.NSW; // happy compiler happy life

            Console.Write("\nAllowable states / territories are: ");
            int stateLength = Enum.GetValues(typeof(State)).Length;
            for (int i = 0; i < stateLength; i++)
            {
                Console.Write(Enum.GetName(typeof(State), i) + " ");
            }

            while (!stateValidInput)
            {
                Console.Write("\nState: ");
                string stateInput = Console.ReadLine();
                stateValidInput = Enum.TryParse(stateInput, out state);
            }

            // Postcode
            bool pcodeValidInput = false;
            int postcode = 4000; // happy compiler happy life

            Console.Write("\nAllowable postcode range is: 0200 - 9999.");

            while (!pcodeValidInput)
            {
                Console.Write("\nState: ");
                string pcodeInput = Console.ReadLine();
                bool pcodeValidParse = Int32.TryParse(pcodeInput, out postcode);
                
                if(pcodeValidParse && postcode >= 200 && postcode <= 9999)
                {
                    pcodeValidInput = true;
                }
            }

            // Password
            bool passwordValidInput = false;
            int password = 0000; // happy compiler happy life

            while (!passwordValidInput)
            {
                Console.Write("\n\nPassword (4 digit number): ");
                string passwordInput = Console.ReadLine();
                bool passwordValidParse = Int32.TryParse(passwordInput, out password);

                if (passwordValidParse && password >= 0 && password <= 9999)
                {
                    passwordValidInput = true;
                }
            }

            // Determine which model to use


            // Review
            bool reviewValidInput = false;
            while (!reviewValidInput)
            {
                Console.Clear();
                Console.Write("==========Add New Member==========\n" +
                    "Please review the below information to ensure everything is correct.\n" +
                    "\n First Name: " + firstName +
                    "\n Last Name: " + lastName +
                    "\n Phone Number: " + phoneNumber +
                    "\n\n Unit Number: " + unitNumber +
                    "\n Property Number: " + propertyNumber +
                    "\n Street Name: " + streetName +
                    "\n Suburb: " + suburb +
                    "\n State: " + state.ToString() +
                    "\n Postcode: " + postcode +
                    "\n\n Username: " + lastName + firstName +
                    "\n Password: " + password +
                    "\n\nTo add this member to the database, press Y. To start again, press N.");
                string reviewChoice = Console.ReadLine();

                switch (reviewChoice.ToLower())
                {
                    case "y":
                        // add db
                        StaffMainMenu();
                        break;
                    case "n":
                        StaffAddMember();
                        break;
                }

                // no need for default as it will loop again if invalid response
                Console.Beep();
            }

        }

        public static void StaffFindMember()
        {

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
