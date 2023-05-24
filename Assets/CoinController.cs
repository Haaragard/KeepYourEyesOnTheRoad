using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            this.PlayerCollided(other.gameObject.GetComponent<Player>());
        }
    }

    private void PlayerCollided(Player player)
    {
        StartCoroutine(player.AddACoin());

        Destroy(this.gameObject);
    }
}
