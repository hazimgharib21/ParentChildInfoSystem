using System.ComponentModel;

namespace ParentChildInfoSystem.Model
{
    public class Person
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;
                }
            }
        }

        
    }
}
