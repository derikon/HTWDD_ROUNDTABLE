using UnityEngine;
using SocketIO;

public class SocketIOReceiver : MonoBehaviour {

    private SocketIOComponent socket;

    private void Start() {
        var socketIO = GameObject.Find("SocketIO");
        socket = socketIO.GetComponent<SocketIOComponent>();
        socket.Connect();
        socket.On("open", OnOpen);
        socket.On("error", OnError);
        socket.On("close", OnClose);
        socket.On("lift", OnLift);
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


    public void AckToServer() {
        socket.Emit("ack");
    }
}
