using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CustomDialogs
{
    public partial class CustomMessageBox : Form
    {
        private readonly bool isStandart;
        private readonly string text;
        private readonly string title;
        private readonly string caption;
        private readonly Bitmap icon;
        private readonly CustomMessageBoxButton[] buttons;
        private readonly int defaultButtonNo;
        private readonly SystemSound systemSound;

        public string VerificationText { get; set; }
        public bool IsVerificationChecked
        {
            get { return cbVerification.Checked; }
            set { cbVerification.Checked = value; }
        }

        public CustomButtonResult CustomResult { get; private set; }

        #region constructors

        public CustomMessageBox(string text, string title, string caption, MessageBoxIcon icon, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            InitializeComponent();
            isStandart = true;

            this.text = text;
            this.title = title;
            this.caption = caption;
            this.icon = GetSystemIcon(icon);
            this.systemSound = GetSystemSound(icon);
            this.buttons = GetCustomMessageBoxButtons(buttons);
            this.defaultButtonNo = GetDefaultButtonNo(defaultButton);
            if (this.buttons == null || this.buttons.Length == 0)
                this.buttons = new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.OK) };
        }

        public CustomMessageBox(string text, string title, string caption, Bitmap icon, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            InitializeComponent();
            isStandart = true;

            this.text = text;
            this.title = title;
            this.caption = caption;
            this.icon = icon;
            this.buttons = GetCustomMessageBoxButtons(buttons);
            this.defaultButtonNo = GetDefaultButtonNo(defaultButton);
            if (this.buttons == null || this.buttons.Length == 0)
                this.buttons = new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.OK) };
        }

        public CustomMessageBox(string text, string title, string caption, MessageBoxIcon icon, CustomMessageBoxButton[] buttons, int defaultButtonNo)
        {
            InitializeComponent();
            isStandart = false;

            this.text = text;
            this.title = title;
            this.caption = caption;
            this.icon = GetSystemIcon(icon);
            this.systemSound = GetSystemSound(icon);
            this.buttons = buttons;
            this.defaultButtonNo = defaultButtonNo;
            if (this.buttons == null || this.buttons.Length == 0)
                this.buttons = new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.OK) };
        }

        public CustomMessageBox(string text, string title, string caption, Bitmap icon, CustomMessageBoxButton[] buttons, int defaultButtonNo)
        {
            InitializeComponent();
            isStandart = false;

            this.text = text;
            this.title = title;
            this.caption = caption;
            this.icon = icon;
            this.buttons = buttons;
            this.defaultButtonNo = defaultButtonNo;
            if (this.buttons == null || this.buttons.Length == 0)
                this.buttons = new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.OK) };
        } 
        #endregion

        #region baseLogic

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
            ShowCore();
        }

        private void ShowCore()
        {
            if (icon != null)
                lblIcon.Image = icon;

            Text = caption;
            lblTitle.Visible = !string.IsNullOrEmpty(title);
            if (lblTitle.Visible)
                lblTitle.Text = title;
            lblContent.Text = text;

            for (int i = 0; i < buttons.Length; i++)
                AddMessageBoxButton(buttons[i], i > 0, i == defaultButtonNo - 1);

            cbVerification.Visible = !string.IsNullOrEmpty(VerificationText);
            if (cbVerification.Visible)
                cbVerification.Text = VerificationText;

            lblTitle.Left = lblContent.Left = (icon != null) ? 48 : 10;

            int customHeight = lblContent.Height + (lblTitle.Visible ? lblTitle.Height + 10 : 0) + 20 + bottomPanel.Height;
            if (ClientSize.Height < customHeight)
                Height = customHeight + Height - ClientSize.Height;

            int customWidth = -6 + 20 + (cbVerification.Visible ? cbVerification.Width + 10 : 0);

            foreach (var control in bottomPanel.Controls)
                if (control is Button)
                    customWidth += 80;

            if (lblTitle.Visible)
                if (customWidth < lblTitle.Left + lblTitle.Width + 10)
                    customWidth = lblTitle.Left + lblTitle.Width + 10;
            if (customWidth < lblContent.Left + lblContent.Width + 10)
                customWidth = lblContent.Left + lblContent.Width + 10;

            if (ClientSize.Width < customWidth)
                Width = customWidth + Width - ClientSize.Width;

            int customPos = (mainPanel.Height - 20 - (lblTitle.Visible ? lblTitle.Height : 0) - lblContent.Height) / 2;
            lblContent.Top = 10 + (lblTitle.Visible ? lblTitle.Height : 0) + customPos;

            if (icon != null)
                lblIcon.Height = lblTitle.Visible ? lblTitle.Height + 30 : mainPanel.Height;

            CustomResult = CustomButtonResult.None;
        }

        private void CustomMessageBox_Shown(object sender, EventArgs e)
        {
            systemSound?.Play();
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
            if (isStandart)
                DialogResult = (DialogResult) ((Button) sender).Tag;
            else
            {
                CustomResult = (CustomButtonResult) ((Button)sender).Tag;
                Close();
            }
        }

        private Bitmap GetSystemIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Question:
                    return SystemIcons.Question.ToBitmap();
                case MessageBoxIcon.Warning:
                    return SystemIcons.Warning.ToBitmap();
                case MessageBoxIcon.Error:
                    return SystemIcons.Error.ToBitmap();
                case MessageBoxIcon.Information:
                    return SystemIcons.Information.ToBitmap();
                default:
                    return null;
            }
        }

        private SystemSound GetSystemSound(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Question:
                    return SystemSounds.Question;
                case MessageBoxIcon.Exclamation:
                    return SystemSounds.Exclamation;
                case MessageBoxIcon.Asterisk:
                    return SystemSounds.Asterisk;
                case MessageBoxIcon.Hand:
                    return SystemSounds.Hand;
                default:
                    return null;
            }
        }

        private CustomMessageBoxButton[] GetCustomMessageBoxButtons(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    return new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.OK) };
                case MessageBoxButtons.OKCancel:
                    return new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.OK), new CustomMessageBoxButton(CustomButtonResult.Cancel) };
                case MessageBoxButtons.AbortRetryIgnore:
                    return new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.Abort), new CustomMessageBoxButton(CustomButtonResult.Retry), new CustomMessageBoxButton(CustomButtonResult.Ignore) };
                case MessageBoxButtons.YesNoCancel:
                    return new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.Yes), new CustomMessageBoxButton(CustomButtonResult.No), new CustomMessageBoxButton(CustomButtonResult.Cancel) };
                case MessageBoxButtons.YesNo:
                    return new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.Yes), new CustomMessageBoxButton(CustomButtonResult.No) };
                case MessageBoxButtons.RetryCancel:
                    return new CustomMessageBoxButton[] { new CustomMessageBoxButton(CustomButtonResult.Retry), new CustomMessageBoxButton(CustomButtonResult.Cancel) };
                default:
                    return null;
            }
        }

        private int GetDefaultButtonNo(MessageBoxDefaultButton defaultButton)
        {
            switch (defaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    return 1;
                case MessageBoxDefaultButton.Button2:
                    return 2;
                case MessageBoxDefaultButton.Button3:
                    return 3;
                default:
                    return 1;
            }
        }

        #endregion

        public static DialogResult Show(string text, string title, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, defaultButton) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, string title, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, MessageBoxDefaultButton.Button1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, string title, string caption, MessageBoxButtons buttons,
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, MessageBoxIcon.None, buttons, MessageBoxDefaultButton.Button1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, string title, string caption, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, MessageBoxIcon.None, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, String.Empty, String.Empty, MessageBoxIcon.None, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, string title, string caption, CustomMessageBoxButton[] buttons, MessageBoxIcon icon,
            int defaultButtonNo, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, defaultButtonNo) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, string title, string caption, CustomMessageBoxButton[] buttons, MessageBoxIcon icon,
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, 1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, string title, string caption, CustomMessageBoxButton[] buttons,
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, MessageBoxIcon.None, buttons, 1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, string title, string caption, CustomMessageBoxButton[] buttons, Bitmap icon,
            int defaultButtonNo, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, defaultButtonNo) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(string text, string title, string caption, CustomMessageBoxButton[] buttons, Bitmap icon,
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, 1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog();
        }

        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, defaultButton) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, MessageBoxDefaultButton.Button1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, MessageBoxButtons buttons, 
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, MessageBoxIcon.None, buttons, MessageBoxDefaultButton.Button1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, MessageBoxIcon.None, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, String.Empty, String.Empty, MessageBoxIcon.None, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, CustomMessageBoxButton[] buttons, MessageBoxIcon icon,
            int defaultButtonNo, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, defaultButtonNo) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }
    
        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, CustomMessageBoxButton[] buttons, MessageBoxIcon icon,
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, 1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, CustomMessageBoxButton[] buttons,
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, MessageBoxIcon.None, buttons, 1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, CustomMessageBoxButton[] buttons, Bitmap icon,
            int defaultButtonNo, FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, defaultButtonNo) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, string title, string caption, CustomMessageBoxButton[] buttons, Bitmap icon,
            FormStartPosition startPosition = FormStartPosition.WindowsDefaultLocation)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(text, title, caption, icon, buttons, 1) { StartPosition = startPosition })
                return customMessageBox.ShowDialog(owner);
        }

    }

}
