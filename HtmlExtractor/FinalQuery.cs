using HtmlAgilityPack;

namespace SiteExtractor.HtmlExtractor
{
    public class FinalQuery
    {
        public ValueType Type;
        public string SubType;

        public FinalQuery(string finalQueryString)
        {
            string[] typeSubtype = finalQueryString.Split('.');
            if (typeSubtype.Length > 2)
            {
                throw new UnknownTypeException(finalQueryString);
            }

            string type = typeSubtype[0].ToLower();
            if (type == "attr")
            {
                if (typeSubtype.Length != 2)
                {
                    throw new UnknownTypeException(finalQueryString);
                }
                Type = ValueType.AttributeValue;
                SubType = typeSubtype[1];
                return;
            }

            if (typeSubtype.Length != 1)
            {
                throw new UnknownTypeException(finalQueryString);
            }

            if (type == "html")
            {
                Type = ValueType.InnerHtml;
            }
            else if (type == "tag")
            {
                Type = ValueType.TagName;
            }
            else
            {
                throw new UnknownTypeException(finalQueryString);
            }
        }

        public string GetValue(HtmlNode node)
        {
            switch (Type)
            {
                case ValueType.AttributeValue:
                    if (node.Attributes[SubType] == null)
                    {
                        throw new CannotGetFinalValueException();
                    }
                    return node.Attributes[SubType].Value;
                case ValueType.TagName:
                    return node.Name;
                case ValueType.InnerHtml:
                    return node.InnerHtml;
            }

            throw new CannotGetFinalValueException();
        }
    }
}
