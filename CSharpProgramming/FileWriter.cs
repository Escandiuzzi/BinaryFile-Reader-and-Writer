using CSharpProgramming.Assets;
using System.Collections.Generic;
using System.IO;

namespace CSharpProgramming
{
    public class FileWriter
    {
        public void CreateBinaryFile() 
        {
            List<Person> people = new List<Person>();

            using (var reader = new StreamReader(AssetsPath.INPUTS_PATH))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var values = line.Split(" ");

                    Person person = new Person(int.Parse(values[0]), values[1], int.Parse(values[2]), int.Parse(values[3]));

                    people.Add(person);
                }

                reader.Close();
            }

            using (FileStream fileStream = new FileStream(AssetsPath.OUTPUT_PATH, FileMode.Create))
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            {
                foreach (var person in people)
                {
                    binaryWriter.Write(person.Index);
                    binaryWriter.Write(person.Name);
                    binaryWriter.Write(person.Age);
                    binaryWriter.Write(person.Location);
                }

                binaryWriter.Close();
            }
        }
    }
}
