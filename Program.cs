using System;

namespace XportHunter
{
    class Program
    {
        private static void Banner()
        {
            Console.WriteLine(@"__   __                 _     _   _             _            ");
            Console.WriteLine(@"\ \ / /                | |   | | | |           | |           ");
            Console.WriteLine(@" \ V / _ __   ___  _ __| |_  | |_| |_   _ _ __ | |_ ___ _ __ ");
            Console.WriteLine(@" /   \| '_ \ / _ \| '__| __| |  _  | | | | '_ \| __/ _ \ '__|");
            Console.WriteLine(@"/ /^\ \ |_) | (_) | |  | |_  | | | | |_| | | | | ||  __/ |   ");
            Console.WriteLine(@"\/   \/ .__/ \___/|_|   \__| \_| |_/\__,_|_| |_|\__\___|_|   ");
            Console.WriteLine(@"      | |                                                    ");
            Console.WriteLine(@"      |_|                                                    ");
            Console.WriteLine("===============================================================");
            Console.WriteLine("");
        }
        static void Main(string[] args)
        {
            Banner();

            string directoryPath = args[0];
            string nameToCheck = args[1];

            string[] filesToCheck = System.IO.Directory.GetFiles(directoryPath, "*.dll");

            try
            {
                foreach (var dllFile in filesToCheck)
                {
                    var peFile = new PeNet.PeFile(dllFile);
                    var exportedFunctions = peFile.ExportedFunctions;

                    if (exportedFunctions != null)
                    {
                        foreach (var exportedFunction in exportedFunctions)
                        {
                            if (exportedFunction.Name != null)
                            {
                                string efName = exportedFunction.Name.ToLower();
                                if (efName.Contains(nameToCheck))
                                {
                                    Console.WriteLine("[+] Found match in file {0} --> {1}", dllFile, efName);
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("[!] - FINISHED");
            }
            catch (Exception e)
            {
                Console.WriteLine("[X] Something went wrong. See error message below!");
                Console.WriteLine(e.Message);
            }
        }
    }
}
