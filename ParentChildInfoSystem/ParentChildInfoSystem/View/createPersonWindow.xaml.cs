using ParentChildInfoSystem.Data;
using ParentChildInfoSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
                udpClient.Connect("192.168.43.102", 8080);
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
                addressID = Convert.ToInt16(textBox_addressid.Text);
                string Street = textBox_address.Text;
                string City = textBox_City.Text;
                string State = textBox_state.Text;
                string Zipcode = textBox_zipcode.Text;

                DatabaseLayer.createAddress(addressID, Street, City, State, Zipcode);
            }
            else if(addressType == "old")
            {
            }

            if(dataType == "Student")
            {
                DatabaseLayer.createStudent(Convert.ToInt16(textboxID.Text), textboxName.Text, addressID);
            }
            else if(dataType == "Parent")
            {
                DatabaseLayer.createParent(Convert.ToInt16(textboxID.Text), textboxName.Text, addressID);
            }
            
            
        }

        private void list_address_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = list_address.SelectedItem as DataRowView;
            if (row == null) return;
            addressID = (int)row.Row.ItemArray[0];
        }
    }
}
