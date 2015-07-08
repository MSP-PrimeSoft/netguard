using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginControl.Model
{
    public class Role
    {

        private int _id;
        private string _roleName;
        private bool _isActive;

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

        public string RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                _roleName = value;
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
    }
}