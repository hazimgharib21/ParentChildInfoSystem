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
    }
}
