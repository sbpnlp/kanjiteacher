using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using SMC = System.ServiceModel.Channels;
using System.Net;

namespace NewClient20090908
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            try
            {
                localIP.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No NIC found?");
            }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            int a = 0;
            int b = 0;

            try
            {
                a = int.Parse(first.Text);
                b = int.Parse(second.Text);
            }
            catch 
            {
                // parsing failed, just bail out
                return;
            }

            //SMC.Binding binding = CalculatorClient.CreateDefaultBinding();
            //string remoteAddress = CalculatorClient.EndpointAddress.Uri.ToString();
            //remoteAddress = remoteAddress.Replace("localhost", serviceAddress.Text);
            //EndpointAddress endpoint = new EndpointAddress(remoteAddress);

            //CalculatorClient client = new CalculatorClient(binding, endpoint);

            SMC.Binding binding = MessageReceiverClient.CreateDefaultBinding();
            string remoteAddress = MessageReceiverClient.EndpointAddress.Uri.ToString();
            remoteAddress = remoteAddress.Replace("localhost", serviceAddress.Text);
            EndpointAddress endpoint = new EndpointAddress(remoteAddress);

            MessageReceiverClient client = new MessageReceiverClient(binding, endpoint);

            try
            {
                List<DateTime> liste = new List<DateTime>();
                liste.Add(DateTime.Now);
                List<System.Windows.Forms.MouseEventArgs> pointList =
                    new List<System.Windows.Forms.MouseEventArgs>() 
                    {
                    new System.Windows.Forms.MouseEventArgs(MouseButtons.Left, 1, 1, 2, 0),
                    new System.Windows.Forms.MouseEventArgs(MouseButtons.Left, 1, 3, 4, 0)};
                                            

                List<int> xs = new List<int>();
                List<int> ys = new List<int>();
                foreach (System.Windows.Forms.MouseEventArgs mea in pointList)
                {
                    xs.Add(mea.X);
                    ys.Add(mea.Y);
                }

                answer.Text = client.ReceiveMessage(xs.ToArray(), ys.ToArray(), liste.ToArray()).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}