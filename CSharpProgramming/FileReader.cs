using CSharpProgramming.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpProgramming
{
    public class FileReader
    {
        public bool Search(int record) 
        {
            using (BinaryReader b = new BinaryReader(File.Open(AssetsPath.OUTPUT_PATH, FileMode.Open)))
            {
                bool searching = true;
                if (b.BaseStream.Length != 0)
                {
                    int middle =  (int)(b.BaseStream.Length / 2) + 1;
                    while (searching)
                    {
                    
                    }
                }
            }

            return false;
        }
        private static string DecodeData(byte[] data)
        {
            var encoding = new UTF8Encoding();
            return encoding.GetString(data);
        }
    }
}
