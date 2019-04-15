using HtmlAgilityPack;
using System;
using System.Text;

namespace SiteExtractor.HtmlExtractor
{
    public class Parser
    {
        public string Url;
        public Query[] Queries;
        public FinalQuery FinalQuery;
        public ValueType FinalValueType;

        public Parser() { }
        public Parser(string url, string queryString, string finalQueryString, bool urlEncoded = false): this()
        {
            if (urlEncoded)
            {
                Url = Encoding.UTF8.GetString(Convert.FromBase64String(url));
            }
            else
            {
                Url = url;
            }
            Queries = Query.ParseQueries(queryString);
            FinalQuery = new FinalQuery(finalQueryString);
        }

        public string Execute()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(Url);

            HtmlNode currentNode = document.DocumentNode;
            foreach (Query query in Queries)
            {
                currentNode = query.GetCorrespondingNode(currentNode);
            }

            return FinalQuery.GetValue(currentNode);
        }
    }
}
