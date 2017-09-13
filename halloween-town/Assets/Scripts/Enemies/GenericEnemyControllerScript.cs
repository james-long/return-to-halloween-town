using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyControllerScript : MonoBehaviour {

    public bool beingHit = false;
    public bool facingRight = true;
    public float health = 1;
    public int damageDone = 1;
    public GameObject playerGameObject;

    float forceOfKnockback;

    // Use this for initialization
    void Start () {
        forceOfKnockback = 200f;

    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (health <= 0)
        {
            Destroy(gameObject, 0.2f);
        }
    }


    public void GetHit(Vector3 comparisonX_damage_knockback)
    {
        beingHit = true;
        health -= comparisonX_damage_knockback.y;

        forceOfKnockback = comparisonX_damage_knockback.z;
        if (forceOfKnockback != 0f)
        {
            if (comparisonX_damage_knockback.x <= GetComponent<Transform>().position.x)
            {

                GetComponent<Rigidbody2D>().AddForce(new Vector2(forceOfKnockback, 200), ForceMode2D.Impulse);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * forceOfKnockback, 200), ForceMode2D.Impulse);
            }
        }
        StartCoroutine(flashRed());
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator flashRed()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        
    }
}
