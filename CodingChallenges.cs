using System;
using System.Collections;

namespace csharp
{

    public class BinaryGap
    {

// time started 6:55pm
        public int solution(int n)
        {
            // given a number convert the number to binary 
            ArrayList binary = new ArrayList();
            String binaryString = "";

            int total = n;
            while(total != 1) {
                if(total %2 == 0) {
                    // even
                    binaryString = "0" + binaryString; // simple preappend ;)
                    total /=2;
                } else  {
                    binaryString = "1" + binaryString; // simple preappend ;)
                    total /=2;
                }
            }
            int currentLongest = 0;
            int longish = 0;
            Array chars = binaryString.ToCharArray();
            foreach(char v in chars){
                if(v == '0'){
                    currentLongest +=1;
                } else {
                    if(currentLongest > longish) {
                        longish = currentLongest;
                        currentLongest=0;
                    }
                }
            }

            return longish;
        }
        public static void binaryGap()
        {
            BinaryGap obj = new BinaryGap();

            int answer =obj.solution(4225);
            Console.WriteLine("Solution:"+ answer);

            // done! 7:06
        }
    }
}