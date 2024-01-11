namespace SalesApp.Model
{
    public class JqueryDatatableParam
    {
        public int Draw { get; set; }
        public int start { get; set; }

        public int length { get; set; }
        public Search? search { get; set; }
        public List<Order> orderColumn { get; set; }
        public string? orderDirection { get; set; }
        }

        public class Search
        {
            public string value { get; set; }
            public bool IsRegex { get; set; }
        }

        public class Order
        {
            public int column { get; set; }
            public string dir { get; set; }
        }
}

