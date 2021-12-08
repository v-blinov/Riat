using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Laba1.Models;
using Laba1.Services;
using Laba2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Laba3.Controllers
{
    [ApiController]
    [Route("study")]
    public class StudyController : Controller
    {
        private static readonly ConcurrentDictionary<string, KeyValue> Repo = new();
        private readonly Serializer _serializer;
        private readonly TempInputStore _store;

        public StudyController()
        {
            _serializer = new Serializer();
            _store = new TempInputStore();
        }

        [Route("ping")]
        [HttpGet]
        public IActionResult Index()
        {
            return Content("Ok");
        }

        [Route("find")]
        [HttpGet]
        public IActionResult Find(string key)
        {
            return Content(Repo.TryGetValue(key, out var keyValue)
                ? _serializer.SerializeToJson(keyValue)
                : _serializer.SerializeToJson<KeyValue>(null));
        }

        [Route("PostInputData")]
        [HttpPost]
        public async Task<IActionResult> PostInput()
        {
            try
            {
                using var streamReader = new StreamReader(Request.Body);
                var body = await streamReader.ReadToEndAsync();

                var inputModel = _serializer.DeserializeJsonToModel<InputModel>(body);
                _store.SaveInput(inputModel);
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
        public IActionResult GetAnswer()
        {
            try
            {
                var outputModel = _store.CalculateAndGet();
                return Ok(outputModel);
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
                _store.Stop();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            using var streamReader = new StreamReader(Request.Body);
            var body = await streamReader.ReadToEndAsync();
            var keyValue = _serializer.DeserializeJsonToModel<KeyValue>(body);

            if(Repo.ContainsKey(keyValue.Key))
            {
                Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase =
                    $"Key {keyValue.Key} is already presented in store.";

                return StatusCode((int)HttpStatusCode.BadRequest, string.Empty);
            }

            Repo[keyValue.Key] = keyValue;

            return Ok();
        }

        [Route("update")]
        [HttpPost]
        public IActionResult Update(string key, string value)
        {
            if(Repo.TryGetValue(key, out var keyValue))
                keyValue.Value = value;
            else
            {
                Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase =
                    $"Key {key} is not presented in store.";

                return StatusCode((int)HttpStatusCode.BadRequest, string.Empty);
            }

            return Ok();
        }
    }
}
