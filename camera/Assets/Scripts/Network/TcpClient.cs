using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using UnityEngine;

namespace TCPConnector
{
	public class TCPClienter
	{
		const int READ_BUFFER_SIZE = 255;
		const int WRITE_BUFFER_SIZE = 255;

		private TcpClient client;
		private NetworkStream clientStream;
		private byte[] readBuffer = new byte[READ_BUFFER_SIZE];

		private ASCIIEncoding encoder = new ASCIIEncoding ();
		
		public string strMessage = string.Empty;
		public string res = String.Empty;		//report string


		
		public TCPClienter(){}
		
		public bool fnConnect(string sNetIP, int iPORT_NUM)
		{
			try 
			{
				// The TcpClient is a subclass of Socket, providing higher level 
				// functionality like streaming.
				client = new TcpClient(sNetIP, iPORT_NUM); 			//new TCP Client
				clientStream = client.GetStream();
				// Start an asynchronous read invoking DoRead to avoid lagging the user interface.
				//AsyncCallback will operation in a seperate thread, everytime begin read function is finished
				clientStream.BeginRead(readBuffer, 0, READ_BUFFER_SIZE, new AsyncCallback(DoRead), null);
	
				return true;
			} 
			catch(Exception ex)
			{
				Console.WriteLine(ex.StackTrace+ex.Message);
				return false;
			}
		}

		public bool fnClose(){
			client.Close ();
			return true;
		}

		private void DoRead(IAsyncResult ar)
		{ 
			int BytesRead;
			try
			{
				// Finish asynchronous read into readBuffer and return number of bytes read.
				BytesRead = clientStream.EndRead(ar);
				if (BytesRead < 1) 
				{
					// if no bytes were read server has close.  
					res = "Disconnected";
					return;
				}
				// Convert the byte array the message was saved into, minus two for the
				// Chr(13) and Chr(10)
				strMessage = Encoding.ASCII.GetString(readBuffer, 0, BytesRead);
				ProcessCommands(strMessage);
				// Start a new asynchronous read into readBuffer.
				clientStream.BeginRead(readBuffer, 0, READ_BUFFER_SIZE, new AsyncCallback(DoRead), null);
			} 
			catch
			{
				res = "Disconnected";
			}
		}
		
		// Process the command received from the server, and take appropriate action.
		private void ProcessCommands(string strMessage)
		{
			string[] dataArray;
			
			// Message parts are divided by "|"  Break the string into an array accordingly.
			dataArray = strMessage.Split((char) 124);
			// dataArray(0) is the command.
			switch( dataArray[0])
			{
			case "BROAD":
				// Server sent a broadcast message
				res=  "ServerMessage: " + dataArray[1].ToString();
				break;
			}
		}
		
		// Use a StreamWriter to send a message to server.
		public void SendData(string data)
		{
			byte[] byteData = encoder.GetBytes (data);
			clientStream.Write(byteData, 0, byteData.Length);		//(char)13 is carriage return , char(10) is line feed
			clientStream.Flush();
		}
		

	}
}