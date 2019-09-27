namespace EcoHand.Models
{
    public interface ILoggedInUser
    {
         string UserName { get; set; }

         int Id { get; set; }
    }
}