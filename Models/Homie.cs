namespace HomiesAPI.Models 
{
    public class Homie 
    {
        public int Id { get;set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsHome { get; set; }

        public bool HasGuest { get; set; }
    }
}