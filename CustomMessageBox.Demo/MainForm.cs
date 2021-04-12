using CustomDialogs;
using CustomDialogs.Demo.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomDialogs.Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] randomText = Resources.ResourceManager.GetObject("LoremIpsum").ToString().Split('.');
            List<string> newContent = new List<string>();

            for (int j = 0; j < 10; j++)
            {
                newContent.Clear();
                int lineCount = StaticRandomizer.RandomInt(0, 10) > 4 ? StaticRandomizer.RandomInt(1, 6) : 1;
                for (int i = 0; i < lineCount; i++)
                    newContent.Add(randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)].Trim() + ".");

                MessageBox.Show(string.Join(Environment.NewLine, newContent), "StandartMessageBox Demo", (MessageBoxButtons)StaticRandomizer.RandomInt(0, 5), (MessageBoxIcon)(16 * StaticRandomizer.RandomInt(0, 4)), MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] randomText = Resources.ResourceManager.GetObject("LoremIpsum").ToString().Split('.');
            List<string> newContent = new List<string>();
            List<CustomMessageBoxButton> buttons = new List<CustomMessageBoxButton>();

            for (int j = 0; j < 10; j++)
            {
                newContent.Clear();
                int lineCount = StaticRandomizer.RandomInt(0, 10) > 4 ? StaticRandomizer.RandomInt(1, 6) : 1;
                for (int i = 0; i < lineCount; i++)
                    newContent.Add(randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)].Trim() + ".");

                if (StaticRandomizer.RandomInt(0, 10) < 7)
                    using (CustomMessageBox customMessageBox = new CustomMessageBox(string.Join(Environment.NewLine, newContent), StaticRandomizer.RandomInt(0, 10) > 3 ? "Lorem Ipsum Title" : "", "CustomMessageBox Demo", (MessageBoxIcon)(16 * StaticRandomizer.RandomInt(0, 4)), (MessageBoxButtons)StaticRandomizer.RandomInt(0, 5), MessageBoxDefaultButton.Button1) { StartPosition = FormStartPosition.CenterParent })
                    {
                        customMessageBox.VerificationText = StaticRandomizer.RandomInt(0, 10) > 6 ? randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)].Trim() : "";
                        customMessageBox.ShowDialog();
                    }
                else
                {
                    buttons.Clear();
                    int buttonCount = StaticRandomizer.RandomInt(1, 5);
                    for (int i = 0; i < buttonCount; i++)
                        buttons.Add(new CustomMessageBoxButton(CustomButtonResult.Button1, randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)].Trim().Split(' ').FirstOrDefault()));

                    using (CustomMessageBox customMessageBox = new CustomMessageBox(string.Join(Environment.NewLine, newContent), StaticRandomizer.RandomInt(0, 10) > 3 ? "Lorem Ipsum Title" : "", "CustomMessageBox Demo", SystemIcons.Information.ToBitmap(), buttons.ToArray(), StaticRandomizer.RandomInt(1, buttonCount)) { StartPosition = FormStartPosition.CenterParent })
                    {
                        customMessageBox.VerificationText = StaticRandomizer.RandomInt(0, 10) > 6 ? randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)] : "";
                        customMessageBox.ShowDialog();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] randomText = Resources.ResourceManager.GetObject("LoremIpsum").ToString().Split('.');
            List<CustomMessageBoxButton> buttons = new List<CustomMessageBoxButton>();

            for (int j = 0; j < 10; j++)
            {
                string textValue = StaticRandomizer.RandomInt(0, 10) > 6 ? randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)] : "";

                buttons.Clear();
                int buttonCount = StaticRandomizer.RandomInt(1, 5);
                for (int i = 0; i < buttonCount; i++)
                    buttons.Add(new CustomMessageBoxButton(CustomButtonResult.Button1, randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)].Trim().Split(' ').FirstOrDefault()));

                using (InputTextBox customMessageBox = new InputTextBox(textValue, randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)], "CustomMessageBox Demo", buttons.ToArray(), StaticRandomizer.RandomInt(1, buttonCount)) { StartPosition = FormStartPosition.CenterParent })
                {
                    customMessageBox.VerificationText = StaticRandomizer.RandomInt(0, 10) > 6 ? randomText[StaticRandomizer.RandomInt(0, randomText.Length - 1)] : "";
                    customMessageBox.ShowDialog();
                }
            }
        }

    }
}
