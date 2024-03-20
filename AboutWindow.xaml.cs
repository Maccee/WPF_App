using System.Windows; // For Window, RoutedEventArgs, etc.
// Add any other necessary namespaces here

namespace HelloWorldApp
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
