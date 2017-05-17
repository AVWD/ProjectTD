using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Linq;
using System.Net.Sockets;

public class NetManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerPacket
    {
        public int uid;
        public string evt;
        public string dat;

        public static PlayerPacket FromPacket(string json)
        {
            return JsonUtility.FromJson<PlayerPacket>(json);
        }
        public string ToPacket()
        {
            return JsonUtility.ToJson(this);
        }
    }

    TcpClient tcpClient;
    NetworkStream serverStream;

    public string host;
    public int port;

    public GameObject[] onMessageListeners; // OnReceiveMesssage(string)
    public GameObject[] onConnectListeners;
    public GameObject[] onDisconnectListeners;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (tcpClient == null || serverStream == null || !serverStream.DataAvailable)
            return;

        byte[] msg = new byte[1024];
        int bytesRead = serverStream.Read(msg, 0, 1024);
        ASCIIEncoding encoder = new ASCIIEncoding();
        string message = encoder.GetString(msg, 0, bytesRead);
        Debug.Log(message);

        //PlayerPacket p = PlayerPacket.FromPacket(message);

        for (int i = 0; i < onMessageListeners.Length; i++)
        {
            onConnectListeners[i].SendMessage("OnMessage", message);
        }
    }

    public void Connect()
    {
        tcpClient = new TcpClient();
        try
        {
            tcpClient.Connect(host, port);
        } catch(SocketException e)
        {
            Debug.LogError(string.Format("{0}: {1}", e.ErrorCode, e.Message));
            tcpClient = null;
            return;
        }

        serverStream = tcpClient.GetStream();
        for(int i = 0; i < onConnectListeners.Length; i++)
        {
            onConnectListeners[i].SendMessage("OnConnect");
        }
    }
    public void Disconnect()
    {
        for (int i = 0; i < onDisconnectListeners.Length; i++)
        {
            onConnectListeners[i].SendMessage("OnDisonnect");
        }
        serverStream.Close();
        serverStream = null;
        tcpClient.Close();
        tcpClient = null;
    }

    public bool Send(string evt, string data)
    {
        if (tcpClient == null) return false;

        byte[] outStream = Encoding.ASCII.GetBytes(data);
        try
        {
            serverStream.Write(outStream, 0, outStream.Length);
        } catch(IOException e)
        {
            Debug.LogError(string.Format("{0}: {1}", e.Message, e.InnerException.Message));
            Disconnect();
            return false;
        }
        return true;
    }

    private void OnApplicationQuit()
    {
        if (tcpClient != null) Disconnect();
    }

}
