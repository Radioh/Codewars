using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codewars.Solutions.Core;

namespace Codewars.Solutions.Tasks
{
    /// <summary>
    /// Rail Fence Cipher: Encoding and Decoding 
    /// Create two functions to encode and then decode a string using the Rail Fence Cipher. 
    /// This cipher is used to encode a string by placing each character successively in a diagonal along a set of "rails". 
    /// First start off moving diagonally and down. When you reach the bottom, reverse direction and move diagonally and up until you reach the top rail.
    /// Continue until you reach the end of the string. Each "rail" is then read left to right to derive the encoded string.
    /// https://www.codewars.com/kata/58c5577d61aefcf3ff000081
    /// </summary>
    public class RailFenceCipher : ITask
    {
        public string Name => "Rail Fence Cipher: Encoding and Decoding ";
        public string Rank => "3 Kuy";

        public string Run()
        {
            var cases = new string[]
            {
                "WEAREDISCOVEREDFLEEATONCE"
            };

            var results = new StringBuilder();

            foreach (var testCase in cases)
            {
                var encode = Encode(testCase, 3);
                var decode = Decode(encode, 3);

                results.Append($"{testCase} -> {encode} -> {decode} \n");
            }

            return results.ToString();
        }

        private static string Encode(string s, int n)
        {
            return new string(GetRails(s, n).SelectMany(x => x).ToArray());
        }       

        private static string Decode(string s, int n)
        {
            var encodeRails = GetRails(s, n);
            var rails = new Queue<char>[encodeRails.Count()];

            var skip = 0;
            for (int i = 0; i < rails.Count(); i++)
            {
                rails[i] = new Queue<char>(s.Skip(skip).Take(encodeRails[i].Count()));
                skip += encodeRails[i].Count();
            }

            var result = new StringBuilder();
            (int current, bool direction) = (0, true);

            for (int i = 0; i < s.Length; i++)
            {
                result.Append(rails[current].Dequeue());
                (current, direction) = SetNext(current, direction, n);               
            }

            return result.ToString();
        }

        private static (int current, bool direction) SetNext(int current, bool direction, int n)
        {
            if (direction)
            {
                if (current == n - 1)
                    direction = false;
            }
            else
            {
                if (current == 0)
                    direction = true;
            }

            if (direction)
                current++;
            else
                current--;

            return (current, direction);
        }

        private static List<char>[] GetRails(string s, int n)
        {
            var rails = new List<char>[n];

            for (int i = 0; i < n; i++)
                rails[i] = new List<char>();

            (int current, bool direction) = (0, true);

            for (int i = 0; i < s.Length; i++)
            {
                rails[current].Add(s[i]);
                (current, direction) = SetNext(current, direction, n);
            }

            return rails;
        }
    }
}
