namespace ParentChildInfoSystem.Model
{
    public class Student : Person
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private Address _address;

        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public Student()
        {
            this._address = new Address();
        }
    }
}
