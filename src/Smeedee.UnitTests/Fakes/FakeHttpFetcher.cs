using Smeedee.Services;

namespace Smeedee.UnitTests.Services
{
    public class FakeHttpFetcher : IFetchHttp
    {
        private string htmlString;

        public FakeHttpFetcher(string html)
        {
            htmlString = html;
        }

        public void SetHtmlString(string html)
        {
            htmlString = html;
        }

        public string DownloadString(string url)
        {
            return htmlString;
        }
    }
}