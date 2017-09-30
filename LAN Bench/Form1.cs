using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace LANBench
{
    public partial class Form1 : Form
    {
        private NetworkInterface activeInterface;
        private Networker networker;
        private DataManager dm;
        private BenchManager bm;
        private delegate void uiUpdate();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            networker = new Networker();
            networker.Init();

            if (networker.upInterfaces.Length > 0) activeInterface = networker.upInterfaces[0];
            else
            {
                MessageBox.Show("Aucune interface réseau n'a été trouvée, l'application va être fermée", "Erreur - Interfaces réseau", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            dm = new DataManager();
            dm.Init();

            bm = new BenchManager();

            initForm();
            Process.Start("NCPA.cpl");
            Process.Start("taskmgr.exe");
            Thread.Sleep(1000);
            benchStatus.Text = "Prêt à démarrer";
        }

        private void initForm()
        {
            tbNetworkInfos.Text = networker.wlanInfos;
            tbNetworkInfos.Select(tbNetworkInfos.TextLength, 0);

            tbResults.Select(tbResults.TextLength, 0);
            tbResults.ScrollToCaret();

            if (!bm.isRunning)
            {
                bTest1.Text = dm.rootSP1 + " => PC";
                bTest2.Text = dm.rootSP2 + " => PC";
                bTest3.Text = "PC => " + dm.rootSP1;
                bTest4.Text = "PC => " + dm.rootSP2;

                bTest1.Enabled = (dm.isSP1 && dm.rootSP1.StartsWith(@"\\"));
                bTest2.Enabled = (dm.isSP2 && dm.rootSP2.StartsWith(@"\\"));
                bTest3.Enabled = (dm.isSPL && dm.isSP1 && dm.rootSP1.StartsWith(@"\\"));
                bTest4.Enabled = (dm.isSPL && dm.isSP2 && dm.rootSP2.StartsWith(@"\\"));
            }
        }

        private string getWlanInfos()
        {
            string results = string.Empty;

            try { results = networker.wlanInfos; }
            catch { MessageBox.Show("Une erreur est survenue pendant la récupération des informations du réseau", "Erreur - Informations réseau", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            return results;
        }

        private void prepareBench(int cas)
        {
            long fs = 0;

            bTest1.Enabled = bTest2.Enabled = bTest3.Enabled = bTest4.Enabled = false;
            if (tbResults.Lines.Length > 1) tbResults.AppendText(Environment.NewLine);

            switch (cas)
            {
                case 1:
                    fs = dm.fi_SP1.Length / 1024 / 1024;
                    bTest1.Text = "Test en cours...";
                    tbResults.AppendText(string.Format("{0} => PC", dm.rootSP1));
                    bm.init(dm.sourcePath1, dm.destTempPath, true, activeInterface);
                    break;
                case 2:
                    fs = dm.fi_SP2.Length / 1024 / 1024;
                    bTest2.Text = "Test en cours...";
                    tbResults.AppendText(string.Format("{0} => PC", dm.rootSP2));
                    bm.init(dm.sourcePath2, dm.destTempPath, true, activeInterface);
                    break;
                case 3:
                    fs = dm.fi_SP1.Length / 1024 / 1024;
                    bTest3.Text = "Test en cours...";
                    tbResults.AppendText(string.Format("PC => {0}", dm.rootSP1));
                    bm.init(dm.destTempPath, dm.sourcePath1, false, activeInterface);
                    break;
                case 4:
                    fs = dm.fi_SP2.Length / 1024 / 1024;
                    bTest4.Text = "Test en cours...";
                    tbResults.AppendText(string.Format("PC => {0}", dm.rootSP2));
                    bm.init(dm.destTempPath, dm.sourcePath2, false, activeInterface);
                    break;
            }

            tbResults.AppendText(string.Format(" - Fichier de {0} Mo :{1}", (fs).ToString("0 000"), Environment.NewLine));
            tbResults.AppendText("Test en cours...");
        }

        private void timerTick(object state)
        {
            bm.updateStats();

            pbBench.Invoke(new uiUpdate(() => pbBench.Value = bm.pbPct));
            stripBench.Invoke(new uiUpdate(() => benchStatus.Text = "Statut : " + bm.status));
            tbNetworkInfos.Invoke(new uiUpdate(() => tbNetworkInfos.Text = getWlanInfos()));

            bm.bytesReceived = activeInterface.GetIPStatistics().BytesReceived;
            bm.bytesSent = activeInterface.GetIPStatistics().BytesSent;
            bm.dt = DateTime.Now;
        }

        private void updateResults(TimeSpan sw, long fs)
        {
            string resultSeconds = sw.TotalSeconds.ToString("#");
            string resultSpeed = (fs / sw.TotalSeconds).ToString("0.00");
            string resultBandwidth = (8 * fs / sw.TotalSeconds).ToString("0.00");

            tbResults.Lines = tbResults.Lines.Where(line => !line.Contains("Test en cours...")).ToArray();
            tbResults.AppendText(Environment.NewLine);

            benchStatus.Text = string.Format("Statut : Test terminé en {0} s ", resultSeconds);
            tbResults.AppendText(string.Format(" => Résultat : {0} s, soit {1} Mo/s ({2} Mb/s){3}", resultSeconds, resultSpeed, resultBandwidth, Environment.NewLine));

            initForm();
        }

        private async void bTest1_Click(object sender, EventArgs e)
        {
            prepareBench(1);
            System.Threading.Timer t = new System.Threading.Timer(timerTick, null, 0, 1000);
            await bm.bench();
            t.Dispose();
            updateResults(bm.result, bm.fileSize);
        }

        private async void bTest2_Click(object sender, EventArgs e)
        {
            prepareBench(2);
            System.Threading.Timer t = new System.Threading.Timer(timerTick, null, 0, 1000);
            await bm.bench();
            t.Dispose();
            updateResults(bm.result, bm.fileSize);

        }

        private async void bTest3_Click(object sender, EventArgs e)
        {
            prepareBench(3);
            System.Threading.Timer t = new System.Threading.Timer(timerTick, null, 0, 1000);
            await bm.bench();
            t.Dispose();
            updateResults(bm.result, bm.fileSize);
        }

        private async void bTest4_Click(object sender, EventArgs e)
        {
            prepareBench(4);
            System.Threading.Timer t = new System.Threading.Timer(timerTick, null, 0, 1000);
            await bm.bench();
            t.Dispose();
            updateResults(bm.result, bm.fileSize);
        }

        private void tbWlanInfos_DoubleClick(object sender, EventArgs e)
        {
            initForm();
        }

        private void tbResults_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText(dm.logPath, tbResults.Text);
        }

        private void tbResults_DoubleClick(object sender, EventArgs e)
        {
            if (File.Exists(dm.logPath) && !bm.isRunning) Process.Start("notepad.exe", dm.logPath);
        }
        private void statusStrip1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process.Start("notepad.exe", dm.errorPath);
        }
    }
}
