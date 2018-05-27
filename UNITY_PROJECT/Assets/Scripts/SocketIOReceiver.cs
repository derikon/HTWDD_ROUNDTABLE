using UnityEngine;
using SocketIO;

public class SocketIOReceiver : MonoBehaviour {

    private SocketIOComponent socket;

    public JsonReader JsonReader;

    private void Start() {
        var socketIO = GameObject.Find("SocketIO");
        socket = socketIO.GetComponent<SocketIOComponent>();
        socket.Connect();
        socket.On("open", OnOpen);
        socket.On("error", OnError);
        socket.On("close", OnClose);
        socket.On("lift", OnLift);
        socket.On("new_discussion", OnNewDiscussion);
        socket.On("start_discussion", OnStartDiscussion);
        socket.On("end_discussion", OnEndDiscussion);
        //socket.On("remaining_time", OnRemainingTime);
        //socket.On("start_pause", OnStartPause);
        //socket.On("end_pause", OnEndPause);
        //socket.On("topic", OnTopic);
        //socket.On("silence", OnSilence);
    }


    void OnOpen(SocketIOEvent e) {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }


    void OnError(SocketIOEvent e) {
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }


    void OnClose(SocketIOEvent e) {
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }


    void OnLift(SocketIOEvent e) {
        var jsonData = e.data;
        Debug.Log("[SocketIO] lift triggered: " + e.name + " " + jsonData);
    }

    void OnNewDiscussion(SocketIOEvent e)
    {
        Discussion discussion = null;
        JSONObject jsonMessage = (JSONObject) e.data;
        var keyArray = jsonMessage.keys.ToArray();
        JSONObject payload = null;
        foreach (var key in keyArray)
        {
            if (key.Equals("payload"))
            {
                Debug.Log("Got Payload: " + jsonMessage[key]);
                payload = jsonMessage[key];
            }
        }
        if(payload != null)
        {
            discussion = JsonReader.onJSONDiscussionReceived(payload);
        }
       
        if (discussion != null)
        {

        }
    }

    void OnStartDiscussion(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Start Discussion Triggered");
    }

    void OnEndDiscussion(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] End Discussion Triggered");
    }


    public void AckToServer() {
        socket.Emit("ack");
    }
}
