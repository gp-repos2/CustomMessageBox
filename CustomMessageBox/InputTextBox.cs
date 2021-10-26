using System;
using System.Windows.Forms;

namespace CustomDialogs
{
    public partial class InputTextBox : Form
    {
        private readonly string title;
        private readonly string caption;
        private readonly CustomMessageBoxButton[] buttons;
        private readonly int defaultButtonNo;

        public string TextValue
        {
            get { return tbText.Text; }
            set { tbText.Text = value; }
        }

        public string VerificationText { get; set; }
        public bool IsVerificationChecked
        {
            get { return cbVerification.Checked; }
            set { cbVerification.Checked = value; }
        }

        public CustomButtonResult CustomResult { get; private set; }

        public InputTextBox(string textValue, string title, string caption, CustomMessageBoxButton[] buttons, int defaultButtonNo)
        {
            InitializeComponent();
            this.TextValue = textValue;
            this.title = title;
            this.caption = caption;
            this.buttons = buttons;
            this.defaultButtonNo = defaultButtonNo;
            if (this.buttons == null || this.buttons.Length == 0)
            {
                this.buttons = new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.OK), new CustomMessageBoxButton(CustomButtonResult.Cancel) };
                this.defaultButtonNo = 1;
            }
        }

        private void InputTextBox_Load(object sender, EventArgs e)
        {
            Text = caption;
            lblTitle.Text = title;
            tbText.Text = TextValue;

            for (int i = 0; i < buttons.Length; i++)
                AddMessageBoxButton(buttons[i], i > 0, i == defaultButtonNo - 1);

            cbVerification.Visible = !string.IsNullOrEmpty(VerificationText);
            if (cbVerification.Visible)
                cbVerification.Text = VerificationText;

            int customWidth = -6 + 20 + (cbVerification.Visible ? cbVerification.Width + 10 : 0);

            foreach (var control in bottomPanel.Controls)
                if (control is Button)
                    customWidth += 80;

            if (ClientSize.Width < customWidth)
                Width = customWidth + Width - ClientSize.Width;

            CustomResult = CustomButtonResult.None;
        }

        private void AddMessageBoxButton(CustomMessageBoxButton button, bool addBreak, bool select)
        {
            if (addBreak)
                bottomPanel.Controls.Add(new Label()
                {
                    Dock = DockStyle.Right,
                    AutoSize = false,
                    Width = 6
                });

            Button newButton = new Button()
            {
                Dock = DockStyle.Right,
                Width = 74,
                Text = button.Text,
                Tag = button.Result
            };
            newButton.Click += ButtonClick;
            bottomPanel.Controls.Add(newButton);

            if (select)
                newButton.Select();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            CustomResult = (CustomButtonResult)(sender as Button).Tag;
            Close();
        }

        public static CustomButtonResult Show(string caption, string title, ref string textValue, CustomMessageBoxButton[] buttons = null, int defaultButtonNo = 0, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (InputTextBox inputTextBox = new InputTextBox(textValue, title, caption, buttons, defaultButtonNo) { StartPosition = startPosition })
            {
                inputTextBox.ShowDialog();
                textValue = inputTextBox.TextValue;
                return inputTextBox.CustomResult;
            }
        }

        public static CustomButtonResult Show(IWin32Window owner, string caption, string title, ref string textValue, CustomMessageBoxButton[] buttons = null, int defaultButtonNo = 0, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (InputTextBox inputTextBox = new InputTextBox(textValue, title, caption, buttons, defaultButtonNo) { StartPosition = startPosition })
            {
                inputTextBox.ShowDialog(owner);
                textValue = inputTextBox.TextValue;
                return inputTextBox.CustomResult;
            }
        }

    }

}
