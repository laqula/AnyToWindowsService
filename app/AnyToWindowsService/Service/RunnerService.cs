using AnyToWindowsService.Settings;
using Serilog;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Topshelf;

namespace AnyToWindowsService.Service
{
    public class RunnerService : ServiceControl
    {
        private BackgroundWorker backgroundWorker;
        private AppSettings settings;
        private bool isExecuting = false;

        public RunnerService(AppSettings settings)
        {
            this.settings = settings;
            backgroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };
        }

        private void DoWork(object? sender, DoWorkEventArgs e)
        {
            DateTime? lastRun = null;

            while (true)
            {
                if (backgroundWorker.CancellationPending)
                {
                    Log.Debug("Cancellation");
                    break;
                }

                if (!lastRun.HasValue || DateTime.Now >= lastRun.Value.AddSeconds(settings.intervalInSec ?? 0))
                {
                    Log.Debug("Time to execute command.");
                    lastRun = DateTime.Now;
                    if (!isExecuting)
                    {
                        isExecuting = true;
                        Log.Debug("Worker not busy.");
                        RunCommand(settings);
                        isExecuting = false;
                    }
                }

                if (settings.runOnlyOnce == true)
                {
                    Log.Debug("Running only once.");
                    break;
                }

                Thread.Sleep(1000);
            }
        }

        private void RunCommand(AppSettings settings)
        {
            if (settings is null) throw new ArgumentException();
            if (string.IsNullOrEmpty(settings.command)) throw new ArgumentException();

            Log.Debug($"Executing command: cmd.exe /s /c \"{settings.command}\"");
            var process = Process.Start("cmd.exe", $" /s /c \"{settings.command}\"");
            process.WaitForExit();
        }

        public bool Start(HostControl hostControl)
        {
            if (settings.runOnlyOnce != true && settings.intervalInSec is null) throw new ArgumentException();

            Log.Information("Starting");
            backgroundWorker.DoWork += DoWork;
            backgroundWorker.RunWorkerAsync();
            
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            backgroundWorker.CancelAsync();

            if (backgroundWorker.IsBusy)
            {
                var cancellTime = DateTime.Now;
                while (backgroundWorker.IsBusy)
                {
                    Log.Debug("Waiting while worker busy.");
                    Thread.Sleep(1000);
                    if (DateTime.Now > cancellTime.AddSeconds(settings.maxWaitForFinishInSec ?? 0))
                    {
                        Log.Debug("Max waiting time for finish exceeded.");
                        break;
                    }
                }
            }

            backgroundWorker.Dispose();

            Log.Information("Stopping");
            return true;
        }
    }
}