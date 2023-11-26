using System;
using System.Collections.Generic;
using System.Linq;

class Contact
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Preference { get; set; }

    public Contact(string fullName, string phoneNumber, string preference)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Preference = preference;
    }
}

class Call
{
    public DateTime CallTime { get; set; }
    public string CallStatus { get; set; }

    public Call(DateTime callTime, string callStatus)
    {
        CallTime = callTime;
        CallStatus = callStatus;
    }
}

class Program
{
    static Dictionary<Contact, List<Call>> phoneBook = new Dictionary<Contact, List<Call>>();

    static void Main(string[] args)
    {
        bool runApp = true;
        while (runApp)
        {
            Console.Clear();
            Console.WriteLine("Odaberite opciju:");
            Console.WriteLine("1. Ispis svih kontakata");
            Console.WriteLine("2. Dodavanje novih kontakata u imenik");
            Console.WriteLine("3. Brisanje kontakata iz imenika");
            Console.WriteLine("4. Editiranje preference kontakta");
            Console.WriteLine("5. Upravljanje kontaktom");
            Console.WriteLine("6. Ispis svih poziva");
            Console.WriteLine("7. Izlaz iz aplikacije");

            int option;
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        PrintAllContacts();
                        break;
                    case 2:
                        AddContact();
                        break;
                    case 3:
                        DeleteContact();
                        break;
                    case 4:
                        EditContactPreference();
                        break;
                    case 5:
                        ContactManagement();
                        break;
                    case 6:
                        PrintAllCalls();
                        break;
                    case 7:
                        runApp = false;
                        break;
                    default:
                        Console.WriteLine("Unesite valjanu opciju.");
                        break;
                }
                Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Unesite brojčanu vrijednost.");
                Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
                Console.ReadLine();
            }
        }
    }

    static void PrintAllContacts()
    {
        Console.Clear();
        foreach (var contact in phoneBook.Keys)
        {
            Console.WriteLine($"Ime i prezime: {contact.FullName}, Broj mobitela: {contact.PhoneNumber}, Preferenca: {contact.Preference}");
        }
        Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
        Console.ReadLine();
    }

    static void AddContact()
    {
        Console.Clear();
        Console.WriteLine("Unesite ime i prezime:");
        string fullName = Console.ReadLine();

        Console.WriteLine("Unesite broj mobitela:");
        string phoneNumber = Console.ReadLine();

        Console.WriteLine("Unesite preferencu (favorit, normalan, blokiran kontakt):");
        string preference = Console.ReadLine();

        if (!phoneBook.Keys.Any(c => c.PhoneNumber == phoneNumber))
        {
            Contact newContact = new Contact(fullName, phoneNumber, preference);
            phoneBook.Add(newContact, new List<Call>());
            Console.WriteLine("Kontakt uspješno dodan.");
        }
        else
        {
            Console.WriteLine("Kontakt s tim brojem mobitela već postoji.");
        }
        Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
        Console.ReadLine();
    }

    static void DeleteContact()
    {
        Console.Clear();
        Console.WriteLine("Unesite broj mobitela kontakta kojeg želite izbrisati:");
        string phoneNumber = Console.ReadLine();

        Contact contactToDelete = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (contactToDelete != null)
        {
            phoneBook.Remove(contactToDelete);
            Console.WriteLine("Kontakt uspješno izbrisan.");
        }
        else
        {
            Console.WriteLine("Kontakt s tim brojem mobitela ne postoji.");
        }
        Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
        Console.ReadLine();
    }

    static void EditContactPreference()
    {
        Console.Clear();
        Console.WriteLine("Unesite broj mobitela kontakta čiju preferencu želite urediti:");
        string phoneNumber = Console.ReadLine();

        Contact contactToEdit = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (contactToEdit != null)
        {
            Console.WriteLine("Unesite novu preferencu (favorit, normalan, blokiran kontakt):");
            string newPreference = Console.ReadLine();
            contactToEdit.Preference = newPreference;
            Console.WriteLine("Preferenca kontakta uspješno uređena.");
        }
        else
        {
            Console.WriteLine("Kontakt s tim brojem mobitela ne postoji.");
        }
        Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
        Console.ReadLine();
    }

    static void ContactManagement()
    {
        bool runContactMenu = true;

        while (runContactMenu)
        {
            Console.Clear();
            Console.WriteLine("Odaberite opciju za upravljanje kontaktom:");
            Console.WriteLine("1. Ispis svih poziva sa kontaktom");
            Console.WriteLine("2. Kreiranje novog poziva");
            Console.WriteLine("3. Izlaz iz upravljanja kontaktom");

            int contactOption;
            if (int.TryParse(Console.ReadLine(), out contactOption))
            {
                switch (contactOption)
                {
                    case 1:
                        PrintAllCallsWithContact();
                        break;
                    case 2:
                        MakeCall();
                        break;
                    case 3:
                        runContactMenu = false;
                        break;
                    default:
                        Console.WriteLine("Unesite valjanu opciju.");
                        break;
                }
                Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Unesite brojčanu vrijednost.");
                Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
                Console.ReadLine();
            }
        }
    }

    static void PrintAllCallsWithContact()
    {
        Console.Clear();
        Console.WriteLine("Unesite broj mobitela kontakta čije pozive želite ispisati:");
        string phoneNumber = Console.ReadLine();

        Contact selectedContact = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (selectedContact != null)
        {
            if (phoneBook.ContainsKey(selectedContact))
            {
                List<Call> calls = phoneBook[selectedContact];
                if (calls.Any())
                {
                    calls = calls.OrderByDescending(c => c.CallTime).ToList();
                    foreach (var call in calls)
                    {
                        Console.WriteLine($"Vrijeme poziva: {call.CallTime}, Status: {call.CallStatus}");
                    }
                }
                else
                {
                    Console.WriteLine("Nema poziva za odabrani kontakt.");
                }
            }
        }
        else
        {
            Console.WriteLine("Kontakt s tim brojem mobitela ne postoji.");
        }
        Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
        Console.ReadLine();
    }

    static void MakeCall()
    {
        Console.Clear();
        Console.WriteLine("Unesite broj mobitela kontakta kojem želite telefonirati:");
        string phoneNumber = Console.ReadLine();

        Contact selectedContact = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (selectedContact != null)
        {
            if (selectedContact.Preference == "blokiran kontakt")
            {
                Console.WriteLine("Nemoguće pozvati blokiran kontakt.");
                return;
            }

            bool isInCall = phoneBook.Any(entry => entry.Value.Any(call => call.CallStatus == "u tijeku"));
            if (isInCall)
            {
                Console.WriteLine("Postoji poziv u tijeku. Molimo pričekajte.");
                return;
            }

            Random random = new Random();
            int callDuration = random.Next(1, 21);
            DateTime callTime = DateTime.Now;
            Call newCall = new Call(callTime, "u tijeku");

            if (phoneBook.ContainsKey(selectedContact))
            {
                phoneBook[selectedContact].Add(newCall);
            }
            else
            {
                phoneBook.Add(selectedContact, new List<Call> { newCall });
            }

            Console.WriteLine($"Poziv uspješno uspostavljen. Trajanje poziva: {callDuration} sekundi.");
            System.Threading.Thread.Sleep(callDuration * 1000);
            newCall.CallStatus = "završen";
            Console.WriteLine("Poziv završen.");
        }
        else
        {
            Console.WriteLine("Kontakt s tim brojem mobitela ne postoji.");
        }
        Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
        Console.ReadLine();
    }

    static void PrintAllCalls()
    {
        Console.Clear();
        foreach (var contact in phoneBook)
        {
            foreach (var call in contact.Value)
            {
                Console.WriteLine($"Kontakt: {contact.Key.FullName}, Vrijeme poziva: {call.CallTime}, Status: {call.CallStatus}");
            }
        }
        Console.WriteLine("Pritisnite Enter za povratak na izbornik.");
        Console.ReadLine();
    }
}
