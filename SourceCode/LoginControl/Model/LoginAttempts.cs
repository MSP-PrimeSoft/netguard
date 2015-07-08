using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginControl.Model
{
    public class LoginAttempts
    {

        private int _id;
        private int _loginAttempt;
        private int _createdBy;
        private DateTime _createdOn;
        private string _createdIp;
        private int _updatedBy;
        private DateTime _updatedOn;
        private string _updatedIp;

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

        public int LoginAttempt
        {
            get
            {
                return _loginAttempt;
            }
            set
            {
                _loginAttempt = value;
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
    }
}