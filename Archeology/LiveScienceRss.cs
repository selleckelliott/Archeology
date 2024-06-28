using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Xml;
using HtmlAgilityPack;

namespace Archeology
{
    public class LiveScienceRss
    {
        public static async void LiveScienceRssFeed()
        {
            //Access RSS Link
            string url = "https://www.livescience.com/feeds/all";

            using (XmlReader reader = XmlReader.Create(url))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);

                if (feed != null)
                {

                    Console.WriteLine("Live Science Archaeology");
                    Console.WriteLine("==========================");
                    Console.WriteLine();

                    foreach (SyndicationItem item in feed.Items)
                    {
                        Console.WriteLine($"Title: {item.Title.Text}");
                        Console.WriteLine($"Publish Date: {item.PublishDate}");
                        Console.WriteLine($"Summary: {item.Summary.Text}");
                        Console.WriteLine($"Link: {item.Links[0].Uri}");
                        Console.WriteLine();

                        //Get and display article

                        string articleUrl = item.Links[0].Uri.ToString();
                        Console.WriteLine($"Fetching article from url: {articleUrl}"); //Debug line

                        string article = await GetFullArticleContent(articleUrl);

                        if (!string.IsNullOrEmpty(article))
                        {
                            Console.WriteLine($"Article: \n{article}");
                        }
                        else
                        {
                            Console.WriteLine("No content found.");
                        }
                        Console.WriteLine("===================");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Unable to load feed.");
                }
            }

        }
        private static async Task<string> GetFullArticleContent(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                //Change XPath expression according to article structure
                var articleNode = htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'article-body')]");

                if (articleNode != null)
                {
                    var paragraphs = articleNode.SelectNodes(".//p");
                    if (paragraphs != null)
                    {
                        string fullArticle = string.Join("\n\n", paragraphs.Select(p => p.InnerText.Trim()));
                        return fullArticle;
                    }
                    else
                    {
                        Console.WriteLine($"No paragraphs found"); //Debug line
                        return string.Empty;
                    }
                }
                else
                {
                    return "Article not found using XPath."; //Debug line
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching article content {ex.Message}"); //Debug line
                return string.Empty;
            }
        }
    }
}
