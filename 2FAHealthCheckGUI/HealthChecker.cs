using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;       
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2FAHealthCheckGUI
{
    /// <summary>
    /// provides methods, to read remote information
    /// </summary>
    public class HealthChecker
    {
        // LogFile
        System.IO.StreamWriter logFile;       
        String timeStamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");  
        String remoteComputer = "";             

        String userName;                                          
        String machineName;                           
        String operatingSystem;
        String osVersion;
        bool error=false;            

        String filePath = "";
        List<String> remoteComputerList=null;
        List<String> driverList = null;

        ExcelWriter excelWriter = new ExcelWriter();   

        /// <summary>
        /// create the first (upper) header list
        /// </summary>
        /// <returns>header list</returns>
        private String[,] firstHeader()
        {   
            String[,] headerArray = new String[1, 17];
            headerArray[0, 0] = "Overall information";            
            headerArray[0, 1] = "";
            headerArray[0, 2] = "";
            headerArray[0, 3] = "";
            headerArray[0, 4] = "";
            headerArray[0, 5] = "Smartcard enforced";
            headerArray[0, 6] = "AET Middleware installed";
            headerArray[0, 7] = "";
            headerArray[0, 8] = ""; 
            headerArray[0, 9] = "Services";
            headerArray[0, 10] = "";
            headerArray[0, 11] = "";
            headerArray[0, 12] = "";
            headerArray[0, 13] = "Driver";
            headerArray[0, 14] = "";
            headerArray[0, 15] = "";
            headerArray[0, 16] = "";                          
            return headerArray;  
        }

        /// <summary>
        /// create the second header list
        /// </summary>
        /// <returns></returns>
        private String[,] secondHeader()
        {
            String[,] headerArray2 = new String[1, 17];
            headerArray2[0, 0] = "Time stamp";
            headerArray2[0, 1] = "Machine name";
            headerArray2[0, 2] = "User Name";       
            headerArray2[0, 3] = "Operating System";
            headerArray2[0, 4] = "OS Version";
            headerArray2[0, 5] = "";
            headerArray2[0, 6] = "Name"; //AET
            headerArray2[0, 7] = "Version";
            headerArray2[0, 8] = "Build";
            headerArray2[0, 9] = "Service Name";     
            headerArray2[0, 10] = "State";
            headerArray2[0, 11] = "StartMode";
            headerArray2[0, 12] = "Status";
            headerArray2[0, 13] = "Driver Name";   
            headerArray2[0, 14] = "Pathname";
            headerArray2[0, 15] = "Caption";
            headerArray2[0, 16] = "StartMode";    
            return headerArray2;     
        }


        /// <summary>
        /// constructor writes information into the report txt
        /// </summary>           
        public HealthChecker(String filePath, List<String> remoteComputerList, List<String> driverList)
        {
            this.filePath = filePath;
            this.remoteComputerList = remoteComputerList;
            this.driverList = driverList;   
        }


        /// <summary>
        /// Queries the local security group policy
        /// Filter if scforceoption is set or not
        /// get install state of AET software/middleware
        /// </summary>
        private String[,] queryTheRegistry()
        {          
            String[,] resultArray = new String[1, 4];
            try
            {      
                #region is enforced on client      
                RegistryKey queryResultRemote1 = RegistryKey.OpenRemoteBaseKey(
                RegistryHive.LocalMachine, remoteComputer).OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\");

                if (queryResultRemote1.GetValueNames().Contains("scforceoption"))
                {
                    String valueRemote = queryResultRemote1.GetValue("scforceoption").ToString();   
                    String resultString = "Smartcard enforced on client :";
                    switch (valueRemote)
                    {
                        case "0":
                            //Client is not enforced
                            resultString = resultString + " FALSE";
                            resultArray[0, 0] = "FALSE";
                            break;
                        case "1":
                            //Client is enforced
                            resultString = resultString + " TRUE";
                            resultArray[0, 0] = "TRUE";
                            break;
                        default:
                            //Not defined / ERROR case
                            resultString = resultString + " NOT DEFINED";
                            resultArray[0, 0] = "NOT DEFINED";
                            break;
                    }      
                }
                #endregion

                #region is AET middelware installed
                RegistryKey sqlsrvKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, remoteComputer).OpenSubKey(@"SOFTWARE\A.E.T. Europe B.V.\SafeSign\2.0");
                Dictionary<String, String> queryResult2 = listRegistryKey(sqlsrvKey);

                if (queryResult2.Count > 0)
                {                                             
                    resultArray[0, 1] = queryResult2["ProductName"];
                    resultArray[0, 2] = queryResult2["ProductVersion"];
                    resultArray[0, 3] = queryResult2["ProductSpecialBuild"];    
                }
                else
                {     
                    resultArray[0, 1] = "Not found in registry";
                    resultArray[0, 2] = "";
                    resultArray[0, 3] = "";
                }
            }
            #endregion
         
            catch (Exception e)
            {
                remoteHandling(e);  
            }
            return resultArray;
        }
        /// <summary>
        ///  get a list of keys and it´s values
        /// </summary>
        /// <param name="key"></param>
        /// <returns>2D List of key and value</returns>
        private Dictionary<String, String> listRegistryKey(RegistryKey registryKey)
        {
            Dictionary<String, String> registryResultList = new Dictionary<String, String>();  
            //opening the subkey 
            //if it does exist, retrieve the stored values  
            if (registryKey != null)
            {
                foreach (String subKeyName in registryKey.GetValueNames())
                {
                    String key = subKeyName.ToString();
                    String value = registryKey.GetValue(subKeyName).ToString();   
                    registryResultList.Add(key, value);
                }
                registryKey.Close();
            }
            return registryResultList;   
        }


        /// <summary>
        /// get properties of given drivers as String
        /// </summary>
        /// <param name="driverName"></param>
        /// <returns>List<String> of drivers </returns>
        private String[,] listInstalledDriver(List <String> driverNames)
        {
            List<String> installedDriverQueryResult = new List<String>();           

            String[,] resultArray = new String[driverNames.Count, 4];
            List<ManagementObject> shares = getDriverMO(remoteComputer);
            int d2 = 0;
            for (int i = 0; i < driverNames.Count(); i++)
            {
                String driverName = driverNames.ElementAt(i);
                foreach (ManagementObject obj in shares)
                {
                    if (obj["Name"].Equals(driverName))
                    {
                        if (d2 == 0)
                        {
                            resultArray[d2, 0] = obj.GetPropertyValue("Name").ToString();
                            resultArray[d2, 1] = obj.GetPropertyValue("PathName").ToString();
                            resultArray[d2, 2] = obj.GetPropertyValue("Caption").ToString();
                            resultArray[d2, 3] = obj.GetPropertyValue("StartMode").ToString();
                        }
                        if (d2 > 0)
                        {   //apend the new driver string to the old, with a seperator 
                            resultArray[0, 0] = resultArray[d2 - 1, 0] + "\n_______\n" + obj.GetPropertyValue("Name");
                            resultArray[0, 1] = resultArray[d2 - 1, 1] + "\n_______\n" + obj.GetPropertyValue("PathName");
                            resultArray[0, 2] = resultArray[d2 - 1, 2] + "\n_______\n" + obj.GetPropertyValue("Caption");
                            resultArray[0, 3] = resultArray[d2 - 1, 3] + "\n_______\n" + obj.GetPropertyValue("StartMode");
                        }
                        d2++;
                        goto loop1;       //used to break out and get the new driverName from the upper loop
                    }
                }
                MessageBox.Show("The driver " + driverName + " was not found, check spelling");
            loop1:;
            }    
            return resultArray;
        }
        /// <summary>
        /// get drivers as managementObject
        /// </summary>
        /// <param name="computername"></param>
        /// <returns>List <ManagementObject> </ManagementObject></returns>
        private List<ManagementObject> getDriverMO(string computername)
        {
            List<ManagementObject> shares = new List<ManagementObject>();
            if (computername != string.Empty)
            {    
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_SystemDriver");   
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(getScope(), query);
                foreach (ManagementObject obj in searcher.Get())
                {
                    shares.Add(obj);
                }  
            }
            return shares;
        }


        /// <summary>
        /// get servicename, state, status and startmode from smartcard and smartcard removal service 
        /// </summary>
        private void queryTheServices()
        {   
            List<String> services = new List<String>();

            services.Add("SCardSvr");       //Smart card services
            services.Add("SCPolicySvc");    //Smart card removal services            
            List<String> result = new List<String>();    
        }
        /// <summary>
        /// Query the service
        /// </summary>   
        /// <returns>list of services (SCardSvr and SCPolicySvc)</returns>           
        private String [,] listServices()
        {
            String[,] resultArray = new String[1,4];
            List<String> resultList = new List<string>();
            List<String> servicesResult = new List<String>(); 
            ObjectQuery query = new ObjectQuery(
                "SELECT * FROM Win32_Service");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(getScope(), query);
            int d2 = 0;
            foreach (ManagementObject service in searcher.Get())     //iterate throught management obj. collection
            {
                if (service["Name"].Equals("SCardSvr") || service["Name"].Equals("SCPolicySvc"))      //service to be capured
                {
                    if (d2 == 0)
                    {
                        resultArray[d2, 0] = service["Name"].ToString();
                        resultArray[d2, 1] = service["State"].ToString();
                        resultArray[d2, 2] = service["Status"].ToString();
                        resultArray[d2, 3] = service["StartMode"].ToString();
                    }
                    if (d2 > 0)
                    {
                        //apend the new service string to the old, with a seperator 
                        resultArray[0, 0] = resultArray[d2 - 1, 0] + "\n_______\n" + service["Name"];
                        resultArray[0, 1] = resultArray[d2 - 1, 1] + "\n_______\n" + service["State"];
                        resultArray[0, 2] = resultArray[d2 - 1, 2] + "\n_______\n" + service["Status"];
                        resultArray[0, 3] = resultArray[d2 - 1, 3] + "\n_______\n" + service["StartMode"];          
                    }
                    d2++;
                } 
            }
            return resultArray;
        }


        /// <summary>
        /// set overall information: Machine name, User name, OS, OS version(64/32 Bit)   
        /// </summary>
        private String[,] setRemoteInfo()
        {
            List<String> resultList = new List<string>();
            String[,] resultArray = new String[1, 5];
            ObjectQuery qUserName = new ObjectQuery(
            "SELECT UserName FROM Win32_ComputerSystem");

            try
            {
                ManagementObjectSearcher searcherUserName = new ManagementObjectSearcher(getScope(), qUserName);
                    userName = searcherUserName.Get().Cast<ManagementBaseObject>().First()["UserName"].ToString();

                    machineName = remoteComputer;

                    ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(getScope(), query);
                    foreach (ManagementObject maschine in searcher.Get())
                    {
                        operatingSystem = maschine["Caption"].ToString();
                    }
                    #region osVersion (64/32 Bit)
                    ManagementScope scopeOSvers = new ManagementScope(@"\\" + remoteComputer + "\\root\\cimv2");
                    ObjectQuery qCPU = new ObjectQuery("SELECT AddressWidth FROM Win32_Processor");

                    ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(scopeOSvers, qCPU);
                    foreach (ManagementObject cpu in searcher1.Get())
                    {
                        if (cpu["AddressWidth"].ToString().Contains("64"))
                            osVersion = "64 Bit";
                        else if (cpu["AddressWidth"].ToString().Contains("32"))
                            osVersion = "32 Bit";
                    }
                    #endregion
                }
            catch (Exception e)
            {
                remoteHandling(e);
            }
            resultArray[0, 0] = timeStamp;
            resultArray[0, 1] = machineName;
            resultArray[0, 2] = userName;
            resultArray[0, 3] = operatingSystem;
            resultArray[0, 4] = osVersion;
            return resultArray;
        
        }

        /// <summary>
        /// get the scope: @"\\" + remoteComputer + "\\root\\cimv2"
        /// and connect to it
        /// </summary>
        /// <returns>ManagementScope Object</returns>
        private ManagementScope getScope()
        {
            ManagementScope scope = null;
            try
            {
                scope = new ManagementScope(@"\\" + remoteComputer + "\\root\\cimv2");
                scope.Connect();
            }
            catch (Exception connectError)
            {
                remoteHandling(connectError);
            }
            return scope;
        }
           
     
        /// <summary>
        /// handles connection exceptions and other, writes message to log file
        /// </summary>
        /// <param name="e"></param>
        private void remoteHandling(Exception e)
        {
            if (e.ToString().StartsWith("System.Security.SecurityException: Requested registry access is not allowed.")||e.ToString().StartsWith("System.UnauthorizedAccessException: Zugriff verweigert"))
            {  
                MessageBox.Show("! access to computer " + remoteComputer + " is not allowed ! \n for more information -> desktop/_2FA_LOG.txt");
                // Environment.Exit(-1);     kill complete program
                // Thread.CurrentThread.Abort();  throw exception, because other Threads are waiting for the current
            }
            else
            {
                MessageBox.Show("An exception occured, for more information -> desktop/_2FA_LOG.txt");            
            }
            this.logFile = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\\_" + timeStamp + "_2FA_LOG.txt");        
            logFile.Write(e);
            this.logFile.Close();
            error = true; 
        }

        /// <summary>
        /// run the report
        /// </summary>
        /// <returns>current thread</returns>
        public  async Task reportAsync() {                
            excelWriter.writeHeader("A1", firstHeader());
            excelWriter.writeHeader("A2", secondHeader());
            int row = 3;
            foreach (String currentRemotePC in remoteComputerList)
            {
                this.remoteComputer = currentRemotePC;
                excelWriter.fill(excelWriter.getCellName(row, 1), setRemoteInfo());
                if (!error)
                {
                    excelWriter.fill(excelWriter.getCellName(row, 6), queryTheRegistry());
                    excelWriter.fill(excelWriter.getCellName(row, 10), listServices());
                    excelWriter.fill(excelWriter.getCellName(row, 14), listInstalledDriver(driverList));
                }
                row++;
            }
            #region formatting
            excelWriter.mergeCells("A1", "E1"); //Overall info  
            excelWriter.mergeCells("G1", "I1"); //AET Middleware
            excelWriter.mergeCells("J1", "M1"); //Services          
            excelWriter.mergeCells("N1", "Q1"); // Drivers        

            excelWriter.setHeader("A1", "S1");
            excelWriter.FreezeRange("A3", "Q3");
            excelWriter.setFilter("A2", "R2");
            excelWriter.OptimizeCellSize("A1", excelWriter.getCellName(remoteComputerList.Count + 3, 16));  //instead of "16" firstHeader().Length   for the hole table

            excelWriter.drawGridLines("A1", excelWriter.getCellName(remoteComputerList.Count + 2, secondHeader().Length));                                                                                                         //change row color to orange, if any conditions isn´t met
            excelWriter.bigLeftGridLine("F2", excelWriter.getCellName(remoteComputerList.Count + 2, 7));  //enforced section
            excelWriter.bigLeftGridLine("G2", excelWriter.getCellName(remoteComputerList.Count + 2, 8));//Middleware section
            excelWriter.bigLeftGridLine("J2", excelWriter.getCellName(remoteComputerList.Count + 2, 11));//Service section
            excelWriter.bigLeftGridLine("N2", excelWriter.getCellName(remoteComputerList.Count + 2, 15));//Driver section
            excelWriter.bigLeftGridLine("R2", excelWriter.getCellName(remoteComputerList.Count + 2, 19));//Certificate section   
            //Color gray line, if Userprofiles incomplete             Middle ware AET
            for (int i = 3; i < remoteComputerList.Count + 3; i++)
            {
                String sAETInstalled = excelWriter.readExcelWorkbook(1, i, 7);
                if (sAETInstalled.Contains("Not found in registry"))
                {
                    excelWriter.setCellColor("A" + i, "Q" + i, "#AAAAAA");
                }
            }
            #endregion

            excelWriter.Close(filePath + "\\" + timeStamp + " 2FA_HealthReport.xlsx");
            if (error == true)
            {
                if (MessageBox.Show("Report created with error!", "", MessageBoxButtons.OK) == DialogResult.OK)
                {
                }    
            }
            else
            {
                if (MessageBox.Show("Report created successfully", "", MessageBoxButtons.OK) == DialogResult.OK)
                {  
                }            
            }       
            await Task.Delay(1);
        } 

    }
}



