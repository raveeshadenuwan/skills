using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp02
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True");

        public Form2()
        {
            InitializeComponent();
        }

        // ---------- LOAD RegNo INTO COMBOBOX ----------
        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            try
            {
                con.Open();
                string query = "SELECT regNo FROM Registration";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["regNo"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }

        // ---------- REGISTER ----------
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string gender = radioButton1.Checked ? "Male" :
                                radioButton2.Checked ? "Female" : "";

                string query = "INSERT INTO Registration VALUES (@regNo, @firstName, @lastName, @dob, @gender, @address, @email, @mobile, @home, @parent, @nic, @contact)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@regNo", comboBox1.Text);
                cmd.Parameters.AddWithValue("@firstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@lastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@address", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);
                cmd.Parameters.AddWithValue("@mobile", textBox5.Text);
                cmd.Parameters.AddWithValue("@home", textBox6.Text);
                cmd.Parameters.AddWithValue("@parent", textBox7.Text);
                cmd.Parameters.AddWithValue("@nic", textBox8.Text);
                cmd.Parameters.AddWithValue("@contact", textBox9.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }

        // ---------- UPDATE ----------
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string gender = radioButton1.Checked ? "Male" :
                                radioButton2.Checked ? "Female" : "";

                string query = "UPDATE Registration SET firstName=@firstName, lastName=@lastName, dateOfBirth=@dob, gender=@gender, address=@address, email=@email, mobilePhone=@mobile, homePhone=@home, parentName=@parent, nic=@nic, contactNo=@contact WHERE regNo=@regNo";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@regNo", comboBox1.Text);
                cmd.Parameters.AddWithValue("@firstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@lastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@address", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);
                cmd.Parameters.AddWithValue("@mobile", textBox5.Text);
                cmd.Parameters.AddWithValue("@home", textBox6.Text);
                cmd.Parameters.AddWithValue("@parent", textBox7.Text);
                cmd.Parameters.AddWithValue("@nic", textBox8.Text);
                cmd.Parameters.AddWithValue("@contact", textBox9.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }

        // ---------- DELETE ----------
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM Registration WHERE regNo=@regNo";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@regNo", comboBox1.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }
            }
        }

        // ---------- CLEAR ----------
        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
        }

        // ---------- AUTO FILL WHEN RegNo SELECTED ----------
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM Registration WHERE regNo=@regNo";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@regNo", comboBox1.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    textBox1.Text = dr["firstName"].ToString();
                    textBox2.Text = dr["lastName"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["dateOfBirth"]);

                    radioButton1.Checked = dr["gender"].ToString() == "Male";
                    radioButton2.Checked = dr["gender"].ToString() == "Female";

                    textBox3.Text = dr["address"].ToString();
                    textBox4.Text = dr["email"].ToString();
                    textBox5.Text = dr["mobilePhone"].ToString();
                    textBox6.Text = dr["homePhone"].ToString();
                    textBox7.Text = dr["parentName"].ToString();
                    textBox8.Text = dr["nic"].ToString();
                    textBox9.Text = dr["contactNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }

        // ---------- LOGOUT ----------
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        // ---------- EXIT ----------
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?",
                                                  "Exit",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}