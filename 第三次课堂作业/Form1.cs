using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace FileManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        //记录文件路径
        public string filePath;
        //记录文件内容
        public string fileString;

        int lineNum = 0;//行数
        int wordsNum = 0;//单词数

        string[] words;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择文件";
            dialog.InitialDirectory = "E:\\C#work\\.NET架构设计仓库\\第二次课堂作业\\BankSystem";
            dialog.Filter = "C#文件|*.cs|所有文件|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = dialog.FileName;
                label5.Text = Path.GetFileName(dialog.FileName);
            }
        }
        //简单统计
        private void button2_Click(object sender, EventArgs e)
        {
            if (filePath != null)
            {
                StreamReader streamReader = new StreamReader(filePath);
                string lineString = "";
                //计算原始行数
                while ((lineString = streamReader.ReadLine()) != null)
                {
                    if (lineString.Replace(" ","") != "")
                    {
                        lineNum++;
                        //计算单词数
                        //只考虑英文单词，所以数字和汉字都会被替换
                        lineString = Regex.Replace(lineString, @"[^a-zA-Z\s]", " ");
                        words = lineString.Split(' ');
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i] != "")
                            {
                                wordsNum++;
                            }
                        }
                    }
                    
                }
                streamReader.Close();
                label3.Text = lineNum.ToString();
                label4.Text = wordsNum.ToString();
                //重置记录，防止重复点击
                lineNum = 0;
                wordsNum = 0;
            }
            else 
            {
                MessageBox.Show("请先选择文件！");
            }
        }
        //删除注释
        private void button3_Click(object sender, EventArgs e)
        {
            if (filePath != null)
            {
                StreamReader streamReader = new StreamReader(filePath);
                string lineString = "";
                string[] lineStringArr = new string[1000]; 
                //获取原始行数组
                while ((lineString = streamReader.ReadLine()) != null)
                {
                    lineString= Regex.Replace(lineString, @"\s*//.*", "");
                    if (lineString != "")
                    {
                        lineStringArr[lineNum]=lineString;
                        lineNum++;
                    }
                    //计算单词数
                    lineString = Regex.Replace(lineString, @"[^a-zA-Z\s]", " ");
                    words = lineString.Split(' ');
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i] != "")
                        {
                            wordsNum++;
                        }
                    }
                }
                streamReader.Close();

                //写回文件
                StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8); 
                for (int i = 0; i < lineStringArr.Length; i++)
                {
                    if (lineStringArr[i] != "")
                    {
                        sw.WriteLine(lineStringArr[i]);
                    }
                    sw.Flush();  // 清空缓存
                }
                sw.Close();  // 关闭文件
               
                //由于while循环退出时多加了一次，这里要减去
                label3.Text = (lineNum-1).ToString();
                label4.Text = wordsNum.ToString();
                //重置记录，防止重复点击
                lineNum = 0;
                wordsNum = 0;

            }
            else 
            {
                MessageBox.Show("请先选择文件！");
            }
            
        }

        //文件分析
        private void button4_Click(object sender, EventArgs e)
        {
            if (filePath != null)
            {
                //存储单词及其出现次数
                Dictionary<string, int> wordList = new Dictionary<string, int>();
                StreamReader streamReader = new StreamReader(filePath);
                string lineString = "";
                //统计各单词数量
                while ((lineString = streamReader.ReadLine()) != null)
                {
                    if (lineString.Replace(" ", "") != "")
                    {
                        lineString = Regex.Replace(lineString, @"[^a-zA-Z\s]", " ");
                        words = lineString.Split(' ');
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i] != "" && !wordList.ContainsKey(words[i]))
                            {
                                wordList.Add(words[i], 1);
                            }
                            else if(words[i] != "" && wordList.ContainsKey(words[i]))
                            {
                                wordList[words[i]]++;
                            }
                        }
                    }
                }
                streamReader.Close();

                //显示单词列表
                string[][] wordStringList = new string[wordList.Count][];
                int index = 0;
                foreach (var item in wordList)
                {
                    wordStringList[index] = new string[] { item.Key, item.Value.ToString() };
                    index++;
                }
                for (int i = 0; i < wordList.Count; i++)
                {
                    ListViewItem item = new ListViewItem(wordStringList[i]);
                    listView1.Items.Add(item);
                }
                this.listView1.EndUpdate();
                //重置记录，防止重复点击
                lineNum = 0;
                wordsNum = 0;
            }
            else
            {
                MessageBox.Show("请先选择文件！");
            }
        }
    }
}
