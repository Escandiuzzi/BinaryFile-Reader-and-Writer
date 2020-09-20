using CSharpProgramming.Assets;
using System;
using System.IO;

namespace CSharpProgramming
{
    class Program
    {
        static void Main()
        {
            if(!File.Exists(AssetsPath.OUTPUT_PATH))
            {
                FileWriter fileWriter = new FileWriter();
                fileWriter.CreateBinaryFile();
            }

            FileReader fileReader = new FileReader();

            while(true)
            {
                Console.WriteLine("Digite um valor para ser encontrado nos registros\n");

                string line = Console.ReadLine();

                if(int.TryParse(line, out int _))
                {
                    bool valueFound = fileReader.Search(line);

                    Console.WriteLine(valueFound);
                }
                else
                    Console.WriteLine("Digite um valor válido");
            }
        }
    }
}
