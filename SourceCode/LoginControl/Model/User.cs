using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginControl.Model
{
    public class User
    {


        private int _id;
        private string _firstName;
        private string _lastName;
        private string _mobile;
        private string _email;
        private int _gender;
        private string _address;
        private int _countryId;
        private string _city;
        private string _zipCode;
        private int _createdBy;
        private DateTime _createdOn;
        private int _updatedBy;
        private DateTime? _updatedOn;
        private bool _isEmailVerified;
        private UserLogin _userLogin;
        private LoginHistory _loginHistory;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public int Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        public int CountryId
        {
            get
            {
                return _countryId;
            }
            set
            {
                _countryId = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return _zipCode;
            }
            set
            {
                _zipCode = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        public DateTime CreatedOn
        {
            get
            {
                return _createdOn;
            }
            set
            {
                _createdOn = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return _updatedBy;
            }
            set
            {
                _updatedBy = value;
            }
        }

        public DateTime? UpdatedOn
        {
            get
            {
                return _updatedOn;
            }
            set
            {
                _updatedOn = value;
            }
        }

        public bool IsEmailVerified
        {
            get
            {
                return _isEmailVerified;
            }
            set
            {
                _isEmailVerified = value;
            }
        }

        public UserLogin UserLogin
        {
            get
            {
                return _userLogin;
            }
            set
            {
                _userLogin = value;
            }
        }

        public LoginHistory LoginHistory
        {
            get
            {
                return _loginHistory;
            }
            set
            {
                _loginHistory = value;
            }
        }
    }
}