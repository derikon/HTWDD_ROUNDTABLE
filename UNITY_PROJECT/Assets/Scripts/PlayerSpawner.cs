﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    public GameObject VideoPlayerPrefab;

    public GameObject PlayerPositionsHolder;

    // Use this for initialization
    void Start()
    {
        if (PlayerPositionsHolder == null)
        {
            Debug.LogError("No position holder obeject specified");
            return;
        }

        if (PlayerPrefab == null)
        {
            Debug.LogError("No prefab for player specified");
            return;
        }

        var transforms = PlayerPositionsHolder.GetComponentsInChildren<Transform>();
        var arduinoScripts = PlayerPositionsHolder.GetComponentsInChildren<TokenMember>();

        // start from i=1 to exclude the parent object itself
        for (var i = 1; i < transforms.Length; i++)
        {
            var t = transforms[i];
            var player = GameObject.Instantiate(PlayerPrefab, t);
            var videoplayer = GameObject.Instantiate(VideoPlayerPrefab, t);

            arduinoScripts[i - 1].Cube = videoplayer;

            // TODO: will be different if we use TextMeshPro
            var textMesh = player.GetComponent<TextMesh>();
            if (textMesh != null)
            {
                textMesh.text = "Player " + i;
            }
        }
    }
}
