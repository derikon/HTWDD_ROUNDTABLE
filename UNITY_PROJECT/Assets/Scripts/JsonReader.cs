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
                        Dictionary<string, JSONObject> localMap2 = new Dictionary<string, JSONObject>();

                        foreach (JSONObject currentMember in localMap[key].list)
                        {
                            Member newMember = new Member();
                            var keyArray2 = currentMember.keys.ToArray();
                            Debug.Log("Got MemberList:");

                            foreach (var key2 in keyArray2)
                            {
                                if (!localMap.ContainsKey(key2))
                                {
                                    localMap.Add(key, currentMember[key]);

                                    switch (key2)
                                    {
                                        case "name":
                                            if (localMap2[key2].type == JSONObject.Type.STRING)
                                            {
                                                Debug.Log("Got Title: " + localMap2[key2]);
                                                newMember.name = localMap2[key2].ToString();
                                            }
                                            else
                                            {
                                                Debug.Log("False Datatype " + localMap2[key2]);
                                            }
                                            break;


                                        case "title":
                                            if (localMap2[key2].type == JSONObject.Type.STRING)
                                            {
                                                Debug.Log("Got Title: " + localMap2[key2]);
                                                newMember.title = localMap2[key2].ToString();
                                            }
                                            else
                                            {
                                                Debug.Log("False Datatype " + localMap2[key2]);
                                            }
                                            break;

                                        case "role":
                                            if (localMap2[key2].type == JSONObject.Type.STRING)
                                            {
                                                Debug.Log("Got Role: " + localMap2[key2]);
                                                newMember.role = localMap2[key2].ToString();
                                            }
                                            else
                                            {
                                                Debug.Log("False Datatype " + localMap2[key2]);
                                            }
                                            break;

                                        case "placenumber":
                                            if (localMap2[key2].type == JSONObject.Type.NUMBER)
                                            {
                                                Debug.Log("Got Duration: " + localMap2[key2]);
                                                discussion.duration = (int)localMap2[key2].n;
                                            }
                                            else
                                            {
                                                Debug.Log("False Datatype " + localMap2[key2]);
                                            }
                                            break;
                                    }
                                    memberList.Add(newMember);
                                }
                                else
                                {
                                    Debug.Log("UnknownTag: ");
                                }
                            }
                        }
                    }
                    break;
            }

        }
        return discussion;
    }
}