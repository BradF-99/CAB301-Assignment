using CAB301_Assignment.Models;
using CAB301_Assignment.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CAB301_Assignment.Views
{
    class MenuView
    {
        readonly MemberCollection memberCollection = new MemberCollection();
        readonly MovieCollection movieCollection = new MovieCollection();

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Welcome to the Community Library\n" +
                              "============Main Menu===========\n" +
                              " 1. Staff Login\n" +
                              " 2. Member Login\n" +
                              " 0. Exit\n" +
                              "================================\n\n" +
                              "Please make a selection (1-2, or 0 to exit): ");
                string menuInput = Console.ReadLine();
                bool menuValidInput = int.TryParse(menuInput, out int menuChoice);

                if (menuValidInput)
                {
                    switch (menuChoice)
                    {
                        case 1:
                            this.StaffLogin();
                            break;
                        case 2:
                            this.MemberLogin();
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                        default:
                            ErrorView.Error("Invalid selection. Please choose 1, 2 or 0.");
                            break;
                    }
                }
            }
        }

        public void StaffLogin()
        {
            Console.Clear();
            Console.Write("==========Staff Login==========\n" +
                          "Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            if (userName == "staff" && password == "today123")
            {
                this.StaffMainMenu();
            }
            else
            {
                ErrorView.Error("Invalid username and/or password. " +
                    "Please check your credentials and try again.");
            }
        }

        public void MemberLogin()
        {
            Console.Clear();
            Console.Write("==========Member Login==========\n" +
                          "Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            try
            {
                Member member = memberCollection.MemberLogin(userName, password);
                MemberMainMenu(member);
            }
            catch (Exception e)
            {
                ErrorView.Error(e);
            }
        }

        public void StaffMainMenu()
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
                        this.StaffAddMovie();
                        break;
                    case 2:
                        this.StaffRemoveMovie();
                        break;
                    case 3:
                        this.StaffAddMember();
                        break;
                    case 4:
                        this.StaffFindMember();
                        break;
                    case 0:
                        this.MainMenu();
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

        public void StaffAddMovie()
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
                Console.Write(Enum.GetName(typeof(Genre), i) + " ");
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
                        Console.Clear();
                        Console.Write("Please wait...");
                        Movie newMovie = new Movie(title, starringList, director, duration, genre, classification, releaseDate, copies);
                        movieCollection.Tree.Insert(newMovie);
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

        public void StaffRemoveMovie()
        {
            Console.Clear();
            Console.Write("==========Delete Movie==========\n" +
                "Movie Name: ");
            string movieName = Console.ReadLine();

            try
            {
                Console.Clear();
                Console.Write("Please wait...");
                Movie result = movieCollection.Tree.Search(movieName);

                bool reviewValidInput = false;
                while (!reviewValidInput)
                {
                    Console.Clear();
                    Console.Write("==========Delete Movie==========\n" +
                        "Please review the below information to ensure everything is correct.\n" +
                        "\n Title: " + result.Title +
                        "\n Starring: " + String.Join(",", result.Starring) +
                        "\n Director: " + result.Director +
                        "\n Duration: " + result.Duration + " mins" +
                        "\n Genre: " + result.Genre.ToString() +
                        "\n Classification: " + result.Classification.ToString() +
                        "\n Release Date: " + result.ReleaseDate.ToString("dd/MM/yyyy") +
                        "\n\nTo confirm deletion of this movie, press Y. To start again, press N.");
                    string reviewChoice = Console.ReadLine();

                    switch (reviewChoice.ToLower())
                    {
                        case "y":
                            Console.Clear();
                            Console.Write("Please wait...");
                            movieCollection.Tree.Remove(result);
                            return;
                        case "n":
                            return;
                    }

                    // no need for default as it will loop again if invalid response
                    Console.Beep();
                }
            }
            catch (Exception e)
            {
                ErrorView.Error(e);
            }
        }

        public void StaffAddMember()
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
                Console.Write("\nPostcode: ");
                string pcodeInput = Console.ReadLine();
                bool pcodeValidParse = int.TryParse(pcodeInput, out postcode);

                if (pcodeValidParse && postcode >= 200 && postcode <= 9999)
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
                        Console.Clear();
                        Console.Write("Please wait...");
                        Address address = new Address(propertyNumber, streetName, suburb, state, postcode);
                        memberCollection.MemberAdd(firstName, lastName, address, phoneNumber, password);
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

        public void StaffFindMember()
        {
            Console.Clear();
            Console.Write("==========Member Lookup==========\n" +
                "First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            try
            {
                Member result = memberCollection.MemberSearch(firstName, lastName);
                Console.Write("==========Member Lookup==========\n" +
                "Results: Phone number for " + firstName + " " + lastName + " is " + result.PhoneNumber +
                "\nPress any key to return to the main menu.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                ErrorView.Error(e);
            }

        }

        // MEMBER MENUS
        public void MemberMainMenu(Member member)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("==========Member Menu==========\n" +
                              "Welcome, " + member.FirstName + ". Select from the options below.\n\n" +
                              " 1. Display all movies\n" +
                              " 2. Borrow a movie DVD\n" +
                              " 3. Return a movie DVD\n" +
                              " 4. List current borrowed movie DVDs\n" +
                              " 5. Display top 10 most popular movies\n" +
                              " 0. Return to main menu\n" +
                              "==============================\n\n" +
                              "Please make a selection (1-5, or 0 to return to main menu): ");
                string menuInput = Console.ReadLine();
                bool menuValidInput = int.TryParse(menuInput, out int menuChoice);

                if (menuValidInput)
                {
                    switch (menuChoice)
                    {
                        case 1:
                            MemberDisplayAllMovies();
                            break;
                        case 2:
                            MemberBorrowMovie(member);
                            break;
                        case 3:
                            MemberReturnMovie(member);
                            break;
                        case 4:
                            MemberListBorrowed(member);
                            break;
                        case 5:
                            MemberDisplayPopular();
                            break;
                        case 0:
                            MainMenu();
                            break;
                        default:
                            ErrorView.Error("Invalid selection. Please choose 1, 2, 3, 4, 5 or 0.");
                            break;
                    }
                }
            }
        }

        public void MemberDisplayAllMovies()
        {
            List<Movie> movieList = movieCollection.Tree.Traverse();
            if (movieList.Count == 0)
            {
                ErrorView.Error("There are no movies yet.");
            }

            Console.Clear();
            Console.Write("==========List of Movies==========\n" +
                "Here are the movies on file.\n\n");

            for (int i = 0; i < movieList.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + movieList[i].Title);
            }

            Console.Write("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }

        public void MemberBorrowMovie(Member member)
        {
            Console.Clear();
            Console.Write("==========Borrow Movie==========\n" +
                "Movie Name: ");
            string movieName = Console.ReadLine();

            try
            {
                Console.Clear();
                Console.Write("Please wait...");
                Movie result = movieCollection.Tree.Search(movieName);

                bool reviewValidInput = false;
                while (!reviewValidInput)
                {
                    Console.Clear();
                    Console.Write("==========Borrow Movie==========\n" +
                        "Please review the below information to ensure everything is correct.\n" +
                        "\n Title: " + result.Title +
                        "\n Starring: " + String.Join(",", result.Starring) +
                        "\n Director: " + result.Director +
                        "\n Duration: " + result.Duration + " mins" +
                        "\n Genre: " + result.Genre.ToString() +
                        "\n Classification: " + result.Classification.ToString() +
                        "\n Release Date: " + result.ReleaseDate.ToString("dd/MM/yyyy") +
                        "\n\nTo confirm borrowing this movie, press Y. To start again, press N.");
                    string reviewChoice = Console.ReadLine();

                    switch (reviewChoice.ToLower())
                    {
                        case "y":
                            Console.Clear();
                            Console.Write("Please wait...");
                            if (member.RentedMovies.Count >= 10)
                                throw new Exception("You have reached your quota of borrowed movies. " +
                                "Please return some movies before attempting to borrow more.");
                            movieCollection.Tree.BorrowMovie(result);
                            member.RentedMovies.Add(result);
                            return;
                        case "n":
                            return;
                    }

                    // no need for default as it will loop again if invalid response
                    Console.Beep();
                }
            }
            catch (Exception e)
            {
                ErrorView.Error(e);
            }
        }

        public void MemberReturnMovie(Member member)
        {
            if (member.RentedMovies.Count == 0)
            {
                ErrorView.Error("You have no movies to return.");
            }
            else
            {
                Console.Clear();
                Console.Write("==========Return Movie==========\n" +
                    "Here are your currently borrowed movies. Please select the one you wish to return or type 0 to exit.\n\n");

                for (int i = 0; i < member.RentedMovies.Count; i++)
                {
                    Console.WriteLine(i + 1 + ". " + member.RentedMovies[i].Title);
                }

                Movie returnChoice = null;
                int choice = 0;
                bool returnChoiceValidInput = false;

                while (!returnChoiceValidInput)
                {
                    string choiceInput = Console.ReadLine();
                    bool choiceValidParse = Int32.TryParse(choiceInput, out choice);

                    if (choiceValidParse)
                    {
                        returnChoiceValidInput = true;
                        if (choice == 0)
                        {
                            return;
                        }
                        if (choice <= member.RentedMovies.Count)
                        {
                            returnChoice = member.RentedMovies[choice - 1];
                        }
                    }
                }

                try
                {
                    Console.Clear();
                    Console.Write("Please wait...");
                    movieCollection.Tree.ReturnMovie(returnChoice);
                    member.RentedMovies.RemoveAt(choice - 1);
                }
                catch (Exception e)
                {
                    ErrorView.Error(e);
                }
            }
        }

        public void MemberListBorrowed(Member member)
        {
            if (member.RentedMovies.Count == 0)
                throw new Exception("You have borrowed no movies.");

            Console.Clear();
            Console.Write("==========Borrowed Movies==========\n" +
                "Here are your currently borrowed movies.\n\n");

            for (int i = 0; i < member.RentedMovies.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + member.RentedMovies[i].Title);
            }

            Console.Write("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }

        public void MemberDisplayPopular()
        {
            List<Movie> movieList = movieCollection.Tree.Traverse();
            if (movieList.Count == 0)
            {
                ErrorView.Error("There are no movies yet.");
            }

            movieList = movieList.OrderByDescending(movie => movie.AmountBorrowed).Take(10).ToList();

            Console.Clear();
            Console.Write("==========Most Popular Movies==========\n" +
                "Here are the top 10 most frequently borrowed movies.\n\n");

            for (int i = 0; i < movieList.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + movieList[i].Title);
            }

            Console.Write("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }
    }
}
