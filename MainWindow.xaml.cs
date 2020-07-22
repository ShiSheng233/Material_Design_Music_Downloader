using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Music_Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            string music_id, filepath;
            music_id = id.Text;

            string serverFilePath = "http://music.163.com/song/media/outer/url?id=";
            serverFilePath += music_id;
            filepath = "\\" + music_id + ".mp3";
            DownloadFile(serverFilePath, filepath);
        }
        public void DownloadFile(string serverFilePath, string targetPath)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(serverFilePath);
            System.Net.WebResponse respone = request.GetResponse();
            System.IO.Stream netStream = respone.GetResponseStream();
            using (System.IO.Stream fileStream = new System.IO.FileStream(targetPath, System.IO.FileMode.Create))
            {
                byte[] read = new byte[1024];
                int realReadLen = netStream.Read(read, 0, read.Length);
                while (realReadLen > 0)
                {
                    fileStream.Write(read, 0, realReadLen);
                    realReadLen = netStream.Read(read, 0, read.Length);
                }
                netStream.Close();
                fileStream.Close();
            }
        }

    }
}
