using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentChildInfoSystem.Model
{
    public class Address
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _streets;

        public string Streets
        {
            get { return _streets; }
            set { _streets = value; }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _state;

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _zipcode;

        public string Zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }


    }
}
