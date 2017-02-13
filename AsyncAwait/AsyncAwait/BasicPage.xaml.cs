using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AsyncAwait
{
    public partial class BasicPage : ContentPage
    {
        public BasicPage()
        {
            InitializeComponent();
        }

        int clicks;

        private async void ButtonClicked(object sender, EventArgs e)
        {
            if (sender == AsyncButton)
            {
                TaskStatusLabel.Text = "Tarea iniciada";
                await EjecutaTareaAsincrona();
                TaskStatusLabel.Text = "Tarea terminada";
            }
            else if (sender == NormalButton)
            {
                TaskStatusLabel.Text = "Tarea iniciada";
                EjecutaTarea();
                TaskStatusLabel.Text = "Tarea terminada";
            }
            else if (sender == CountButton)
            {
                CountButton.Text = "Clicks: " + (++clicks);
            }
        }

        void EjecutaTarea()
        {
            CountLabel.Text = "0";
            ProgressBar.Progress = 0;
            for (int i = 0; i < 100; i++)
            {
                Task.Delay(100).Wait(); // Retardo
                CountLabel.Text = (i + 1).ToString();
                ProgressBar.Progress = ((double)i + 1) / 100;
            }
        }

        async Task EjecutaTareaAsincrona()
        {
            
            CountLabel.Text = "0";
            ProgressBar.Progress = 0;
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Delay(100).Wait();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        CountLabel.Text = (i + 1).ToString();
                        ProgressBar.Progress = ((double)i + 1) / 100;
                    });
                }
            });
        }
    }
}
