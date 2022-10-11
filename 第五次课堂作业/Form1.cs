using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;


namespace WebCrawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            urlcount = 0;
            ThreadNum = 0;
        }

        //用哈希表存储<email,url>和<urlcount,url>，去除重复值
        static Hashtable email_url = Hashtable.Synchronized(new Hashtable());
        static Hashtable searched_url = Hashtable.Synchronized(new Hashtable());
        //待查找的url队列
        static Queue urlqueue = new Queue();
        //用于记录查找过的url个数
        static int urlcount;
        //当前创建的线程数
        static int ThreadNum;   


        private void btsearch_Click(object sender, EventArgs e)
        {
            //每次点击查找会清空上次查找的数据
            lVEmailUrl.Items.Clear();
            lVUrl.Items.Clear();
            if(tBsearchtxt.Text=="")
            {
                MessageBox.Show("请输入关键词！");
            }
            else 
            {
                try
                {

                    //从搜索框获取搜索网址
                    string baidustr = "https://www.baidu.com/s?wd=" + HttpUtility.UrlEncode(tBsearchtxt.Text) + "&rsv_spt=1&rsv_iqid=0x88b43a5c00074f90&issp=1&f=8&rsv_bp=1&rsv_idx=2&ie=utf-8&tn=baiduhome_pg&rsv_enter=1&rsv_sug3=10&rsv_sug1=2&rsv_sug7=100";
                    string bingstr = "https://cn.bing.com/search?q=" + HttpUtility.UrlEncode(tBsearchtxt.Text);
                    if (checkBox1.Checked)
                        urlqueue.Enqueue(baidustr);
                    if(checkBox2.Checked)
                        urlqueue.Enqueue(bingstr);

                    //先从搜索结果页面获取下级网址，并添加到url队列
                    GetHttpLinks(baidustr);
                    GetHttpLinks(bingstr);
                    
                    //循环查找，如果查到的邮箱数未达到预定值，
                    //就从待查找队列中取出一个url进行查找，并将其下一级url添加到待查找队列中
                    while(email_url.Count<Constants.MaxEmailNum)
                    {
                        string url = urlqueue.Dequeue().ToString();
                        GetHttpLinks(url);

                        //当线程数未达到设置的最大值时可以创建新线程
                        if (ThreadNum < Constants.MaxThreadNum)
                        {
                            ThreadNum++;
                            new Thread(new ThreadStart(delegate ()
                            {
                                try
                                {
                                    //获取email
                                    //由于每条url中可能不止一条email，因此返回值类型为ArrayList
                                    ArrayList itemarr = new ArrayList();
                                    itemarr = GetEmails(url);
                                    //将返回值逐条加载到窗体
                                    for (int i = 0; i < itemarr.Count; i++)
                                    {
                                        AddEmail_urlItem((ListViewItem)itemarr[i]);
                                    }
                                    //获取已查找url
                                    if (!searched_url.Contains(url))
                                    {
                                        ListViewItem SUlvi = new ListViewItem(url);
                                        AddSearched_urlItem(SUlvi);
                                    }
                                    //线程执行完后将当前线程数减一
                                    ThreadNum--;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }

                            })).Start();


                        }
                    }

                }
                catch (Exception ex)
                {
                    lVEmailUrl.Clear();
                    lVUrl.Clear();
                    tBsearchtxt.Text = "";
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //创建委托，用于异步加载窗口数据
        private delegate void AddListView(ListViewItem item);
        private void AddEmail_urlItem(ListViewItem item)
        {
            if (lVEmailUrl.InvokeRequired)
            {
                AddListView lst = new AddListView(AddEmail_urlItem);
                this.Invoke(lst, item);
                return;
            }
            lVEmailUrl.Items.Add(item);
        }
        private void AddSearched_urlItem(ListViewItem item)
        {
            if (lVUrl.InvokeRequired)
            {
                AddListView lst = new AddListView(AddSearched_urlItem);
                this.Invoke(lst, item);
                return;
            }
            lVUrl.Items.Add(item);
        }


        //获取url的响应文本
        public static string HtmlCodeRequest(string Url)
        {
            if (string.IsNullOrEmpty(Url))
            {
                return "";
            }
            try
            {
                //创建一个请求
                HttpWebRequest httprequst = (HttpWebRequest)WebRequest.Create(Url);
                //不建立持久性链接
                httprequst.KeepAlive = true;
                //设置请求的方法
                httprequst.Method = "GET";
                //设置标头值
                httprequst.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 ";
                httprequst.Accept = "*/*";
                httprequst.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                httprequst.ServicePoint.Expect100Continue = false;
                httprequst.Timeout = 5000;
                httprequst.AllowAutoRedirect = true;
                ServicePointManager.DefaultConnectionLimit = 30;
                //获取响应
                HttpWebResponse webRes = (HttpWebResponse)httprequst.GetResponse();
                //获取响应的文本流
                string content = string.Empty;
                using (System.IO.Stream stream = webRes.GetResponseStream())
                {
                    using (System.IO.StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                //取消请求
                httprequst.Abort();
                //返回数据内容
                return content;
            }
            catch (Exception)
            {
                return "";
            }
        }

        

        //从url中检索email，并返回存有email的ArrayList
        public static ArrayList GetEmails(string url)
        {
            ArrayList itemarr = new ArrayList();
            //获取响应文本
            string html = HtmlCodeRequest(url);
            //保证网址有效，不为空且不重复
            if (string.IsNullOrEmpty(html)||searched_url.ContainsValue(url))
            {
                return itemarr;
            }

            // 定义正则表达式用来匹配email
            Regex regEmail = new Regex(@"[a-zA-Z0-9]+([-+.'][a-zA-Z0-9]+)*@\w+([-.]\w+)*\.[a-zA-Z0-9]+([-.][a-zA-Z0-9]+)*", RegexOptions.IgnoreCase);            
            // 搜索匹配的字符串  
            MatchCollection matches = regEmail.Matches(html);

            //添加url到哈希表
            if(!searched_url.ContainsValue(url))
            {
                lock (searched_url.SyncRoot)
                {
                    searched_url.Add(urlcount, url);
                    urlcount++;
                }
            }

            //添加email到哈希表  
            foreach (Match match in matches)
            {
                if(!email_url.ContainsKey(match.Value)&&!match.Value.Contains(".png"))
                {
                    lock(email_url.SyncRoot)
                    {
                        email_url.Add(match.Value, url);
                    }
                    string[] itemstr = { match.Value, url };
                    ListViewItem item = new ListViewItem(itemstr);
                    itemarr.Add(item);
                }
                
            }

            return itemarr;
        }
        

        //获取url的下级url，并添加到待查找队列
        public static void GetHttpLinks(string url)
        {
            //获取网址内容
            string html = HtmlCodeRequest(url);
            Regex r1 = new Regex(@"", RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(html))
            {
                return;
            }
            //匹配http链接
            Regex r2 = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            //获得匹配结果
            MatchCollection m2 = r2.Matches(html);
            foreach (Match url2 in m2)
            {
                if (urlqueue.Count < Constants.MaxQueueNum && !searched_url.ContainsValue(url2.ToString()))
                    urlqueue.Enqueue(url2.ToString());
            }
            //匹配href里面的链接
            Regex r3 = new Regex(@"href=.[https?]+://[^\s;<>]*", RegexOptions.IgnoreCase);
            //获得匹配结果
            MatchCollection m = r3.Matches(html);

            
            foreach (Match url1 in m)
            {
                //匹配时为了筛选保留了href，这里要删除掉得到url字符串
                string href1 = url1.Value;
                href1 = href1.Replace("href=", "").Replace("\"", "");
                //添加到队列
                if (urlqueue.Count < Constants.MaxQueueNum&&!searched_url.ContainsValue(href1))
                    urlqueue.Enqueue(href1);
            }
            return;
        }
    }
    //定义数据常量
    static class Constants
    {
        public const int MaxQueueNum = 2000;
        public const int MaxEmailNum = 200;
        public const int MaxThreadNum = 30;
    }
}
