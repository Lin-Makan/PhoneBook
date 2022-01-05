using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        Database databaseobject = new Database();
        string id = "";
        public Form1()
        {
            InitializeComponent();
            showAll();
            clear();
           
        }

        // Add New Info
        private void addClick(object sender, EventArgs e)
        {
            string fullname = textBox1.Text;
            string phone = textBox2.Text;
            string email = textBox3.Text;
            string address = textBox4.Text;
            string dob = dateTimePicker1.Text;
           

            if(fullname != "" && phone != "")
            {
               try 
               {
                    //Query
                    string query = "INSERT INTO info ('name', 'phone', 'email', 'address', 'dob') VALUES (' " + fullname + " ', ' " + phone + " ', ' " + email + " ', ' " + address + " ', ' " + dob + "')";

                    //Command
                    SQLiteCommand cmd = new SQLiteCommand(query, databaseobject.myConnection);

                    //Connection Open
                    databaseobject.openDB();

                    //Command Execution
                    int check = cmd.ExecuteNonQuery();
                    if (check > 0)
                    {
                        MessageBox.Show("Insert Successful");
                        showAll();
                    }

                    //Connection Close
                    databaseobject.closeDB();


               }
                catch(Exception ex)
               {
                    MessageBox.Show(ex.Message);
                    
               }

                

               


            }

            else
            {
                MessageBox.Show("Name and Phone Number should not be empty");
            }
            
           
           

        }

        //Field Clear
        public void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            id = "";
            

        }

        // Show All data
        public void showAll()
        {


            try
            {
                //query
                string query = "SELECT * FROM info";
                // Command
                SQLiteCommand cmd = new SQLiteCommand(query, databaseobject.myConnection);
                // Open Connection
                databaseobject.openDB();

                SQLiteDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    dataGridView1.Rows.Clear();

                    while (result.Read())
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = result[0].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = result[1].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = result[2].ToString();
                        dataGridView1.Rows[n].Cells[3].Value = result[3].ToString();
                        dataGridView1.Rows[n].Cells[4].Value = result[4].ToString();
                        dataGridView1.Rows[n].Cells[5].Value = result[5].ToString();
                    }
                }

                //Close Connection
                databaseobject.closeDB();



            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        
        // Search 
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //query
                string query = "SELECT * FROM info where name like '%" + textBox5.Text + "%'";
                // Command
                SQLiteCommand cmd = new SQLiteCommand(query, databaseobject.myConnection);
                // Open Connection
                databaseobject.openDB();

                SQLiteDataReader result = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();

                if (result.HasRows)
                {
                    

                    while (result.Read())
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = result[0].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = result[1].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = result[2].ToString();
                        dataGridView1.Rows[n].Cells[3].Value = result[3].ToString();
                        dataGridView1.Rows[n].Cells[4].Value = result[4].ToString();
                        dataGridView1.Rows[n].Cells[5].Value = result[5].ToString();
                    }
                }

                //Close Connection
                databaseobject.closeDB();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showAll();

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

            MessageBox.Show(id);

        }

        // Delete
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (id != "")
                {
                    DialogResult d = MessageBox.Show("Do you want to Delete all data for ID: " + id + "?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    
                    if (d == DialogResult.Yes)
                    {
                        try
                        {
                            //Query
                            string query = "DELETE FROM info WHERE id = ' "+id+" ' ";

                            //Command
                            SQLiteCommand cmd = new SQLiteCommand(query, databaseobject.myConnection);

                            //Connection Open
                            databaseobject.openDB();

                            //Command Execution
                            int check = cmd.ExecuteNonQuery();
                            if (check > 0)
                            {
                                MessageBox.Show("Delete Successful");
                                clear();
                                showAll();
                            }

                            //Connection Close
                            databaseobject.closeDB();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }


                    }
                }
                else
                {
                    MessageBox.Show("First you have to select from Datagrid");
                }

                
            } catch (Exception ex) { MessageBox.Show(ex.Message);}
        }
        // Update
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (id != "")
                {
                    DialogResult d = MessageBox.Show("Do you want to Update all data for ID: " + id + "?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (d == DialogResult.Yes)
                    {
                        try
                        {
                            //Query
                            string query = "UPDATE info SET name = ' "+textBox1.Text+" ', phone = ' "+textBox2.Text+" ', email = ' "+textBox3.Text+" ', address = ' "+textBox4.Text+" ', dob = ' "+dateTimePicker1.Text+" ' WHERE id = ' "+id+" ' ";

                            //Command
                            SQLiteCommand cmd = new SQLiteCommand(query, databaseobject.myConnection);

                            //Connection Open
                            databaseobject.openDB();

                            //Command Execution
                            int check = cmd.ExecuteNonQuery();
                            if (check > 0)
                            {
                                MessageBox.Show("Update Successful");
                                clear();
                                showAll();
                            }

                            //Connection Close
                            databaseobject.closeDB();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }


                    }
                }
                else
                {
                    MessageBox.Show("First you have to select from Datagrid");
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
