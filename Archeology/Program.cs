using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Playwright;
using System.ServiceModel.Syndication;
using System.Xml;
using Archeology;
internal class Program
{
    private static void Main(string[] args)
    {
        LiveScienceWebScrape.LiveScience();
    }
}