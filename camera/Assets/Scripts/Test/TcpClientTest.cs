using UnityEngine;
using System.Collections;
using TCPConnector;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine.UI;

public class TcpClientTest : MonoBehaviour {

//	TCPClienter client = new TCPClienter ();
//	string lastMsg;
//	public Transform PlayerCoord;

	public Text cmdDisplayer;
	public InputField sendCmdInput;
	private TCPConnector.TCPClienter client; 

	// Use this for initialization
	void Start () {
		cmdDisplayer = GameObject.FindGameObjectWithTag (Tags.cmdRsver).GetComponent<Text>();
		client = GameObject.FindGameObjectWithTag(Tags.systemController).GetComponent<SystemControl> ().netClient;
	}
	
	// Update is called once per frame
	public void FunctionTest () {
	/*
		TcpClient client = new TcpClient ();
		IPEndPoint serverEndPoint = new IPEndPoint (IPAddress.Parse ("127.0.0.1"), 3000);
		client.Connect (serverEndPoint);
		NetworkStream clientStream = client.GetStream ();
		ASCIIEncoding encoder = new ASCIIEncoding ();
		byte[] buffer = encoder.GetBytes("hello server!");
		clientStream.Write (buffer, 0, buffer.Length);
		clientStream.Flush ();
		*/
	//	TCPClienter client = new TCPClienter ();
		//client.fnConnectResult ("127.0.0.1", 3000);
	}

	public void SendMsgToServer(){
		client.SendData (sendCmdInput.text);
	}

	public void ReceiveServerMsgAndPrintOut(string msg){
		cmdDisplayer.text = msg;
	}

}
