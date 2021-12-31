using System;
using System.Collections;
using System.Configuration;
using System.Linq;



namespace MoviePlex
{
    class Program
    {
        static void Main(string[] args)
        {
            //ArrayList is using System.Collections Package
            //Global List of Movie and Age Arraylist
            ArrayList List_movie = new ArrayList();
            ArrayList List_age = new ArrayList();

        Start:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t**************************************");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t*****Welcome To MoviePlex Theatre*****");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t**************************************");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Please Select From the following Options:");
            Console.WriteLine(" 1) Administrator");
            Console.WriteLine(" 2) User");

            //Declaring value to take in loop 
            bool flag_para = false;

            do

            {
                Console.Write("\nEnter Your selection: ");

                string user_option = Convert.ToString(Console.ReadLine());
                if (user_option == "1" || user_option == "2")
                {
                    flag_para = true;
                    switch (user_option)
                    {
                        case "1":
                            bool valid_check = valid_authorization();
                            if (valid_check == false)
                            {
                                goto Start;
                            }
                            else
                            {
                                //send List_movie and List_age Parameters
                                dash_Admin(List_movie, List_age);

                                Console.Clear();
                                goto Start;
                            }
                        case "2":
                            //string user = "Guest";
                            //test(user, a1, a2);
                            //List_movie(user);
                            dash_User(List_movie, List_age);
                            goto Start;

                        case "\0":
                            break;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid selection.");
                }

            } while (flag_para == false);
        }


        private static bool valid_authorization()
        {
            Console.Clear();

            string admin_Id = ConfigurationManager.AppSettings["AdminUserName"];
            string admin_Password = ConfigurationManager.AppSettings["AdminPassword"];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t**************************************");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t*****Welcome To MoviePlex Theatre*****");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t**************************************");
            Console.ResetColor();

            Console.WriteLine();

            Console.WriteLine("Welcome Administrator !! Please enter your login credentials ");

            //Initialzise Counter Value and i=5 for Login Chances
            int count = 0, i = 5;

            Console.Write("\nEnter your UserID: ");
            string user_input_id = Console.ReadLine();
            do
            {
                Console.Write("Enter your Password: ");

                string user_input_password = "";
                ConsoleKeyInfo data = Console.ReadKey(true);
                while (data.Key != ConsoleKey.Enter)
                {
                    
                    if (data.Key != ConsoleKey.Backspace)
                    {
                        user_input_password += data.KeyChar;
                        data = Console.ReadKey(true);
                        Console.Write("*");
                    }
                     else if (data.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(user_input_password))
                        {
                            user_input_password = user_input_password.Substring(0, user_input_password.Length - 1);
                            Console.Write("*");
                        }
                        data = Console.ReadKey(true);
                    }
                }


                if (user_input_id != admin_Id || user_input_password != admin_Password)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your credentials are invalid. You have {0} Chances left ", i);
                    Console.ResetColor();

                    Console.WriteLine();
                    if (i == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Oppss!! please reset your password if you forgot it. ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press enter to Redirect main menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        return false;
                    }

                    i--;
                    count++;
                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Welcome !!!");
                    return true;
                }

            } while (count != 6);
            return false;
        }

        private static void dash_Admin(ArrayList movie, ArrayList age)
        {
            ArrayList list_of_movie = movie;
            ArrayList limit_age = age;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t**************************************");
            Console.WriteLine("\t \t \t \t \t*****Welcome To MoviePlex Theatre*****");
            Console.WriteLine("\t \t \t \t \t**************************************");
            Console.ResetColor();


           

            Console.WriteLine();

            Console.WriteLine("Welcome MoviePlex Administrator");
            Console.WriteLine();
            string userInput;
            do
            {

                //clear List_movie and List_age array

                list_of_movie.Clear();
                limit_age.Clear();
                string number_of_movie;
                int count_of_movie, Num, count = 0;
                bool val = false;

                int min_Range = Convert.ToInt32(ConfigurationManager.AppSettings["ageMin"]);
                int max_Range = Convert.ToInt32(ConfigurationManager.AppSettings["ageMax"]);

                string[] List_ratings = { "G", "PG", "PG-13", "R", "NC-17" };

                Console.WriteLine();

                //Console.Write("How many movie list you want us to Play today ? ");

                while (val == false)
                {
                    Console.Write("How many movie list you want us to Play today ? ");
                    number_of_movie = Console.ReadLine();
                    if (int.TryParse(number_of_movie, out Num))
                    {
                        if (!(Convert.ToInt32(number_of_movie) <= 10 && Convert.ToInt32(number_of_movie) >= 1))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter a number in between 1 to 10");
                            Console.ResetColor();
                            val = false;
                        }
                        else
                        {
                            count_of_movie = Convert.ToInt32(number_of_movie);
                            do
                            {
                                count = 0;
                                list_of_movie.Clear();
                                limit_age.Clear();

                                for (int i = 0; i < count_of_movie; i++)
                                {
                                    Console.Write("Enter the {0} movie Name: ", i + 1);
                                    string userList_movie = Console.ReadLine();
                                    Console.Write("Please enter valid age limit or rating for {0} movie: ", i + 1);
                                    string userlimit_age = Console.ReadLine();

                                    Console.WriteLine();

                                    foreach (string j in List_ratings)
                                    {
                                        if (userlimit_age.Equals(j))
                                        {
                                            count++;
                                            list_of_movie.Add(userList_movie);
                                            limit_age.Add(userlimit_age);
                                        }
                                    }
                                    if (int.TryParse(userlimit_age, out Num))
                                    {
                                        if (!(Convert.ToInt32(userlimit_age) < min_Range || Convert.ToInt32(userlimit_age) > max_Range))
                                        {
                                            count++;
                                            list_of_movie.Add(userList_movie);
                                            limit_age.Add(userlimit_age);
                                        }
                                        else
                                        {
                                            // Console.WriteLine("Please enter a valid age. [PS: Age should be between 1 to 100]");
                                            val = false;
                                        }
                                    }
                                    else
                                    {
                                        //Console.WriteLine("Please Enter age in correct Format as given above.");
                                        val = false;
                                    }
                                }
                                if (count == count_of_movie)
                                {
                                    val = true;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Please Enter age in correct Format as given below.\n \t" + "G OR " + "PG OR" + " PG-13 OR " + " R OR " + " NC-17 OR " + "\n \tOr any age In between {0} and {1}.", min_Range, max_Range);
                                    val = false;
                                    Console.ResetColor();
                                }

                                //valid = true;

                            } while (val == false);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not an integer, please  again.");
                    }

                }
                Console.WriteLine();

                //create an array of list of movies depending upon the input [Maximum 10]:
                for (int i = 0; i < list_of_movie.Count; i++)
                {
                    Console.WriteLine("{0}. {1}  : {2}{3:D}{4}", i + 1, list_of_movie[i], "{", limit_age[i], "}");
                }
                Console.WriteLine();
                Console.Write("You movie Playing today are listed above. Are you satisfied? (Y/N): ");
                userInput = Console.ReadLine();
            } while (userInput.ToUpper() != "Y");
        }


        private static void dash_User(ArrayList movie, ArrayList age)
        {
        movie_selectionlist:
            Console.Clear();

            ArrayList list_of_movie = movie;
            ArrayList limit_age = age;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t**************************************");
            Console.ResetColor();
            Console.WriteLine("\t \t \t \t \t*****Welcome To MoviePlex Theatre*****");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t \t \t \t \t**************************************");
            Console.ResetColor();

            string[] List_ratings = { "G", "PG", "PG-13", "R", "NC-17" };

            int min_Range = Convert.ToInt32(ConfigurationManager.AppSettings["ageMin"]);
            int max_Range = Convert.ToInt32(ConfigurationManager.AppSettings["ageMax"]);
            Console.WriteLine();
            int counter = list_of_movie.Count;
            if (counter == 0)
            {
                Console.WriteLine("There are no movies currently playing. Please reach out to us after some time");
            }
            else
            {
                Console.WriteLine("There are {0} movies playing today. Please choose from the following movies: ", movie.Count);
                Console.WriteLine();

                //create an array of list of movies depending upon the input [Maximum 10]:

                for (int i = 0; i < list_of_movie.Count; i++)
                {
                    Console.WriteLine("{0}. {1}  : {2}{3:D}{4}", i + 1, list_of_movie[i], "{", limit_age[i], "}");
                }
                Console.WriteLine();

                bool flag_para = false;
                int movie_Count = list_of_movie.Count;
                do
                {
                    Console.Write("Which movie would you like to watch : ");
                    string movie_Selection = Console.ReadLine();

                    //Console.Write("Enter Your selection: ");
                    //string user_option = Convert.ToString(Console.ReadLine());

                    try
                    {
                        if (Convert.ToInt32(movie_Selection) <= movie_Count && Convert.ToInt32(movie_Selection) > 0)
                        {
                            flag_para = true;

                            do
                            {
                                Console.Write("Please enter your Age for verification : ");
                                flag_para = false;
                                try
                                {
                                    int verify_Age = Convert.ToInt32(Console.ReadLine());

                                    int num = Convert.ToInt32(movie_Selection);

                                    if (limit_age[num - 1] is string)
                                    {

                                        //"G", , "PG" , "PG-13", "R", "NC-17"

                                        if (limit_age[num - 1].Equals("G"))
                                        {
                                            if (Enumerable.Range(min_Range, max_Range).Contains(verify_Age))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Enjoy the Movie!!");
                                                Console.ResetColor();
                                                flag_para = true;
                                            }
                                            else if (!Enumerable.Range(min_Range, max_Range).Contains(verify_Age))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("You do not qualify the age limit permitted");
                                                Console.ResetColor();
                                                //goto movie_selectionlist;
                                            }

                                        }
                                        else if (limit_age[num - 1].Equals("PG"))
                                        {
                                            min_Range = 10;
                                            if (Enumerable.Range(min_Range, max_Range).Contains(verify_Age))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Enjoy the Movie!!");
                                                Console.ResetColor();
                                                flag_para = true;
                                            }
                                            else if (!Enumerable.Range(min_Range, max_Range).Contains(verify_Age))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("You do not qualify the age limit permitted");
                                                Console.ResetColor();
                                                // goto movie_selectionlist;
                                            }

                                        }
                                        else if (limit_age[num - 1].Equals("PG-13"))
                                        {
                                            min_Range = 13;
                                            if (Enumerable.Range(min_Range, max_Range).Contains(verify_Age))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Enjoy the Movie!!");
                                                Console.ResetColor();
                                                flag_para = true;
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("You do not qualify the age limit permitted");
                                                Console.ResetColor();
                                                //goto movie_selectionlist;
                                            }

                                        }
                                        else if (limit_age[num - 1].Equals("R"))
                                        {
                                            min_Range = 17;
                                            if (Enumerable.Range(min_Range, max_Range).Contains(verify_Age))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Enjoy the Movie!!");
                                                Console.ResetColor();
                                                flag_para = true;
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("You do not qualify the age limit permitted");
                                                Console.ResetColor();
                                                //goto movie_selectionlist;
                                            }

                                        }
                                        else if (limit_age[num - 1].Equals("NC-17"))
                                        {
                                            min_Range = 17;
                                            if (Enumerable.Range(min_Range, max_Range).Contains(verify_Age))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Enjoy the Movie!!");
                                                Console.ResetColor();
                                                flag_para = true;
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("You do not qualify the age limit permitted");
                                                Console.ResetColor();
                                                //goto movie_selectionlist;
                                            }

                                        }
                                        else if (Convert.ToInt32(limit_age[num - 1]) is int)
                                        {
                                            min_Range = Convert.ToInt32(limit_age[num - 1]);
                                            if (Enumerable.Range(min_Range, max_Range).Contains(verify_Age))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Enjoy the Movie!!");
                                                Console.ResetColor();
                                                flag_para = true;
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("You do not qualify the age limit permitted");
                                                Console.ResetColor();
                                                //goto movie_selectionlist;
                                            }
                                        }

                                    }


                                    

                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid Age Entered.", e.Message);
                                    Console.ResetColor();
                                }

                            } while (flag_para == false);


                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid selection.");
                            Console.ResetColor();
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Movie Selected", ex.Message);
                        Console.ResetColor();
                    }

                    //Console.WriteLine("A");

                } while (flag_para == false);
            }
            bool flag_para_Out = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please Press M to go back to the Guest Menu.");
                Console.WriteLine("Please Press S to go back to the Start Page.");


                string user_option = Convert.ToString(Console.ReadLine());
                if (user_option == "M" || user_option == "S" || user_option == "s" || user_option == "m")
                {
                    flag_para_Out = true;
                    switch (user_option)
                    {
                        case "M":
                            goto movie_selectionlist;
                        case "m":
                            goto movie_selectionlist;
                        case "S":

                            break;
                        case "s":
                            break;
                        case "\0":
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection.");
                    Console.ResetColor();
                }

            } while (flag_para_Out == false);
        }
    }
}
