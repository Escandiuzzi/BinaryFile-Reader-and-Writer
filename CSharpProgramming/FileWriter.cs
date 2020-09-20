using CSharpProgramming.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpProgramming
{
    public class FileWriter
    {
        private const string SEPARATOR = "; ";

        public void CreateBinaryFile()
        {
            List<Person> people = new List<Person>();

            using(var reader = new StreamReader(AssetsPath.INPUTS_PATH))
            {
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var values = line.Split(" ");

                    Person person = new Person(int.Parse(values[0]), values[1], int.Parse(values[2]), int.Parse(values[3]));

                    people.Add(person);
                }

                reader.Close();
            }

            using(FileStream fileStream = new FileStream(AssetsPath.OUTPUT_PATH, FileMode.Create))
            using(BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            {
                foreach(var person in people)
                {
                    binaryWriter.Write(Encoding.UTF8.GetBytes(person.Index.ToString()).AsSpan());
                    binaryWriter.Write(SEPARATOR);
                    binaryWriter.Write(Encoding.UTF8.GetBytes(person.Name).AsSpan());
                    binaryWriter.Write(SEPARATOR);
                    binaryWriter.Write(Encoding.UTF8.GetBytes(person.Age.ToString()).AsSpan());
                    binaryWriter.Write(SEPARATOR);
                    binaryWriter.Write(Encoding.UTF8.GetBytes(person.Location.ToString()).AsSpan());
                    binaryWriter.Write("\r\n");
                }

                binaryWriter.Close();
            }
        }
    }
}
