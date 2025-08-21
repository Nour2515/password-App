using passwordmanager;
using System;
using System.Text;

namespace MyApplication
{
    internal class Program
    {
        private static readonly Dictionary<string, string> enteriespasswords = new Dictionary<string, string>();
        private static readonly String filepath = @"C:\Users\nourp\OneDrive\Desktop\DEPI\train\passwordmanager\passwordmanager\pass.txt";
        static void Main(string[] args)
        {
            readpasswords();
            while (true)
            {
                Console.WriteLine("Welcome at password manager project :");
                Console.WriteLine("Enter the option:");
                Console.WriteLine(" 1-list all passwords \n 2-Add/Change password \n 3-get passwords \n 4-Delete password \n 5-EXIT");
                var selected_option = Console.ReadLine();

                if (selected_option == "1")
                {
                    listallpasswords();
                }
                else if (selected_option == "2")
                {
                    AddorChangepasswords();
                }
                else if (selected_option == "3")
                {
                    getpasswords();
                }
                else if (selected_option == "4")
                {
                    Deletepasswords();
                }
                else if (selected_option == "5") { 
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("invalid option");
                }
                Console.WriteLine("---------------------------");
            }
        }

         private  static void listallpasswords() {
            foreach (var entery in enteriespasswords) { 
                Console.WriteLine($"{entery.Key}={entery.Value}");
            }
        }
        private static void AddorChangepasswords()
        {
            Console.WriteLine("Enter your app:");
            var App=Console.ReadLine();
            Console.WriteLine("Enter your password:");
            var pass=Console.ReadLine();

            string strength = CheckPasswordStrength(pass);
            Console.WriteLine($"Password strength: {strength}");

            if (enteriespasswords.ContainsKey(App))
            {
                enteriespasswords[App] = pass;
            }
            else { 
                enteriespasswords.Add(App, pass);
            }

            savepasswords();
        }
        private static void getpasswords()
        {
            Console.WriteLine("Enter your app:");
            var App = Console.ReadLine();
            if (enteriespasswords.ContainsKey(App))
            {
                Console.WriteLine($"your password is:{enteriespasswords[App]}");
            }
            else
            {
                Console.WriteLine("password is not found");
            }

        }
        private static void Deletepasswords()
        {
            Console.WriteLine("Enter your app:");
            var App = Console.ReadLine();
            if (enteriespasswords.ContainsKey(App)) {
                enteriespasswords.Remove(App);
                savepasswords() ;
            }
            else
            {
                Console.WriteLine("password is not found");
            }
        }
        private static void readpasswords()
        {
            if (File.Exists(filepath))
            {
                foreach (var line in File.ReadAllLines(filepath))
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        var equal_operator = line.IndexOf("=");
                        var appname = line.Substring(0, equal_operator);
                        var pass = line.Substring(equal_operator + 1);
                        enteriespasswords.Add(appname, Encription.decrypt(pass));
                    }
                }
            }
            else {
                File.Create(filepath).Close();
            }
        }
        private static void savepasswords()
        {
            var sb = new StringBuilder();
            foreach(var entery in enteriespasswords) {
                sb.AppendLine($"{entery.Key}={Encription.encrypt(entery.Value)}");
            }
            File.WriteAllText(filepath, sb.ToString());
        }
        private static string CheckPasswordStrength(string password)
        {
            int score = 0;
            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else hasSpecial = true;
            }

            if (password.Length >= 8)
                score++; 
            if (hasUpper) score++;
            if (hasLower) score++;
            if (hasDigit) score++;
            if (hasSpecial) score++;

            string strength;
            switch (score)
            {
                case 0:
                case 1:
                case 2:
                    strength = "Weak";
                    break;
                case 3:
                case 4:
                    strength = "Medium";
                    break;
                default:
                    strength = "Strong";
                    break;
            }

            return strength;
        }


    }
}
