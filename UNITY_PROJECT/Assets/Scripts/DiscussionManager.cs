using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionManager : MonoBehaviour
{

    public Text TabletText;

    private List<GameObject> discussionMembers = new List<GameObject>();
    public GameObject pauseObject;
    public GameObject discussionObject;

    // Use this for initialization
    void Start()
    {
        if (TabletText == null)
        {
            TabletText = GameObject.FindGameObjectWithTag("TabletText").GetComponent<Text>();
        }
    }

    public void FindAllPlayers()
    {
        if (discussionMembers != null && discussionMembers.Count > 0) return;

        discussionMembers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    public void OnDiscussionReady(Discussion discussion)
    {
        for (int i = 0; i < discussion.memberList.Count; i++)
        {
            discussionMembers[i].GetComponent<TextMesh>().text = discussion.memberList[i].name;
        }
    }

    public void OnStartPause()
    {
        discussionObject.SetActive(false);
        pauseObject.SetActive(true);
    }

    public void OnEndPause()
    {
        pauseObject.SetActive(false);
        discussionObject.SetActive(true);
    }
}
