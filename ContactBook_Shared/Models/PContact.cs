﻿using ContactBook_Shared.Interfaces;

namespace ContactBook_Shared.Models;

public class PContact : IPContact
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
}   
