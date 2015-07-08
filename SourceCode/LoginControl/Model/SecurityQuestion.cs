using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginControl.Model
{
    public class SecurityQuestion
    {

        private int _id;
        private string _questionName;
        private int _updatedBy;
        private DateTime? _updatedOn;


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

        public string QuestionName
        {
            get
            {
                return _questionName;
            }
            set
            {
                _questionName = value;
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
    }
}