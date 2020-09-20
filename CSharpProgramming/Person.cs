using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpProgramming
{
    public class Person
    {
        public Person(int Index, string Name, int Age, int Location)
        {
            this.Index = Index;
            this.Name = Name;
            this.Age = Age;
            this.Location = Location;
        }

        public int Index { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public int Location { get; private set; }
    }
}
