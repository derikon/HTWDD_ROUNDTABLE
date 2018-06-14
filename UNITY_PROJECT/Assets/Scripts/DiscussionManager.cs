using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionManager : MonoBehaviour
{

    private List<GameObject> discussionMembers = new List<GameObject>();
    public GameObject pauseObject;
    public GameObject discussionObject;
    public GameObject DiscussionTopic;
    public GameObject RemainingTime;

    // Use this for initialization
    void Start()
    {
    }

    public void FindAllPlayers()
    {
        if (discussionMembers != null && discussionMembers.Count > 0) return;

        discussionMembers.AddRange(GameObject.FindGameObjectsWithTag("PlayerName"));
    }

    public void OnDiscussionReady(Discussion discussion)
    {
        for (int i = 0; i <= discussion.memberList.Count; i++)
        {
            if (i == 0)
            {
                discussionMembers[i].GetComponentInChildren<TextMesh>().text = "Moderator";
            }
            else
            {
                discussionMembers[i].GetComponentInChildren<TextMesh>().text = discussion.memberList[i - 1].name;
            }
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

    public void OnRecievedTopic(string topic)
    {
        DiscussionTopic.GetComponent<TextSpinner>().Restore();
        DiscussionTopic.GetComponent<TextMesh>().text = topic;
        DiscussionTopic.GetComponent<TextSpinner>().Enabled = DiscussionTopic.GetComponent<TextSpinner>().EnableFading = true;

    }

    public void OnRecievedRemainingTime(string remainingTime)
    {
        RemainingTime.GetComponent<TextMesh>().text = remainingTime;
    }

}
