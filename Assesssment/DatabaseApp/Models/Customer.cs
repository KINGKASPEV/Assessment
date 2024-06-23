using MongoDB.Bson;

namespace DatabaseApp.Models
{
    public class Customer
    {
        public ObjectId CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
