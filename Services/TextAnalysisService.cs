using Azure;
using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTextAnalysisTest.Services
{
    public class TextAnalysisService
    {
        //Mixed feeling === 3
        static string document = @"I hate the weather in Brest , i couldn't do what i loved the most
        which is cycling . The teachers weren't friendly as well , they bombarded us with homework. But i'm still
        glad i was able to study in a foreign country , i met some wonderful people and created some great experiences ";

        //negative feeling === 2
        static string document2 = @"I hate the weather in Brest , i couldn't do what i loved the most
        which is cycling . The teachers weren't friendly as well , they bombarded us with homework. ";

        //positive feeling === 0
        static string document3 = @" But i'm still glad i was able to study in a foreign country , i met some wonderful people and created some great experiences ";

        //Neutral feeling === 1
        static string document4 = @"C'est juste une phrase qui n'a pas de sence ";

        //french and english sentence
        static string frenchEnglish = "Salut ,aujourd'hui c'est le jeudi .Hello , today is thursday . ";

        //random letters
        static string randomm = "jfjkgp gkgk gflk jkfjgkj ";

        static string stdreply = " I think the course was great .However,i didn't like the fact that most of it was just theory. ";


        List<string> docs = new List<string>()
       {
           document,
           document2,
           document3,
           document4

       };
        private string endpoint;
        private string apiKey;
        private TextAnalyticsClient client;

        public TextAnalysisService()
        {
            this.endpoint = "";
            this.apiKey = "";
            this.client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
        }
        public Response<DetectedLanguage> GetLanguage()
        {

            try
            {
                Response<DetectedLanguage> response = client.DetectLanguage(frenchEnglish);

                DetectedLanguage language = response.Value;
                //string result = $"Detected language {language.Name} with confidence score {language.ConfidenceScore}.";
                return response;

            }
            catch (RequestFailedException ex)
            {
                throw new Exception("Une exception à été lévée au moment de l'utilisation de " + nameof(this.GetLanguage) + ": " + ex.Message);

            }

        }

        public DocumentSentiment GetSentiment()
        {

            try
            {
                DocumentSentiment docSentiment = client.AnalyzeSentiment(stdreply);
                /*
                 Sentiment sentiment = new Sentiment();
                 sentiment.Feeling=docSentiment.Sentiment;
                 sentiment.PositiveScore = docSentiment.ConfidenceScores.Positive;
                 sentiment.NeutralScore= docSentiment.ConfidenceScores.Neutral;
                 sentiment.NegativeScore=  docSentiment.ConfidenceScores.Negative;*/

                return docSentiment;

            }
            catch (Exception ex)
            {

                throw new Exception("Une exception à été lévée au moment de l'utilisation de " + nameof(this.GetSentiment) + ": " + ex.Message);
            }

        }

        public Response<DetectLanguageResultCollection> GetLanguageMultipleDocs()
        {

            try
            {

                Response<DetectLanguageResultCollection> response = client.DetectLanguageBatch(docs);
                DetectLanguageResultCollection documentsLanguage = response.Value;
                //string result = $"Detected language {language.Name} with confidence score {language.ConfidenceScore}.";
                return response;

            }
            catch (RequestFailedException ex)
            {
                throw new Exception("Une exception à été lévée au moment de l'utilisation de " + nameof(this.GetLanguageMultipleDocs) + ": " + ex.Message);
            }

        }

        public AnalyzeSentimentResultCollection GetSentimentMultipleDocs()
        {

            try
            {
                //DocumentSentiment docSentiment = client.AnalyzeSentiment(document);
                AnalyzeSentimentResultCollection results = client.AnalyzeSentimentBatch(docs);
                /*
                 Sentiment sentiment = new Sentiment();
                 sentiment.Feeling=docSentiment.Sentiment;
                 sentiment.PositiveScore = docSentiment.ConfidenceScores.Positive;
                 sentiment.NeutralScore= docSentiment.ConfidenceScores.Neutral;
                 sentiment.NegativeScore=  docSentiment.ConfidenceScores.Negative;*/

                return results;

            }
            catch (Exception ex)
            {

                throw new Exception("Une exception à été lévée au moment de l'utilisation de " + nameof(this.GetSentimentMultipleDocs) + ": " + ex.Message);
            }

        }

        
    }
}
