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
                int fileLength = (int)b.BaseStream.Length;

                b.Close();
                
                return BinarySearch(record, 0, fileLength, 0, false);
            }
        }

        private bool BinarySearch(int key, int start, int end, int pos, bool found) 
        {
            int middle = 0;

            if (end - start > 0) 
            {
                using (BinaryReader b = new BinaryReader(File.Open(AssetsPath.OUTPUT_PATH, FileMode.Open)))
                {
                    middle = ((start + end) / 2) + 1;

                    b.BaseStream.Seek(pos, SeekOrigin.Current);

                    var recordFound = b.ReadUInt16();
                    var position = (int)b.BaseStream.Position;
                    b.Close();
                    
                    if (recordFound > key)
                        BinarySearch(key, start, middle, position, false);
                    else if (recordFound == key)
                        return true;
                    else
                        BinarySearch(key, middle, end, position, false);

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
