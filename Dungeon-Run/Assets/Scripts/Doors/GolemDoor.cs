using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDoor : MonoBehaviour
{
    [SerializeField] private GolemPlate activatorPlate;
    [SerializeField] private string triggerName;
    public AudioClip openingSound;
    public bool opened;
    public float openingTime = 7f;

    private Collider2D collider2d;
    private SpriteRenderer sprRenderer;
    private AudioSource audioSource;
    private Animator anim;

    private void Awake()
    {
        collider2d = GetComponentInChildren<Collider2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activatorPlate != null && activatorPlate.active && !opened)
        {
            audioSource.PlayOneShot(openingSound);
            opened = true;

            StartCoroutine("Opening");
        }
    }

    private IEnumerator Opening()
    {
        collider2d.isTrigger = true;
        anim.SetBool(triggerName, true);
        yield return new WaitForSeconds(openingTime);
        

        sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 0);
        anim.SetBool(triggerName, false);
        anim.SetBool("opened", true);
    }
}
