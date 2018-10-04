using System.ComponentModel;

namespace ParentChildInfoSystem.Model
{
    public class Person
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if(_firstName != value)
                {
                    _firstName = value;
                }
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if(_lastName != value)
                {
                    _lastName = value;
                }
            }
        }
    }
}
