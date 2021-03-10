using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject ScorePlusPrefab, CoinParticlePrefab;
    private GameObject Player;
    public float magnetDistance, speed;
    private float distance;
    private Vector2 playerPosition, coinPosition;
    Vector2 playerDirection;

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        /*
        playerPosition = Player.transform.position;
        coinPosition = transform.position;
        distance = Vector2.Distance(playerPosition, coinPosition);
        if (distance < magnetDistance)
        {
            float step = speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CoinManager.coins++;
            //GameObject score = Instantiate(ScorePlusPrefab);
            //score.transform.position = transform.position +new Vector3(0, .3f, 0);
            //GameObject particle = Instantiate(CoinParticlePrefab);
            //particle.transform.position = transform.position + new Vector3(0, 0, 0);
            this.gameObject.SetActive(false);
        }
    }
}
