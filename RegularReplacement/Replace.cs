using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegularReplacement
{
    public partial class Replace : Form
    {
        private const char WILD_CHARACTER = '%';

        public Replace()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sourceText = ((Text)Owner).GetText();
            var startPosition = GetCursorPosition();
            startPosition += ((Text)Owner).GetSelectedTextIndexEnd();

            var searchText = textBox1.Text;
            var regex = BuildSearchPattern(searchText);

            foreach (var word in sourceText.Substring(startPosition).Split(' '))
            {
                if(regex.IsMatch(word))
                {
                    ((Text)Owner).HigthSelectedText(startPosition, word);
                    return;
                }

                startPosition += word.Length;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sourceText = ((Text)Owner).GetText();
            var position = GetCursorPosition();
            var transformationResultresult = new StringBuilder();

            var searchText = textBox1.Text;
            var replacement = textBox2.Text;

            var regex = BuildSearchPattern(searchText);

            foreach (var word in sourceText.Substring(position).Split(' '))
            {
                var resultWord = regex.IsMatch(word)
                    ? replacement
                    : word;
                transformationResultresult.Append($"{resultWord} ");
            }

            var result = this.radioButton2.Checked
                ? sourceText.Substring(0, position)
                : string.Empty;

            ((Text)Owner).InsertReplacedText(result + transformationResultresult.ToString().Trim());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            IsActionButtonsEnabled();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            IsActionButtonsEnabled();
        }

        private void IsActionButtonsEnabled()
        {
            var isReplacementButtonsEnable = !string.IsNullOrEmpty(textBox1.Text.Trim())
                                    && !string.IsNullOrEmpty(textBox2.Text.Trim());

            var isNextButtonEnable = !string.IsNullOrEmpty(textBox1.Text.Trim());

            button1.Enabled = isNextButtonEnable;
            button2.Enabled = isReplacementButtonsEnable;
            button3.Enabled = isReplacementButtonsEnable;
        }

        private int GetCursorPosition()
        {
            return this.radioButton1.Checked
                            ? 0
                            : ((Text)Owner).GetCursorPosition();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sourceText = ((Text)Owner).GetText();
            var startPosition = GetCursorPosition();
            startPosition += ((Text)Owner).GetSelectedTextIndexStart();

            var searchText = textBox1.Text;
            var replacement = textBox2.Text;
            var regex = BuildSearchPattern(searchText);

            foreach (var word in sourceText.Substring(startPosition).Split(' '))
            {
                if (regex.IsMatch(word))
                {
                    ((Text)Owner).ReplaceText(startPosition, word.Length, replacement);
                    return;
                }

                startPosition += word.Length;
            }
        }

        private Regex BuildSearchPattern(string searchText)
        {
            var regIterator = 1;
            var regPattern = string.Empty;
            foreach (var searchChar in searchText)
            {
                if (searchChar == WILD_CHARACTER)
                {
                    regPattern += $"(.)\\{regIterator}*";
                    regIterator++;
                }
                else
                {
                    regPattern += searchChar;
                }
            }

            return new Regex($"^{regPattern}$");
        }
    }
}
