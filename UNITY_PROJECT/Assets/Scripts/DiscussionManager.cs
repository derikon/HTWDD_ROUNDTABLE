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
        pauseObject.SetActive(true);
        discussionObject.SetActive(false);
    }

    public void OnEndPause()
    {
        discussionObject.SetActive(true);
        pauseObject.SetActive(false);

        // Enable Ring System for every Player
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            var tokenMember = player.GetComponent<TokenMember>();
            var tokenModerator = player.GetComponent<TokenModerator>();

            if (tokenMember.enabled && !tokenMember.initialPlacement)
            {
                tokenMember.particleSystemRing.Play();
            }

            if (tokenModerator.enabled && !tokenModerator.initialPlacement)
            {
                tokenModerator.particleSystemRing.Play();
            }
        }
    }

    public void OnRecievedTopic(string topic)
    {
        DiscussionTopic.GetComponent<TextSpinner>().Restore();
        DiscussionTopic.GetComponent<TextMesh>().text = topic;
        DiscussionTopic.GetComponent<TextSpinner>().Enabled = DiscussionTopic.GetComponent<TextSpinner>().EnableFading = true;

    }

    public void OnRecievedRemainingTime(string remainingTime)
    {
        RemainingTime.GetComponent<FadeInOut>().FadeIn(0);
        RemainingTime.GetComponent<TextMesh>().text = remainingTime;
        RemainingTime.GetComponent<FadeInOut>().FadeOut(6);
    }

    public void OnRecievedSilence()
    {
        discussionObject.GetComponentInChildren<TokenModerator>().BuzzerAction();
    }


}
