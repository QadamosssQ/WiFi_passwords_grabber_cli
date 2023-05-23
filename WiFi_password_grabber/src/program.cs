using System;
using System.Diagnostics;

namespace wifi_password_grabber
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Detected networks:\n");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = "wlan show profile",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int a = 0;
            string[] sieci = new string[lines.Length];

            foreach (string line in lines)
            {
                if (line.Contains("All User Profile"))
                {
                    a++;
                    string linia_1 = line.Replace("All User Profile     : ", "");

                    string linia_2 = linia_1.Substring(4);

                    sieci[a] = linia_2;
                }
            }

            if (a > 0)
            {
                for (int i = 1; i <= a; i++)
                {
                    Console.Write(i + ". ");
                    Console.WriteLine(sieci[i]);
                }
            }

            Console.WriteLine("\nChoose network");
            string input = Console.ReadLine();
            int input_n = 0;

            if (int.TryParse(input, out int n))
            {
                input_n = Convert.ToInt32(input);

                if (input_n <= 0 || input_n > a)
                {
                    wrong_input();
                }
            }
            else
            {
                wrong_input();
            }


            Console.WriteLine(sieci[input_n]);
            Console.WriteLine("\nChoose action \n 1. Show password \n 2. Show all info");

            string action = Console.ReadLine();

            switch (action)
            {
                case "2":
                    Console.WriteLine(show_wifi(sieci[input_n]));
                    reset();
                    break;

                case "1":
                    Console.WriteLine(Show_wifi_password(sieci[input_n]));
                    reset();
                    break;

                default:
                    wrong_input();
                    break;
            }

            Console.ReadKey();
        }

        static string show_wifi(string wifi_name)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = "wlan show profile name=\"" + wifi_name + "\" key=clear",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }

        static string Show_wifi_password(string wifi_name)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = "wlan show profile name=\"" + wifi_name + "\" key=clear",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int a = 0;
            string[] sieci = new string[lines.Length];

            foreach (string line in lines)
            {
                if (line.Contains("    Key Content            : "))
                {
                    a++;
                    string linia_1 = line.Replace("    Key Content            : ", "");
                    sieci[0] = linia_1;
                }
            }

            return "\nPassword: " + sieci[0];
        }

        static void reset()
        {
            Console.WriteLine("\nContinue Y/N");
            string continue_ask = Console.ReadLine().ToLower();

            switch (continue_ask)
            {
                case "y":
                    Console.Clear();
                    Main();
                    break;

                default:
                    Environment.Exit(0);
                    break;
            }
        }

        static void wrong_input()
        {
            Console.WriteLine("Wrong input!");
            reset();
        }


    }
}

