using CSharpProgramming.Assets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharpProgramming
{
    public class FileReader
    {

        private static List<byte[]> _lines;
        public bool Search(string record)
        {
            //using(BinaryReader b = new BinaryReader(File.Open(AssetsPath.OUTPUT_PATH, FileMode.Open)))
            //{
            //    bool searching = true;
            //    if(b.BaseStream.Length != 0)
            //    {
            //        int middle = (int)(b.BaseStream.Length / 2) + 1;

            //        var leo = b.BaseStream.Seek(middle, SeekOrigin.Begin);

            //        byte[] by = b.ReadBytes(10);

            //        string a = DecodeData(by);
            //    }
            //}


            _lines = new List<byte[]>();

            var rawBuffer = new byte[1024 * 1024];
            using(BinaryReader b = new BinaryReader(File.Open(AssetsPath.OUTPUT_PATH, FileMode.Open)))
            {
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
                            var line = new Span<byte>(rawBuffer, bytesConsumed, lineLength);
                            var firstCommaPos = line.IndexOf((byte)';');

                            _lines.Add(line.ToArray());
                            bytesConsumed += lineLength + 1;
                        }

                    } while(linePosition >= 0);

                    Array.Copy(rawBuffer, bytesConsumed, rawBuffer, 0, (bytesBuffered - bytesConsumed));
                    bytesBuffered -= bytesConsumed;
                    bytesConsumed = 0;
                }
            }

            return SearchOnLine(record, 0, _lines.Count);
        }

        private bool SearchOnLine(string key, int start, int end)
        {
            if(end - start > 0)
            {
                var lookingFor = Encoding.UTF8.GetBytes(key).AsSpan();

                var middle = ((start + end) / 2) +1;

                var span = _lines.ElementAt(middle - 1).AsSpan();
                var dejair = Array.IndexOf(span.ToArray(), (byte)';', 0);



                var firstCommaPos = span.IndexOf((byte)';');
                var movieId = span.Slice(0, firstCommaPos);

                if(movieId.SequenceCompareTo(lookingFor) == 0)
                    return true;
                else if(movieId.SequenceCompareTo(lookingFor) > 0)
                    return SearchOnLine(key, middle, end);

                else if(movieId.SequenceCompareTo(lookingFor) < 0)
                    return SearchOnLine(key, start, middle);
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
