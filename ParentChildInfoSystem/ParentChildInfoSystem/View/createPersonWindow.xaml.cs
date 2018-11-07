using ParentChildInfoSystem.Data;
using ParentChildInfoSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ParentChildInfoSystem.View
{
    /// <summary>
    /// Interaction logic for createPersonWindow.xaml
    /// </summary>
    /// 

    

    public partial class createPersonWindow : Window
    {
        ObservableCollection<Address> addressess;
        string dataType;
        string addressType;
        Address selectedAddress;
        int maxId;
        int addressID;

        public createPersonWindow()
        {
            InitializeComponent();
            addressess = new ObservableCollection<Address>(DatabaseLayer.GetAddresses());
            list_address.ItemsSource = addressess;
        }

        public createPersonWindow(string type, int id)
        {
            InitializeComponent();
            addressess = new ObservableCollection<Address>(DatabaseLayer.GetAddresses());
            list_address.ItemsSource = addressess;
            dataType = type;
            maxId = id;
            groupbox_listaddress.IsEnabled = false;
            groupbox_newaddress.IsEnabled = false;
            if(type == "Student")
            {
                label_form.Content = "Create Student Form";
            }else if(type == "Parent")
            {
                label_form.Content = "Create Parent Form";
            }
        }

        private void button_pickAddress_Click(object sender, RoutedEventArgs e)
        {
            groupbox_listaddress.IsEnabled = true;
            groupbox_newaddress.IsEnabled = false;
            addressType = "old";
        }

        private void button_newAddress_Click(object sender, RoutedEventArgs e)
        {
            groupbox_newaddress.IsEnabled = true;
            groupbox_listaddress.IsEnabled = false;
            addressType = "new";
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UdpClient udpClient = new UdpClient();
                udpClient.Connect("192.168.0.104", 8080);
                Byte[] senddata = Encoding.ASCII.GetBytes("CreateFaceDataset");
                udpClient.Send(senddata, senddata.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (textboxName.Text == "")
            {
                MessageBox.Show("Please fill up name");
                return;
            }

            if(addressType == "new")
            {
                Address address = new Address();
                address.Streets = textBox_address.Text;
                address.City = textBox_City.Text;
                address.State = textBox_state.Text;
                address.Zipcode = textBox_zipcode.Text;
                address.ID = addressess.Max(x => x.ID) + 1;
                addressID = address.ID;
                DatabaseLayer.createAddress(address);
            }
            else if(addressType == "old")
            {
                if(selectedAddress == null)
                {
                    MessageBox.Show("Please select address");
                    return;
                }
                
            }

            if(dataType == "Student")
            {
                Student student = new Student();
                student.Name = textboxName.Text;
                student.ID = maxId;
                student.Address.ID = addressID;
                DatabaseLayer.createStudent(student);
            }
            else if(dataType == "Parent")
            {
                Parent parent = new Parent();
                parent.Name = textboxName.Text;
                parent.ID = maxId;
                parent.Address.ID = addressID;
                DatabaseLayer.createParent(parent);
            }
            
            
        }

        private void list_address_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAddress = list_address.SelectedItem as Address;
            addressID = selectedAddress.ID;
        }
    }
}
