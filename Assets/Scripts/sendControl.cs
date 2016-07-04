using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class sendControl : MonoBehaviour {

	public string teamName;

	TcpClient tcpclient;
	NetworkStream nwStream;
	bool connected;

	public GameObject currentCanvas;
	public Sprite[] helpScreens;

	public string usingScreen;

	GameObject[] letters;
	GameObject[] multi;

	void Start () {
		letters = GameObject.FindGameObjectsWithTag ("letters");
		multi = GameObject.FindGameObjectsWithTag ("multi");
	}

	public void InitialConnect()
	{
		tcpclient = new TcpClient ();
		tcpclient.Connect ("192.168.0.12", 2023);
		connected = true;
		nwStream = tcpclient.GetStream ();
		string data = "qs.connectResponse(iosVlg6l7t6zRQnx5," + teamName + ",3.0.7)"; 
		byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes (data);
		nwStream.Write (bytesToSend, 0, bytesToSend.Length);
	}

	public void SendNormalAnswer(string ans) {
		string data = "qs.ans(iosVlg6l7t6zRQnx5," + ans + ")"; 
		byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes (data);
		nwStream.Write (bytesToSend, 0, bytesToSend.Length);
	}

	void Update () {
		if (connected == true) {
			if (nwStream.DataAvailable && tcpclient.Connected == true) {
				byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
				int bytesRead = nwStream.Read (bytesToRead, 0, tcpclient.ReceiveBufferSize);
				if (bytesRead > 0) {
					string returndata = Encoding.UTF8.GetString (bytesToRead);

					if (returndata.Contains ("vb.pulse")) {
						string numbersOnly = Regex.Replace(returndata, "[^0-9]", "");
						string inMS = numbersOnly [0] + "." + numbersOnly [1] + numbersOnly [2] + numbersOnly [3];
						Invoke ("Pulse", float.Parse(inMS));
					}

					if (returndata.Contains ("preloadImage")) {
						string url = returndata.Substring (34, 16);
						currentCanvas.transform.localPosition = new Vector3 (-800, 0, 0);
						GameObject.Find ("pictureScreen").transform.localPosition = Vector3.zero;
						currentCanvas = GameObject.Find ("pictureScreen");
						StartCoroutine (downloadImage (url));
					}

					if (returndata.Contains ("view:buzz")) {
						currentCanvas.transform.localPosition = new Vector3 (-800, 0, 0);
						GameObject.Find ("buzzScreen").transform.localPosition = Vector3.zero;
						currentCanvas = GameObject.Find ("buzzScreen");
					}
					print (returndata);
					if (returndata.Contains ("view:letters_1")) {
						foreach (GameObject obj in letters) {
							Color normalBox, normalText = new Color();
							ColorUtility.TryParseHtmlString ("#383253FF", out normalBox);
							ColorUtility.TryParseHtmlString ("#fffeffFF", out normalText);
							obj.GetComponent<Image> ().color = normalBox;
							obj.GetComponent<Image> ().raycastTarget = true;
							obj.transform.GetChild (0).GetComponent<Text> ().color = normalText;
						}
						currentCanvas.transform.localPosition = new Vector3 (-800, 0, 0);
						GameObject.Find ("lettersScreen").transform.localPosition = Vector3.zero;
						currentCanvas = GameObject.Find ("lettersScreen");
						usingScreen = "letters";
					}

					if (returndata.Contains ("view:multi_1")) {
						foreach (GameObject obj in multi) {
							Color normalBox, normalText = new Color();
							ColorUtility.TryParseHtmlString ("#383253FF", out normalBox);
							ColorUtility.TryParseHtmlString ("#fffeffFF", out normalText);
							obj.GetComponent<Image> ().color = normalBox;
							obj.GetComponent<Image> ().raycastTarget = true;
							obj.transform.GetChild (0).GetComponent<Text> ().color = normalText;
						}
						currentCanvas.transform.localPosition = new Vector3 (-800, 0, 0);
						GameObject.Find ("multiScreen").transform.localPosition = Vector3.zero;
						currentCanvas = GameObject.Find ("multiScreen");
						usingScreen = "multi";
					}

					if (returndata.Contains ("view:numbers_1")) {
						GameObject.Find ("answerText").GetComponent<Text> ().text = "";
						currentCanvas.transform.localPosition = new Vector3 (-800, 0, 0);
						GameObject.Find ("numbersScreen").transform.localPosition = Vector3.zero;
						currentCanvas = GameObject.Find ("numbersScreen");
					}

					if (returndata.Contains ("+h")) {
						currentCanvas.transform.localPosition = new Vector3 (-800, 0, 0);
						GameObject.Find ("helpScreen").transform.localPosition = Vector3.zero;
						currentCanvas = GameObject.Find ("helpScreen");
						if (returndata.Contains ("multi+h")) {
							GameObject.Find ("helpImage").GetComponent<Image> ().sprite = helpScreens [0];
						}
						if (returndata.Contains ("numbers+h")) {
							GameObject.Find ("helpImage").GetComponent<Image> ().sprite = helpScreens [1];
						}
						if (returndata.Contains ("letters+h")) {
							GameObject.Find ("helpImage").GetComponent<Image> ().sprite = helpScreens [2];
						}
						if (returndata.Contains ("letters+h1")) {
							GameObject.Find ("helpImage").GetComponent<Image> ().sprite = helpScreens [3];
						}
					}

				} else {
					print ("error");
				}
			}
		}
	}

	public void Pulse() {
		string data = "qs.pulse(iosVlg6l7t6zRQnx5)"; 
		byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes (data);
		nwStream.Write (bytesToSend, 0, bytesToSend.Length);
	}

	public void Buzz() {
		string data = "qs.buzz(iosVlg6l7t6zRQnx5)"; 
		byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes (data);
		nwStream.Write (bytesToSend, 0, bytesToSend.Length);
	}

	public IEnumerator downloadImage (string url) {
		WWW www = new WWW("http://192.168.0.12:2024/" + url);
		yield return www;
		GameObject.Find("Imager").GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0,0,www.texture.width,www.texture.height), new Vector2(256,256f));
		print (www.error);
	}

}
