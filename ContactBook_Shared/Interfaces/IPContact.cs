using ContactBook_Shared.Models;

namespace ContactBook_Shared.Interfaces;

public interface IPContact
{
    /// <summary>
    /// First name of PContact
    /// </summary>
    string FirstName { get; set; }
    /// <summary>
    /// Last name of PContact
    /// </summary>
    string LastName { get; set; }
    /// <summary>
    /// Email of PContact
    /// </summary>
    string Email { get; set; }
    /// <summary>
    /// Phone number of PContact
    /// </summary>
    string PhoneNumber { get; set; }
    /// <summary>
    /// Addres of PContact
    /// </summary>
    string Address { get; set; }
    /// <summary>
    /// First name and last name of PContact concatenated.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
}