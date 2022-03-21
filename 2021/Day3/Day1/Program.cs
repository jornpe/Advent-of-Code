using System;
using System.Collections;
using System.Linq;

namespace Day1
{
    enum Direction
    {
        forward,
        down, 
        up
    }

    class Program
    {
        static void Main(string[] args)
        {

            var input = System.IO.File.ReadLines(@".\input.txt").ToList();
            var remaining = input;
            var remaining2 = input;

            var gamma = new BitArray(input.First().Length);
            var epsilon = new BitArray(input.First().Length);
            var oxygen = new BitArray(input.First().Length);
            var co2 = new BitArray(input.First().Length);
            var convertedArray = new BitArray(input.Count);

            for (int j = 0; j < input.First().Length; j++)
            {
                int count = 0;
                foreach (var test in input)
                {
                    convertedArray.Set(count, test[j] == '1');
                    count++;
                }

                var trueCount = convertedArray.Cast<bool>().Count(i => i);
                var falseCount = convertedArray.Cast<bool>().Count(i => !i);

                
                gamma.Set(gamma.Length - 1 - j, trueCount > falseCount);
                epsilon.Set(epsilon.Length - 1 - j, trueCount < falseCount);


                
            }

            int iGamma = getIntFromBitArray(gamma);
            int iEpsilon = getIntFromBitArray(epsilon);
            int value = iGamma * iEpsilon;


            Console.WriteLine(value);
            //Console.ReadKey();

            //second part

            for (int j = 0; j < input.First().Length; j++)
            {
                if (remaining.Count == 1)
                {
                    break;
                }
                int count = 0;
                convertedArray = new BitArray(remaining.Count);

                foreach (var test in remaining)
                {
                    convertedArray.Set(count, test[j] == '1');
                    count++;
                }

                var trueCount = convertedArray.Cast<bool>().Count(i => i);
                var falseCount = convertedArray.Cast<bool>().Count(i => !i);

                if (trueCount == falseCount)
                {
                    falseCount = 0;
                }

                remaining = remaining.Where(s => (trueCount > falseCount)
                    ? s[j] == '1'
                    : s[j] == '0')
                    .ToList();
            }

            var array = new BitArray(remaining.First().Length);

            for (int i = 0; i < remaining.First().Length; i++)
            {
                array.Set(remaining.First().Length - 1 - i, remaining.First()[i] == '1');
            }
            
            Console.WriteLine(getIntFromBitArray(array));



            for (int j = 0; j < input.First().Length; j++)
            {
                if (remaining2.Count == 1)
                {
                    break;
                }
                int count = 0;
                convertedArray = new BitArray(remaining2.Count);

                foreach (var test in remaining2)
                {
                    convertedArray.Set(count, test[j] == '1');
                    count++;
                }

                var trueCount = convertedArray.Cast<bool>().Count(i => i);
                var falseCount = convertedArray.Cast<bool>().Count(i => !i);

                if (trueCount == falseCount)
                {
                    falseCount = 0;
                }

                remaining2 = remaining2.Where(s => (trueCount < falseCount)
                        ? s[j] == '1'
                        : s[j] == '0')
                    .ToList();
            }

            var array2 = new BitArray(remaining2.First().Length);

            for (int i = 0; i < remaining2.First().Length; i++)
            {
                array2.Set(remaining2.First().Length - 1 - i, remaining2.First()[i] == '1');
            }

            Console.WriteLine(getIntFromBitArray(array2));
            Console.WriteLine(getIntFromBitArray(array2)* getIntFromBitArray(array));


            Console.ReadKey();
        
        }

        public static int getIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }
    }
}
