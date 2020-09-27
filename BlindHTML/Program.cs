using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace BlindHTML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BlindHTML [created by: @sosakiloz]";
            Console.WriteLine("Welcome to BlindHTML with ip logging proxies!");
            //Console.WriteLine("Processing documents...");
            processdirs(args);
            Console.WriteLine("Processing has finished.");
            Console.ReadLine();
        }
        static void processdirs(string[] array)
        {
            foreach (string directory in array)
            {
                if (Directory.Exists(directory))
                {
                    Console.WriteLine($"Processing directory {Path.GetFileNameWithoutExtension(directory)}...");
                    string[] names = new[] { "index", "default", "home", "placeholder" };
                    string[] exts = new[] { "html", "htm", "shtml", "php", "php5", "php4", "php3", "cgi"};
                    List<bool> filebools = new List<bool>();

                    foreach (string file in Directory.GetFiles(directory))
                    {
                        foreach (string name in names)
                        {
                            foreach (string extension in exts)
                            {
                                filebools.Add(Path.GetFileNameWithoutExtension(file).StartsWith(name) && Path.GetExtension(file).Replace(".", "") == extension.Replace(".", ""));
                            }
                        }
                    }
                    if (!filebools.Contains(true))
                    {
                        Console.WriteLine($"Protecting vulnarable directory: {Path.GetFileNameWithoutExtension(directory)}...");
                        File.WriteAllText(Path.Combine(directory, "index.php"), BlindHTML.Properties.Resources.proxy);
                    }
                    processdirs(Directory.GetDirectories(directory));
                }
            }
        }
    }
}
