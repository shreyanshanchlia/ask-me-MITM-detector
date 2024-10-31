using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
	string prevIP2 = "";
	public TextMeshProUGUI pathText;
	public TextMeshProUGUI IP_label, MAC_label;
	public TMP_InputField IP, MAC;
	[Tooltip("Nmap button text")]
	public TextMeshProUGUI safeText, nmapText;
	public Button start_Button, terminate_Button, nmapButton;
	public Color WifiOffColor, WifiOnColor;
	public Image wifiToggle;

	private void Start()
	{
		Application.targetFrameRate = 3;
	}
	public void startButton()
	{
		string currentDirectory = Application.dataPath;
		//pathText.text = currentDirectory;

		Process p = new Process();
		p.StartInfo.UseShellExecute = true;
		p.StartInfo.CreateNoWindow = true;
		p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

#if UNITY_EDITOR
		p.StartInfo.FileName = $"{Application.dataPath}/Scripts/askme/Start_ASK_ME/get_ip_WiFi.bat";
#else
		p.StartInfo.FileName = $"{Application.dataPath}/askme/Start_ASK_ME/get_ip_WiFi.bat";
#endif
		p.Start();

		checkprocessID();
	}
	public void OpenNmapDirectory()
	{
		if(nmapText.IsActive())
		{
			string itemPath = Application.dataPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
			//System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);

			Application.OpenURL($"file://{itemPath}\\..\\nmap");
		}
	}
	public void ToggleWifi()
	{
		UnityEngine.Debug.Log("toggling wifi");
		if(wifiToggle.color == WifiOffColor)
		{
			print("switching on wifi");
			ToggleWifiOn();
		}
		else
		{
			print("switching off wifi");
			ToggleWifiOff();
		}
	}
	void ToggleWifiOn()
	{
		Process p1 = new Process();
		p1.StartInfo.UseShellExecute = true;
		p1.StartInfo.CreateNoWindow = true;
		p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		p1.StartInfo.FileName = $"{Application.dataPath}/askme/Start_ASK_ME/WiFi_ON.bat";
		p1.Start();
		wifiToggle.color = WifiOnColor;
	}
	void ToggleWifiOff()
	{
		Process p1 = new Process();
		p1.StartInfo.UseShellExecute = true;
		p1.StartInfo.CreateNoWindow = true;
		p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		p1.StartInfo.FileName = $"{Application.dataPath}/askme/Start_ASK_ME/WiFi_Off.bat";
		p1.Start();
		wifiToggle.color = WifiOffColor;
	}
	public void TerminateButton()
	{
		Process p = new Process();
		p.StartInfo.UseShellExecute = true;
		p.StartInfo.CreateNoWindow = true;
		p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
#if UNITY_EDITOR
		p.StartInfo.FileName = $"{Application.dataPath}/Scripts/askme/Terminate_ASK_ME/Terminate.bat";
#else
		p.StartInfo.FileName = $"{Application.dataPath}/askme/Terminate_ASK_ME/Terminate.bat";
#endif
		p.Start();

		checkprocessID();
	}
	void checkprocessID()
	{
		Process p1 = new Process();
		p1.StartInfo.UseShellExecute = true;
		p1.StartInfo.CreateNoWindow = true;
		p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		p1.StartInfo.FileName = $"{Application.dataPath}/askme/Terminate_ASK_ME/_check.bat";
		p1.Start();
		p1.WaitForExit();
	}
	void changeDetected()
	{
		Process p = new Process();
		p.StartInfo.UseShellExecute = true;
		p.StartInfo.CreateNoWindow = true;
		//p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		p.StartInfo.FileName = $"{Application.dataPath}/askme/Start_ASK_ME/MIMConfirmed.bat";
		p.Start();
		p.WaitForExit();

		Process p1 = new Process();
		p1.StartInfo.UseShellExecute = true;
		p1.StartInfo.CreateNoWindow = true;
		p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		p1.StartInfo.FileName = $"{Application.dataPath}/askme/Start_ASK_ME/SendMail.vbs";
		p1.Start();

		//not safe
		safeText.gameObject.SetActive(false);
		nmapText.gameObject.SetActive(true);
	}
	private void Update()
	{
		try
		{
			string path = $"{Application.dataPath}/../IP/IP-2.txt";
			string ip2 = File.ReadAllText(@path).Trim();

			string path2 = $"{Application.dataPath}/../processID.txt";
			if (int.TryParse(File.ReadAllText(@path2).Trim(), out int x))
			{
				//is running
				start_Button.gameObject.SetActive(false);
				terminate_Button.gameObject.SetActive(true);
			}
			else
			{
				start_Button.gameObject.SetActive(true);
				terminate_Button.gameObject.SetActive(false);
			}
			if(prevIP2 != "" && prevIP2 != ip2)
			{
				changeDetected();
			}
			prevIP2 = ip2;

			string ip = ip2.Substring(0, ip2.IndexOf(' '));
			ip2 = ip2.Substring(ip2.IndexOf(' ') + 1).Trim();
			string mac = ip2.Substring(0, ip2.IndexOf(' '));
			IP.text = ip;
			MAC.text = mac;
		}
		catch { }
	}
}
