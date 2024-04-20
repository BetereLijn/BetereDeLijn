using System.Reflection.Emit;
using System;
using System.Text.Json;
using System.Net.Http;

namespace BetereDeLijn_Mobile
{
    public partial class MainPage : ContentPage
    {
        HttpClient client = new HttpClient();
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private async void testprocedure(object sender, EventArgs e)
        {
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c94acbf3136844b08c1fa4281955241b");
            //hier komt de url
            var uri = "https://api.delijn.be/DLKernOpenData/api/v1/api";

            var response = await client.GetStringAsync(uri);
            using (JsonDocument document = JsonDocument.Parse(response))
            {
                test.Text = "";
                JsonElement root = document.RootElement;
                JsonElement links = root.GetProperty("links");
                foreach (JsonElement link in links.EnumerateArray())
                {
                    test.Text += link.GetProperty("url");
                }
            }
        }
    }

}
