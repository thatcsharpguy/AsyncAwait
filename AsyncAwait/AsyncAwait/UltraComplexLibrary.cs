using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
	public class UltraComplexLibrary
	{
		public static async Task SlowTask(IProgress<double> progress, CancellationToken token)
		{
			while (true)
			{
				for (int i = 0; i < 100; i++)
				{
					await Task.Delay(10);
					if (token.IsCancellationRequested)
						return;
					progress.Report(i / 100d);
				}
				for (int i = 100; i > 0; i--)
				{
					await Task.Delay(10);
					token.ThrowIfCancellationRequested();
					progress.Report(i / 100d);
				}
			}
		}


		public static async Task SlowTask(IProgress<double> progress)
		{
			while (true)
			{
				for (int i = 0; i < 100; i++)
				{
					await Task.Delay(10);
					progress.Report(i / 100d);
				}
				for (int i = 100; i > 0; i--)
				{
					await Task.Delay(10);
					progress.Report(i / 100d);
				}
			}
		}
	}
}
