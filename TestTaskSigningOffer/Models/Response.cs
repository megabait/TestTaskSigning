namespace TestTaskSigningOffer.Models
{
    public class Response
    {
        public Data data { get; set; }
        public Error error { get; set; }
    }
    public class Error
    {
        public string Message { get; set; }
    }
    public class Data
    {
        public Company company{ get; set; }
        public Contract contract { get; set; }
    }
}
