using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace LANBench
{
    class Networker
    {
        public NetworkInterface[] upInterfaces { get => _upInterfaces; }
        public string wlanInfos { get => _wlanInfosToString(_getWlanInfos()); }
        public List<Dictionary<string, string>> wlanStructuredInfos { get => _getWlanInfos(); }
        private static NetworkInterface[] _upInterfaces, _interfaces;

        public void Init()
        {
            Refresh();
        }

        public void Refresh()
        {
            _interfaces = NetworkInterface.GetAllNetworkInterfaces();
            _upInterfaces = _interfaces.Where(ni =>
                (ni.OperationalStatus == OperationalStatus.Up
                    && (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                        || ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))).ToArray();
        }

        private List<Dictionary<string, string>> _getWlanInfos()
        {
            string results = string.Empty;

            Process p = new Process();
            p.StartInfo.FileName = "netsh.exe";
            p.StartInfo.Arguments = "wlan show interfaces";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding(850);
            p.Start();

            List<int> splitIds = new List<int>();
            List<string[]> wlan = new List<string[]>();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            string[] lines = p.StandardOutput.ReadToEnd().Split('\n');
            lines = lines.Skip(3).ToArray();

            for (int i = 0; i < lines.Length; i++) if (lines[i].Length < 5) splitIds.Add(i);

            foreach (int id in splitIds)
            {
                wlan.Add(lines.Take(id).ToArray());
                lines = lines.Skip(id).ToArray();
            }

            foreach (string[] w in wlan)
            {
                if (w.Length > 5)
                {
                    Dictionary<string, string> d = new Dictionary<string, string>();
                    foreach (var l in w)
                    {
                        if (l.Length > 5 && l.Contains(":"))
                        {
                            string cleaned = Regex.Replace(l, @"\s+", " ");
                            string key = cleaned.Split(new string[] { " : " }, StringSplitOptions.None)[0];
                            string value = cleaned.Split(new string[] { " : " }, StringSplitOptions.None)[1];
                            d.Add(key, value);
                        }
                    }
                    list.Add(d);
                }
            }

            return list;
        }

        private string _wlanInfosToString(List<Dictionary<string, string>> list)
        {
            Dictionary<string, string> active = new Dictionary<string, string>();
            string results = string.Empty;

            foreach (Dictionary<string, string> i in list)
            {
                if (i.Keys.Count > active.Keys.Count) active = i;
            }

            if (active.Keys.Count > 16)
            {
                results += string.Format("{0}", active.ElementAt(1).Value) + Environment.NewLine;
                results += string.Format("Type : {0} - {1} (Canal {2})", active.ElementAt(8).Value, active.ElementAt(5).Value, active.ElementAt(12).Value) + Environment.NewLine;
                results += string.Format("Rx : {0} Mb/s - Tx : {1} Mb/s - Signal : {2}", active.ElementAt(13).Value, active.ElementAt(14).Value, active.ElementAt(15).Value) + Environment.NewLine;
            }
            else results = "Aucune puce Wi-Fi n'a été trouvée, vous semblez utiliser une connexion filaire classique.";

            return results;
        }
    }
}
