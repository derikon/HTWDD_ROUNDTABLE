using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Member
{
    public string name;
    public string title;
    public string role;
    public int placenumber;
}

public class Discussion
{
    public string title;
    public int duration;
    public List<Member> memberList;
}




public class JsonReader : MonoBehaviour
{


    public Discussion onJSONDiscussionReceived(JSONObject jsonDiscussion)
    {
        //var keyArray = jsonDiscussion.keys.ToArray();
        Discussion discussion = null;
        List<Member> memberList = null;
        var keyArray = jsonDiscussion.keys.ToArray();

        foreach (var key in keyArray)
        {
            if (jsonDiscussion["title"] != null)
            {
                Debug.Log("Got tittle: " + jsonDiscussion["title"]);
                if (jsonDiscussion["title"].type == JSONObject.Type.STRING)
                {
                    discussion.title = jsonDiscussion["title"].ToString();
                }

            }
            else if (jsonDiscussion["duration"] != null)
            {
                Debug.Log("Got Duration: " + jsonDiscussion["duration"]);
                if (jsonDiscussion["duration"].type == JSONObject.Type.NUMBER)
                {
                    discussion.duration = (int)jsonDiscussion["duration"].n;
                }

            }
            else if (jsonDiscussion["member"] != null)
            {
                Debug.Log("Got MemberList: ");
                if (jsonDiscussion["member"].type == JSONObject.Type.ARRAY)
                {
                    foreach (JSONObject currentMember in jsonDiscussion["member"].list)
                    {
                        Member newMember = new Member();
                        var keyArray2 = currentMember.keys.ToArray();

                        foreach (var key2 in keyArray2)
                        {
                            if (currentMember["name"] != null)
                            {
                                Debug.Log("Got newMember name: " + currentMember["name"]);
                                if (currentMember["name"].type == JSONObject.Type.STRING)
                                {
                                    newMember.name = currentMember["name"].ToString();
                                }
                            }
                            else if (currentMember["title"] != null)
                            {
                                Debug.Log("Got newMember title: " + currentMember["title"]);
                                if (currentMember["title"].type == JSONObject.Type.STRING)
                                {
                                    newMember.title = currentMember["title"].ToString();
                                }
                            }

                            else if (currentMember["role"] != null)
                            {
                                Debug.Log("Got newMember role: " + currentMember["role"]);
                                if (currentMember["role"].type == JSONObject.Type.STRING)
                                {
                                    newMember.role = currentMember["role"].ToString();
                                }
                            }

                            else if (currentMember["placenumber"] != null)
                            {
                                Debug.Log("Got newMember placenumber: " + currentMember["placenumber"]);
                                if (currentMember["placenumber"].type == JSONObject.Type.NUMBER)
                                {
                                    newMember.placenumber = (int)currentMember["role"].n;
                                }
                            }

                            memberList.Add(newMember);
                        }

                    }

                }
                else
                {
                    Debug.Log("UnknownTag: " + jsonDiscussion[key].ToString());
                }

            }
        }
        return discussion;
    }
}
