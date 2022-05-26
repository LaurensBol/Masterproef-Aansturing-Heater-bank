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

        List<String> vermogenLampen = new List<String>
        {
            "0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"
        };

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
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ClearAllDigital(CardAddress);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SetAllAnalog(CardAddress);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ClearAllAnalog(CardAddress);
        }

        private void CheckBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox30.Checked)
                Timer2.Enabled = true;
            else
            {
                Timer2.Enabled = false;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int DigitalOut;
            int[] AnalogOut = new int[8]; ;
            DigitalOut = ReadBackDigitalOut(CardAddress);
            ReadBackAnalogOut(CardAddress, AnalogOut);
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

        
        private void Start(object sender, EventArgs e)
        {
            
            for(int i = 0; i<vermogenLampen.Count; i++)
            {
                switch(i)
                {
                    case 0:
                        aansturingLamp300W(0, 1, vermogenLampen[0]);
                        break;
                    case 1:
                        aansturingLamp700W(0, 2, vermogenLampen[1]);
                        break;
                    case 2:
                        aansturingLamp300W(0, 3, vermogenLampen[2]);
                        break;                       
                    case 3:
                        aansturingLamp700W(0, 4, vermogenLampen[3]);
                        break;                        
                    case 4:
                        aansturingLamp700W(0, 5, vermogenLampen[4]);
                        break;
                    case 5:
                        aansturingLamp300W(0, 6, vermogenLampen[5]);
                        break;
                    case 6:
                        aansturingLamp700W(0, 7, vermogenLampen[6]);
                        break;
                    case 7:
                        aansturingLamp300W(0, 8, vermogenLampen[7]);
                        break;
                    case 8:
                        aansturingLamp700W(1, 1, vermogenLampen[8]);
                        break;
                    case 9:
                        aansturingLamp700W(1, 2, vermogenLampen[9]);
                        break;
                    case 10:
                        aansturingLamp300W(1, 3, vermogenLampen[10]);
                        break;
                    case 11:
                        aansturingLamp700W(1, 4, vermogenLampen[11]);
                        break;
                    case 12:
                        aansturingLamp300W(1, 5, vermogenLampen[12]);
                        break;
                    case 13:
                        aansturingLamp700W(1, 6, vermogenLampen[13]);
                        break;
                    case 14:
                        aansturingLamp700W(1, 7, vermogenLampen[14]);
                        break;
                }
            }
            uitsturenCheck.Checked = true;
            afleggenCheck.Checked = false;
        }
               

        private void Stop(object sender, EventArgs e)
        {
            ClearAllAnalog(0);
            ClearAllAnalog(1);
            afleggenCheck.Checked = true;
            uitsturenCheck.Checked = false;

        }

        public void aansturingLamp300W(int kaart, int uitgang, String tekst)
        {
            int spanning;
                      
            //Kaart adres (bepaald via de 3 jumpers op de kaart)
            //Analoge uitgang


            //Voor de omzetting percentage naar elektrisch vermogen (lamp 300W)
            float tellerVermogenMax = 280f;
            float noemerPercentageMax = 100f;

            //Inlezen percentage en omzetten van tekst naar een getal (type float)
            float percentage = 0f;
            if (tekst != "") percentage = float.Parse(tekst);


            if (percentage == 100)
            {
                OutputAnalogChannel(kaart, uitgang, 255);
            }

            else if (percentage < 100 & percentage > 29.9f)
            {
                //Elektrisch vermogen zoeken uit het ingestelde percentage
                float vermogenElektrisch = (tellerVermogenMax / noemerPercentageMax) * percentage;


                //Tweedegraads functie instellen met elektrisch vermogen al in verwerkt
                float a = -3.9268f;
                float b = 92.714f;
                float f = -249.48f;
                float c = f - vermogenElektrisch;


                //Nulpunten (DC spanning) zoeken uit de tweedegraadsfunctie en uitsturen naar dimmer
                float x1;
                float x2;
                float d = b * b - 4 * a * c;
                float tellerSpanning = 255f;
                float noemerSpanning = 10f;

                if (d == 0)
                {
                    x1 = -b / (2f * a);
                    x2 = x1;

                    float waarde = (tellerSpanning / noemerSpanning) * x1;
                    spanning = (int)Math.Round(waarde);

                    OutputAnalogChannel(kaart, uitgang, spanning);


                }
                else if (d > 0f)
                {
                    x1 = (-b + (float)Math.Sqrt(d)) / (2.0f * a);
                    x2 = (-b - (float)Math.Sqrt(d)) / (2.0f * a);

                    if (x1 < 10f)
                    {
                        float waarde = (tellerSpanning / noemerSpanning) * x1;
                        spanning = (int)Math.Round(waarde);

                        OutputAnalogChannel(kaart, uitgang, spanning);

                    }
                    else
                    {
                        float waarde = (tellerSpanning / noemerSpanning) * x2;
                        spanning = (int)Math.Round(waarde);

                        OutputAnalogChannel(kaart, uitgang, spanning);

                    }

                }
            }
            else
            {
                OutputAnalogChannel(kaart, uitgang, 0);
            }
        }
        

        public void aansturingLamp700W(int kaart, int uitgang, String tekst)
        {
            //Kaart adres (bepaald via de 3 jumpers op de kaart)
            //Analoge uitgang


            //Voor de omzetting percentage naar elektrisch vermogen (lamp 700W)
            float tellerVermogenMax = 650f;
            float noemerPercentageMax = 100f;

            //Inlezen percentage en omzetten van tekst naar een getal (type float)
            float percentage = 0f;
            if (tekst != "") percentage = float.Parse(tekst);


            if (percentage == 100)
            {
                OutputAnalogChannel(kaart, uitgang, 255);
            }

            else if (percentage < 100 & percentage > 29.9f)
            {
                //Elektrisch vermogen zoeken uit het ingestelde percentage
                float vermogenElektrisch = (tellerVermogenMax / noemerPercentageMax) * percentage;


                //Tweedegraads functie instellen met elektrisch vermogen al in verwerkt
                float a = -10.116f;
                float b = 229.75f;
                float f = -632.4f;
                float c = f - vermogenElektrisch;


                //Nulpunten (DC spanning) zoeken uit de tweedegraadsfunctie en uitsturen naar dimmer
                float x1;
                float x2;
                float d = b * b - 4 * a * c;
                float tellerSpanning = 255f;
                float noemerSpanning = 10f;

                if (d == 0)
                {
                    x1 = -b / (2f * a);
                    x2 = x1;

                    float waarde = (tellerSpanning / noemerSpanning) * x1;
                    int spanning = (int)Math.Round(waarde);

                    
                    OutputAnalogChannel(kaart, uitgang, spanning);
                    
                }
                else if (d > 0f)
                {
                    x1 = (-b + (float)Math.Sqrt(d)) / (2.0f * a);
                    x2 = (-b - (float)Math.Sqrt(d)) / (2.0f * a);

                    if (x1 < 10f)
                    {
                        float waarde = (tellerSpanning / noemerSpanning) * x1;
                        int spanning = (int)Math.Round(waarde);

                        
                        OutputAnalogChannel(kaart, uitgang, spanning);
                        
                    }
                    else
                    {
                        float waarde = (tellerSpanning / noemerSpanning) * x2;
                        int spanning = (int)Math.Round(waarde);

                        
                        OutputAnalogChannel(kaart, uitgang, spanning);
                       
                    }
                }
            }
            else
            {
                OutputAnalogChannel(kaart, uitgang, 0);
            }
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

                
        private void lamp1A(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            String vermogenLamp1A = textBox2.Text;
            vermogenLampen[0] = vermogenLamp1A;
        }

        private void lamp1B(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            String vermogenLamp1B = textBox1.Text;
            vermogenLampen[1] = vermogenLamp1B;

        }

        private void lamp1C(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.White;
            String vermogenLamp1C = textBox3.Text;
            vermogenLampen[2] = vermogenLamp1C;
        }

        private void lamp2A(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.White;
            String vermogenLamp2A = textBox4.Text;
            vermogenLampen[3] = vermogenLamp2A;
        }

        private void lamp2B(object sender, EventArgs e)
        {
            textBox5.BackColor = Color.White;
            String vermogenLamp2B = textBox5.Text;
            vermogenLampen[4] = vermogenLamp2B;
        }

        private void lamp3A(object sender, EventArgs e)
        {
            textBox6.BackColor = Color.White;
            String vermogenLamp3A = textBox6.Text;
            vermogenLampen[5] = vermogenLamp3A;
        }

        private void lamp3B(object sender, EventArgs e)
        {
            textBox8.BackColor = Color.White;
            String vermogenLamp3B = textBox8.Text;
            vermogenLampen[6] = vermogenLamp3B;
        }

        private void lamp3C(object sender, EventArgs e)
        {
            textBox7.BackColor = Color.White;
            String vermogenLamp3C = textBox7.Text;
            vermogenLampen[7] = vermogenLamp3C;
        }

        private void lamp4A(object sender, EventArgs e)
        {
            textBox9.BackColor = Color.White;
            String vermogenLamp4A = textBox9.Text;
            vermogenLampen[8] = vermogenLamp4A;
        }

        private void lamp4B(object sender, EventArgs e)
        {
            textBox10.BackColor = Color.White;
            String vermogenLamp4B = textBox10.Text;
            vermogenLampen[9] = vermogenLamp4B;
        }

        private void lamp5A(object sender, EventArgs e)
        {
            textBox11.BackColor = Color.White;
            String vermogenLamp5A = textBox11.Text;
            vermogenLampen[10] = vermogenLamp5A;
        }

        private void lamp5B(object sender, EventArgs e)
        {
            textBox13.BackColor = Color.White;
            String vermogenLamp5B = textBox13.Text;
            vermogenLampen[11] = vermogenLamp5B;
        }

        private void lamp5C(object sender, EventArgs e)
        {
            textBox12.BackColor = Color.White;
            String vermogenLamp5C = textBox12.Text;
            vermogenLampen[12] = vermogenLamp5C;
        }

        private void lamp6A(object sender, EventArgs e)
        {
            textBox14.BackColor = Color.White;
            String vermogenLamp6A = textBox14.Text;
            vermogenLampen[13] = vermogenLamp6A;
        }

        private void lamp6B(object sender, EventArgs e)
        {
            textBox15.BackColor = Color.White;
            String vermogenLamp6B = textBox15.Text;
            vermogenLampen[14] = vermogenLamp6B;
        }
    }
}

    


