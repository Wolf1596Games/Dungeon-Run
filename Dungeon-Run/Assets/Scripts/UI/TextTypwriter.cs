﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypwriter : MonoBehaviour
{
    Text txt;
    string text;
    public float textSpeed = 0.125f;
    public AudioClip[] typingSounds;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Display()
    {
        txt = GetComponent<Text>();
        text = txt.text;
        txt.text = "";

        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach(char c in text)
        {
            txt.text += c;
            //AudioClip sound = typingSounds[Random.Range(0, typingSounds.Length)];
            //audioSource.PlayOneShot(sound);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}