using System;

namespace SiteExtractor.HtmlExtractor
{
    public class ConditionHasMultipleValuesException : Exception
    {
        public ConditionHasMultipleValuesException(string conditionString)
            : base("Cannot have multiple value on single condition: " + conditionString) { }
    }

    public class TooManyQueryKeyException : Exception
    {
        public TooManyQueryKeyException(string queryKey)
            : base("There is too many query key for extracting type: " + queryKey) { }
    }

    public class UnneccessaryValueException : Exception
    {
        public UnneccessaryValueException(string queryKey, string queryValue)
            : base($"Value is unneccessary in this condition: {queryKey}={queryValue}") { }
    }

    public class ValueNotFoundException : Exception
    {
        public ValueNotFoundException(string queryKey, string queryValue)
            : base($"Value is required in this condition: {queryKey}={queryValue}") { }
    }

    public class UnknownTypeException : Exception
    {
        public UnknownTypeException(string type) : base("Unknown type: " + type) { }
    }

    public class MalformatQueryException : Exception
    {
        public MalformatQueryException(string query) : base("Query is in bad format: " + query) { }
    }

    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() : base("Element is not found") { }
    }

    public class TooManyTagQueryException : Exception
    {
        public TooManyTagQueryException(string queryString)
            : base("A query is allowed to have only one tag condition: " + queryString) { }
    }

    public class CannotGetFinalValueException : Exception
    {
        public CannotGetFinalValueException() : base("Cannot get final value") { }
    }
}
