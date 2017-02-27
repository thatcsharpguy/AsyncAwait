using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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

		public void Report(double value)
		{
			Device.BeginInvokeOnMainThread(() => 
			{
				ProgressBar.Progress = value;
				IntegerReport.Text = (value * 100).ToString();
			});
		}

		CancellationTokenSource CancellationTokenSource;

		async void StartTaskClicked(object sender, System.EventArgs e)
		{
			CancellationTokenSource = new CancellationTokenSource();
			var task = UltraComplexLibrary.SlowTask(this, CancellationTokenSource.Token);
			try
			{
				await task;
			}
			catch (OperationCanceledException op)
			{
				
			}
			finally
			{
				IntegerReport.Text = task.Status.ToString();
			}
		}


		async void StartTimedTaskClicked(object sender, System.EventArgs e)
		{
			CancellationTokenSource = new CancellationTokenSource();
			CancellationTokenSource.CancelAfter(3000);
			var task = UltraComplexLibrary.SlowTask(this, CancellationTokenSource.Token);
			await task;
			if (task.Exception != null)
			{
				IntegerReport.Text = "Excepción";
			}
		}

		void StopTaskClicked(object sender, System.EventArgs e)
		{
			CancellationTokenSource.Cancel();
		}

    }
}
