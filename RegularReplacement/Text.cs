using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RegularReplacement
{
    public partial class Text : Form
    {
        public Text()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var replaceForm = new Replace();
            replaceForm.StartPosition = FormStartPosition.CenterParent;
            replaceForm.Show(this);
        }

        public string GetText()
        {
            return this.richTextBox1.Text;
        }

        public void InsertReplacedText(string input)
        {
            this.richTextBox1.Text = input;
        }

        public int GetCursorPosition()
        {
            return this.richTextBox1.SelectionStart;
        }

        public void HigthSelectedText(int startPosition, string word)
        {
            this.richTextBox1.Select(0, this.richTextBox1.Text.Length);
            this.richTextBox1.SelectionBackColor = Color.White;

            this.richTextBox1.Find(word, startPosition, RichTextBoxFinds.MatchCase);
            this.richTextBox1.SelectionBackColor = Color.Yellow;
        }

        public void ReplaceText(int startPosition, int length, string replacement)
        {
            this.richTextBox1.Text = this.richTextBox1.Text
                                            .Remove(startPosition, length)
                                            .Insert(startPosition, replacement);
        }

        public int GetSelectedTextIndexEnd()
        {
            return this.richTextBox1.SelectionLength == 0
                ? 0
                : this.richTextBox1.SelectionStart + this.richTextBox1.SelectionLength;
        }

        public int GetSelectedTextIndexStart()
        {
            return this.richTextBox1.SelectionLength == 0
                ? 0
                : this.richTextBox1.SelectionStart;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
