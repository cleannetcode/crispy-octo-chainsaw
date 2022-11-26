using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using Xunit.Abstractions;

namespace CrispyOctoChainsaw.IntegrationalTests
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ITestOutputHelper _outputHelper;

        public LoggingHandler(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        private void PrintJson(HttpContent content)
        {
            if (content.Headers.ContentType.MediaType != MediaTypeHeaderValue.Parse("multipart/form-data").MediaType)
            {
                var streamContent = content.ReadAsStream();
                var bytesBuffer = new byte[streamContent.Length];
                streamContent.Read(bytesBuffer, 0, bytesBuffer.Length);

                var stringContent = Encoding.UTF8.GetString(bytesBuffer);

                if (stringContent is not null)
                {
                    var json = JToken.Parse(stringContent).ToString();
                    _outputHelper.WriteLine(json);
                }
            }
        }

        private async Task PrintJsonAsync(HttpContent content)
        {
            if (content.Headers.ContentType.MediaType != MediaTypeHeaderValue.Parse("multipart/form-data").MediaType)
            {
                var stringContent = await content.ReadAsStringAsync();
                if (stringContent is not null && stringContent.Length != 0)
                {
                    var json = JToken.Parse(stringContent).ToString();
                    _outputHelper.WriteLine(json);
                }
            }
        }

        protected override HttpResponseMessage Send
            (HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Content is not null)
            {
                PrintJson(request.Content);
            }

            var response = base.Send(request, cancellationToken);
            if (response.Content is not null)
            {
                _outputHelper.WriteLine(request.RequestUri.ToString());
                PrintJson(request.Content);
            }

            return response;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Content is not null)
            {
                await PrintJsonAsync(request.Content);
            }

            var response = await base.SendAsync(request, cancellationToken);
            if (response.Content is not null)
            {
                _outputHelper.WriteLine(request.RequestUri.ToString());
                await PrintJsonAsync(response.Content);
            }

            return response;
        }
    }
}
