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
        discussionMembers[0].GetComponentInChildren<TextMesh>().text = "Moderator";

        for (int i = 1; i <= 6; i++)
        {
            bool placeNumberFound = false;
            for (int j = 0; j < discussion.memberList.Count; j++)
            {
                if (discussion.memberList[j].placenumber == i)
                {
                    placeNumberFound = true;
                    discussionMembers[i].GetComponentInChildren<TextMesh>().text = discussion.memberList[j].name;
                }
            }

            if (!placeNumberFound)
            {
                discussionMembers[i].GetComponentInChildren<TextMesh>().text = "";
                TokenMember tokenMember = discussionMembers[i].GetComponentInParent<TokenMember>();
                tokenMember.InitialTokenAction();
                tokenMember.GetComponent<MicVisualizer>().enabled = false;
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
