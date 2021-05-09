using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoor : MonoBehaviour
{
    [SerializeField] private Switch activatorSwitch;
    [SerializeField] private PressurePlate activatorPlate;
    [SerializeField] private string triggerName;
    public AudioClip openingSound;
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
        if(activatorSwitch != null && activatorSwitch.active)
        {
            StartCoroutine("Opening");
        }
        else if(activatorPlate != null && activatorPlate.active)
        {
            StartCoroutine("Opening");
        }
    }

    private IEnumerator Opening()
    {
        audioSource.PlayOneShot(openingSound);
        collider2d.isTrigger = true;
        anim.SetBool(triggerName, true);

        yield return new WaitForSeconds(openingTime);

        sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 0);
        anim.SetBool(triggerName, false);
        anim.SetBool("opened", true);
    }
}
