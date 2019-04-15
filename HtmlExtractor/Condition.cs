using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace SiteExtractor.HtmlExtractor
{
    public class Condition
    {
        public QueryType Type;
        public string SubType = null;
        public string Value = null;

        public Condition(string conditionString)
        {
            string[] keyValue = conditionString.Split('=');
            if (keyValue.Length > 2)
            {
                throw new ConditionHasMultipleValuesException(conditionString);
            }

            ProcessKeyValue(
                keyValue[0],
                keyValue.Length == 2 ? keyValue[1] : null);
        }

        public IEnumerable<HtmlNode> ApplyFilter(IEnumerable<HtmlNode> nodes)
        {
            if (Type == QueryType.Attribute)
            {
                return nodes.Where(child => child.Attributes[SubType] != null && child.Attributes[SubType].Value == Value);
            }

            return nodes;
        }

        private void ProcessKeyValue(string key, string value)
        {
            string[] queryKeyType = key.Split('.');
            if (queryKeyType.Length > 2)
            {
                throw new TooManyQueryKeyException(key);
            }

            string typeString = queryKeyType[0].ToLower();
            SubType = queryKeyType[1];
            if (typeString == "tag")
            {
                if (value != null)
                {
                    throw new UnneccessaryValueException(key, value);
                }

                Type = QueryType.Tag;
                return;
            }
            else if (typeString == "attr")
            {
                if (value == null)
                {
                    throw new ValueNotFoundException(key, value);
                }

                Type = QueryType.Attribute;
                Value = value;
                return;
            }

            throw new UnknownTypeException(typeString);
        }
    }
}
