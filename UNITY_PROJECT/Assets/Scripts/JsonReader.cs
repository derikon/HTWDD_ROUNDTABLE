using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Member
{
    public string name;
    public string title;
    public string description;
    public int placenumber;
    public string organisation;
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
        Discussion discussion = new Discussion();
        List<Member> memberList = null;
        var keyArray = jsonDiscussion.keys.ToArray();
        Dictionary<string, JSONObject> localMap = new Dictionary<string, JSONObject>();

        foreach (var key in keyArray)
        {
            if (!localMap.ContainsKey(key))
            {
                localMap.Add(key, jsonDiscussion[key]);
            }

            switch (key)
            {
                case "title":
                    if (localMap[key].type == JSONObject.Type.STRING)
                    {
                        Debug.Log("Got Title: " + localMap[key]);
                        discussion.title = localMap[key].ToString();
                    }
                    break;
                case "duration":
                    if (localMap[key].type == JSONObject.Type.NUMBER)
                    {
                        Debug.Log("Got Duration: " + localMap[key]);
                        discussion.duration = (int)localMap[key].n;
                    }
                    else
                    {
                        Debug.Log("False Datatype " + localMap[key]);
                    }
                    break;
                case "members":
                    if (localMap[key].type == JSONObject.Type.ARRAY)
                    {
                        Debug.Log("Got MemberList:");
                        Dictionary<string, JSONObject> localMap2 = new Dictionary<string, JSONObject>();
                        Member newMember = new Member();
                        foreach (JSONObject currentMember in localMap[key].list)
                        {
                            var deepKey = currentMember.keys[0];

                            switch (deepKey)
                            {
                                case "name":
                                    if (currentMember.list[0].type == JSONObject.Type.STRING)
                                    {
                                        Debug.Log("Got Title: " + currentMember.list[0]);
                                        newMember.name = currentMember.list[0].ToString();
                                    }
                                    else
                                    {
                                        Debug.LogWarning("Case 'name'. STRING expected, but got " + currentMember.list[0].type + "!");
                                    }
                                    break;

                                case "title":
                                    if (currentMember.list[0].type == JSONObject.Type.STRING)
                                    {
                                        Debug.Log("Got Title: " + currentMember.list[0]);
                                        newMember.title = currentMember.list[0].ToString();
                                    }
                                    else
                                    {
                                        Debug.LogWarning("Case 'title'. STRING expected, but got " + currentMember.list[0].type + "!");
                                    }
                                    break;

                                case "description":
                                    if (currentMember.list[0].type == JSONObject.Type.STRING)
                                    {
                                        Debug.Log("Got description: " + currentMember.list[0]);
                                        newMember.description = currentMember.list[0].ToString();
                                    }
                                    else
                                    {
                                        Debug.LogWarning("Case 'description'. STRING expected, but got " + currentMember.list[0].type + "!");
                                    }
                                    break;

                                case "placeNumber":
                                    if (currentMember.list[0].type == JSONObject.Type.NUMBER)
                                    {
                                        Debug.Log("Got Place Number: " + currentMember.list[0]);
                                        newMember.placenumber = (int)currentMember.list[0].n;
                                    }
                                    else
                                    {
                                        Debug.LogWarning("Case 'placeNumber'. STRING expected, but got " + currentMember.list[0].type + "!");
                                    }
                                    break;
                                case "organisation":
                                    if (currentMember.list[0].type == JSONObject.Type.STRING)
                                    {
                                        Debug.Log("Got organisation: " + currentMember.list[0]);
                                        newMember.organisation = currentMember.list[0].ToString();
                                    }
                                    else
                                    {
                                        Debug.LogWarning("Case 'role'. STRING expected, but got " + currentMember.list[0].type + "!");
                                    }
                                    break;
                                default:
                                    Debug.LogWarning("Unkown case! Key: " + deepKey);
                                    break;
                            }
                        }
                        memberList.Add(newMember);

                    }
                    break;
            }

        }
        return discussion;
    }
}