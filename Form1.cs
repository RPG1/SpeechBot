using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.IO;
using System.Diagnostics;

namespace SBOT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SpeechSynthesizer sp = new SpeechSynthesizer();

        SpeechRecognitionEngine rec = new SpeechRecognitionEngine();

        Grammar gramer = new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"cmd.txt"))));

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Teen);
            rec.SetInputToDefaultAudioDevice();
            rec.LoadGrammar(gramer);
            rec.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(defualtspeech);
            rec.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void defualtspeech(object sender, SpeechRecognizedEventArgs e)
        {
            string res = e.Result.Text;
            if (res == "hello")
            {
                sp.SpeakAsync("Hi my name is Sbot your professional assistant");
            }
            if (res == "exit")
            {
                this.Close();
            }
            if (res == "show cmd")
            {
                string[] cmd = (File.ReadAllLines(@"cmd.txt"));

                foreach (var item in cmd)
                {
                    cmdbox.Items.Add(item);
                    cmdbox.Visible = true;
                }
            }
            if (res == "hide cmd")
            {
                cmdbox.Items.Clear();
                cmdbox.Visible = false;
            }
            if (res == "open youtube")
            {
                string youtube = "https://www.youtube.com/";
                System.Diagnostics.Process.Start(youtube);
            }
            if (res == "go to my channel in youtube")
            {
                sp.SpeakAsync("Ok go to your channel");
                string mychannel = "https://www.youtube.com/@mwcode8052/videos";
                System.Diagnostics.Process.Start(mychannel);
            }
            if (res == "what time")
            {
                sp.SpeakAsync(DateTime.Now.ToString("h mm tt"));
            }
        }
    }
}
