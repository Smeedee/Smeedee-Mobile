using Smeedee.Services;

namespace Smeedee.UnitTests.Services
{
    public class FakeHtmlFetcher : IFetchHtml
    {
        private string htmlString;

        public FakeHtmlFetcher(string html)
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