using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    /* Taken from the demo / practise https://app.codility.com/programmers/lessons/1-iterations/ */

    public class BinaryGap
    {

        // time started 6:55pm
        public int solution(int n)
        {
            // given a number convert the number to binary 
            ArrayList binary = new ArrayList();
            String binaryString = "";

            int total = n;
            while (total != 1)
            {
                if (total % 2 == 0)
                {
                    // even
                    binaryString = "0" + binaryString; // simple preappend ;)
                    total /= 2;
                }
                else
                {
                    binaryString = "1" + binaryString; // simple preappend ;)
                    total /= 2;
                }
            }
            int currentLongest = 0;
            int longish = 0;
            Array chars = binaryString.ToCharArray();
            foreach (char v in chars)
            {
                if (v == '0')
                {
                    currentLongest += 1;
                }
                else
                {
                    if (currentLongest > longish)
                    {
                        longish = currentLongest;
                        currentLongest = 0;
                    }
                }
            }

            return longish;
        }
        public static void binaryGap()
        {
            BinaryGap obj = new BinaryGap();

            int answer = obj.solution(4225);
            Console.WriteLine("Solution:" + answer);

            // done! 7:06
        }
    }

    public class unpairedElements
    {

        public int solution(int[] A)
        {
            // STARTING 7:52pm

            // 1) create a hashmap or Dictionary like so `pairs[value] = OCCURANCE`
            // 2) look for where the `OCCURANCE` == 1 return the `KEY`

            Dictionary<int, int> pairs = new Dictionary<int, int>();

            foreach (int val in A)
            {
                if (pairs.ContainsKey(val))
                {
                    pairs[val] += 1;
                }
                else
                {
                    pairs.Add(val, 1);
                }
            }

            // gotta be an easy way to get key by VALUE!
            int allAlone = pairs.FirstOrDefault(x => x.Value == 1).Key;
            return allAlone;

            // done 8:01pm
        }

        public static void unpairedElementSolution()
        {
            unpairedElements obj = new unpairedElements();
            int[] testArray = { 1, 2, 3, 3, 1, 2, 9, 9, 8 };
            int output = obj.solution(testArray);
            Console.WriteLine("single number:" + output);
        }
    }
    class Solution
    {
        public int solution(int[] A)
        {

            // 1 sort list
            Array.Sort(A);

            // 2 smallest positive integer (greater than 0)
            // lets get the "Largest" number in the list A.Length-1
            int largestArrayVal = A[A.Length - 1];
            if (largestArrayVal < 0)
            {
                return 1;
            }

            // create a map with all the numbers 1-> largestArrayVal
            // this will give us a list of all `possible` numbers in the array. O(n)time meh N*log(n)
            Dictionary<int, bool> valuesUsed = new Dictionary<int, bool>();
            for (int i = 1; i <= largestArrayVal + 1; i++)
            {
                valuesUsed.Add(i, false);
            }

            // now we have a map of possible values we can check if they are used...
            for (int i = 0; i < A.Length; i++)
            {
                // ignore - and update the map int, true if exists
                if (A[i] > 0)
                {
                    valuesUsed[A[i]] = true;
                }
            }

            // now that we have our updated map we can look for the first number thats false..
            for (int i = 1; i <= valuesUsed.Count; i++)
            {
                if (valuesUsed[i] == false)
                {
                    return i;
                }
            }
            return 1;
        }
    }
}