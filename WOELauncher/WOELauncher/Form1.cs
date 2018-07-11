using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WOELauncher
{
    public partial class WOELauncher : Form
    {
        WebClient client = new WebClient();
        public WOELauncher()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void WOELauncher_Load(object sender, EventArgs e)
        {
            if (IsUpToDate())
            {
                prgUpdate.Value = 100;
            }
            else {
                btnLaunch.Text = "Update";
                prgUpdate.Value = 0;
            }
        }

        public bool IsUpToDate() {
            try
            {
                int i = 0;
                string defaultVersion = "1.0.0";
                string officialVersion = client.DownloadString("https://thoe.ro/woe/version.php");
                string[] defaultVersionParts = defaultVersion.Split(new[] { "." }, StringSplitOptions.None);
                string[] officialVersionParts = officialVersion.Split(new[] { "." }, StringSplitOptions.None);

                for (i = 0; i < officialVersionParts.Length; i++) {
                    if (Int32.Parse(officialVersionParts[i]) > Int32.Parse(defaultVersionParts[i])) {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public void DownloadNewVersion() {           
            string inputfilepath = @".\version.txt";
            string ftphost = "thoe.ro";
            string ftpfilepath = "/version.txt";

            string ftpfullpath = "ftp://" + ftphost +ftpfilepath;

            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential("w03@thoe.ro", "w03Downloader");
                byte[] fileData = request.DownloadData(ftpfullpath);

                using (FileStream file = File.Create(inputfilepath))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
                edtNoutati.Text = "S-a descarcat";
                btnLaunch.Text = "Porneste jocul";
                prgUpdate.Value = 100;
            }
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (btnLaunch.Text == "Update")
            {
                DownloadNewVersion();
            }
            else { 
            }
        }
    }
}
