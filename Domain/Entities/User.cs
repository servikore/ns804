
  
namespace Domain.Entities
{
    public class User : BaseEntity
    {        
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; }
        
        public UserAddress Address { get; set; }
    }
}
