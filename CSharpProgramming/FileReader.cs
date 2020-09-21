using CSharpProgramming.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharpProgramming
{
    public class FileReader
    {
        private List<(int, int)> _lines;

        public bool Search(string record)
        {
            _lines = new List<(int,int)>();

            var rawBuffer = new byte[1024];
            using BinaryReader b = new BinaryReader(File.Open(AssetsPath.OUTPUT_PATH, FileMode.Open));

            var bytesBuffered = 0;
            var bytesConsumed = 0;

            while(true)
            {
                var bytesRead = b.Read(rawBuffer, bytesBuffered, rawBuffer.Length - bytesBuffered);

                if(bytesRead == 0) break;
                bytesBuffered += bytesRead;

                int linePosition;

                do
                {
                    linePosition = Array.IndexOf(rawBuffer, (byte)'\n', bytesConsumed,
                        bytesBuffered - bytesConsumed);

                    if(linePosition >= 0)
                    {
                        var lineLength = linePosition - bytesConsumed;
                        _lines.Add((linePosition + 1, lineLength));
                        bytesConsumed += lineLength + 1;
                    }

                } while(linePosition >= 0);

                Array.Copy(rawBuffer, bytesConsumed, rawBuffer, 0, (bytesBuffered - bytesConsumed));
                bytesBuffered -= bytesConsumed;
                bytesConsumed = 0;
            }

            b.Close();
            b.Dispose();

            return SearchOnLine(record, 0, _lines.Count);
        }

        private bool SearchOnLine(string key, int start, int end)
        {
            var rawBuffer = new byte[1024];
            using BinaryReader b = new BinaryReader(File.Open(AssetsPath.OUTPUT_PATH, FileMode.Open));

            if(end - start > 0)
            {
                var middle = ((end + start) / 2);

                var line = _lines.ElementAt(middle);

                var pos = (int)b.BaseStream.Seek(line.Item1, SeekOrigin.Begin);

                b.BaseStream.Read(rawBuffer, pos, line.Item2);

                var actual = DecodeData(new Span<byte>(rawBuffer, pos, Array.IndexOf(rawBuffer, (byte)';', pos) - pos - 1).ToArray());
                var comp = int.Parse(actual) - int.Parse(key);

                Console.WriteLine($"Chave atual = {actual}, pesquisando = {key}");

                if(comp == 0)
                    return true;
                else if(comp < 0)
                {
                    b.Close();
                    b.Dispose();
                    return SearchOnLine(key, middle + 1, end);
                }
                else if(comp > 0)
                {
                    b.Close();
                    b.Dispose();
                    return SearchOnLine(key, start, middle - 1);
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
