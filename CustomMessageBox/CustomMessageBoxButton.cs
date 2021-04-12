namespace CustomDialogs
{
    public class CustomMessageBoxButton
    {
        public string Text { get; set; }
        public CustomButtonResult Result { get; set; }

        public CustomMessageBoxButton(CustomButtonResult result, string text = null)
        {
            this.Result = result;
            this.Text = !string.IsNullOrEmpty(text) ? text : result.ToString();
        }

    }
}