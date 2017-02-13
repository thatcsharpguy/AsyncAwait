using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AsyncAwait
{
    public partial class ParallelPage : ContentPage
    {
        public ParallelPage()
        {
            InitializeComponent();
        }



        private async void ButtonClicked(object sender, EventArgs e)
        {
            TaskStatusLabel.Text = "Tarea iniciada";
            ProgressBar1.Progress = 0;
            ProgressBar2.Progress = 0;
            if (sender == ParallelButton)
            {
                var t1 = EjecutaTareaAsincrona1();
                var t2 = EjecutaTareaAsincrona2();

                await Task.WhenAll(t1, t2);
            }
            else if (sender == SequentialButton)
            {
                 await EjecutaTareaAsincrona1();
                 await EjecutaTareaAsincrona2();
            }
            TaskStatusLabel.Text = "Tarea terminada";
        }

        async Task EjecutaTareaAsincrona1()
        {
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Delay(100).Wait();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ProgressBar1.Progress = ((double)i + 1) / 100;
                    });
                }
            });
        }

        async Task EjecutaTareaAsincrona2()
        {
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Delay(100).Wait();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ProgressBar2.Progress = ((double)i + 1) / 100;
                    });
                }
            });
        }
    }
}
