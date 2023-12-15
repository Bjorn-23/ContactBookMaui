using ContactBook_Shared.Models;

namespace ContactBook_Shared.Interfaces;

public interface IPContact
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    string Address { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}