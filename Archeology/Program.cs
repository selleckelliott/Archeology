using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Archeology;
internal class Program
{
    private static void Main(string[] args)
    {
        //LiveScienceRss.LiveScienceRssFeed();
        LiveScienceWebScrape.LiveScience();
    }
}