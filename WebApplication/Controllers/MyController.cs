using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Laba3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{
    [ApiController]
    [Route("client")]
    public class MyController : Controller
    {
        private MyHttpClient _client; 
        
        public MyController()
        {
            _client = new MyHttpClient("http://127.0.0.1:5000/study/");
        }

        [Route("PostInputData")]
        [HttpPost]
        public async Task<IActionResult> PostInput()
        {
            try
            {
                var response = await _client.Ping();
                if(!response.IsSuccessStatusCode)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                using var streamReader = new StreamReader(Request.Body);
                var body = await streamReader.ReadToEndAsync();

                // _client.GetInputData(body);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [Route("GetAnswer")]
        [HttpGet]
        public async Task<IActionResult> GetAnswer()
        {
            try
            {
                var response = await _client.Ping();
                if(!response.IsSuccessStatusCode)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                
                return Ok(_client.WriteAnswer());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [Route("Stop")]
        [HttpGet]
        public IActionResult Stop()
        {
            try
            {
                _client.Stop();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
