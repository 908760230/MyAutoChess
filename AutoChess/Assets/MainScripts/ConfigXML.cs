using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ConfigXML 
{
    public struct Server
    {
        public string strIP;
        public string strName;
        public int nPort;
    }

    private XmlDocument xmlDoc = null;
    private XmlNode root = null;
    private ArrayList aWorldList = null;

    public void load()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("ServerConfig");
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        root = xmlDoc.SelectSingleNode("XML");
    }

    public ArrayList GetServerList()
    {
        if (null == aWorldList)
        {
            aWorldList = new ArrayList();

            XmlNode node = root.SelectSingleNode("Servers");

            XmlNodeList nodeList = node.SelectNodes("Server");
            for (int i = 0; i < nodeList.Count; ++i)
            {
                XmlNode nodeServer = nodeList.Item(i);
                XmlAttribute strIP = nodeServer.Attributes["IP"];
                XmlAttribute strName = nodeServer.Attributes["Name"];
                XmlAttribute strPort = nodeServer.Attributes["Port"];

                Server server = new Server();
                server.strIP = strIP.Value;
                server.nPort = int.Parse(strPort.Value);
                server.strName = strName.Value;

                aWorldList.Add(server);
            }
        }

        return aWorldList;
    }

    public bool GetSelectServer(int n, ref string strIP)
    {
        ArrayList serverList = GetServerList();
        if (n >= 0 && n < serverList.Count)
        {
            Server strData = (Server)serverList[n];
            strIP = strData.strIP;

            return true;
        }

        return false;
    }

    public bool GetSelectServer(ref string strIP)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            strIP = "104.160.35.67";
            return true;
        }

        ArrayList serverList = GetServerList();
        if (null != serverList && serverList.Count > 0)
        {
            Server strData = (Server)serverList[0];
            strIP = strData.strIP;

            return true;
        }

        return false;
    }

    public string GetDataPath()
    {
        XmlNode node = root.SelectSingleNode("Evironment");

        XmlNode nodeDataPath = node.SelectSingleNode("DataPath");
        return nodeDataPath.Attributes["ID"].Value;
    }
}
