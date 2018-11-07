using ParentChildInfoSystem.Model;
using System.Collections.ObjectModel;
using Npgsql;
using System.Windows;
using System;
using System.Data;

namespace ParentChildInfoSystem.Data
{
    class DatabaseLayer
    {
        static string server = "192.168.0.4";
        static string id = "pi";
        static string password = "448346";
        static string database = "db_studentguardian";

        static string connStr = "Server=" + server + ";User Id=" + id + ";Password=" + password + ";Database=" + database + ";";

        public static DataTable FillStudentList()
        {
            DataTable dt = new DataTable();
            
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();

                NpgsqlCommand command = new NpgsqlCommand("select tb_student.id,tb_student.name,tb_address.address,tb_address.city,tb_address.state,tb_address.zipcode from tb_student,tb_address where tb_student.address_id = tb_address.id order by tb_student.id asc;", conn);
                
                NpgsqlDataAdapter da = new NpgsqlDataAdapter();

                da.SelectCommand = command;
                
                da.Fill(dt);
                
                conn.Close();
            }
            catch(System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        public static DataTable FillParentList()
        {
            DataTable dt = new DataTable();
            
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();

                NpgsqlCommand command = new NpgsqlCommand("select tb_guardians.id,tb_guardians.name,tb_address.address,tb_address.city,tb_address.state,tb_address.zipcode from tb_guardians,tb_address where tb_guardians.address_id = tb_address.id;", conn);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter();

                da.SelectCommand = command;

                da.Fill(dt);

                conn.Close();


            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        public static DataTable getRespectiveParentFromDatabase(int id)
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                //select tb_guardians.id, tb_guardians.name, tb_guardians.guardian_type from tb_families  left join tb_guardians  on tb_families.guardian_id = tb_guardians.id  where tb_families.student_id = 15;

                NpgsqlCommand command = new NpgsqlCommand("select tb_guardians.id, tb_guardians.name, tb_guardians.guardian_type from tb_families  left join tb_guardians  on tb_families.guardian_id = tb_guardians.id  where tb_families.student_id = " + id + ";", conn);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter();

                da.SelectCommand = command;

                da.Fill(dt);

                conn.Close();
            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        public static DataTable getRespectiveStudentFromDatabase(int id)
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                //select tb_student.* from tb_families left join tb_student on tb_families.student_id = tb_student.id where tb_families.guardian_id = 19;
                NpgsqlCommand command = new NpgsqlCommand("select tb_student.id,tb_student.name  from tb_families left join tb_student on tb_families.student_id = tb_student.id where tb_families.guardian_id = " + id + ";", conn);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter();

                da.SelectCommand = command;

                da.Fill(dt);

                conn.Close();


            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        public static DataTable getStudentData(int id)
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                //select tb_student.* from tb_families left join tb_student on tb_families.student_id = tb_student.id where tb_families.guardian_id = 19;
                NpgsqlCommand command = new NpgsqlCommand("select tb_student.name,tb_address.address,tb_address.city,tb_address.state,tb_address.zipcode from tb_student,tb_address where tb_student.address_id = tb_address.id and tb_student.id = " + id + ";", conn);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter();

                da.SelectCommand = command;

                da.Fill(dt);

                conn.Close();


            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        public static DataTable getParentData(int id)
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                //select tb_student.* from tb_families left join tb_student on tb_families.student_id = tb_student.id where tb_families.guardian_id = 19;
                NpgsqlCommand command = new NpgsqlCommand("select tb_guardians.name,tb_address.address,tb_address.city,tb_address.state,tb_address.zipcode from tb_guardians,tb_address where tb_guardians.address_id = tb_address.id and tb_guardians.id = " + id + ";", conn);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter();

                da.SelectCommand = command;

                da.Fill(dt);

                conn.Close();


            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        public static void updateStudent(int id, string name, string address, string city, string state, string zipcode)
        {
            //update tb_address set state = 'LA' where tb_address.id = (select address_id from tb_student where tb_student.id = 2);
            // update tb_student set name = 'Spader' where tb_student.id = 31;
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                // update parent set first_name = 'hazim' where id = 2
                // NpgsqlCommand command = new NpgsqlCommand("update tb_student set name = :name where id = :id", conn);
                NpgsqlCommand command = new NpgsqlCommand("update tb_student set name = :name where tb_student.id = :id", conn);

                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));

                command.Prepare();

                command.Parameters[0].Value = name;
                command.Parameters[1].Value = id;


                int recordAffected = command.ExecuteNonQuery();

                if (Convert.ToBoolean(recordAffected))
                {
                    MessageBox.Show("Student Data Succesfully updated");
                }

                command = new NpgsqlCommand("update tb_address set address = :address, city = :city, state = :state, zipcode = :zipcode where tb_address.id = (select address_id from tb_student where tb_student.id = :id);", conn);

                command.Parameters.Add(new NpgsqlParameter("address", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("city", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("state", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("zipcode", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));

                command.Prepare();

                command.Parameters[0].Value = address;
                command.Parameters[1].Value = city;
                command.Parameters[2].Value = state;
                command.Parameters[3].Value = zipcode;
                command.Parameters[4].Value = id;


                recordAffected = command.ExecuteNonQuery();

                if (Convert.ToBoolean(recordAffected))
                {
                    MessageBox.Show("Address Data Succesfully updated");
                }


                conn.Close();


            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }
        }

        public static void updateParent(int id, string name, string address, string city, string state, string zipcode)
        {
            //update tb_address set state = 'LA' where tb_address.id = (select address_id from tb_student where tb_student.id = 2);
            // update tb_student set name = 'Spader' where tb_student.id = 31;
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                // update parent set first_name = 'hazim' where id = 2
                NpgsqlCommand command = new NpgsqlCommand("update tb_guardians set name = :name where id = :id", conn);

                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));

                command.Prepare();
                

                int recordAffected = command.ExecuteNonQuery();

                if (Convert.ToBoolean(recordAffected))
                {
                    MessageBox.Show("Parent Data Succesfully updated");
                }

                command = new NpgsqlCommand("update tb_guardians set name = :name where id = :id", conn);

                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));

                command.Prepare();


                recordAffected = command.ExecuteNonQuery();

                if (Convert.ToBoolean(recordAffected))
                {
                    MessageBox.Show("Parent Data Succesfully updated");
                }


                conn.Close();


            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }
        }

        public static void deleteEntry(int id, string type)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                // update parent set first_name = null, last_name = null where id = 1;
                NpgsqlCommand command = new NpgsqlCommand("update " + type + " set name = null where id = " + id, conn);


                command.Prepare();
                

                int recordAffected = command.ExecuteNonQuery();

                if (Convert.ToBoolean(recordAffected))
                {
                    MessageBox.Show("Data Succesfully deleted");
                }


                conn.Close();


            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }
        }

        public static ObservableCollection<Address> GetAddresses()
        {
            ObservableCollection<Address> addresses = new ObservableCollection<Address>();
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                //select tb_student.* from tb_families left join tb_student on tb_families.student_id = tb_student.id where tb_families.guardian_id = 19;
                NpgsqlCommand command = new NpgsqlCommand("select * from tb_address", conn);

                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    //if (dr[1] == DBNull.Value) break;
                    Address address = new Address();
                    address.ID = (int)dr[0];
                    address.Streets = dr[1].ToString();
                    address.City = dr[2].ToString();
                    address.State = dr[3].ToString();
                    address.Zipcode = dr[4].ToString();

                    if (address.Streets != "") addresses.Add(address);

                }

                conn.Close();


            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }

            return addresses;
        }

        public static void createAddress(Address address)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("insert into tb_address(id,address,city,state,zipcode) values(:id,:address,:city,:state,:zipcode)", conn);

                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("address", NpgsqlTypes.NpgsqlDbType.Text));
                command.Parameters.Add(new NpgsqlParameter("city", NpgsqlTypes.NpgsqlDbType.Text));
                command.Parameters.Add(new NpgsqlParameter("state", NpgsqlTypes.NpgsqlDbType.Text));
                command.Parameters.Add(new NpgsqlParameter("zipcode", NpgsqlTypes.NpgsqlDbType.Text));

                command.Prepare();

                command.Parameters[0].Value = address.ID;
                command.Parameters[1].Value = address.Streets;
                command.Parameters[2].Value = address.City;
                command.Parameters[3].Value = address.State;
                command.Parameters[4].Value = address.Zipcode;

                int recordAffected = command.ExecuteNonQuery();
                if (Convert.ToBoolean(recordAffected))
                {
                    MessageBox.Show("Data Add Successfulll la fucker");
                }

                conn.Close();
            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }
        }

        public static void createStudent(Student student)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("insert into tb_student(id,name,face_image_path,address_id) values(:id,:name,'D:\\\\ImagePath\\\\',:address_id)", conn);

                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Text));
                command.Parameters.Add(new NpgsqlParameter("address_id", NpgsqlTypes.NpgsqlDbType.Integer));

                command.Prepare();

                command.Parameters[0].Value = student.ID;
                command.Parameters[1].Value = student.Name;
                command.Parameters[2].Value = student.Address.ID;
                

                int recordAffected = command.ExecuteNonQuery();
                if (Convert.ToBoolean(recordAffected))
                {
                    MessageBox.Show("Data Add Successfulll la fucker");
                }

                conn.Close();
            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }
        }

        public static void createParent(Parent parent)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("insert into tb_guardians(id,name,face_image_path,address_id) values(:id,:name,'D:\\\\ImagePath\\\\',:address_id)", conn);

                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Text));
                command.Parameters.Add(new NpgsqlParameter("address_id", NpgsqlTypes.NpgsqlDbType.Integer));

                command.Prepare();

                command.Parameters[0].Value = parent.ID;
                command.Parameters[1].Value = parent.Name;
                command.Parameters[2].Value = parent.Address.ID;


                int recordAffected = command.ExecuteNonQuery();
                if (Convert.ToBoolean(recordAffected))
                {
                    MessageBox.Show("Data Add Successfulll la fucker");
                }

                conn.Close();
            }
            catch (System.Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }
        }
        public static ObservableCollection<Teacher> GetTeacherFromLocalDatabase()
        {
            return new ObservableCollection<Teacher>
            {
                new Teacher {ID=1, Name="Cikgu"},
                new Teacher {ID=2, Name="Cikgu"},
                new Teacher {ID=3, Name="Cikgu"},
            };
        }
    }
}
