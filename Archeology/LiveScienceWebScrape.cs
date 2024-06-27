using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Playwright;
using System.ServiceModel.Syndication;
using System.Xml;
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
            var aricleLinkNode = mainPageDocument.DocumentNode.SelectSingleNode("//a[contains(@class, 'article-link')]");
            if (aricleLinkNode != null)
            {
                var articleUrl = aricleLinkNode.GetAttributeValue("href", string.Empty);
                if (!string.IsNullOrEmpty(articleUrl))
                {
                    //Absolute url
                    /* if (!Uri.IsWellFormedUriString(articleUrl, UriKind.Absolute))
                     {
                         articleUrl = new Uri(new Uri(mainPageUrl), articleUrl).ToString();
                     }*/
                    //Article page request
                    var articleHtml = httpClient.GetStringAsync(articleUrl).Result;
                    var articleDoc = new HtmlDocument();
                    articleDoc.LoadHtml(articleHtml);

                    // Select all <p> elements within the article body
                    var paragraphNodes = articleDoc.DocumentNode.SelectNodes("//*[@id='article-body']//p");

                    // Check if there are any paragraphs found
                    if (paragraphNodes != null)
                    {
                        foreach (var paragraph in paragraphNodes)
                        {
                            // Filter out paragraphs that contain only text (no nested HTML tags)
                            //if (paragraph.ChildNodes.All(node => node.InnerHtml.StartsWith("<p>")))
                            //{
                                // Print each paragraph's inner text
                                Console.WriteLine(paragraph.InnerText.Trim() + "\n\n");
                            //}
                        }
                    }
                    else
                    {
                        Console.WriteLine("No paragraphs found in the article body.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Article URL Not Found");
            }
        }
    }
}
