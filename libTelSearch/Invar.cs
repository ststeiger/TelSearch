

using System;
using System.Collections.Generic;
using System.Text;


namespace TelSearch
{


    class InvariantTest
    {


        public static void Test()
        {
            string[] words = { 
                                "Tuesday", "Salı", "Вторник", "Mardi", 
                                "Τρίτη", "Martes", "יום שלישי", 
                                "الثلاثاء", "วันอังคาร" 
            };

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"output.txt"))
            {
                // Display array in unsorted order. 
                foreach (string word in words)
                    sw.WriteLine(word);

                sw.WriteLine();

                // Create parallel array of words by calling ToUpperInvariant. 
                string[] upperWords = new string[words.Length];
                for (int ctr = words.GetLowerBound(0); ctr <= words.GetUpperBound(0); ctr++)
                    upperWords[ctr] = words[ctr].ToUpperInvariant();

                // Sort the words array based on the order of upperWords.
                Array.Sort(upperWords, words, StringComparer.InvariantCulture);

                // Display the sorted array. 
                foreach (string word in words)
                    sw.WriteLine(word);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                foreach (string word in upperWords)
                    sw.WriteLine(word);

                sw.Close();
            }


        }


    } // End Class InvariantTest 


} // End Namespace TelSearch 


// http://www.search.ch/jobs/engineer.html
