using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public bool isInAir = false;
    public float TTL = 10;
    public float CoinMass;

    //if in aire play animation (hoverings)


    // Position Storage Variables
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
    private void Update()
    {
        if (isInAir)
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            transform.position = tempPos;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TTL">if TTL = 0 then use prefab original TTL</param>
    public void StartTimer(float newTTL)
    {
        TTL = newTTL != 0 ? newTTL : TTL;
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(TTL);
        print("vanish)");
        Destroy(this.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GetComponent<Rigidbody2D>().velocity == Vector2.zero && collision.transform.CompareTag("Ground"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}

