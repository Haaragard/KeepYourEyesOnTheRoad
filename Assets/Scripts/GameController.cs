using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOver;
    public float score;
    private int coins;

    public Text scoreText;
    public Text coinsText;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isDead)
        {
            this.UpdateScore();
            this.UpdateCoins();
        }
    }

    private void UpdateScore()
    {
        score += Time.deltaTime * 5f;
        scoreText.text = Mathf.Round(score).ToString() + "m";
    }

    private void UpdateCoins()
    {
        this.coins = this.player.coins;
        this.coinsText.text = this.coins.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }
}
