using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyOldButNewApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                toolStripProgressBar1.Value = i;
                Thread.Sleep(100);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Invoke(new MethodInvoker(delegate { toolStripProgressBar1.Value = i; }));
                    Thread.Sleep(100);
                }
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            cts = new CancellationTokenSource();

            ((Control)sender).Enabled = false;

            var t1 = Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    //if (i == 33)
                    //    throw new ExecutionEngineException();

                    Task.Factory.StartNew(() => { toolStripProgressBar1.Value = i; }, CancellationToken.None, TaskCreationOptions.None, ts);
                    Thread.Sleep(20);

                    if (cts.IsCancellationRequested)
                        break;
                }

                Task.Factory.StartNew(() => { ((Control)sender).Enabled = true; }, CancellationToken.None, TaskCreationOptions.None, ts);
            });

            t1.ContinueWith(t => { MessageBox.Show("Task ist beendet"); }); // kommt immer
            t1.ContinueWith(t => { MessageBox.Show("Task erfolgreich beendet"); }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, ts); // nur wenn ok
            t1.ContinueWith(t => { MessageBox.Show($"Task ERROR: {t.Exception.InnerException.Message}"); }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, ts); // nur wenn exception

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            ((Control)sender).Enabled = false;
            cts = new CancellationTokenSource();

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    toolStripProgressBar1.Value = i;
                    await Task.Delay(100, cts.Token);
                }
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show("Erfolgreich abgebrochen");
            }

            ((Control)sender).Enabled = true;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            ((Control)sender).Enabled = false;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            //MessageBox.Show($"Alte Function: {string.Join("\n", MyOldFunction(4))}");
            try
            {
                MessageBox.Show($"Alte Function: {string.Join("\n", await MyOldFunctionAsync(4))}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Schade: {ex.Message}");
            }
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            ((Control)sender).Enabled = true;

        }

        private async Task<IEnumerable<long>> MyOldFunctionAsync(int zahl)
        {
            return await Task.Run(() => MyOldFunction(zahl).ToList());
        }

        private IEnumerable<long> MyOldFunction(int zahl)
        {
            Thread.Sleep(5000);
            yield return zahl * DateTime.Now.Millisecond;
            yield return zahl * DateTime.Now.Second;
            throw new ExecutionEngineException();
            yield return zahl * DateTime.Now.Minute;
            yield return zahl * DateTime.Now.Hour;
        }

        CancellationTokenSource cts;
        private void button6_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }

        private void booksClient1_Load(object sender, EventArgs e)
        {

        }
    }
}
