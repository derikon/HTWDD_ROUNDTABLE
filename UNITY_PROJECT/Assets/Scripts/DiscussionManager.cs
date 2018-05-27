using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionManager : MonoBehaviour
{

    public Text TabletText;

    private List<GameObject> discussionMembers = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        discussionMembers.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        if (TabletText == null)
        {
            TabletText = GameObject.FindGameObjectWithTag("TabletText").GetComponent<Text>();
        }
    }

    public void OnDiscussionReady(Discussion discussion)
    {
        for (int i = 0; i < discussion.memberList.Count; i++)
        {
            discussionMembers[i].GetComponent<TextMesh>().text = discussion.memberList[i].name;
        }
    }
}
