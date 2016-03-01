using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data2;
using Data2.DTOs;

namespace MVC.Models
{
    public class ContactModel
    {
        public List<EmailDTO> Emails { get; set; }
        public List<PhoneDTO> Phones { get; set; }
        public List<AddressDTO> Address { get; set; }

        public static ContactModel Load(int empID)
        {
            ContactModel model = new ContactModel();
            model.Address = new List<AddressDTO>();
            model.Emails = new List<EmailDTO>();
            model.Phones = new List<PhoneDTO>();

            List<Address> addresses = Employees.getAllAdressesOfEmployee(empID);
            List<Email> emails = Employees.getAllEmailsOfEmployee(empID);
            List<Telephone> telephones = Employees.getAllPhoneNubersOfEmployee(empID);

            foreach (var a in addresses)
            {
                model.Address.Add(CreateAddressDTO(a));
            }

            foreach (var e in emails)
            {
                model.Emails.Add(CreateEmailDTO(e));
            }

            foreach (var t in telephones)
            {
                model.Phones.Add(CreatePhoneDTO(t));
            }

            return model;
        }

        public static AddressDTO CreateAddressDTO(Address a)
        {
            return new AddressDTO()
            {
                AddressID = a.addressID,
                Address = a.address1
            };
        }

        public static EmailDTO CreateEmailDTO(Email e)
        {
            return new EmailDTO()
            {
                EmailID = e.emailID,
                Email = e.emailAddress
            };
        }

        public static PhoneDTO CreatePhoneDTO(Telephone t)
        {
            return new PhoneDTO()
            {
                PhoneID = t.telephoneID,
                Phone = t.telephoneNumber
            };
        }
    }
}