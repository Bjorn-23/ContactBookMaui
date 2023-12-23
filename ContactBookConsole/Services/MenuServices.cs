using System.Diagnostics.Contracts;
using System.Diagnostics;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;

namespace ContactBookConsole.Services;

internal class MenuServices : IMenuServices
{
    private readonly IPContactServices _pContactService;
    public MenuServices(IPContactServices pContactService)
    {
        _pContactService = pContactService;
    }

    /// <summary>
    /// Main menu method.
    /// Controls all text output, takes user input and calls the correct method or logic.
    /// </summary>
    public void MenuStart()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("------Address Book 3000------");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("\nPlease Choose An Option");
            Console.WriteLine($"{"\n1.",-5} Add new Contact");
            Console.WriteLine($"{"\n2.",-5} Show All Contacts");
            Console.WriteLine($"{"\n3.",-5} Show Contact By Email");
            Console.WriteLine($"{"\n4.",-5} Update Contact By Email");
            Console.WriteLine($"{"\n5.",-5} Delete Contact By Email");
            Console.WriteLine($"{"\n0.",-5} Exit Application");

            Console.Write("\nPlease choose an option: ");
            var answer = Console.ReadLine() ?? "";

            switch (answer)
            {
                case "1":
                    ShowAddContactToList();
                    break;
                case "2":
                    ShowGetAllContacts();
                    break;
                case "3":
                    ShowGetContactByEmail();
                    break;
                case "4":
                    ShowUpdateContactByEmail();
                    break;
                case "5":
                    ShowDeleteContactByEmail();
                    break;
                case "0":
                    ShowExitApplication();
                    break;
                default:
                    Console.WriteLine("Invalid Option. Press Any Key To Try Again.");
                    Console.ReadKey();
                    break;
            }

            void ShowExitApplication()
            {
                Console.Write($"\nDo You Want To Quit The Application? (y/n):\n");
                string answer = Console.ReadLine()!;
                if (answer == "y")
                {
                    Environment.Exit(0);
                }
            }

            void ShowAddContactToList()
            {
                PContact contact = new();

                Console.Clear();
                Console.WriteLine("----------Add Contact--------");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("\nAdd New Contact Details.\n");
                Console.Write("FirstName: ");
                contact.FirstName = Console.ReadLine()!;
                Console.Write("LastName: ");
                contact.LastName = Console.ReadLine()!;
                Console.Write("Email: ");
                contact.Email = Console.ReadLine()!;
                Console.Write("PhoneNumber: ");
                contact.PhoneNumber = Console.ReadLine()!;
                Console.Write("Address (Streetname & Number, Post Code, City): ");
                contact.Address = Console.ReadLine()!;

                var res = _pContactService.AddContactToList(contact);

                if (res == true)
                {
                    Console.WriteLine($"\n{contact.FirstName} Was SuccesFully Added To The List.");
                }
                else
                {
                    if (contact.Email == "" || contact.FirstName == "")
                    {
                        string firstName = "Contact";
                        contact.FirstName = firstName;
                        string email = "Email";
                        contact.Email = email;

                    }
                    Console.WriteLine($"\nContact Could Not Be Added - '{contact.Email}' And/Or '{contact.FirstName}' Is Empty, Invalid Or Already In Use");
                }
                PressAnyKey();
            }

            void ShowGetAllContacts()
            {
                var contacts = _pContactService.GetAllContactsFromList();
                Console.Clear();
                Console.WriteLine("----------Add Contact--------");
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"\nThere Are {contacts.Count()} Contacts In Your List.");

                foreach (var contact in contacts)
                {
                    Console.WriteLine($"\nFirst Name: {contact.FirstName}\nLast Name: {contact.LastName}\nEmail: {contact.Email}\nPhone Number: {contact.PhoneNumber}\nAddress: {contact.Address}");
                }
                PressAnyKey();
            }

            void ShowGetContactByEmail()
            {
                PContact contact = new PContact();

                Console.Clear();
                Console.WriteLine("----------Show Contact by Email--------");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("\nType in Email of Employee to show.\n");
                Console.Write("Email: ");
                contact.Email = Console.ReadLine()!;
                var result = _pContactService.GetContactFromListByEmail(contact);
                if (result != null)
                {
                    LoopName(result);
                }
                else
                    Console.WriteLine("\nNo Contact With That Email address.");
                PressAnyKey();
            }

            void ShowUpdateContactByEmail()
            {
                PContact contact = new PContact();

                Console.Clear();
                Console.WriteLine("---------Update Contact by Email--------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("\nType Email Of Employee To Update.\n");
                Console.Write("Email: ");
                contact.Email = Console.ReadLine()!;
                var result = _pContactService.GetContactFromListByEmail(contact);
                if (result == null)
                {
                    Console.WriteLine("\nNo Contact With That Email Address.");
                }
                else
                {
                    Console.Write($"\nIs This The Employee To Update:\n");
                    var match = LoopName(result);
                    Console.Write("\n(y/n) ");
                    string answer = Console.ReadLine()!;

                    if (answer.Equals("y", StringComparison.CurrentCultureIgnoreCase))
                    {
                        PContact updatedContact = new PContact();

                        Console.WriteLine($"\nAdd Updated Details For Contact.");
                        Console.Write("FirstName: ");
                        updatedContact.FirstName = Console.ReadLine()!;
                        Console.Write("LastName: ");
                        updatedContact.LastName = Console.ReadLine()!;
                        Console.Write("Email: ");
                        updatedContact.Email = Console.ReadLine()!;
                        Console.Write("PhoneNumber: ");
                        updatedContact.PhoneNumber = Console.ReadLine()!;
                        Console.Write("Address (Streetname & Number, Post Code, City): ");
                        updatedContact.Address = Console.ReadLine()!;

                        var res = _pContactService.UpdateContactToListByEmail(match, updatedContact);

                        if (res)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nContact Updated Succesfully.\nFrom:");
                            PrintContactMatch(match);
                            Console.WriteLine($"\nTo:");
                            Console.WriteLine($"\nFirst Name: {updatedContact.FirstName}\nLast Name: {updatedContact.LastName}\nEmail: {updatedContact.Email}\nPhone Number: {updatedContact.PhoneNumber}\nAddress: {updatedContact.Address}");

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Contact:");
                            PrintContactMatch(match);
                            Console.WriteLine($"\nCould Not Be Updated:");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n{match.FirstName} {match.LastName} Will Not Be Updated.");
                    }                    
                }
                PressAnyKey();
            }

            void ShowDeleteContactByEmail()
            {
                IPContact contact = new PContact();

                Console.Clear();
                Console.WriteLine("---------Delete Contact by Email--------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("\nType Email Of Employee To Delete.\n");
                Console.Write("Email: ");
                contact.Email = Console.ReadLine()!;
                var result = _pContactService.GetContactFromListByEmail(contact);
                if (result == null)
                {
                    Console.WriteLine("\nNo Contact With That Email Address.");
                }
                else
                {
                    Console.Write($"\nIs This The Employee To Delete:\n");
                    var match = LoopName(result);
                    Console.Write("\n(y/n) ");
                    string answer = Console.ReadLine()!;

                    if (answer.ToLower() == "y")
                    {
                        var contactToDelete = _pContactService.DeleteContactByEmail(match);

                        if (contactToDelete)
                        {
                            Console.Clear();
                            Console.WriteLine($"Contact:");
                            PrintContactMatch(match);
                            Console.WriteLine($"\nWas Deleted.");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Contact:");
                            PrintContactMatch(match);
                            Console.WriteLine($"\nCould Not Be Deleted.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n{match.FirstName} {match.LastName} Will Not Be Deleted.");
                    }
                    PressAnyKey();
                }
            }

            /** <summary>
            * Prints to Console and awaits user input
            * </summary>*/
            void PressAnyKey()
            {
                Console.WriteLine("\n\nPress Any Key To Continue.");
                Console.ReadKey();
            }

            /** <summary>
            * Prints the Contact that matches the email when using LoopName().
            * </summary>*/
            void PrintContactMatch(IPContact match)
            {
                Console.WriteLine($"\nFirst Name: {match.FirstName}\nLast Name: {match.LastName}\nEmail: {match.Email}\nPhone Number: {match.PhoneNumber}\nAddress: {match.Address}");
            }

            /// <summary>
            /// Takes in a a List of IContact, loops through and displays the match.
            /// </summary>
            /// <param name="result"></param>
            /// <returns>IContact</returns>
            IPContact LoopName(ObservableCollection<IPContact> result)
            {
                try
                {
                    foreach (var match in result)
                    {
                        Console.WriteLine($"\nFirst Name: {match.FirstName}\nLast Name: {match.LastName}\nEmail: {match.Email}\nPhone Number: {match.PhoneNumber}\nAdress: {match.Address}");
                        return match;
                    }
                    return null!;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                return null!;
            }
        }
    }
}
