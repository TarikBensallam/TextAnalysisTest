using Azure;
using Azure.AI.TextAnalytics;
using AzureTextAnalysisTest.Models;
using AzureTextAnalysisTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureTextAnalysisTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextAnalysisController : ControllerBase
    {

        private static TextAnalysisService Tservice;
        public TextAnalysisController()
        {
            Tservice = new TextAnalysisService();
        }

      
        [HttpGet]
        public IActionResult GetLanguage()
        {

            return Ok(Tservice.GetLanguage());

        }

        [HttpGet]
        [Route("feeling")]
        public IActionResult GetSentiment()
        {

            return Ok(Tservice.GetSentiment());
        }
        [HttpGet]
        [Route("Languagedocs")]
        public IActionResult GetLanguageMultipleDocs()
        {

            return Ok(Tservice.GetLanguageMultipleDocs());
        }
        [HttpGet]
        [Route("Sentimentdocs")]
        public IActionResult GetSentimentMultipleDocs()
        {

            return Ok(Tservice.GetSentimentMultipleDocs());
        }

        //Recevoir un object contenant les reponses aux 4 questions et renvoyer la langue de ces 4 reponses 
        [HttpPost]
        [Route("OneAnswerLanguage")]
        public IActionResult GetLanguageAnswer([FromBody] StudentAnswer res)
        {

            return Ok(Tservice.GetLanguage(res));
        }

        //Recevoir un object contenant les reponses aux 4 questions et renvoyer le sentiment de ces 4 reponses 
        [HttpPost]
        [Route("OneAnswerFeeling")]
        public IActionResult GetSentimentAnswers([FromBody] StudentAnswer res)
        {

            return Ok(Tservice.GetSentiment(res));
        }

        //Recevoir plusieurs reponses contenant les reponses aux 4 questions et renvoyer le nombre des reponses ( positives , negatives ,neutre ,mixte )
        [HttpPost]
        [Route("AllAnswersFeeling")]
        public IActionResult GetSentimentJson([FromBody] StudentAnswer[] res)
        {
           
            return Ok(Tservice.GetSentimentJSON(res));
        }

        //Recevoir plusieurs reponses contenant les reponses aux 4 questions et renvoyer le nombre des reponses ( positives , negatives ,neutre ,mixte )
        [HttpPost]
        [Route("KeyWords")]
        public IActionResult GetKeyWords([FromBody] StudentAnswer[] res)
        {

            return Ok(Tservice.GetKeyWords(res));
        }


    }
}
