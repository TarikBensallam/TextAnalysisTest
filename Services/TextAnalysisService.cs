using Azure;
using Azure.AI.TextAnalytics;
using AzureTextAnalysisTest.Models;
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

        static string realAnswer = "This course is very useful and clear. The teacher was clear and explained well the theory and exercices. ;no;Zoom is not very adapted to this course, but of course we know it is not your fault;";

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
                Response<DetectedLanguage> response = client.DetectLanguage(realAnswer);

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
                DocumentSentiment docSentiment = client.AnalyzeSentiment(document2);
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

        // Get language of four questions
        public Response<DetectedLanguage> GetLanguage(StudentAnswer answer)
        {

            try
            {
                
                string toDetectLanguage = $"{answer.AnswerQst1} {answer.AnswerQst2} {answer.AnswerQst3} {answer.AnswerQst4}";
                Response<DetectedLanguage> response = client.DetectLanguage(toDetectLanguage);
                
                DetectedLanguage language = response.Value;
                //string result = $"Detected language {language.Name} with confidence score {language.ConfidenceScore}.";
                return response;

            }
            catch (RequestFailedException ex)
            {
                throw new Exception("Une exception à été lévée au moment de l'utilisation de " + nameof(this.GetLanguage) + ": " + ex.Message);

            }

        }
        public DocumentSentiment GetSentiment(StudentAnswer answer)
        {

            try
            {
                string toAnalyse = $"{answer.AnswerQst1} {answer.AnswerQst2} {answer.AnswerQst3} {answer.AnswerQst4}";
                DocumentSentiment docSentiment = client.AnalyzeSentiment(toAnalyse);
              
                return docSentiment;

            }
            catch (Exception ex)
            {

                throw new Exception("Une exception à été lévée au moment de l'utilisation de " + nameof(this.GetSentiment) + ": " + ex.Message);
            }

        }
        public FinalResponse GetSentimentJSON(StudentAnswer[] ToutesReponses)
        {

            try
            {
                FinalResponse result = new FinalResponse();
                //vars to add keyphrases 
                List<string> PositivePhrases = new List<string>();
                List<string> NegativePhrases = new List<string>();

                foreach (StudentAnswer answer in ToutesReponses)
                {
                    string toAnalyse2 = $"{answer.AnswerQst1} {answer.AnswerQst2} {answer.AnswerQst3} {answer.AnswerQst4}";
                    DocumentSentiment docSentiment2 = client.AnalyzeSentiment(toAnalyse2);
                    
                    Response<DetectedLanguage> response = client.DetectLanguage(toAnalyse2);
                    DetectedLanguage language = response.Value;
                    
                    // Code pour determiner les languages utilisées et le nombre des fois
                    if (result.LangaugesUsed.ContainsKey(language.Name))
                        result.LangaugesUsed[language.Name]++;
                    else
                        result.LangaugesUsed.Add(language.Name, 1);
                    switch ((int)docSentiment2.Sentiment)
                    {
                        case 0:
                            result.nbrAnswerPos++;
                            break;
                        case 1:
                            result.nbrAnswerNeutre++;
                            break;
                        case 2:
                            result.nbrAnswerNeg++;
                            break;
                        case 3:
                            result.nbrAnswerMix++;
                            break;
                    }
                   

                    // calling the service if sentiment is positive 
                    if ((int)docSentiment2.Sentiment == 0)
                    {
                        Response<KeyPhraseCollection> responseKeyphrase = client.ExtractKeyPhrases(toAnalyse2);
                        foreach(string s in responseKeyphrase.Value)
                        {
                            PositivePhrases.Add(s);
                        }
                    }
                    else if((int)docSentiment2.Sentiment == 2)
                    {
                        Response<KeyPhraseCollection> responseKeyphrase = client.ExtractKeyPhrases(toAnalyse2);
                        foreach (string s in responseKeyphrase.Value)
                        {
                            NegativePhrases.Add(s);
                        }
                    }
                }
                result.Keyphrases.Add("PositiveKeyWords", PositivePhrases);
                result.Keyphrases.Add("NegativeKeyWords", NegativePhrases);

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception("Une exception à été lévée au moment de l'utilisation de " + nameof(this.GetSentimentJSON) + ": " + ex.Message);
            }

        }

        public Response<KeyPhraseCollection> GetKeyWords(StudentAnswer[] res)
        {

            try
            {
                List<string> ListDocument = new List<string>();
                string answerC;
                foreach (StudentAnswer answer in res)
                {
                    answerC = $"{answer.AnswerQst1} {answer.AnswerQst2} {answer.AnswerQst3} {answer.AnswerQst4}";
                    ListDocument.Add(answerC);

                }
                Response<KeyPhraseCollection> response = client.ExtractKeyPhrases(ListDocument[3]);
             return response;

            }
            catch (RequestFailedException ex)
            {
                throw new Exception("Une exception à été lévée au moment de l'utilisation de " + nameof(this.GetKeyWords) + ": " + ex.Message);

            }

        }
    }
}
