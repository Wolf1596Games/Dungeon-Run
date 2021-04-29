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
    //private Animator animator;

    private void Awake()
    {
        collider2d = GetComponentInChildren<Collider2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        //animator = GetComponent<Animator>();
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
        yield return new WaitForSeconds(openingTime);

        collider2d.isTrigger = true;
        sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 0);
        //animator.SetTrigger(triggerName);
    }
}
