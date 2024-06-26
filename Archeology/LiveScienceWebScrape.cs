using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace Archeology
{
    public class LiveScienceWebScrape
    {
        public static async void LiveScience()
        {
            //Send get request to livescience.com
            String mainPageUrl = "https://www.livescience.com/archaeology";

            var httpClient = new HttpClient();
            var mainPageHtml = httpClient.GetStringAsync(mainPageUrl).Result;
            var mainPageDocument = new HtmlDocument();
            mainPageDocument.LoadHtml(mainPageHtml);

            //Get Editor's pick headline
            var editorsPickHeadline = mainPageDocument.DocumentNode.SelectSingleNode("//span[@class='article-name']");
            if (editorsPickHeadline != null)
            {
                var headline = editorsPickHeadline.InnerText.Trim();
                Console.WriteLine($"Live Science Editor's Pick: {headline}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Article Not Found");
            }
            // Get Editor's pick sub-title
            var editorsPickSubTitle = mainPageDocument.DocumentNode.SelectSingleNode("//span[@class='article-strapline']");
            if (editorsPickSubTitle != null)
            {
                var subTitle = editorsPickSubTitle.InnerText.Trim();
                Console.WriteLine(subTitle);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Subtitle Not Found");
            }

            //Get link to full article
            var aricleLinkNode = mainPageDocument.DocumentNode.SelectSingleNode("//a[contains(@class, 'article-nam')]");
            if (aricleLinkNode != null)
            {
                var articleUrl = aricleLinkNode.GetAttributeValue("href", string.Empty);
                if (!string.IsNullOrEmpty(articleUrl))
                {
                    //Absolute url
                    if (!Uri.IsWellFormedUriString(articleUrl, UriKind.Absolute))
                    {
                        articleUrl = new Uri(new Uri(mainPageUrl), articleUrl).ToString();
                    }
                    //Article page request
                    var articleHtml = await httpClient.GetStringAsync(articleUrl);
                    var articleDoc = new HtmlDocument();
                    articleDoc.LoadHtml(articleHtml);
                }

                //Get Editor's pick article link
                var editorsPickArticle = mainPageDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'article-body')]");
                if (editorsPickArticle != null)
                {
                    var paragraphs = editorsPickArticle.SelectNodes(".//p");
                    foreach (var paragraph in paragraphs)
                    {
                        Console.WriteLine(paragraph.InnerText.Trim());
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Article Content Not Found");
                }
            }
            else
            {
                Console.WriteLine("Article URL Not Found");
            }
        }
    }
}
