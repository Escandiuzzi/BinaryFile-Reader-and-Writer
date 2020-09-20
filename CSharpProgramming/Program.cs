using CSharpProgramming.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CSharpProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(AssetsPath.OUTPUT_PATH)) {

                FileWriter fileWriter = new FileWriter();
                fileWriter.CreateBinaryFile();
            }

            FileReader fileReader = new FileReader();

            while (true) 
            {
                Console.WriteLine("Digite um valor para ser encontrado nos registros\n");

                string line = Console.ReadLine();

                int record = -1;

                int.TryParse(line, out record);

                bool valueFound = fileReader.Search(record);
            }
        }
    }
}
