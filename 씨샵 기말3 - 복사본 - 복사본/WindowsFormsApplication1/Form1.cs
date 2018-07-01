using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using Papago;




namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        //알람 설정 가능한지 여부
        bool setup_enable = true;

        //설정할 시간
        int m_hour = 0;
        int m_min = 0;

        //DateTime 인터벌 변경

        static readonly int MinTimeInterval = 60 * 1000;
        static readonly int HourTimeInterval = 60 * 60 * 1000;

        public Form1()
        {
            InitializeComponent();

            //알림 시작할때? tray아이콘 뜨도록
            NotifyIcon notifycon1 = new NotifyIcon();

            notifyIcon1.Visible = true;

            notifyIcon1.Icon = SystemIcons.Information;

            notifyIcon1.BalloonTipTitle = "영어 공부할 시간 입니다.";

            notifyIcon1.BalloonTipText = "영어.";

            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

            notifyIcon1.ShowBalloonTip(1000);

        }

        // 현재시간 
        private void Form1_Load(object sender, EventArgs e)
        {
            label_alarm.Text = "현재시간 :" + DateTime.Now.ToString();

            label_alarm.Visible = true;

            timer_realtime.Enabled = true;


        }

        // 링크 설정
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.duolingo.com/");


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://edition.cnn.com/studentnews/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.coursera.org/");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.ted.com/");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //현재 시간 타이머
        private void timer_realtime_Tick(object sender, EventArgs e)
        {
            label_alarm.Text = "현재시간:" + DateTime.Now.ToString();
        }

        //설정 시간 타이머 
        private void timer_setting_Tick(object sender, EventArgs e)
        {
            if (!setup_enable)
            {
                MessageBox.Show("영어 공부할 시간입니다 !!");

                InitAll_Item(); //알람 후 초기화
            }
        }

        // notifyIcon 을 더블 클릭 할 때 
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        //설정 버튼 클릭 할 때
        private void btn_setup_Click(object sender, EventArgs e)
        {
            if (setup_enable)
            {
                setup_enable = false; //재설정 방지

                CalculateInterval();

                //설정 시간 간격 설정
                timer_setting.Interval = m_hour * HourTimeInterval + m_min * MinTimeInterval;

                timer_setting.Enabled = true;

            }

            DisableTextBox();
            btn_cancel.Enabled = true;
            btn_setup.Enabled = false;

        }

        //취소 버튼 클릭 할 때
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("알람을 취소하겠습니까?");
            InitAll_Item(); //취소, 초기화
        }

        // '몇 분후' 텍스트 박스 설정 
        private void txt_minutes_TextChanged(object sender, EventArgs e)
        {
            txt_hour.Enabled = false;
            txt_min.Enabled = false;

            if (txt_minutes.TextLength == 0)
            {
                txt_hour.Enabled = true;   //특정 시각 '시간'
                txt_min.Enabled = true; //특정 시각 '분'
            }


        }

        //특정시각 '시' 텍스트 박스
        private void txt_hour_TextChanged(object sender, EventArgs e)
        {
            //특정시각 설정시 '몇 분후' 설정 불가
            txt_minutes.Enabled = false;

            if (txt_hour.TextLength == 0)
            {
                txt_minutes.Enabled = true;

            }
        }

        //특정시각 '분' 텍스트 박스
        private void txt_min_TextChanged(object sender, EventArgs e)
        {
            //특정시각 설정시 '몇 분후' 설정 불가
            txt_minutes.Enabled = false;

            if (txt_min.TextLength == 0)
            {
                txt_minutes.Enabled = true;
            }



        }

        //설정 초기화
        private void InitAll_Item()
        {
            setup_enable = true;
            timer_setting.Interval = 100;
            m_hour = 0;
            m_min = 0;
            txt_minutes.Text = "";
            txt_hour.Text = "";
            txt_min.Text = "";
            txt_hour.Enabled = true;
            txt_min.Enabled = true;
            btn_setup.Enabled = true;
            btn_cancel.Enabled = false;

        }

        //비활성화 하기
        private void DisableTextBox()
        {
            txt_minutes.Enabled = false;
            txt_hour.Enabled = false;
            txt_min.Enabled = false;

        }

        //인터벌 계산
        private void CalculateInterval()
        {
            if (txt_minutes.Enabled)
            {
                m_hour = Convert.ToInt32(txt_minutes.Text) / 60;
                m_min = Convert.ToInt32(txt_minutes.Text);

            }
            else
            {
                m_hour = (Convert.ToInt32(txt_hour.Text) - System.DateTime.Now.Hour);

                m_min = Convert.ToInt32(txt_min.Text) - System.DateTime.Now.Minute;

            }

        }

        private void label_alarm_Click(object sender, EventArgs e)
        {
            label_alarm.Text = "현재시간 :" + DateTime.Now.ToString();

            label_alarm.Visible = true;

            timer_realtime.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            papago();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public static void papago()

        {
            string temp;
            Translate PapagoTest = new Translate();
            Translate.NaverClientId = "lo35yY6UeWL7ePyAGX28";
            Translate.NaverClientSecret = "nmeqoqRucw";

            temp = PapagoTest.NMT("en", "ko", "");
            Console.WriteLine(temp);



        }
    }


 }


