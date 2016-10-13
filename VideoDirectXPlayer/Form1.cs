using System;
using System.Drawing;

using System.Collections;

using System.ComponentModel;

using System.Windows.Forms;

using System.Data;

using Microsoft.DirectX.AudioVideoPlayback;
using SaleSupport.database;
using System.Collections.Generic;
using VideoDirectXPlayer.dao;
using VideoDirectXPlayer.bean;
using VideoDirectXPlayer.srt;

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
        string curSrtFile = "Friends.S01E01";
        System.Timers.Timer timer; //实例化Timer类，设置间隔时间为10000毫秒；   
    
    
    public void theout(object source, System.Timers.ElapsedEventArgs e)   
     {
         Console.WriteLine("Timer");
         if (DataHolder.getAllSrtInfos() != null)
         {
             double pos = MyVideo.CurrentPosition*1000;
             Console.WriteLine("Have data:"+pos);
             Boolean insrtFlag = false;
             SrtInfo newSrt = new SrtInfo();
             foreach(SrtInfo srt in DataHolder.getAllSrtInfos()){
                 if (pos >= TimeHelper.getTime(srt.getFromTime()) && pos < TimeHelper.getTime(srt.getToTime()))
                 {
                     Console.WriteLine("find a new srt."+srt);
                     //setSrtContent(srt);
                     insrtFlag = true;
                     newSrt = srt;
                     break;
                 }
             }
             if(insrtFlag){
             this.Invoke((MethodInvoker)(() => setSrtContent(newSrt)));
             }
             else{
                 this.Invoke((MethodInvoker)(() => hideSrtContent()));
             }
         }
     }  
        private void Form1_Load(object sender, EventArgs e)
        {
           
            if (MyVideo == null)
            {
                bt_play.Enabled = false;
                bt_pre.Enabled = false;
                bt_next.Enabled = false;
            }

            else
            {
                bt_play.Enabled = true;
                bt_pre.Enabled = true;
                bt_next.Enabled = true;
            }
            DataHolder.switchFile(curSrtFile);
            DataHolder.product(curSrtFile, SrtInfoDao.getSrtInfoOfEpidose(119));
            Console.WriteLine(DataHolder.getCurrent());
            //List<SrtInfo> list = SrtInfoDao.getSrtInfoOfEpidose(119);
            //Console.WriteLine("字幕数:" + list.Count);
            
           //TimeInfo info = srt.TimeHelper.parseTimeInfo("01:01:01,133");
           //Console.WriteLine(info);
           //Console.WriteLine(srt.TimeHelper.getTime(info));
        }

        private string getProcessStr()
        {
            if(MyVideo != null){
                return MyVideo.CurrentPosition+" / "+video_duration;
            }
            return "";
        }

        private void setSrtContent(SrtInfo srt)
        {
            this.tb_srtcontent.Text = srt.getEng() + "\n" + srt.getChs();
            this.textBox1.Text = getProcessStr();
        }
        private void hideSrtContent()
        {
            this.tb_srtcontent.Text = "";
        }

        private void bt_open_Click_1(object sender, EventArgs e)
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
                video_duration = MyVideo.Duration;
                MyVideo.Play();
                //MyVideo.Pause();
                timer = new System.Timers.Timer(100);  
                timer.Elapsed += new System.Timers.ElapsedEventHandler(theout); //到达时间的时候执行事件；   
                timer.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
                timer.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件；  
            }

            //确定窗体中的各按钮状态

            if (MyVideo == null)
            {
                bt_play.Enabled = false;
                bt_pre.Enabled = false;
                bt_next.Enabled = false;
            }

            else
            {
                bt_play.Enabled = true;
                bt_pre.Enabled = true;
                bt_next.Enabled = true;
            }
        }

        private void bt_play_Click(object sender, EventArgs e)
        {
            if (MyVideo != null)
            {
                if (MyVideo.Playing)
                {
                    MyVideo.Pause();
                }
                else
                {
                    MyVideo.Play();
                    Console.WriteLine("video_duration:::" + video_duration);
                }
            }
        }

        private void bt_pre_Click_1(object sender, EventArgs e)
        {
            SrtInfo srt = DataHolder.getPre();
            MyVideo.CurrentPosition = (double)(TimeHelper.getTime(srt.getFromTime())/1000);
            setSrtContent(srt);
        }

        private void bt_next_Click(object sender, EventArgs e)
        {
            SrtInfo srt = DataHolder.getNext();
            MyVideo.CurrentPosition = (double)(TimeHelper.getTime(srt.getFromTime()) / 1000);
            setSrtContent(srt);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }
    }
}
