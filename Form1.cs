using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            gMapControl1.Bearing = 0;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.GrayScaleMode = true;
            gMapControl1.MaxZoom = 18;
            gMapControl1.MinZoom = 2;
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gMapControl1.NegativeMode = false;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.Zoom = 2;
            //gMapControl1.Dock = DockStyle.Fill;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string line;
            using (WebClient wc = new WebClient())
                line = wc.DownloadString($"http://free.ipwhois.io/xml/{textBox1.Text}");
            Match match = Regex.Match(line, "<country>(.*?)</country>(.*?)<latitude>(.*?)</latitude>(.*?)<longitude>(.*?)</longitude>");
            Match match1 = Regex.Match(line, "<region>(.*?)</region>(.*?)<city>(.*?)</city>");
            //"(.*?)<region>(.*?)</region>(.*?)<city>(.*?)</city>");
            string country = match.Groups[1].Value;
            string lat = match.Groups[3].Value.Replace(".",",");
            string lon = match.Groups[5].Value.Replace(".", ",");
            string reg = match1.Groups[1].Value;
            string city = match1.Groups[3].Value;
            label1.Text = country + "\n" + reg + "\n" + city;

            double latd = Convert.ToDouble(lat);
            double lond = Convert.ToDouble(lon);

            gMapControl1.Visible = true;

            gMapControl1.Position = new GMap.NET.PointLatLng(latd, lond);
            gMapControl1.Zoom = 10;

        }
    }
}
