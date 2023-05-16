using System;
namespace TestAPI_1.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string? statusDescription { get; set; }
        public List<Customer> customers { get; set; } = new List<Customer>();
        public List<Subscription> subscriptions { get; set; } = new List<Subscription>();
        public List<Post> posts { get; set; } = new List<Post>();

    }
}
