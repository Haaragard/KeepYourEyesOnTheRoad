using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private AudioSource sound;

    private void Start()
    {
        this.sound = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            this.PlayerCollided(other.gameObject.GetComponent<Player>());
        }
    }

    private void PlayerCollided(Player player)
    {
        StartCoroutine(PlaySound());
        StartCoroutine(player.AddACoin());
        StartCoroutine(RemoveCoin());
    }

    public IEnumerator PlaySound()
    {
        this.sound.Play();

        yield return null;
    }

    public IEnumerator RemoveCoin()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;

        Invoke("DestroyCoin", 1f);

        yield return null;
    }

    private void DestroyCoin()
    {
        Destroy(this.gameObject);
    }
}
