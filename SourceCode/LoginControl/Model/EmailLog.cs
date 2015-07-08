using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginControl.Model
{
    public class EmailLog
    {
        private int _id;
        private string _subject;
        private string _body;
        private string _to;
        private string _from;
        private int _createdBy;
        private DateTime _createdOn;

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

        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
            }
        }

        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
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
    }
}