using ParentChildInfoSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    /// Interaction logic for newRelation.xaml
    /// </summary>
    public partial class newRelation : Window
    {
        int studentId = 0;
        int parentId = 0;

        public newRelation()
        {
            InitializeComponent();
            var dt = new DataTable();
            studentList.ItemsSource = DatabaseLayer.FillStudentList().DefaultView;
            parentList.ItemsSource = DatabaseLayer.FillParentList().DefaultView;
        }



        private void parentlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = parentList.SelectedItem as DataRowView;
            if (row == null) return;
            parentId = (int)row.Row.ItemArray[0];
        }

        private void studentlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = studentList.SelectedItem as DataRowView;
            if (row == null) return;
            studentId = (int)row.Row.ItemArray[0];
        }

        private void button_createRelation(object sender, RoutedEventArgs e)
        {
            if(studentId == 0 || parentId == 0)
            {
                MessageBox.Show("Please select parent or student");
            }
            DatabaseLayer.createRelation(studentId, parentId);
        }
    }
}
