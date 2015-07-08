using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginControl.Model
{
    public class LoginHistory
    {


        private int _id;
        private int _userId;
        private DateTime _loginTime;
        private DateTime? _logoutTime;
        private int _createdBy;
        private DateTime _createdOn;
        private string _createdIp;
        private int _updatedBy;
        private DateTime? _updatedOn;
        private string _updatedIp;
        private string _userName;

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

        public DateTime LoginTime
        {
            get
            {
                return _loginTime;
            }
            set
            {
                _loginTime = value;
            }
        }

        public DateTime? LogoutTime
        {
            get
            {
                return _logoutTime;
            }
            set
            {
                _logoutTime = value;
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

        public string CreatedIp
        {
            get
            {
                return _createdIp;
            }
            set
            {
                _createdIp = value;
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

        public string UpdatedIp
        {
            get
            {
                return _updatedIp;
            }
            set
            {
                _updatedIp = value;
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
    }
}