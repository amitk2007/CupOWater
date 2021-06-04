using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObsecalsType
{
    MovingBlock,
    Bloder,
    Wind,
}
public enum WindDirection
{
    up,
    down,
    left,
    right
}

public class ObsecalsScript : MonoBehaviour
{
    public ObsecalsType type;
    public AnimationVariables animationVariables;

    public WindVariables windVariables;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case ObsecalsType.MovingBlock:
                animationVariables.tempPos = animationVariables.posOffset;
                animationVariables.tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * animationVariables.frequency) * animationVariables.amplitude;
                transform.position = animationVariables.tempPos;
                break;
            case ObsecalsType.Bloder:
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& type == ObsecalsType.Wind)
        {
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            switch (windVariables.direction)
            {
                case WindDirection.up:
                    if (rigidbody.velocity.y < windVariables.maxVelocity)
                    {
                        rigidbody.AddForce(Vector2.up * windVariables.basicForce);
                    }
                    break;
                case WindDirection.down:
                    if (rigidbody.velocity.y > -windVariables.maxVelocity)
                    {
                        rigidbody.AddForce(Vector2.up * -1*windVariables.basicForce);
                    }
                    break;
                case WindDirection.left:
                    if (rigidbody.velocity.y < windVariables.maxVelocity)
                    {
                        rigidbody.AddForce(Vector2.right * windVariables.basicForce);
                    }
                    break;
                case WindDirection.right:
                    if (rigidbody.velocity.y > -windVariables.maxVelocity)
                    {
                        rigidbody.AddForce(Vector2.right * -1 * windVariables.basicForce);
                    }
                    break;
                default:
                    break;
            }

        }
    }
}

[System.Serializable]
public struct AnimationVariables
{
    // Position Storage Variables
    public float amplitude;// = 0.5f;
    public float frequency;// = 1f;

    //[HideInInspector]
    public Vector3 posOffset;// = new Vector3();
    [HideInInspector]
    public Vector3 tempPos;// = new Vector3();
}

[System.Serializable]
public struct WindVariables
{
    public float basicForce;
    public float maxVelocity;
    public WindDirection direction;
}