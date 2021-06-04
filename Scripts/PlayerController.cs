using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int coinAmount;

    public float moveSpeed;
    public float jumpSpeed;
    public GameObject coin;

    float playerMass = 0.1f;

    float horizontal;
    float vertical;
    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        coinAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.LevelFinished(false);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //get rid of gold
            ThrowCoin();
        }
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        //                                                                          move slower if have coins (origional mass is 0.5)
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontal * moveSpeed / (GetComponent<Rigidbody2D>().mass + 0.5f));
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * vertical * jumpSpeed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            GetComponent<Rigidbody2D>().mass += collision.transform.GetComponent<CoinScript>().CoinMass / 100;
            coinAmount++;

            SoundManagerScript.PlaySound(SoundNames.CoinCollect);

            LevelManager.UpdateCoinText();
        }
        else if (collision.CompareTag("Obstacles"))
        {
            SoundManagerScript.PlaySound(SoundNames.Hit);

            //Die
            LevelManager.LevelFinished(false);
        }
        else if (collision.CompareTag("Finish"))
        {
            //game is finished
            LevelManager.LevelFinished(true);
        }
        else if (collision.CompareTag("Fake"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            GetComponent<Rigidbody2D>().mass += collision.transform.GetComponent<CoinScript>().CoinMass / 100;
            coinAmount++;
            //coin colected sound
            Destroy(collision.gameObject);

            SoundManagerScript.PlaySound(SoundNames.CoinCollect);
        }
    }

    void ThrowCoin()
    {
        if (coinAmount > 0)
        {
            float throwForce = 100;
            //instanciate coin
            GameObject outCoin = Instantiate(coin, transform.position + Vector3.up * 1.2f, Quaternion.identity);

            if (facingRight)
            {
                outCoin.GetComponent<Rigidbody2D>().AddForce(this.transform.right * throwForce);
            }
            else
            {
                outCoin.GetComponent<Rigidbody2D>().AddForce(this.transform.right * -1 * throwForce + this.transform.up * throwForce / 5);
            }
            coinAmount--;

            outCoin.GetComponent<CoinScript>().StartTimer(0);

            GetComponent<Rigidbody2D>().mass -= outCoin.transform.GetComponent<CoinScript>().CoinMass / 100;

            SoundManagerScript.PlaySound(SoundNames.CoinsThrow);

            LevelManager.UpdateCoinText();
        }
    }
}
