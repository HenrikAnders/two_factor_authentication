using MetroFramework.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;         
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2FAHealthCheckGUI
{
    public partial class Form : MetroForm
    {
        /// <summary>
        /// initiaize GUI components, set text/path
        /// </summary>
        public Form()
        {
            InitializeComponent();
            tbDriver.Text = "scfilter,WUDFRd";
            tbPath.Text = getPath();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void bApply_Click(object sender, EventArgs e)
        {

            lmessage.Text = "";
            apply();
        }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(tbPath.Text))
            //{
            //    lExport.ForeColor = System.Drawing.Color.Black;
            //}  
            this.bApply.Enabled = this.ValidateEnableBtn();
        }

        private void tbPC_TextChanged(object sender, EventArgs e)
        {
            this.bApply.Enabled = this.ValidateEnableBtn();
        }

        private void tbDriver_TextChanged(object sender, EventArgs e)
        {
            this.bApply.Enabled = this.ValidateEnableBtn();
        }

        /// <summary>
        /// check textboxes, if null or empty. Also the directory of the path
        /// </summary>
        /// <returns></returns>
        private bool ValidateEnableBtn()
        {
            bool bPath = !string.IsNullOrEmpty(this.tbPath.Text) && Directory.Exists(this.tbPath.Text) && !Directory.GetAccessControl(this.tbPath.Text).AreAccessRulesProtected;
            bool bBoxPC = !String.IsNullOrEmpty(tbPC.Text);
            bool bBoxDriver = !String.IsNullOrEmpty(tbDriver.Text);
            return bPath && bBoxPC && bBoxDriver;
        }

        /// <summary>
        /// get path from registry, if it exsists
        /// </summary>
        /// <returns>String path</returns>
        public String getPath()
        {
            String regPath = "";
            RegistryKey reg = Registry.CurrentUser;
            if (reg.GetValueNames().Contains("2FALogPath"))
            {   //if key exists--> get value
                regPath = (string)reg.GetValue("2FALogPath");
            }

            return regPath;
        }

        /// <summary>
        /// set the path in registry, key called: 2FALogPath 
        /// </summary>
        public String setPath(String path)
        {
            String regPath = "";
            RegistryKey reg = Registry.CurrentUser;
            try
            {
                //create the key
                reg = Registry.CurrentUser;
                regPath = tbPath.Text;
                reg.SetValue("2FALogPath", path);
            }
            catch { }
            finally
            {
                if (reg != null)
                {
                    reg.Dispose();
                    reg.Close();
                }
            }
            return regPath;
        }

        /// <summary>
        /// invoke connectivity() test of pc and run the report
        /// </summary>
        public async void apply()
        {
            loadGif.Visible = true;
            bool connected = await Task.Run(() => connectivity(tbPC.Text));
            if (connected)
            {
                List<string> pcList = new List<string>();
                pcList.AddRange(tbPC.Text.Split(','));
                List<String> driverList = new List<string>();
                driverList.AddRange(tbDriver.Text.Split(','));
                String path = tbPath.Text;
                HealthChecker hc = new HealthChecker(setPath(path), pcList, driverList);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
                await Task.Run(() => hc.reportAsync());          
            }
            loadGif.Visible = false;
        }

        /// <summary>
        /// Check if maschines are available by sending a ping
        /// </summary>
        /// <param name="computers"></param>
        /// <returns>false if any computer is not available</returns>
        public async Task<bool> connectivity(String computers)
        {
            bool connect = false;
            try
            {
                foreach (String computer in computers.Trim(' ').Replace("\r\n", string.Empty).Split(','))
                {
                    try
                    {
                        if (NetworkInterface.GetIsNetworkAvailable() && !String.IsNullOrEmpty(computer))
                        {
                            Ping ping = new Ping();
                            PingReply pingReply = ping.Send(Dns.GetHostAddresses(computer).GetValue(0).ToString());
                            if (pingReply.Status != IPStatus.Success)
                            {   //use Invoke((MethodInvoker...  because you need to update another thread (gui thread)
                                this.Invoke((MethodInvoker)delegate { lmessage.Text = "Failed to write report, because " + computer + " is not reachable. Check connection!"; });
                                connect = false;
                            }
                            else
                            {
                                connect = true;
                            }
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                lmessage.Text = "Your maschine is offline, please connect!";
                            });
                            connect = false;
                        }
                    }

                    catch (Exception)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            lmessage.Text = "The computer " + computer + " isn´t reachable";
                        });
                        connect = false;
                    }
                }
            }
            catch (Exception)
            {

            }
            await Task.Delay(1);     
            return connect;
        }
    }
}
