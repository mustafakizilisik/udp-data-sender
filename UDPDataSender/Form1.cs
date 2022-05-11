using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UDPDataSender
{
    public partial class Form1 : Form
    {
        List<string> deviceDataList = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 15; i++)
            //{
            //    deviceDataList.Add(DeviceDataGenerate());
            //}

            DeviceDataGenerate();

            while (true)
            {
                Send();
            }
        }

        private List<string> DeviceDataGenerate()
        {
            //Random rnd = new Random();
            //char[] macAddressList = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

            //string macAddress = "";
            //for (int i = 0; i < 4; i++)
            //{
            //    macAddress += macAddressList[rnd.Next(15)].ToString();
            //}
            //string deviceNo = rnd.Next(99).ToString();
            //return $"%CMS{macAddress}{deviceNo.PadLeft(3, '0')}111111111111111111111111000000001111111100000000000**..5";

            string[] macAddressList = { "1111", "2222", "3333", "4444", "5555", "6666", "7777", "8888", "9999", "1010", "1111", "1212", "1313", "1414", "1515", "1616" };

            string[] deviceNoList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };

            for (int i = 0; i < 16; i++)
            {
                var data = $"%CMS{macAddressList[i]}{deviceNoList[i].PadLeft(3, '0')}111111111111111111111111000000001111111100000000000**..5";
                deviceDataList.Add(data);
            }
            return deviceDataList;
        }
        private void Send()
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress serverAddr = IPAddress.Parse("192.168.2.255");

            IPEndPoint endPoint = new IPEndPoint(serverAddr, Convert.ToInt32(txtUDPPort.Text));

            //string data = txtData.Text;

            foreach (var data in deviceDataList)
            {
                byte[] send_buffer = Encoding.ASCII.GetBytes(data);

                sock.SendTo(send_buffer, endPoint);
                Thread.Sleep(100);
            }
        }
    }
}
