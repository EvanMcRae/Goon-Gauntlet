using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    public SpriteRenderer sr, sr2;
    public bool killMode = false;
    private IEnumerator coru;
    private bool waiting = false;
    private AudioSource[] sources;
    public AudioClip stepSound, openSound, closeSound;

    // Start is called before the first frame update
    void Start()
    {
        sources = transform.GetComponents<AudioSource>();
        coru = Wait();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            if (!killMode)
            {
                if (!waiting)
                {
                    GetComponent<SimpleFlash>().Flash(2.0f, 6, false);
                    StartCoroutine(Wait());
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.GetComponent<EnemyMelee>().ApplyDamage(3);
                }
                else if (collision.gameObject.CompareTag("Player"))
                {
                    collision.GetComponent<playerHealth>().takeDamage();
                }
            }
        }
    }

    IEnumerator Wait()
    {
        PlaySound(stepSound);
        waiting = true;
        yield return new WaitForSeconds(2f);
        PlaySound(openSound);
        sr.enabled = false;
        sr2.enabled = true;
        killMode = true;
        yield return new WaitForSeconds(4f);
        PlaySound(closeSound);
        sr.enabled = true;
        sr2.enabled = false;
        killMode = false;
        waiting = false;
    }

    public void PlaySound(AudioClip clip)
    {
        foreach (AudioSource source in sources)
        {
            if (source.clip == clip && source.isPlaying)
            {
                if (source.time < 0.2f && source.isPlaying) return;
                else source.Stop();
            }
        }
        for (int index = sources.Length - 1; index >= 0; index--)
        {
            if (!sources[index].isPlaying)
            {
                sources[index].clip = clip;
                sources[index].loop = false;
                sources[index].Play();
                return;
            }
        }
    }
}
