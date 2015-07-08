using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginControl.Model
{
    public partial class UserLogin
    {

        private int _id;
        private string _userName;
        private string _password;
        private int _securityQuestion;
        private string _answer;
        private int _passwordWrongAttempts;
        private DateTime? _lastPasswordWrong;
        private int _createdBy;
        private DateTime _createdOn;
        private int _updatedBy;
        private DateTime _updatedOn;
        private int _roleId;
        private bool _isActive;
        private int _userId;
        private bool _accountLocked;

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

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public int SecurityQuestion
        {
            get
            {
                return _securityQuestion;
            }
            set
            {
                _securityQuestion = value;
            }
        }

        public string Answer
        {
            get
            {
                return _answer;
            }
            set
            {
                _answer = value;
            }
        }

        public int PasswordWrongAttempts
        {
            get
            {
                return _passwordWrongAttempts;
            }
            set
            {
                _passwordWrongAttempts = value;
            }
        }

        public DateTime? LastPasswordWrong
        {
            get
            {
                return _lastPasswordWrong;
            }
            set
            {
                _lastPasswordWrong = value;
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

        public DateTime UpdatedOn
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

        public int RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }


        public bool AccountLocked
        {
            get
            {
                return _accountLocked;
            }
            set
            {
                _accountLocked = value;
            }
        }
    }
}