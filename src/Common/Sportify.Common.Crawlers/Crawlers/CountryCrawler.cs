namespace Sportify.Common.Crawlers.Crawlers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Data;
    using Data.Models;
    using HtmlAgilityPack;
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    internal class CountryCrawler : ICrawler
    {
        //private const string MainCountryCrawlerUrl = "http://www.worldometers.info/geography/alphabetical-list-of-countries/";
        private const string MainCountryCodeUrl = "https://en.wikipedia.org/wiki/List_of_IOC_country_codes";

        private readonly IServiceProvider serviceProvider;
        private readonly WebService webService;

        public CountryCrawler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.webService = new WebService();
        }

        public void Run()
        {
            var context = this.serviceProvider.GetService<SportifyDbContext>();

            //var html = this.webService.GetRequest(MainCountryCrawlerUrl);
            //var document = new HtmlDocument();
            //document.LoadHtml(html);

            //var node = document.DocumentNode.SelectSingleNode("//table[@class='table table-hover table-condensed']");
            //this.ExtractAllCountriesInfo(node.OuterHtml, context);

            var html = this.webService.GetRequest(MainCountryCodeUrl);
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var node = document.DocumentNode.SelectSingleNode("//table[@class='wikitable sortable']");
            this.ExtractCountries(node.OuterHtml, context);
        }

        private void ExtractCountries(string outerHtml, SportifyDbContext context)
        {
            var document = new HtmlDocument();
            document.LoadHtml(outerHtml);

            var countryNodes = document.DocumentNode.SelectNodes("//tr").Skip(1);

            foreach (var node in countryNodes)
            {
                var country = new Country();

                var doc = new HtmlDocument();
                doc.LoadHtml(node.OuterHtml);
                var cols = doc.DocumentNode.SelectNodes("//td");
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        var code = this.ExtractData(cols[i].OuterHtml, "<span class=\"monospaced\">(.*?)</span>");
                        country.CountryCode = code;
                    }
                    if (i == 1)
                    {
                        var name = this.ExtractData(cols[i].OuterHtml, "Olympics\">(.*?)</a>");
                        if (name == "The Gambia")
                        {
                            name = "Gambia";
                        }
                        country.Name = name;
                    }
                }

                context.Countries.Add(country);
            }

            context.SaveChanges();
        }

        //private void ExtractAllCountriesInfo(string nodeOuterHtml, SportifyDbContext context)
        //{
        //    var document = new HtmlDocument();
        //    document.LoadHtml(nodeOuterHtml);

        //    var nodes = document.DocumentNode.SelectNodes("//tr").Skip(1);
        //    foreach (var node in nodes)
        //    {
        //        var doc = new HtmlDocument();
        //        doc.LoadHtml(node.OuterHtml);

        //        var cols = doc.DocumentNode.SelectNodes("//td");
        //        var country = new Country();
        //        for (int i = 0; i < 5; i++)
        //        {
        //            if (i == 1)
        //            {
        //                var countryName = this.ExtractData(cols[i].OuterHtml, "\">(.*?)<\\/td>");
        //                country.Name = countryName.UrlDecode();
        //            }

        //            if (i == 2)
        //            {
        //                var population = this.ExtractData(cols[i].OuterHtml, "\\/\">(.*?)<\\/a>");
        //                country.Population = int.Parse(population.Replace(",", ""));
        //            }

        //            if (i == 3)
        //            {
        //                var landArea = this.ExtractData(cols[i].OuterHtml, ">(.*?)</td>");
        //                country.LandArea = int.Parse(landArea.Replace(",", ""));
        //            }

        //            if (i == 4)
        //            {
        //                var density = this.ExtractData(cols[i].OuterHtml, ">(.*?)</td>");
        //                country.Density = int.Parse(density.Replace(",", ""));
        //            }
        //        }

        //        context.Add(country);
        //    }

        //    context.SaveChanges();
        //}

        private string ExtractData(string outerHtml, string pattern)
        {
            var match = Regex.Match(outerHtml, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }

            return null;
        }
    }
}