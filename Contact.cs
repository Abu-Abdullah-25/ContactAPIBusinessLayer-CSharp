using ContactDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPIBusinessLayer
{
    public class Contact
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public ContactDTO CDTO
        {
            get { return new ContactDTO(this.ContactID,this.FirstName,this.LastName,this.Email,this.Phone,
                this.Address,this.DateOfBirth,this.CountryID,this.ImagePath); }
        }

        public int ContactID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public int CountryID { set; get; }
        public string? ImagePath { get; set; } // Nullable string

        public Contact(ContactDTO CDTO, enMode cMode = enMode.AddNew)
        {
            this.ContactID = CDTO.ContactID;
            this.FirstName = CDTO.FirstName;
            this.LastName = CDTO.LastName;
            this.Email = CDTO.Email;
            this.Phone = CDTO.Phone;
            this.Address = CDTO.Address;
            this.DateOfBirth = CDTO.DateOfBirth;
            this.CountryID = CDTO.CountryID;
            this.ImagePath = CDTO.ImagePath;

            Mode = cMode;
        }


        public static Contact Find(int ID)
        {
            ContactDTO CDTO = ContactData.GetContactById(ID);

            if (CDTO != null)
            {
                return new Contact(CDTO, enMode.Update);
            }

            else
                return null;
        }

        public bool _AddNewContact()
        {
            this.ContactID = ContactData.AddNewContact(CDTO);
            return (this.ContactID != -1);
        }

        public bool _UpdateContact()
        {
            bool isUpdatedSuccess = ContactData.UpdateContact(CDTO);
            return isUpdatedSuccess;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateContact();

            }

            return false;
        }

        public static List<ContactDTO> GetAllContacts()
        {
            return ContactData.GetAllContacts();
        }

        public static bool DeleteContact(int ContactID)
        {
            return ContactData.DeleteContact(ContactID);
        }

        public static bool isContactExist(int ID)
        {
            return ContactData.IsContactExist(ID);
        }


        //public static bool isContactExist(string ContactName)
        //{
        //    bool isFound = ContactData.IsContactExist(ContactName);
        //    return isFound;
        //}
    }
}
