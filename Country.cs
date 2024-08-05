using ContactDataAccessLayer;

namespace ContactAPIBusinessLayer
{
    public class Country
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public CountryDTO CDTO
        {
            get { return new CountryDTO(this.CountryID, this.CountryName, this.Code, this.PhoneCode); }
        }

        public int CountryID { set; get; }
        public string CountryName { set; get; }
        public string Code { set; get; }
        public string PhoneCode { set; get; }


        public Country(CountryDTO CDTO, enMode cMode = enMode.AddNew)
        {
            this.CountryID = CDTO.CountryID;
            this.CountryName = CDTO.CountryName;
            this.Code = CDTO.Code;
            this.PhoneCode = CDTO.PhoneCode;

            Mode = cMode;
        }

        public static Country Find(int ID)
        {
            CountryDTO CDTO = CountryData.GetCountryById(ID);

            if (CDTO != null)
            {
                return new Country(CDTO, enMode.Update);
            }

            else
                return null;
        }
        public static Country Find(string CountryName)
        {
            CountryDTO CDTO = CountryData.GetCountryByName(CountryName);

            if (CDTO != null)
            {
                return new Country(CDTO, enMode.Update);
            }

            else
                return null;
        }

        public bool _AddNewCountry()
        {
            this.CountryID = CountryData.AddNewCountry(CDTO);
            return (this.CountryID != -1);
        }

        public bool _UpdateCountry()
        {
            return CountryData.UpdateCountry(CDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateCountry();

            }

            return false;
        }

        public static List<CountryDTO> GetAllCountries()
        {
            return CountryData.GetAllCountries();
        }

        public static bool DeleteCountry(int CountryID)
        {
            return CountryData.DeleteCountry(CountryID);
        }

        public static bool isCountryExist(int ID)
        {
            return CountryData.IsCountryExist(ID);
        }


        public static bool isCountryExist(string CountryName)
        {
            return CountryData.IsCountryExist(CountryName);
        }
    }
}
