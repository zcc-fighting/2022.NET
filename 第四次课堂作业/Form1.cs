using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filebrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_directory();
        }

        //加载一、二级目录
        private void load_directory()
        {
            //根节点
            TreeNode rootNode = new TreeNode("此电脑");
            rootNode.Name = "此电脑";
            treeView1.Nodes.Add(rootNode);
            //二级节点
            add_panNode(rootNode);
        }

        private void add_panNode(TreeNode rootNode)
        {
            //循环遍历获取电脑的所有磁盘符
            foreach (string drive in Environment.GetLogicalDrives())
            {
                //实例化DriveInfo对象
                var dir = new DriveInfo(drive);
                switch (dir.DriveType)           //判断驱动器类型
                {
                    case DriveType.Fixed:        //仅取固定磁盘盘符
                        {
                            //Split仅获取盘符字母
                            TreeNode tNode = new TreeNode(dir.Name.Split(':')[0]);//节点视图的显示文本
                            tNode.Name = dir.Name;//节点名称
                            rootNode.Nodes.Add(tNode);//添加到根节点
                            tNode.Nodes.Add("");//添加空节点以区分不同文件夹  
                        }
                        break;
                }
            }
        }

        //遍历父文件夹下面的所有文件夹和文件并添加到节点下
        private void add_childNode(TreeNode parentNode)
        {
            DirectoryInfo directory = new DirectoryInfo(parentNode.Name);

            if (parentNode.Name != "此电脑")//如果是打开"此电脑"节点就啥也不做
            {
                parentNode.Nodes.RemoveAt(0);//把空节点去掉
                try//忽略无法访问的文件
                {
                    foreach (FileSystemInfo fsinfo in directory.GetFileSystemInfos())//获取当前文件夹中的所有文件夹与文件
                    {
                        //如果是文件就直接创建子节点加入父节点即可
                        if (fsinfo is FileInfo)
                        {
                            TreeNode childNode = new TreeNode(fsinfo.Name);
                            parentNode.Nodes.Add(childNode);
                            childNode.Tag = "file";
                        }
                        //如果是文件夹除了创建子节点加入父节点,还要遍历其内容
                        else if (fsinfo is DirectoryInfo)
                        {
                            TreeNode childNode = new TreeNode(fsinfo.Name);
                            childNode.Name = fsinfo.FullName;
                            parentNode.Nodes.Add(childNode);
                            childNode.Nodes.Add("");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            add_childNode(e.Node);
        }


        string currentFilePath;
        //读取文件并进行相应操作
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try 
            {
                if (e.Node.Tag.ToString() =="file")
                {
                    //从结点读取的路径开头为"此电脑\\E"等，需替换为"E:"才能被识别为盘符
                    string filePath = e.Node.FullPath.Replace("此电脑\\C","C:").Replace("此电脑\\D", "D:").Replace("此电脑\\E", "E:");
                    currentFilePath= filePath;//记录最近打开的文件
                    string filename = System.IO.Path.GetFileName(e.Node.FullPath);//文件名  
                    string extension = System.IO.Path.GetExtension(e.Node.FullPath);//扩展名 
                    //根据扩展名对不同类型的文件执行相应的操作
                    if(extension==".txt")
                    {
                        richTextBox1.Clear();
                        label1.Text = filename;
                        string[] lines = File.ReadAllLines(filePath);
                        foreach (string line in lines)
                        {
                            richTextBox1.AppendText(line + Environment.NewLine);
                        }
                    }
                    else if(extension==".exe")
                    {
                        Process process = new Process();
                        process.StartInfo.FileName = filePath;
                        process.Start();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        //清空文本框内容
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "filename";
            richTextBox1.Clear();
        }

        private void 关闭程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string currentText;
        private void 复制文本内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentText = richTextBox1.Text;
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += currentText;
            WriteForTxt(currentFilePath, currentText);
        }
        //向文件中写入字符串
        private void WriteForTxt(string path, string currentText)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter wr = null;
            wr = new StreamWriter(fs);
            wr.WriteLine(currentText);
            wr.Close();
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(currentFilePath);
            file.Delete();
        }

        private void 更新数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();   //先隐藏主窗体
            Form1 form1 = new Form1(); //重新实例化此窗体
            form1.ShowDialog();//已模式窗体的方法重新打开
            this.Close();//原窗体关闭
        }
    }

}
