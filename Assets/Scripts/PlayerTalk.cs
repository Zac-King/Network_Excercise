using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerTalk : NetworkBehaviour
{
    [SyncVar]
    public string playerName;
    [SyncVar]
    public string playerMessage;
	
	public void UpdateName(string a)
    {
        if(isLocalPlayer)
            playerName = a;
    }
    public void UpdateMessage(string a)
    {
        if (isLocalPlayer)
            playerMessage = a;
    }
}
