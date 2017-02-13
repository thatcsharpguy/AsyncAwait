using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AsyncAwait
{
    public partial class AdvancedPage : ContentPage, IProgress<double>
    {
        public AdvancedPage()
        {
            InitializeComponent();
        }

        private async void ButtonClicked(object sender, EventArgs e)
        {
            TaskStatusLabel.Text = "Tarea iniciada";
            ProgressBar.Progress = 0;

            var tarea = EjecutaTareaAsincrona();

            if (sender == ErrorButton)
            {
                await tarea.ConfigureAwait(false);
            }
            else
            {
                await tarea;
            }

            TaskStatusLabel.Text = "Tarea terminada";
        }

        async Task EjecutaTareaAsincrona()
        {
            ProgressBar.Progress = 0;
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Delay(100).Wait();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ProgressBar.Progress = ((double)i + 1) / 100;
                    });
                }
            });
        }


        async Task EjecutaTareaAsincrona(IProgress<double> progress)
        {
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Delay(100).Wait();
                    progress.Report(((double)i + 1) / 100);
                }
            });
        }

        private async void ProgressButtonClicked(object sender, EventArgs e)
        {
            ProgressBar.Progress = 0;
            await EjecutaTareaAsincrona(this);
        }

        public void Report(double value)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ProgressBarWithProgress.Progress = value;
            });
        }


        private async void FromInternetClicked(object sender, EventArgs e)
		{
			ProgressBar.Progress = 0;
			var client = new HttpClient();
			FromInternetLabel.Text = "Ejecutando...";
			var lorem = await client.GetStringAsync("http://loripsum.net/api/1/short/plaintext/prude");
			FromInternetLabel.Text = lorem;
		}
    }
}
