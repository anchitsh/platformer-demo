using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject ScorePlusPrefab, CoinParticlePrefab;
    private GameObject Player;
    public float magnetDistance, speed;
    private float playerPosition, coinPosition, distance;
    Vector2 playerDirection;

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        playerPosition = Player.transform.position.x;
        coinPosition = transform.position.x;
        distance = Mathf.Abs(playerPosition - coinPosition);
        if (distance < magnetDistance)
        {
            float step = speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CoinManager.coins++;
            GameObject score = Instantiate(ScorePlusPrefab);
            score.transform.position = transform.position +new Vector3(0, .3f, 0);
            GameObject particle = Instantiate(CoinParticlePrefab);
            particle.transform.position = transform.position + new Vector3(0, 0, 0);
            Destroy(this.gameObject);
        }
    }
}
