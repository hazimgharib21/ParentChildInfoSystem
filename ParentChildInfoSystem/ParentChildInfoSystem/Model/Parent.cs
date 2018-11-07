namespace ParentChildInfoSystem.Model
{
    public class Parent : Person
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _addressID;

        private Address _address;

        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public Parent()
        {
            this._address = new Address();
        }
    }
}
