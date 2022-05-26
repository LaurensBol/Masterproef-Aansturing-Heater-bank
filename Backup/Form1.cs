using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace K8061Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int CardAddress;
        int n = 0;

        [DllImport("k8061.dll")]
        public static extern int OpenDevice();

        [DllImport("k8061.dll")]
        public static extern void CloseDevices();

        [DllImport("k8061.dll")]
        public static extern void CloseDevice(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern int ReadAnalogChannel(int CardAddress, int Channel);

        [DllImport("k8061.dll")]
        public static extern void ReadAllAnalog(int CardAddress, int[] Buffer);

        [DllImport("k8061.dll")]
        public static extern void OutputAnalogChannel(int CardAddress, int Channel, int Data);

        [DllImport("k8061.dll")]
        public static extern void OutputAllAnalog(int CardAddress, int[] Buffer);

        [DllImport("k8061.dll")]
        public static extern void ClearAnalogChannel(int CardAddress, int Channel);

        [DllImport("k8061.dll")]
        public static extern void ClearAllAnalog(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern void SetAnalogChannel(int CardAddress, int Channel);

        [DllImport("k8061.dll")]
        public static extern void SetAllAnalog(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern void OutputAllDigital(int CardAddress, int Data);

        [DllImport("k8061.dll")]
        public static extern void ClearDigitalChannel(int CardAddress, int Channel);

        [DllImport("k8061.dll")]
        public static extern void ClearAllDigital(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern void SetDigitalChannel(int CardAddress, int Channel);

        [DllImport("k8061.dll")]
        public static extern void SetAllDigital(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern bool ReadDigitalChannel(int CardAddress, int Channel);

        [DllImport("k8061.dll")]
        public static extern int ReadAllDigital(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern void OutputPWM(int CardAddress, int Data);

        [DllImport("k8061.dll")]
        public static extern bool PowerGood(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern bool Connected(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern void ReadVersion(int CardAddress, int[] Buffer);

        [DllImport("k8061.dll")]
        public static extern int ReadBackDigitalOut(int CardAddress);

        [DllImport("k8061.dll")]
        public static extern void ReadBackAnalogOut(int CardAddress, int[] Buffer);

        [DllImport("k8061.dll")]
        public static extern int ReadBackPWMOut(int CardAddress);

        private void Button1_Click(object sender, EventArgs e)
        {
            int h = OpenDevice();
            switch (h)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    Label1.Text = "Card " + h.ToString() + " connected.";
                    CardAddress = h;
                    Timer1.Enabled = true;
                    break;
                case -1:
                    Label1.Text = "All cards connected.";
                    break;
                case -2:
                    Label1.Text = "Card not found.";
                    break;
            }
            switch (h)
            {
                case 0:
                    RadioButton1.Enabled = true;
                    RadioButton1.Checked = true;
                    break;
                case 1:
                    RadioButton2.Enabled = true;
                    RadioButton2.Checked = true;
                    break;
                case 3:
                    RadioButton4.Enabled = true;
                    RadioButton4.Checked = true;
                    break;
                case 4:
                    RadioButton5.Enabled = true;
                    RadioButton5.Checked = true;
                    break;
                case 5:
                    RadioButton6.Enabled = true;
                    RadioButton6.Checked = true;
                    break;
                case 6:
                    RadioButton7.Enabled = true;
                    RadioButton7.Checked = true;
                    break;
                case 7:
                    RadioButton8.Enabled = true;
                    RadioButton8.Checked = true;
                    break;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseDevices();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            int[] Buffer = new int[8];
            int i;
            Timer1.Enabled = false;
            if (PowerGood(CardAddress))
            {
                Label20.Text = "CPU OK";
            }
            else
            {
                Label20.Text = "CPU FAIL";
            }
            if (Connected(CardAddress))
            {
                Label21.Text = "USB Connected";
            }
            else
            {
                Label21.Text = "USB Disconnected";
                Label1.Text = "Card disconnected";
            }
            i = ReadAllDigital(CardAddress);
            CheckBox1.Checked = (i & 1) > 0;
            CheckBox2.Checked = (i & 2) > 0;
            CheckBox3.Checked = (i & 4) > 0;
            CheckBox4.Checked = (i & 8) > 0;
            CheckBox5.Checked = (i & 16) > 0;
            CheckBox6.Checked = (i & 32) > 0;
            CheckBox7.Checked = (i & 64) > 0;
            CheckBox8.Checked = (i & 128) > 0;

            ReadAllAnalog(CardAddress, Buffer);
            ProgressBar1.Value = Buffer[0];
            Label2.Text = Buffer[0].ToString();
            ProgressBar2.Value = Buffer[1];
            Label3.Text = Buffer[1].ToString();
            ProgressBar3.Value = Buffer[2];
            Label4.Text = Buffer[2].ToString();
            ProgressBar4.Value = Buffer[3];
            Label5.Text = Buffer[3].ToString();
            ProgressBar5.Value = Buffer[4];
            Label6.Text = Buffer[4].ToString();
            ProgressBar6.Value = Buffer[5];
            Label7.Text = Buffer[5].ToString();
            ProgressBar7.Value = Buffer[6];
            Label8.Text = Buffer[6].ToString();
            ProgressBar8.Value = Buffer[7];
            Label9.Text = Buffer[7].ToString();

            Timer1.Enabled = true;
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            ClearDigitalChannel(CardAddress, n);
            n++;
            if (n == 9) n = 1;
            SetDigitalChannel(CardAddress, n);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SetAllDigital(CardAddress);
            CheckBox9.Checked = true;
            CheckBox10.Checked = true;
            CheckBox11.Checked = true;
            CheckBox12.Checked = true;
            CheckBox13.Checked = true;
            CheckBox14.Checked = true;
            CheckBox15.Checked = true;
            CheckBox16.Checked = true;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ClearAllDigital(CardAddress);
            CheckBox9.Checked = false;
            CheckBox10.Checked = false;
            CheckBox11.Checked = false;
            CheckBox12.Checked = false;
            CheckBox13.Checked = false;
            CheckBox14.Checked = false;
            CheckBox15.Checked = false;
            CheckBox16.Checked = false;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SetAllAnalog(CardAddress);
            HScrollBar1.Value = 255;
            HScrollBar2.Value = 255;
            HScrollBar3.Value = 255;
            HScrollBar4.Value = 255;
            HScrollBar5.Value = 255;
            HScrollBar6.Value = 255;
            HScrollBar7.Value = 255;
            HScrollBar8.Value = 255;
            Label10.Text = "255";
            Label11.Text = "255";
            Label12.Text = "255";
            Label13.Text = "255";
            Label14.Text = "255";
            Label15.Text = "255";
            Label16.Text = "255";
            Label17.Text = "255";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ClearAllAnalog(CardAddress);
            HScrollBar1.Value = 0;
            HScrollBar2.Value = 0;
            HScrollBar3.Value = 0;
            HScrollBar4.Value = 0;
            HScrollBar5.Value = 0;
            HScrollBar6.Value = 0;
            HScrollBar7.Value = 0;
            HScrollBar8.Value = 0;
            Label10.Text = "0";
            Label11.Text = "0";
            Label12.Text = "0";
            Label13.Text = "0";
            Label14.Text = "0";
            Label15.Text = "0";
            Label16.Text = "0";
            Label17.Text = "0";
        }

        private void CheckBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox30.Checked)
                Timer2.Enabled = true;
            else
            {
                Timer2.Enabled = false;
                CheckBox9.Checked = false;
                CheckBox10.Checked = false;
                CheckBox11.Checked = false;
                CheckBox12.Checked = false;
                CheckBox13.Checked = false;
                CheckBox14.Checked = false;
                CheckBox15.Checked = false;
                CheckBox16.Checked = false;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int DigitalOut;
            int[] AnalogOut = new int[8]; ;
            DigitalOut = ReadBackDigitalOut(CardAddress);
            CheckBox9.Checked = false;
            CheckBox10.Checked = false;
            CheckBox11.Checked = false;
            CheckBox12.Checked = false;
            CheckBox13.Checked = false;
            CheckBox14.Checked = false;
            CheckBox15.Checked = false;
            CheckBox16.Checked = false;
            if ((DigitalOut & 1) > 0) CheckBox9.Checked = true;
            if ((DigitalOut & 2) > 0) CheckBox10.Checked = true;
            if ((DigitalOut & 4) > 0) CheckBox11.Checked = true;
            if ((DigitalOut & 8) > 0) CheckBox12.Checked = true;
            if ((DigitalOut & 16) > 0) CheckBox13.Checked = true;
            if ((DigitalOut & 32) > 0) CheckBox14.Checked = true;
            if ((DigitalOut & 64) > 0) CheckBox15.Checked = true;
            if ((DigitalOut & 128) > 0) CheckBox16.Checked = true;

            ReadBackAnalogOut(CardAddress, AnalogOut);
            HScrollBar1.Value = AnalogOut[0];
            HScrollBar2.Value = AnalogOut[1];
            HScrollBar3.Value = AnalogOut[2];
            HScrollBar4.Value = AnalogOut[3];
            HScrollBar5.Value = AnalogOut[4];
            HScrollBar6.Value = AnalogOut[5];
            HScrollBar7.Value = AnalogOut[6];
            HScrollBar8.Value = AnalogOut[7];

            HScrollBar9.Value = ReadBackPWMOut(CardAddress);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            CloseDevices();
            Label1.Text = "Card disconnected.";
            Label20.Text = "- - -";
            Label21.Text = "- - -";
            Timer1.Enabled = false;
            Timer2.Enabled = false;
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Label10.Text = (HScrollBar1.Value).ToString();
            OutputAnalogChannel(CardAddress, 1, HScrollBar1.Value);
        }

        private void HScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            Label11.Text = (HScrollBar2.Value).ToString();
            OutputAnalogChannel(CardAddress, 2, HScrollBar2.Value);
        }

        private void HScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            Label12.Text = (HScrollBar3.Value).ToString();
            OutputAnalogChannel(CardAddress, 3, HScrollBar3.Value);
        }

        private void HScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            Label13.Text = (HScrollBar4.Value).ToString();
            OutputAnalogChannel(CardAddress, 4, HScrollBar4.Value);
        }

        private void HScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            Label14.Text = (HScrollBar5.Value).ToString();
            OutputAnalogChannel(CardAddress, 5, HScrollBar5.Value);
        }

        private void HScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            Label15.Text = (HScrollBar6.Value).ToString();
            OutputAnalogChannel(CardAddress, 6, HScrollBar6.Value);
        }

        private void HScrollBar7_Scroll(object sender, ScrollEventArgs e)
        {
            Label16.Text = (HScrollBar7.Value).ToString();
            OutputAnalogChannel(CardAddress, 7, HScrollBar7.Value);
        }

        private void HScrollBar8_Scroll(object sender, ScrollEventArgs e)
        {
            Label17.Text = (HScrollBar8.Value).ToString();
            OutputAnalogChannel(CardAddress, 8, HScrollBar8.Value);
        }

        private void HScrollBar9_Scroll(object sender, ScrollEventArgs e)
        {
            Label18.Text = (HScrollBar9.Value).ToString();
            OutputPWM(CardAddress, HScrollBar9.Value);
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CardAddress = 0;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CardAddress = 1;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CardAddress = 2;
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            CardAddress = 3;
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            CardAddress = 4;
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            CardAddress = 5;
        }

        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            CardAddress = 6;
        }

        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            CardAddress = 7;
        }

        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox9.Checked) SetDigitalChannel(CardAddress, 1); 
                else ClearDigitalChannel(CardAddress, 1);
        }

        private void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox10.Checked) SetDigitalChannel(CardAddress, 2);
            else ClearDigitalChannel(CardAddress, 2);
        }

        private void CheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox11.Checked) SetDigitalChannel(CardAddress, 3);
            else ClearDigitalChannel(CardAddress, 3);
        }

        private void CheckBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox12.Checked) SetDigitalChannel(CardAddress, 4);
            else ClearDigitalChannel(CardAddress, 4);
        }

        private void CheckBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox13.Checked) SetDigitalChannel(CardAddress, 5);
            else ClearDigitalChannel(CardAddress, 5);
        }

        private void CheckBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox14.Checked) SetDigitalChannel(CardAddress, 6);
            else ClearDigitalChannel(CardAddress, 6);
        }

        private void CheckBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox15.Checked) SetDigitalChannel(CardAddress, 7);
            else ClearDigitalChannel(CardAddress, 7);
        }

        private void CheckBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox16.Checked) SetDigitalChannel(CardAddress, 8);
            else ClearDigitalChannel(CardAddress, 8);
        }
    }
}
