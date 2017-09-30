using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANBench
{
    class BenchManager
    {
        internal Stopwatch chrono;
        internal long bytesReceived, bytesSent, bytesTransfered, fileSize;
        internal bool isRunning = false;
        internal string status;
        internal TimeSpan result;
        internal int pbPct;
        internal DateTime dt;

        private bool _isReceive = false;
        private string _source, _destination;
        private NetworkInterface _activeInterface;

        public void init(string source, string destination, bool isReceive, NetworkInterface ni)
        {
            _isReceive = isReceive;
            _source = source;
            _destination = destination;
            _activeInterface = ni;
            chrono = new Stopwatch();

            FileInfo fi = new FileInfo(source);
            fileSize = fi.Length / 1024 / 1024;
        }

        internal async Task bench()
        {
            Task<TimeSpan> mytask = new Task<TimeSpan>(() => copyTask());
            mytask.Start();
            await mytask;
            mytask.Dispose();
            result = mytask.Result;
        }

        private TimeSpan copyTask()
        {
            try
            {
                chrono.Restart();

                bytesReceived = _activeInterface.GetIPStatistics().BytesReceived;
                bytesSent = _activeInterface.GetIPStatistics().BytesSent;

                isRunning = true;
                bytesTransfered = _isReceive ? bytesReceived : bytesSent;
                File.Copy(_source, _destination, true);

                chrono.Stop();
                isRunning = false;
            }
            catch { MessageBox.Show("Une erreur est survenue pendant la phase de copie", "Erreur - Copie du fichier", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            return chrono.Elapsed;
        }

        public void updateStats()
        {
            string bandwidth = string.Empty;
            long pct, transferredT = 0;

            long brNew = _activeInterface.GetIPStatistics().BytesReceived;
            long bsNew = _activeInterface.GetIPStatistics().BytesSent;
            long transferredR = (brNew - bytesReceived) / 1024 / 1024;
            long transferredS = (bsNew - bytesSent) / 1024 / 1024;
            double elapsed = (DateTime.Now - dt).TotalSeconds;
            string elapsedBench = chrono.Elapsed.TotalSeconds.ToString("#");

            if (_isReceive)
            {
                transferredT = (brNew - bytesTransfered) / 1024 / 1024;
                bandwidth = (transferredR / elapsed * 8).ToString("0.00");
            }
            else
            {
                transferredT = (bsNew - bytesTransfered) / 1024 / 1024;
                bandwidth = (transferredS / elapsed * 8).ToString("0.00");
            }

            status = string.Format("test démarré depuis {0} seconde", elapsedBench);
            status += chrono.Elapsed.TotalSeconds > 2 ? "s" : "";

            if (fileSize > 0)
            {
                pct = transferredT * 100 / fileSize / (long)1.03;
                pbPct = (int)pct;
            }

            if (isRunning) status += string.Format(" ({0} Mb/s)", bandwidth);

            if (pbPct > 100) pbPct = 100;
        }
    }
}
