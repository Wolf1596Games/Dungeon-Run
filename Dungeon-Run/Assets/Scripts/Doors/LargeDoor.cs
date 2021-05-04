using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeDoor : MonoBehaviour
{
    public KeySwitch switch1;
    public KeySwitch switch2;
    public string animTriggerName;
    public AudioClip openingSound;
    public bool opened;
    public float openingTime = 7f;

    private Collider2D collider2D;
    private SpriteRenderer sprRenderer;
    private AudioSource audioSource;

    private void Awake()
    {
        collider2D = GetComponentInChildren<Collider2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(switch1.active && switch2.active && !opened)
        {
            audioSource.PlayOneShot(openingSound);
            opened = true;

            StartCoroutine("Opening");
        }
    }

    private IEnumerator Opening()
    {
        collider2D.isTrigger = true;
        yield return new WaitForSeconds(openingTime);

        sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 0);
    }
}
