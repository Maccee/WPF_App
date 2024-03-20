using System.Text;
using System.Windows;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HelloWorldApp;


public partial class MainWindow : Window
{

    public MainWindow()
    {

        InitializeComponent();
    }
    public class ImageData
    {
        public string? Url { get; set; }
        public string? Name { get; set; }
    }


    private async void LoadImagesButtonClick(object sender, RoutedEventArgs e)
    {
        string baseUrl = "https://api.hel.fi/linkedevents/v1/image/";
        string query = $"?text={inputTextBox.Text}";

        using (HttpClient client = new HttpClient())
        using (HttpResponseMessage response = await client.GetAsync(baseUrl + query))
        using (HttpContent content = response.Content)
        {
            string result = await content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(result);
                var imagesData = new List<ImageData>();

                foreach (var item in data.data)
                {
                    if (imagesData.Count >= 50) // Limit to 10 images
                    {
                        break; // Exit the loop once we have 10 images
                    }

                    imagesData.Add(new ImageData
                    {
                        Url = item.url,
                        Name = item.name
                    });
                }

                imagesControl.ItemsSource = imagesData;
            }
        }
    }
    private void QuitMenuItem_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
{
    AboutWindow aboutWindow = new AboutWindow();
    aboutWindow.ShowDialog(); // Show the About window as a dialog
}





}