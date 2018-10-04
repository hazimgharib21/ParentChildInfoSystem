using ParentChildInfoSystem.Data;
using ParentChildInfoSystem.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ParentChildInfoSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string personType = "";
        Student selectedStudent;
        Parent selectedParent;
        Teacher selectedTeacher;
        int studentID;
        int parentID;
        int teacherID;
        bool canUpdate = true;
        bool canCreate = true;
        bool canDelete = true;
        public static ObservableCollection<Student> Student { get; set; }
        public static ObservableCollection<Parent> Parent { get; set; }
        public static ObservableCollection<Teacher> Teacher { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Student = new ObservableCollection<Student>(DatabaseLayer.GetStudentFromLocalDatabase());
            Parent = new ObservableCollection<Parent>(DatabaseLayer.GetParentFromLocalDatabase());
            Teacher = new ObservableCollection<Teacher>(DatabaseLayer.GetTeacherFromLocalDatabase());
            updateButton.IsEnabled = !canUpdate;
            createButton.IsEnabled = !canCreate;
            deleteButton.IsEnabled = !canDelete;
        }

        private void studentButton_Clicked(object sender, RoutedEventArgs e)
        {
            personList.ItemsSource = Student;
            personType = "Student";
            selectedFirstName.Text = "";
            selectedLastName.Text = "";
        }

        private void parentButton_Clicked(object sender, RoutedEventArgs e)
        {
            personList.ItemsSource = Parent;
            personType = "Parent";
            selectedFirstName.Text = "";
            selectedLastName.Text = "";
        }

        private void teacherButton_Clicked(object sender, RoutedEventArgs e)
        {
            personList.ItemsSource = Teacher;
            personType = "Teacher";
            selectedFirstName.Text = "";
            selectedLastName.Text = "";
        }

        private void personlist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(personType == "Student")
            {
                selectedStudent = personList.SelectedItem as Student;
                if (selectedStudent == null)
                {
                    selectedFirstName.Text = "";
                    selectedLastName.Text = "";
                    return;
                }
                studentID = selectedStudent.ID;
                selectedFirstName.Text = selectedStudent.FirstName;
                selectedLastName.Text = selectedStudent.LastName;
            }
            else if(personType == "Teacher")
            {
                selectedTeacher = personList.SelectedItem as Teacher;
                if (selectedTeacher == null)
                {
                    selectedFirstName.Text = "";
                    selectedLastName.Text = "";
                    return;
                }
                teacherID = selectedTeacher.ID;
                selectedFirstName.Text = selectedTeacher.FirstName;
                selectedLastName.Text = selectedTeacher.LastName;
            }
            else if(personType == "Parent")
            {
                selectedParent = personList.SelectedItem as Parent;
                if (selectedParent == null)
                {
                    selectedFirstName.Text = "";
                    selectedLastName.Text = "";
                    return;
                }
                parentID = selectedParent.ID;
                selectedFirstName.Text = selectedParent.FirstName;
                selectedLastName.Text = selectedParent.LastName;
            }
            
        }

        private void deleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            if(personType == "Student")
            {
                Student.Remove(Student.Where(x => x.ID == studentID).Single());
            }
        }

        private void updateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if(personType == "Student")
            {
                Student updateStudent = new Student();
                updateStudent.ID = studentID;
                updateStudent.FirstName = selectedFirstName.Text;
                updateStudent.LastName = selectedLastName.Text;
                
                var found = Student.FirstOrDefault(x => x.ID == studentID);
                int i = Student.IndexOf(found);
                if (i < 0) return;
                Student[i] = updateStudent;
                personList.ItemsSource = Student;
            }
        }

        private void createButton_Clicked(object sender, RoutedEventArgs e)
        {
            if(selectedFirstName.Text == "" || selectedLastName.Text == "")
            {
                MessageBox.Show("Please fill up the form");
                return;
            }
            if(personType == "Student")
            {
                var max = Student.Max(x => x.ID);
                Student createStudent = new Student();
                createStudent.ID = max + 1;
                createStudent.FirstName = selectedFirstName.Text;
                createStudent.LastName = selectedLastName.Text;
                Student.Add(createStudent);
            }
            else if(personType == "Parent")
            {
                var max = Parent.Max(x => x.ID);
                Parent createParent = new Parent();
                createParent.ID = max + 1;
                createParent.FirstName = selectedFirstName.Text;
                createParent.LastName = selectedLastName.Text;
                Parent.Add(createParent);
            }
            else if(personType == "Teacher")
            {
                var max = Teacher.Max(x => x.ID);
                Teacher createTeacher = new Teacher();
                createTeacher.ID = max + 1;
                createTeacher.FirstName = selectedFirstName.Text;
                createTeacher.LastName = selectedLastName.Text;
                Teacher.Add(createTeacher);
            }
            else
            {
                MessageBox.Show("Please Select Person");
            }
        }

        
    }
}
