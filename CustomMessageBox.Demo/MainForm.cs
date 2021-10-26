using CustomDialogs.Demo.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CustomDialogs.Demo
{
    public partial class MainForm : Form
    {
        private Random random;

        public MainForm()
        {
            InitializeComponent();
            random = new Random();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] randomText = Resources.ResourceManager.GetObject("LoremIpsum").ToString().Split('.');
            List<string> newContent = new List<string>();

            for (int j = 0; j < 10; j++)
            {
                newContent.Clear();
                int lineCount = random.Next(0, 10) > 4 ? random.Next(1, 6) : 1;
                for (int i = 0; i < lineCount; i++)
                    newContent.Add(randomText[random.Next(0, randomText.Length - 1)].Trim() + ".");

                MessageBox.Show(string.Join(Environment.NewLine, newContent.ToArray()), "StandartMessageBox Demo", (MessageBoxButtons)random.Next(0, 5), (MessageBoxIcon)(16 * random.Next(0, 4)), MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Show("Something had happened...", // Message text - everything what you want to display
                "Sorry, an error occurred",   // Message title - your personal message title
                "My Super Application");      // Window title, can be application name or something else or blank


            CustomMessageBox.Show("Something had happened...", // Message text - everything what you want to display
                "Sorry, an error occurred",   // Message title - your personal message title
                "My Super Application",  // Window title, can be application name or something else or blank
                MessageBoxButtons.YesNoCancel, // One of most common button actions
                MessageBoxIcon.Exclamation,  // System built-in Icon
                MessageBoxDefaultButton.Button2, // Default button
                FormStartPosition.CenterParent);  // Form location relatively to parent  


            CustomMessageBox.Show("Something had happened...", // Message text - everything what you want to display
                "Sorry, an error occurred",   // Message title - your personal message title
                "My Super Application",  // Window title, can be application name or something else or blank
                new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.Button1, "My Option 1"), new CustomMessageBoxButton(CustomButtonResult.Button1, "My Option 2") },
                MessageBoxIcon.Exclamation,  // System built-in Icon
                0, // Default button No
                FormStartPosition.CenterParent);  // Form location relatively to parent  

            return;

            string[] randomText = Resources.ResourceManager.GetObject("LoremIpsum").ToString().Split('.');
            List<string> newContent = new List<string>();
            List<CustomMessageBoxButton> buttons = new List<CustomMessageBoxButton>();

            for (int j = 0; j < 10; j++)
            {
                newContent.Clear();
                int lineCount = random.Next(0, 10) > 4 ? random.Next(1, 6) : 1;
                for (int i = 0; i < lineCount; i++)
                    newContent.Add(randomText[random.Next(0, randomText.Length - 1)].Trim() + ".");

                if (random.Next(0, 10) < 7)
                    using (CustomMessageBox customMessageBox = new CustomMessageBox(string.Join(Environment.NewLine, newContent.ToArray()), random.Next(0, 10) > 3 ? "Lorem Ipsum Title" : "", "CustomMessageBox Demo", (MessageBoxIcon)(16 * random.Next(0, 4)), (MessageBoxButtons)random.Next(0, 5), MessageBoxDefaultButton.Button1) { StartPosition = FormStartPosition.CenterParent })
                    {
                        customMessageBox.VerificationText = random.Next(0, 10) > 6 ? randomText[random.Next(0, randomText.Length - 1)].Trim() : "";
                        customMessageBox.ShowDialog();
                    }
                else
                {
                    buttons.Clear();
                    int buttonCount = random.Next(1, 5);
                    for (int i = 0; i < buttonCount; i++)
                        buttons.Add(new CustomMessageBoxButton(CustomButtonResult.Button1, randomText[random.Next(0, randomText.Length - 1)].Trim().Split(' ')[0]));

                    using (CustomMessageBox customMessageBox = new CustomMessageBox(string.Join(Environment.NewLine, newContent.ToArray()), random.Next(0, 10) > 3 ? "Lorem Ipsum Title" : "", "CustomMessageBox Demo", SystemIcons.Information.ToBitmap(), buttons.ToArray(), random.Next(1, buttonCount)) { StartPosition = FormStartPosition.CenterParent })
                    {
                        customMessageBox.VerificationText = random.Next(0, 10) > 6 ? randomText[random.Next(0, randomText.Length - 1)] : "";
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
                string textValue = random.Next(0, 10) > 6 ? randomText[random.Next(0, randomText.Length - 1)] : "";

                buttons.Clear();
                int buttonCount = random.Next(1, 5);
                for (int i = 0; i < buttonCount; i++)
                    buttons.Add(new CustomMessageBoxButton(CustomButtonResult.Button1, randomText[random.Next(0, randomText.Length - 1)].Trim().Split(' ')[0]));

                using (InputTextBox customMessageBox = new InputTextBox(textValue, randomText[random.Next(0, randomText.Length - 1)], "CustomMessageBox Demo", buttons.ToArray(), random.Next(1, buttonCount)) { StartPosition = FormStartPosition.CenterParent })
                {
                    customMessageBox.VerificationText = random.Next(0, 10) > 6 ? randomText[random.Next(0, randomText.Length - 1)] : "";
                    customMessageBox.ShowDialog();
                }
            }
        }

    }
}
