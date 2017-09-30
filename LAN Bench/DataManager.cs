using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace LANBench
{
    class DataManager
    {
        internal string sourcePath1, sourcePath2, sourcePathLocal, destTempPath, logPath, errorPath, rootSP1, rootSP2 = string.Empty;
        internal FileInfo fi_SP1, fi_SP2, fi_SPL;
        internal bool isSP1, isSP2, isSPL = false;

        public void Init()
        {
            sourcePath1 = @ConfigurationManager.AppSettings["sourcePath1"] + ConfigurationManager.AppSettings["fileName"];
            sourcePath2 = @ConfigurationManager.AppSettings["sourcePath2"] + ConfigurationManager.AppSettings["fileName"];
            sourcePathLocal = Application.StartupPath + @"\Files\" + ConfigurationManager.AppSettings["fileName"];
            destTempPath = Path.GetTempPath() + ConfigurationManager.AppSettings["fileName"];

            logPath = Path.GetTempPath() + "LANBench.log";
            errorPath = Path.GetTempPath() + "LANBench_error.log";

            File.WriteAllText(errorPath, sourcePath1 + Environment.NewLine
                + sourcePath2 + Environment.NewLine
                + sourcePathLocal + Environment.NewLine
                + destTempPath + Environment.NewLine);

            CheckFiles();
            PrepareFiles();
        }

        private void CheckFiles()
        {
            if (File.Exists(destTempPath)) File.Delete(destTempPath);
            if (File.Exists(sourcePath1 + ".test")) File.Delete(sourcePath1 + ".test");
            if (File.Exists(sourcePath2 + ".test")) File.Delete(sourcePath2 + ".test");

            rootSP1 = @"\\Source1\";
            rootSP2 = @"\\Source2\";

            if (!rootSP1.StartsWith(@"\\") && !rootSP1.StartsWith(@"\\"))
            {
                MessageBox.Show("Aucun emplacement réseau n'a été déclaré", "Erreur - Emplacement réseau", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            isSP1 = File.Exists(sourcePath1);
            isSP2 = File.Exists(sourcePath2);
            isSPL = (File.Exists(sourcePathLocal) && (isSP1 || isSP2));

            if (!isSP1 && !isSP2 && !isSPL)
            {
                MessageBox.Show("Aucun des fichiers nécessaires au test existe, l'application va être fermée", "Erreur - Fichiers introuvables", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }

        private void PrepareFiles()
        {
            fi_SP1 = new FileInfo(sourcePath1);
            fi_SP2 = new FileInfo(sourcePath2);
            fi_SPL = new FileInfo(sourcePathLocal);

            if (rootSP1.StartsWith(@"\\")) rootSP1 = @"\\" + rootSP1.Split('\\')[2];
            if (rootSP2.StartsWith(@"\\")) rootSP2 = @"\\" + rootSP2.Split('\\')[2];
        }
    }
}
