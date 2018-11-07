using ParentChildInfoSystem.Data;
using ParentChildInfoSystem.Model;
using ParentChildInfoSystem.View;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ParentChildInfoSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string selected_person_type = "";
        int selected_id;
        string personType = "";
        string respectivePersonType = "";
        Student selectedStudent;
        Parent selectedParent;
        Teacher selectedTeacher;
        Student resSelectedStudent;
        Parent resSelectedParent;
        Teacher resSelectedTeacher;
        string selection = "";
        string selectFN;
        string selectLN;
        int studentID;
        int parentID;
        int teacherID;
        Boolean hasTextChanged = false;
        string imagePath = "D:\\ImagePath\\";

        public static ObservableCollection<Student> Student { get; set; }
        public static ObservableCollection<Parent> Parent { get; set; }
        public static ObservableCollection<Teacher> Teacher { get; set; }


        public MainWindow()
        {
            InitializeComponent();


            Thread thdUDPServer = new Thread(new ThreadStart(serverThread));
            thdUDPServer.Start();

            Teacher = new ObservableCollection<Teacher>(DatabaseLayer.GetTeacherFromLocalDatabase());
            form_GroupBox.IsEnabled = false;
            button_create.IsEnabled = false;
        }

        private void studentButton_Clicked(object sender, RoutedEventArgs e)
        {
            selected_person_type = "Student";
            mainListLabel.Content = selected_person_type;
            var dt = new DataTable();
            personList.ItemsSource = DatabaseLayer.FillStudentList().DefaultView;
            button_create.IsEnabled = true;
            button_create.Content = "New Student";
            /*
            
            
            personList.ItemsSource = DatabaseLayer.FillStudentList().DefaultView;
            personType = "Student";
            selectedFirstName.Text = "";
            form_GroupBox.IsEnabled = false;
            respectivePersonList.ItemsSource = null;
            respectivePersonType = "Parent";
            mainListLabel.Content = personType;
            button_create.Content = "Create New Student";
            */
        }

        private void parentButton_Clicked(object sender, RoutedEventArgs e)
        {
            selected_person_type = "Parent";
            mainListLabel.Content = selected_person_type;
            var dt = new DataTable();
            personList.ItemsSource = DatabaseLayer.FillParentList().DefaultView;
            button_create.IsEnabled = true;
            button_create.Content = "New Parent";
            /*
            button_create.IsEnabled = true;
            personList.ItemsSource = DatabaseLayer.FillParentList().DefaultView;
            personType = "Parent";
            selectedFirstName.Text = "";
            form_GroupBox.IsEnabled = false;
            respectivePersonList.ItemsSource = null;
            respectivePersonType = "Student";
            mainListLabel.Content = personType;
            respectListLabel.Content = respectivePersonType;
            button_create.Content = "Create New Parent";
            */
        }

        private void teacherButton_Clicked(object sender, RoutedEventArgs e)
        {
            personList.ItemsSource = Teacher;
            selected_person_type = "Teacher";
            form_GroupBox.IsEnabled = false;
            button_create.IsEnabled = false;
            mainListLabel.Content = personType;
            /*
            personList.ItemsSource = Teacher;
            personType = "Teacher";
            selectedFirstName.Text = "";
            form_GroupBox.IsEnabled = false;
            respectivePersonList.ItemsSource = null;
            respectivePersonType = "";
            mainListLabel.Content = personType;
            respectListLabel.Content = respectivePersonType;
            button_create.IsEnabled = false;
            */
        }

        private void personlist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            if (selected_person_type == "resStudent") selected_person_type = "Parent";
            if (selected_person_type == "resParent") selected_person_type = "Student";

            form_GroupBox.IsEnabled = true;
            DataRowView row = personList.SelectedItem as DataRowView;
            if (row == null) return;
            
            selected_id = (int)row.Row.ItemArray[0];
            selectedFirstName.Text = row.Row.ItemArray[1].ToString();
            selected_address.Text = row.Row.ItemArray[2].ToString();
            selected_city.Text = row.Row.ItemArray[3].ToString();
            selected_state.Text = row.Row.ItemArray[4].ToString();
            selected_zipcode.Text = row.Row.ItemArray[5].ToString();
            try
            {
                var Picture = new BitmapImage(new Uri(imagePath + selectedFirstName.Text + ".jpg"));
                personImage.Source = Picture;
            }
            catch
            {
                personImage.Source = null;
            }

            if(selected_person_type == "Student")
            {
                respectivePersonList.ItemsSource = DatabaseLayer.getRespectiveParentFromDatabase(selected_id).DefaultView;
            }
            else if(selected_person_type == "Parent")
            {
                respectivePersonList.ItemsSource = DatabaseLayer.getRespectiveStudentFromDatabase(selected_id).DefaultView;
            }

            hasTextChanged = false;
            /*
            form_GroupBox.IsEnabled = true;
            selection = "main";
            

            if (personType == "Student")
            {
                respectivePersonType = "Parent";
                selectedStudent = personList.SelectedItem as Student;
                if (selectedStudent == null)
                {
                    selectedFirstName.Text = "";
                    return;
                }
                studentID = selectedStudent.ID;
                selectedFirstName.Text = selectedStudent.Name;
                try
                {
                    var Picture = new BitmapImage(new Uri(imagePath + selectedStudent.Name + ".jpg"));
                    if (Picture != null) personImage.Source = Picture;
                }
                catch(System.IO.FileNotFoundException ex)
                {
                    personImage.Source = null;
                }
                



                ObservableCollection<Parent> respectiveParents = new ObservableCollection<Parent>(DatabaseLayer.getRespectiveParentFromDatabase(studentID));
                respectivePersonList.ItemsSource = respectiveParents;
            }
            else if(personType == "Teacher")
            {

                respectivePersonType = "";
                selectedTeacher = personList.SelectedItem as Teacher;
                if (selectedTeacher == null)
                {
                    selectedFirstName.Text = "";
                    return;
                }
                teacherID = selectedTeacher.ID;
                selectedFirstName.Text = selectedTeacher.Name;
                personImage.Source = new BitmapImage(new Uri(imagePath + selectedTeacher.Name + ".jpg"));
            }
            else if(personType == "Parent")
            {
                respectivePersonType = "Student";
                selectedParent = personList.SelectedItem as Parent;
                if (selectedParent == null)
                {
                    selectedFirstName.Text = "";
                    return;
                }
                parentID = selectedParent.ID;
                selectedFirstName.Text = selectedParent.Name;

                personImage.Source = new BitmapImage(new Uri(imagePath + selectedParent.Name + ".jpg"));
                ObservableCollection<Student> respectiveStudents = new ObservableCollection<Student>(DatabaseLayer.getRespectiveStudentFromDatabase(parentID));
                respectivePersonList.ItemsSource = respectiveStudents;
            }
            selectFN = selectedFirstName.Text;
            */
        }

        private void deleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation Dialog Box", MessageBoxButton.YesNo);
            if(messageBoxResult == MessageBoxResult.No)
            {
                return;
            }
            if (selection == "main")
            {
                if (personType == "Student")
                {
                    DatabaseLayer.deleteEntry(studentID, personType);
                    personList.ItemsSource = DatabaseLayer.FillStudentList().DefaultView;
                    return;
                }
                if (personType == "Parent")
                {
                    DatabaseLayer.deleteEntry(parentID, personType);
                    personList.ItemsSource = DatabaseLayer.FillParentList().DefaultView;
                    personList.ItemsSource = Parent;
                    return;
                }
            }
            else if (selection == "respective")
            {
                if (respectivePersonType == "Student")
                {
                    DatabaseLayer.deleteEntry(studentID, personType);
                    respectivePersonList.ItemsSource = DatabaseLayer.getRespectiveStudentFromDatabase(parentID).DefaultView;
                }
                if (respectivePersonType == "Parent")
                {
                    DatabaseLayer.deleteEntry(parentID, personType);
                    respectivePersonList.ItemsSource = DatabaseLayer.getRespectiveParentFromDatabase(studentID).DefaultView;
                }
            }
        }

        private void updateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (hasTextChanged)
            {
                if (selected_person_type == "Student" || selected_person_type == "resStudent")
                {
                    DatabaseLayer.updateStudent(selected_id, selectedFirstName.Text, selected_address.Text, selected_city.Text, selected_state.Text, selected_zipcode.Text);

                    personList.ItemsSource = DatabaseLayer.FillStudentList().DefaultView;
                    return;
                }
                if (selected_person_type == "Parent" || selected_person_type == "resParent")
                {


                    personList.ItemsSource = DatabaseLayer.FillParentList().DefaultView;
                    personList.ItemsSource = Parent;

                    return;
                }
            }
                
            
            
               
        }
        
        private void respectivePersonlist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            DataRowView row = respectivePersonList.SelectedItem as DataRowView;
            if (row == null) return;
            selected_id = (int)row.Row.ItemArray[0];

            if (selected_person_type == "Student" || selected_person_type == "resParent")
            {
                selected_person_type = "resParent";
                var parent = DatabaseLayer.getParentData(selected_id);
                selectedFirstName.Text = parent.Rows[0][0].ToString();
                selected_address.Text = parent.Rows[0][1].ToString();
                selected_city.Text = parent.Rows[0][2].ToString();
                selected_state.Text = parent.Rows[0][3].ToString();
                selected_zipcode.Text = parent.Rows[0][4].ToString();

            }
            else if(selected_person_type == "Parent" || selected_person_type == "resStudent")
            {
                selected_person_type = "resStudent";
                var student = DatabaseLayer.getStudentData(selected_id);
                selectedFirstName.Text = student.Rows[0][0].ToString();
                selected_address.Text = student.Rows[0][1].ToString();
                selected_city.Text = student.Rows[0][2].ToString();
                selected_state.Text = student.Rows[0][3].ToString();
                selected_zipcode.Text = student.Rows[0][4].ToString();
            }

            try
            {
                var Picture = new BitmapImage(new Uri(imagePath + selectedFirstName.Text + ".jpg"));
                personImage.Source = Picture;
            }
            catch
            {
                personImage.Source = null;
            }

            hasTextChanged = false;
            /*
            selection = "respective";
            if(respectivePersonType == "Parent")
            {
                resSelectedParent = respectivePersonList.SelectedItem as Parent;
                if (resSelectedParent == null)
                {
                    return;
                }
                parentID = resSelectedParent.ID;
                selectedFirstName.Text = resSelectedParent.Name;
                personImage.Source = new BitmapImage(new Uri(imagePath + resSelectedParent.Name + ".jpg"));
            }
            else if (respectivePersonType == "Student")
            {
                resSelectedStudent = respectivePersonList.SelectedItem as Student;
                if (resSelectedStudent == null)
                {
                    return;
                }
                studentID = resSelectedStudent.ID;
                selectedFirstName.Text = resSelectedStudent.Name;

                personImage.Source = new BitmapImage(new Uri(imagePath + resSelectedStudent.Name + ".jpg"));
            }

            selectFN = selectedFirstName.Text;
            */
        }

        private void createButton_Clicked(object sender, RoutedEventArgs e)
        {
            int maxID = 0;
            if(personType == "Student")
            {
                maxID = Student.Max(x => x.ID) + 1;
            }
            else if(personType == "Parent")
            {
                maxID = Parent.Max(x => x.ID) + 1;
            }
            createPersonWindow newWindow = new createPersonWindow(personType, maxID);
            newWindow.Show();
        }

        public void serverThread()
        {
            UdpClient udpClient = new UdpClient(8080);
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                this.showWindow(returnData.ToString());
            }
        }

        delegate void StringArgReturningVoidDelegate(string text);

        private void showWindow(string text)
        {
            MessageBox.Show(text);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Environment.Exit(0);
            App.Current.Shutdown();
        }

        private void selectedFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            hasTextChanged = true;
        }

        private void selected_address_TextChanged(object sender, TextChangedEventArgs e)
        {
            hasTextChanged = true;
        }

        private void selected_city_TextChanged(object sender, TextChangedEventArgs e)
        {
            hasTextChanged = true;
        }

        private void selected_state_TextChanged(object sender, TextChangedEventArgs e)
        {
            hasTextChanged = true;
        }

        private void selected_zipcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            hasTextChanged = true;
        }

    }
}
