using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Schema;

namespace Day1
{

    public partial class Program
    {

        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines(@".\input.txt").ToList();


            int total = 0;
            var currentString = inputs[0];


            // Part 1
            for (int i = 1; i < inputs.Count; i++)
            {
                bool hasExploded = false;
                bool hasSplitted = false;

                Add(inputs[i]);


                do
                {
                    do
                    {
                        hasExploded = Explode();

                    } while (hasExploded);

                    hasSplitted = Split();


                } while (hasExploded || hasSplitted);

            }
            total = CalculateNumber(currentString);

            Console.WriteLine(total);

            //Part 2
            int highestMagnitude = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                for (int j = 0; j < inputs.Count; j++)
                {
                    if (i == j) continue;

                    bool hasExploded = false;
                    bool hasSplitted = false;

                    currentString = inputs[i];
                    Add(inputs[j]);


                    do
                    {
                        do
                        {
                            hasExploded = Explode();

                        } while (hasExploded);

                        hasSplitted = Split();


                    } while (hasExploded || hasSplitted);

                    var newMagnitude = CalculateNumber(currentString);
                    if (newMagnitude > highestMagnitude)
                    {
                        highestMagnitude = newMagnitude;
                    }
                }
            }

            Console.WriteLine(highestMagnitude);


            void Add(string toAdd)
            {
                var sb = new StringBuilder();

                sb.Append("[");
                sb.Append(currentString);
                sb.Append(',');
                sb.Append(toAdd);
                sb.Append(']');

                currentString = sb.ToString();
            }
            
            bool Explode()
            {
                int numberOfBrackets = 0;

                for (int c = 0; c < currentString.Length; c++)
                {
                    if (currentString[c] == '[')
                    {
                        numberOfBrackets++;

                        if (numberOfBrackets == 5)
                        {

                            var sb = new StringBuilder();
                            //find the 2 numbers to explode

                            var startIndexToExplode = c;
                            var endIndexToExplode = GetNextIndexOf(currentString, ']', c) + startIndexToExplode;

                            var pair = currentString
                                .Substring(c + 1, endIndexToExplode - startIndexToExplode - 1)
                                .Split(',')
                                .Select(int.Parse)
                                .ToList();

                            int left = pair[0];
                            int right = pair[1];


                            //go to left and add to the left number
                            string leftnumber = "";
                            int leftStartIx = -1;
                            int leftEndIx = -1;

                            for (int i = startIndexToExplode; i > 0; i--)
                            {
                                if (char.IsDigit(currentString[i]))
                                {
                                    if (leftEndIx == -1)
                                    {
                                        leftEndIx = i + 1;
                                    }
                                    leftnumber = currentString[i] + leftnumber;
                                    continue;
                                }

                                else if (!string.IsNullOrWhiteSpace(leftnumber))
                                {
                                    left += int.Parse(leftnumber);
                                    leftStartIx = i + 1;
                                    break;
                                }
                            }


                            //go to right and add to the right number

                            string rightNumber = "";
                            int rightStartIx = -1;
                            int rightEndIx = -1;

                            for (int i = endIndexToExplode; i < currentString.Length; i++)
                            {
                                if (char.IsDigit(currentString[i]))
                                {
                                    if (rightStartIx == -1)
                                    {
                                        rightStartIx = i;
                                    }
                                    rightNumber += currentString[i];
                                    continue;
                                }

                                else if (!string.IsNullOrWhiteSpace(rightNumber))
                                {
                                    right += int.Parse(rightNumber);
                                    rightEndIx = i;
                                    break;
                                }
                            }

                            if (leftStartIx != -1)
                            {
                                sb.Append(currentString.Substring(0, leftStartIx));
                                sb.Append(left);
                                sb.Append(currentString.Substring(leftEndIx, startIndexToExplode - leftEndIx));
                            }
                            else
                            {
                                sb.Append(currentString.Substring(0, startIndexToExplode));
                            }

                            sb.Append('0');

                            if (rightStartIx != -1)
                            {
                                sb.Append(currentString.Substring(endIndexToExplode + 1, rightStartIx - (endIndexToExplode + 1)));
                                sb.Append(right);
                                sb.Append(currentString.Substring(rightEndIx));

                            }
                            else
                            {
                                sb.Append(currentString.Substring(endIndexToExplode + 1));
                            }

                            currentString = sb.ToString();

                            return true;
                        }
                    }
                    else if (currentString[c] == ']')
                    {
                        numberOfBrackets--;
                    }
                }

                return false;

            }

            bool Split()
            {
                string number = "";
                int startIndex = -1;

                var sb = new StringBuilder();

                for (int c = 0; c < currentString.Length; c++)
                {
                    if (char.IsDigit(currentString[c]))
                    {
                        if (startIndex == -1)
                        {
                            startIndex = c;
                        }

                        number += currentString[c];
                        if (double.Parse(number) >= 10)
                        {
                            var toSplit = double.Parse(number);

                            int left = (int)toSplit / 2;
                            int right = (int)Math.Ceiling(toSplit / 2);

                            sb.Append(currentString.Substring(0, startIndex));
                            sb.Append("[");
                            sb.Append(left);
                            sb.Append(",");
                            sb.Append(right);
                            sb.Append("]");
                            sb.Append(currentString.Substring(c + 1));

                            currentString = sb.ToString();
                            return true;
                        }
                    }
                    else
                    {
                        startIndex = -1;
                        number = "";
                    }
                }


                return false;
            }

            int GetNextIndexOf(string s, char t, int n)
            {
                for (int i = n; i < s.Length; i++)
                {
                    if (s[i] == t)
                    {
                        return i - n;
                        
                    }
                }
                return -1;
            }


            int CalculateNumber(string input)
            {
                bool reduce = true;

                while (reduce)
                {
                    var matches = Regex.Matches(input, "(\\[(?:\\[??[^\\[]*?\\]))").Select(x => x.Value);

                    foreach (string match in matches)
                    {
                        var toReplace = match.Replace("[[", "[").Replace("]]", "]");

                        var values = toReplace.Replace("[", "").Replace("]", "").Split(',').Select(int.Parse).ToList();

                        var replaceWith = values[0] * 3 + values[1] * 2;

                        input = input.Replace(toReplace, replaceWith.ToString());

                    }

                    if (!matches.Any())
                    {
                        reduce = false;
                    }
                }

                input = input.Replace("[", "").Replace("]", "");

                var numbers = input.Split(',').Select(int.Parse);

                return numbers.Aggregate((a, b) => a + b);
            }

        }
    }
}

    


