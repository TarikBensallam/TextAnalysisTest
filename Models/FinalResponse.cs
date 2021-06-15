using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTextAnalysisTest.Models
{
    public class FinalResponse
    {
        public int nbrAnswerPos { get; set; } = 0;
        public int nbrAnswerNeg { get; set; } = 0;
        public int nbrAnswerNeutre { get; set; } = 0;
        public int nbrAnswerMix { get; set; } = 0;

        //Langues utilisées
        public Dictionary<string, int> LangaugesUsed { get; set; } = new Dictionary<string, int>();

        //Keyphrases for positive and negative feedback 
        public Dictionary<string, List<string>> Keyphrases { get; set; } = new Dictionary<string, List<string>>();

       
      
    }
}
