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

		private void ButtonClicked(object sender, EventArgs e)
		{
			CountButton.Text = "Clicks: " + (++clicks);
		}

		private void BlockingClicked(object sender, EventArgs e)
		{
			TaskStatusLabel.Text = "Tarea bloqueante iniciada";
			EjecutaTarea();
			TaskStatusLabel.Text = "Tarea bloqueante terminada";
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

		private void FireAndForgetClicked(object sender, EventArgs e)
		{
			TaskStatusLabel.Text = "Tarea f-a-f iniciada";
			EjecutaTareaAsincrona();
			TaskStatusLabel.Text = "Tarea f-a-f terminada";
		}

		void EjecutaTareaAsincrona()
		{
			CountLabel.Text = "0";
			ProgressBar.Progress = 0;
			Task.Run(() =>
			{
				for (int i = 0; i < 100; i++)
				{
					Task.Delay(100).Wait(); // Retardo
					Device.BeginInvokeOnMainThread(() =>
					{
						CountLabel.Text = (i + 1).ToString();
						ProgressBar.Progress = ((double)i + 1) / 100;
					});
				}
			});
		}

		private async void AsyncClicked(object sender, EventArgs e)
		{
			TaskStatusLabel.Text = "Tarea asíncrona iniciada";
			await EjecutaTareaAsync();
			TaskStatusLabel.Text = "Tarea asíncrona terminada";
		}

		async Task EjecutaTareaAsync()
		{
			CountLabel.Text = "0";
			ProgressBar.Progress = 0;
			await Task.Run(() =>
			{
				for (int i = 0; i < 100; i++)
				{
					Task.Delay(100).Wait(); // Retardo
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
