using Azure.AI.TextAnalytics;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTextAnalysisTest.Models
{
    public class Sentiment
    {

        public TextSentiment Feeling { get; set; }
        public double PositiveScore { get; set; }
        public double NegativeScore { get; set; }
        public double NeutralScore { get; set; }
    }
}
