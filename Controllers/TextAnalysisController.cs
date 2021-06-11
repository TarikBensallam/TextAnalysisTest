using Azure;
using Azure.AI.TextAnalytics;
using AzureTextAnalysisTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public IActionResult GetLanguage(string doc)
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
    }
}
