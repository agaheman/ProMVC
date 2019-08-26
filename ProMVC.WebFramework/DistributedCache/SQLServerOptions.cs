namespace ProMVC.WebFramework.DistributedCache
{
    public class SQLServerOptions
    {
		public string ConnectionString { get; set; }
		public string SchemaName { get; set; }
        public string TableName { get; set; }
    }
}
