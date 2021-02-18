namespace Roulette.Data.Providers.MongoDb
{
    public sealed class MongoDbFilter
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
        public FilterOperator Operator { get; set; }
    }
}
