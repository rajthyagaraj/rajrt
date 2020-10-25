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
using System.Threading;

namespace Speech_Recognized
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText(textBox1.Text);
            ss.Speak(pb);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            Choices c = new Choices();
            c.Add(new string[] { "Hello", "How are you", "It Works","exit"});
            Grammar gr = new Grammar(new GrammarBuilder(c));
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }

            catch
            {
                return;
            }

        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "exit")
            {
                Application.Exit();
            }
            else {

                textBox1.Text = textBox1.Text + " " + e.Result.Text.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
