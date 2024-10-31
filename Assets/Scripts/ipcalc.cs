using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ipcalc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string ip2 = "  192.168.29.1          40-49-0f-65-7c-ca     dynamic  ";

        ip2 = ip2.Trim();
        Debug.Log(ip2.Length);  //51
        string ip = ip2.Substring(0, ip2.IndexOf(' '));
        Debug.Log(ip2.IndexOf(' ') + 1);    //13
        Debug.Log(ip2.Length - ip2.IndexOf(' ') + 1);   //40
        ip2 = ip2.Substring(ip2.IndexOf(' ') + 1).Trim();
        string mac = ip2.Substring(0, ip2.IndexOf(' '));
        print(mac);
        Debug.Log(ip);
    }
    
}
