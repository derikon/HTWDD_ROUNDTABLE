using UnityEngine;
using SocketIO;

public class SocketIOReceiver : MonoBehaviour
{

    private SocketIOComponent socket;

    public JsonReader JsonReader;

    public DiscussionManager DiscussionManager;

    private void Start()
    {
        if (DiscussionManager == null)
        {
            DiscussionManager = GameObject.FindGameObjectWithTag("DiscussionManager").GetComponent<DiscussionManager>();
        }

        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        if (socket == null)
        {
            Debug.LogError("Socket not found!");
            return;
        }
        socket.Connect();
        socket.On("open", OnOpen);
        socket.On("error", OnError);
        socket.On("close", OnClose);
        socket.On("lift", OnLift);
        socket.On("new_discussion", OnNewDiscussion);
        socket.On("start_discussion", OnStartDiscussion);
        socket.On("end_discussion", OnEndDiscussion);
        socket.On("remaining_time", OnRemainingTime);
        socket.On("start_pause", OnStartPause);
        socket.On("end_pause", OnEndPause);
        socket.On("topic", OnTopic);
        socket.On("silence", OnSilence);
    }


    void OnOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    void OnRemainingTime(SocketIOEvent e)
    {
        string remainingTime = "";
        JSONObject payload = getPayload(e);

        if (payload != null)
        {
            remainingTime = JsonReader.getRemainingTime(payload);
        }

        if (remainingTime != null)
        {
            DiscussionManager.OnRecievedRemainingTime(remainingTime);
        }
        else
        {
            Debug.LogError("remainingTime is null");
        }
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    void OnTopic(SocketIOEvent e)
    {
        string topic = "";
        JSONObject payload = getPayload(e);

        if (payload != null)
        {
            topic = JsonReader.getTopic(payload);
        }

        if (topic != null)
        {
            DiscussionManager.OnRecievedTopic(topic);
        }
        else
        {
            Debug.LogError("topic is null");
        }
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }


    void OnError(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }


    void OnClose(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }


    void OnLift(SocketIOEvent e)
    {
        var jsonData = e.data;
        Debug.Log("[SocketIO] lift triggered: " + e.name + " " + jsonData);
    }

    void OnNewDiscussion(SocketIOEvent e)
    {
        Discussion discussion = null;

        JSONObject payload = getPayload(e);

        if (payload != null)
        {
            discussion = JsonReader.onJSONDiscussionReceived(payload);
        }

        if (discussion != null && discussion.memberList != null)
        {
            DiscussionManager.OnDiscussionReady(discussion);
        }
        else
        {
            Debug.LogError("discussion or discussion.memberList is null");
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

    void OnStartPause(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Start Pause Triggered");
        DiscussionManager.OnStartPause();
    }

    void OnEndPause(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] End Pause Triggered");
        DiscussionManager.OnEndPause();
    }


    public void AckToServer()
    {
        socket.Emit("ack");
    }


    void OnSilence(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Silence Triggered");
        DiscussionManager.OnRecievedSilence();
    }

    private JSONObject getPayload(SocketIOEvent e)
    {
        JSONObject jsonMessage = (JSONObject)e.data;
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
        return payload;
    }
}
