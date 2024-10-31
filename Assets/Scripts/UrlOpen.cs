using UnityEngine;

public class UrlOpen : MonoBehaviour
{
	public string url;
    public void openURL()
	{
		Application.OpenURL(url);
	}
}
