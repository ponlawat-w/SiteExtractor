using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace SiteExtractor.HtmlExtractor
{

    public class Query
    {
        public Condition[] Conditions;
        public int ElementIndex = 0;

        public Query(string queryString)
        {
            string[] queryWithIndex = queryString.Split(':');
            if (queryWithIndex.Length > 2)
            {
                throw new MalformatQueryException(queryString);
            }
            Conditions = queryWithIndex[0].Split('&')
                .Select(c => new Condition(c)).ToArray();

            if (Conditions.Where(c => c.Type == QueryType.Tag).Count() > 1)
            {
                throw new TooManyTagQueryException(queryString);
            }

            if (queryWithIndex.Length > 1)
            {
                if (int.TryParse(queryWithIndex[1], out int elementIndex))
                {
                    ElementIndex = elementIndex;
                }
                else
                {
                    throw new MalformatQueryException(queryString);
                }
            }
        }

        public static Query[] ParseQueries(string queryString)
        {
            return queryString.Split(',')
                .Select(qS => new Query(qS)).ToArray();
        }

        public HtmlNode GetCorrespondingNode(HtmlNode node)
        {
            Condition tagCondition = Conditions.SingleOrDefault(c => c.Type == QueryType.Tag);

            IEnumerable<HtmlNode> filteredNodes = tagCondition == null ? node.Descendants() : node.Descendants(tagCondition.SubType);
            foreach (Condition condition in Conditions)
            {
                filteredNodes = condition.ApplyFilter(filteredNodes);
            }

            HtmlNode[] nodes = filteredNodes.ToArray();
            if (ElementIndex >= nodes.Length)
            {
                throw new ElementNotFoundException();
            }

            return nodes[ElementIndex];
        }
    }
}
