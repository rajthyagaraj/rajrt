using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Diagnostics;

namespace Speech_Navigation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechSynthesizer speechsynth = new SpeechSynthesizer();
        SpeechRecognitionEngine receng = new SpeechRecognitionEngine();
        Choices choice = new Choices();

        private void startbtn_Click(object sender, EventArgs e)
        {
            startbtn.Enabled = false;
            stopbtn.Enabled = true;
            choice.Add(new string[] { "hello", "are you good", "what is the current time", "open blog", "thank you", "close" });
            Grammar gr = new Grammar(new GrammarBuilder(choice));

            try
            {
                receng.RequestRecognizerUpdate();
                receng.LoadGrammar(gr);
                receng.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(receng_SpeechRecognized);
                receng.SetInputToDefaultAudioDevice();
                receng.RecognizeAsync(RecognizeMode.Multiple);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
            void receng_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "hello":
                    speechsynth.SpeakAsync("hai user");
                    break;
                case "are you good":
                    speechsynth.SpeakAsync("iam fine. what about you");
                    break;
                case "what is the current time":
                    speechsynth.SpeakAsync("right now it is " + DateTime.Now.ToLongTimeString());
                    break;
                case "thank you":
                    speechsynth.SpeakAsync("well. same to you");
                    break;
                case "open google":
                    Process.Start("chrome", "http://www.Google.com");
                    break;
                case "close":
                    speechsynth.Speak("see you later have a good day. bye bye");
                    Application.Exit();
                    break;

            }
            listBox1.Items.Add(e.Result.Text.ToString());
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            receng.RecognizeAsyncStop();
            startbtn.Enabled = true;
            stopbtn.Enabled = false;
        }
    }
}
