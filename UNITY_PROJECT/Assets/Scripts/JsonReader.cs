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
            if (jsonDiscussion[key] != null)
            {
                if (jsonDiscussion[key].Equals("title"))
                {
                    if (jsonDiscussion[key].type == JSONObject.Type.STRING)
                    {
                        Debug.Log("Got title: " + jsonDiscussion[key]);
                        discussion.title = jsonDiscussion[key].ToString();
                    }
                    else
                    {
                        Debug.Log("False Datatype " + jsonDiscussion[key]);
                    }
                }
                else if (jsonDiscussion[key].Equals("duration"))
                {

                    if (jsonDiscussion[key].type == JSONObject.Type.NUMBER)
                    {
                        Debug.Log("Got Duration: " + jsonDiscussion[key]);
                        discussion.duration = (int)jsonDiscussion[key].n;
                    }
                    else
                    {
                        Debug.Log("False Datatype " + jsonDiscussion[key]);
                    }
                }
                else if (jsonDiscussion[key].Equals("member"))
                {
                    if (jsonDiscussion[key].type == JSONObject.Type.ARRAY)
                    {
                        foreach (JSONObject currentMember in jsonDiscussion[key].list)
                        {
                            Member newMember = new Member();
                            var keyArray2 = currentMember.keys.ToArray();
                            Debug.Log("Got MemberList:");

                            foreach (var key2 in keyArray2)
                            {
                                if (currentMember[key2] != null)
                                {
                                    if (currentMember[key2].Equals("name"))
                                    {
                                        if (currentMember[key2].type == JSONObject.Type.STRING)
                                        {
                                            Debug.Log("Got newMember name: " + currentMember[key2]);
                                            newMember.name = currentMember[key2].ToString();
                                        }
                                        else
                                        {
                                            Debug.Log("False Datatype " + jsonDiscussion[key2]);
                                        }
                                    }
                                    else if (currentMember[key2].Equals("title"))
                                    {
                                        if (currentMember[key2].type == JSONObject.Type.STRING)
                                        {
                                            Debug.Log("Got newMember title: " + currentMember[key2]);
                                            newMember.title = currentMember[key2].ToString();
                                        }
                                        else
                                        {
                                            Debug.Log("False Datatype " + jsonDiscussion[key2]);
                                        }
                                    }
                                    else if (currentMember[key2].Equals("role"))
                                    {
                                        if (currentMember[key2].type == JSONObject.Type.STRING)
                                        {
                                            Debug.Log("Got newMember role: " + currentMember["role"]);
                                            newMember.role = currentMember[key2].ToString();
                                        }
                                        else
                                        {
                                            Debug.Log("False Datatype " + jsonDiscussion[key2]);
                                        }
                                    }
                                    else if (currentMember[key2].Equals("placenumber"))
                                    {
                                        if (currentMember[key2].type == JSONObject.Type.NUMBER)
                                        {
                                            Debug.Log("Got newMember placenumber: " + currentMember[key2]);
                                            newMember.placenumber = (int)currentMember[key2].n;
                                        }
                                        else
                                        {
                                            Debug.Log("False Datatype " + jsonDiscussion[key2]);
                                        }
                                    }
                                    memberList.Add(newMember);
                                }
                                else
                                {
                                    Debug.Log("UnknownTag: " + jsonDiscussion[key2].ToString());
                                }
                            }
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
