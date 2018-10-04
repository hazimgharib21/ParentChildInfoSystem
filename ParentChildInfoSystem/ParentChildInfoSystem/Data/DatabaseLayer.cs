using ParentChildInfoSystem.Model;
using System.Collections.ObjectModel;

namespace ParentChildInfoSystem.Data
{
    class DatabaseLayer
    {
        public static ObservableCollection<Student> GetStudentFromLocalDatabase()
        {
            return new ObservableCollection<Student>
            {
                new Student {ID=1, FirstName="Ali", LastName="Roshan"},
                new Student {ID=2, FirstName="Abu", LastName="Bambu"},
                new Student {ID=3, FirstName="Hitomi", LastName="Tanaka"},
            };
        }

        public static ObservableCollection<Parent> GetParentFromLocalDatabase()
        {
            return new ObservableCollection<Parent>
            {
                new Parent {ID=1, FirstName="Pak", LastName="Roshan"},
                new Parent {ID=2, FirstName="Pak", LastName="Bambu"},
                new Parent {ID=3, FirstName="Pak", LastName="Tanaka"},
            };
        }

        public static ObservableCollection<Teacher> GetTeacherFromLocalDatabase()
        {
            return new ObservableCollection<Teacher>
            {
                new Teacher {ID=1, FirstName="Cikgu", LastName="Roshan"},
                new Teacher {ID=2, FirstName="Cikgu", LastName="Bambu"},
                new Teacher {ID=3, FirstName="Cikgu", LastName="Tanaka"},
            };
        }
    }
}
