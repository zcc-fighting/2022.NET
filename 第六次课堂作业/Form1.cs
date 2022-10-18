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
using System.IO;

namespace SchoolSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            SQLiteHelper.CreateDB("E:\\dataTable.db");
            SQLiteHelper.SetConnectionString("E:\\dataTable.db");
            SQLiteHelper.InitDB();
        }

        //定义全局变量 单选按钮的文本
        private string rbtStr = "";

        //查询
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtStr == "") 
                {
                    MessageBox.Show("请选择查找项！");
                }
                else
                {
                    string sql = "SELECT* FROM[" + rbtStr + "]";
                    DataTable dt = SQLiteHelper.ExecuteQuery(sql);
                    dataGridView1.DataSource = dt;
                }

                string str = System.DateTime.Now.ToString() + "  查找"+rbtStr;
                SQLiteHelper.writeLog(str);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //修改
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    string sql = "REPLACE INTO table1(ID, Name, clNum, headMaster) values (@ID, @Name, @clNum, @headMaster)";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                         new SQLiteParameter("@Name", textBox2.Text),
                         new SQLiteParameter("@clNum", textBox3.Text),
                         new SQLiteParameter("@headMaster", textBox4.Text)
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else if (radioButton2.Checked)
                {
                    string sql = "REPLACE INTO table2(ID, Name, belong, headTeacher) values (@ID, @Name, @belong, @headTeacher)";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                         new SQLiteParameter("@Name", textBox2.Text),
                         new SQLiteParameter("@belong", textBox3.Text),
                         new SQLiteParameter("@headTeacher", textBox4.Text)
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else if (radioButton3.Checked)
                {
                    string sql = "REPLACE INTO table3(ID, Name, belong, Tel) values (@ID, @Name, @Age, @Tel)";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                         new SQLiteParameter("@Name", textBox2.Text),
                         new SQLiteParameter("@belong", textBox3.Text),
                         new SQLiteParameter("@Tel", textBox4.Text)
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else
                    MessageBox.Show("请选择查询对象！");

                string str = System.DateTime.Now.ToString() + "  " + rbtStr+"已修改";
                SQLiteHelper.writeLog(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //增加
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    string sql = "INSERT INTO table1(ID, Name, clNum, headMaster) values (@ID, @Name, @clNum, @headMaster)";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                         new SQLiteParameter("@Name", textBox2.Text),
                         new SQLiteParameter("@clNum", textBox3.Text),
                         new SQLiteParameter("@headMaster", textBox4.Text)
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else if (radioButton2.Checked)
                {
                    string sql = "INSERT INTO table2(ID, Name, belong, headTeacher) values (@ID, @Name, @belong, @headTeacher)";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                         new SQLiteParameter("@Name", textBox2.Text),
                         new SQLiteParameter("@belong", textBox3.Text),
                         new SQLiteParameter("@headTeacher", textBox4.Text)
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else if (radioButton3.Checked)
                {
                    string sql = "INSERT INTO table3(ID, Name, belong, Tel) values (@ID, @Name, @Age, @Tel)";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                         new SQLiteParameter("@Name", textBox2.Text),
                         new SQLiteParameter("@belong", textBox3.Text),
                         new SQLiteParameter("@Tel", textBox4.Text)
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else
                    MessageBox.Show("请选择查询对象！");
                string str = System.DateTime.Now.ToString() + "  " + rbtStr+"插入数据";
                SQLiteHelper.writeLog(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //删除
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    string sql = "DELETE FROM table1 WHERE ID = @ID";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else if (radioButton2.Checked)
                {
                    string sql = "DELETE FROM table2 WHERE ID = @ID";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else if (radioButton3.Checked)
                {
                    string sql = "DELETE FROM table3 WHERE ID = @ID";
                    SQLiteParameter[] parameters =
                    {
                         new SQLiteParameter("@ID", textBox1.Text),
                    };
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                    SQLiteHelper.ExecuteNonQuery(sql, parameters);
                }
                else
                    MessageBox.Show("请选择查询对象！");

                string str = System.DateTime.Now.ToString() + "  " + rbtStr + "删除数据";
                SQLiteHelper.writeLog(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //查看log表
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = @"E:\\LogFile.txt";
                string LogTXT="";
                string[] strArray = File.ReadAllLines(path);
                for (int i = 0; i < strArray.Length; i++)
                {
                    LogTXT += strArray[i] + "\r\n";
                }
                MessageBox.Show(LogTXT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //重置数据库
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteHelper.dropTable();
                SQLiteHelper.InitDB();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //导入默认数据
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteHelper.inputData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //还原事件源
            RadioButton rbt = sender as RadioButton;
            //判断是否被选中
            if (rbt.Checked == true)
            {
                rbtStr = rbt.Tag.ToString();
            }
            label4.Text = "clNum";
            label5.Text = "headMaster";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //还原事件源
            RadioButton rbt = sender as RadioButton;
            //判断是否被选中
            if (rbt.Checked == true)
            {
                rbtStr = rbt.Tag.ToString();
            }
            label4.Text = "belong";
            label5.Text = "headTeacher";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //还原事件源
            RadioButton rbt = sender as RadioButton;
            //判断是否被选中
            if (rbt.Checked == true)
            {
                rbtStr = rbt.Tag.ToString();
            }
            label4.Text = "belong";
            label5.Text = "Tel";
        }
    }
}
