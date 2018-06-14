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
        List<Member> memberList = new List<Member>();
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
                        discussion.title = ripQuotationMarks(localMap[key].ToString());
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

                        foreach (var member in localMap[key].list)
                        {
                            memberList.Add(InitializeMember(member));
                        }
                    }
                    break;
            }

        }
        discussion.memberList = memberList;

        return discussion;
    }

    private Member InitializeMember(JSONObject obj)
    {
        Member newMember = new Member();

        foreach (JSONObject currentMemberInfo in obj.list)
        {
            var deepKey = currentMemberInfo.keys[0];

            switch (deepKey)
            {
                case "name":
                    if (currentMemberInfo.list[0].type == JSONObject.Type.STRING)
                    {
                        Debug.Log("Got Title: " + currentMemberInfo.list[0]);
                        newMember.name = ripQuotationMarks(currentMemberInfo.list[0].ToString());
                    }
                    else
                    {
                        Debug.LogWarning("Case 'name'. STRING expected, but got " + currentMemberInfo.list[0].type + "!");
                    }
                    break;

                case "title":
                    if (currentMemberInfo.list[0].type == JSONObject.Type.STRING)
                    {
                        Debug.Log("Got Title: " + currentMemberInfo.list[0]);
                        newMember.title = ripQuotationMarks(currentMemberInfo.list[0].ToString());
                    }
                    else
                    {
                        Debug.LogWarning("Case 'title'. STRING expected, but got " + currentMemberInfo.list[0].type + "!");
                    }
                    break;

                case "role":
                    if (currentMemberInfo.list[0].type == JSONObject.Type.STRING)
                    {
                        Debug.Log("Got role: " + currentMemberInfo.list[0]);
                        newMember.role = ripQuotationMarks(currentMemberInfo.list[0].ToString());
                    }
                    else
                    {
                        Debug.LogWarning("Case 'role'. STRING expected, but got " + currentMemberInfo.list[0].type + "!");
                    }
                    break;

                case "placeNumber":
                    if (currentMemberInfo.list[0].type == JSONObject.Type.NUMBER)
                    {
                        Debug.Log("Got Place Number: " + currentMemberInfo.list[0]);
                        newMember.placenumber = (int)currentMemberInfo.list[0].n;
                    }
                    else
                    {
                        Debug.LogWarning("Case 'placeNumber'. STRING expected, but got " + currentMemberInfo.list[0].type + "!");
                    }
                    break;
                case "organisation":
                    if (currentMemberInfo.list[0].type == JSONObject.Type.STRING)
                    {
                        Debug.Log("Got organisation: " + currentMemberInfo.list[0]);
                        newMember.organisation = ripQuotationMarks(currentMemberInfo.list[0].ToString());
                    }
                    else
                    {
                        Debug.LogWarning("Case 'role'. STRING expected, but got " + currentMemberInfo.list[0].type + "!");
                    }
                    break;
                default:
                    Debug.LogWarning("Unkown case! Key: " + deepKey);
                    break;
            }

        }
        return newMember;
    }

    public string getTopic(JSONObject jsonTopic)
    {
        string topic = "";
        var keyArray = jsonTopic.keys.ToArray();
        Dictionary<string, JSONObject> localMap = new Dictionary<string, JSONObject>();

        foreach (var key in keyArray)
        {
            if (!localMap.ContainsKey(key))
            {
                localMap.Add(key, jsonTopic[key]);
            }
            if (key == "topic")
            {
                if (localMap[key].type == JSONObject.Type.STRING)
                {
                    Debug.Log("Got Topic: " + localMap[key]);
                    topic = ripQuotationMarks(localMap[key].ToString());
                }

            }
        }
        return topic;
    }
    public string getRemainingTime(JSONObject jsonRemainingTime)
    {
        string remainingTime = "";
        var keyArray = jsonRemainingTime.keys.ToArray();
        Dictionary<string, JSONObject> localMap = new Dictionary<string, JSONObject>();

        foreach (var key in keyArray)
        {
            if (!localMap.ContainsKey(key))
            {
                localMap.Add(key, jsonRemainingTime[key]);
            }
            if (key == "remaining_time")
            {
                if (localMap[key].type == JSONObject.Type.STRING)
                {
                    Debug.Log("Got RemainingTime: " + localMap[key]);
                    remainingTime = ripQuotationMarks(localMap[key].ToString());
                }

            }
        }
        return remainingTime;
    }

    private string ripQuotationMarks(string quotetString)
    {
        string ripQuotetString = "";
        if (quotetString != null)
        {
            ripQuotetString = quotetString.Replace("\"", "");
        }


        return ripQuotetString;
        ;
    }
}

