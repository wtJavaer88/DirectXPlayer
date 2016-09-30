using System;
using System.Drawing;

using System.Collections;

using System.ComponentModel;

using System.Windows.Forms;

using System.Data;

using Microsoft.DirectX.AudioVideoPlayback;

namespace VideoDirectXPlayer
{
    public partial class Form1 : Form
    {
        private Video MyVideo = null;
        public Form1()
        {
            InitializeComponent();
        }

        double video_duration = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (MyVideo == null)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }

            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }

        }

        private void button1_Click(object sender, System.EventArgs e)
        {

            openFileDialog1.InitialDirectory = "E:\\BaiduYunDownload\\监狱兔";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 记录panel组件的大小
                int height = panel1.Height;
                int width = panel1.Width;
                // 如果存在打开的Video文件，释放它
                if (MyVideo != null)
                {
                    MyVideo.Dispose();
                }

                // 打开一个新的Video文件
                MyVideo = new Video(openFileDialog1.FileName);
                // 把Video文件分配给创建的Panel组件
                MyVideo.Owner = panel1;
                // 以记录的panel组件的大小来重新定义
                panel1.Width = width;
                panel1.Height = height;
                
                // 播放AVI文件的第一帧，主要是为了在panel中显示
                MyVideo.Play();
                MyVideo.Pause();
            }

            //确定窗体中的各按钮状态

            if (MyVideo == null)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }

            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }

        }
       
        private void button2_Click(object sender, System.EventArgs e)
        {
            if (MyVideo != null)
            {
                MyVideo.Play();
                video_duration = MyVideo.Duration;
                MyVideo.CurrentPosition = 49;
                Console.WriteLine("video_duration:::" + video_duration);
                this.textBox1.Text = getProcessStr();
            }
        }
        private void button3_Click(object sender, System.EventArgs e)
        {
            if (MyVideo != null)
            {
                MyVideo.Pause();
            }
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            if (MyVideo != null)
            {
                //MyVideo.Stop();
                Console.WriteLine(MyVideo.SeekingCaps.CanGetCurrentPosition);
                Console.WriteLine(MyVideo.SeekingCaps.CanGetDuration);
                Console.WriteLine(MyVideo.SeekingCaps.CanGetStopPosition);
                Console.WriteLine(MyVideo.SeekingCaps.CanSeekAbsolute); 
                Console.WriteLine(MyVideo.SeekingCaps.CanSeekBackwards); 
                Console.WriteLine(MyVideo.SeekingCaps.CanSeekForwards);
                Console.WriteLine(MyVideo.CurrentPosition + " " + MyVideo.Duration);
                //MyVideo.SeekCurrentPosition
               // double a =
               // Console.WriteLine(a);
               // MyVideo.

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                // 记录panel组件的大小

                int height = panel1.Height;

                int width = panel1.Width;

                // 如果存在打开的Video文件，释放它

                if (MyVideo != null)
                {

                    MyVideo.Dispose();

                }

                // 打开一个新的Video文件

                MyVideo = new Video(openFileDialog1.FileName);

                // 把Video文件分配给创建的Panel组件

                MyVideo.Owner = panel1;

                // 以记录的panel组件的大小来重新定义

                panel1.Width = width;
                panel1.Height = height;

                // 播放AVI文件的第一帧，主要是为了在panel中显示
                MyVideo.Play();
                MyVideo.Pause();
            }
            //确定窗体中的各按钮状态
            if (MyVideo == null)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }

            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            double c = MyVideo.CurrentPosition;
            if (c > 1)
            {
                MyVideo.CurrentPosition = c - 1;
            }
            this.textBox1.Text = getProcessStr();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            double c = MyVideo.CurrentPosition;
            Console.WriteLine(c);
            if (c+1 < video_duration)
            {
                MyVideo.CurrentPosition = c + 1;
            }
            this.textBox1.Text = getProcessStr();
        }

        private string getProcessStr()
        {
            if(MyVideo != null){
                return MyVideo.CurrentPosition+" / "+video_duration;
            }
            return "";
        }
    }
}
