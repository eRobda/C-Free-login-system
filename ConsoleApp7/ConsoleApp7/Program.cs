using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MyApp 
{
    internal class Program
    {
        //define the web name
        static string web = "you web name";
        //make a dictionary to store all the usernames and passwords
        static Dictionary<string, string> accounts = new();

        //these variables are what you type into console
        static string username;
        static string password;


        static void Main(string[] args)
        {
            //The web it tries to acces can be down or deleted. To avoid the app crash i used try
            try
            {
                LoadAccounts();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //ask for username
            Console.Write("Username: ");
            username = Console.ReadLine();

            //ask for password
            Console.Write("Password: ");
            password = Console.ReadLine();

            //if username and password are mathing one of accounts in databse
            if (Login(username, password, accounts, web))
            {
                //Do something when username and password are correct
                Console.WriteLine("Login succesful.");
            }
            else
            {
                //Do something if username and password are incorrect
                Console.WriteLine("Wrong username or password.");
            }
                
        }

        //This method get all usernames and password from database you created.
        static void LoadAccounts()
        {
            //temponary strings...
            string first;
            string second;

            WebClient client = new();

            //open webclient and streamreader and read from that web
            using (var stream = client.OpenRead(web))
            using (var sr = new StreamReader(stream))
            {
                string line;
                //while cycle til all lines are read
                while (sr.Peek() >= 0)
                {
                    first = sr.ReadLine();
                    second = sr.ReadLine();
                    //add read data to dictionary
                    accounts.Add(first, second);
                }
                //close stream reader and web
                sr.Close();
                stream.Close();
            }
        }

        //this method chcecks if entered username and password are mathing one of acconts in database
        public static bool Login(string user, string pass, Dictionary<string, string> dict, string net)
        {
            foreach (KeyValuePair<string,string> pair in dict)
            {
                if(pair.Value == pass && pair.Key == user)
                    return true;
            }
            return false;
        }
    }
}