using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    public Rigidbody2D rb; 
    public int damage;
    public GameObject explosion;
    private Transform opponent;
    public float knockbackForce;
    public float knockbackDuration;
    private Rigidbody2D enemy;


    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        PlayerLife2 playerLife = hitInfo.GetComponent<PlayerLife2>();
        if(playerLife != null)
        {
            enemy = hitInfo.attachedRigidbody; 
            opponent = hitInfo.transform; 
            playerLife.TakeDamage(damage);
            StartCoroutine(Knockback(knockbackDuration,knockbackForce,opponent));

        }
        
        Instantiate(explosion,transform.position,transform.rotation);
        Destroy(gameObject);
    }

     public IEnumerator Knockback(float knockbackDuration, float knockbackForce, Transform obj)
    {
        float timer = 0;

        while(knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            UnityEngine.Vector2 direction =  (this.transform.position-obj.transform.position).normalized;
            enemy.AddForce(-direction * knockbackForce);
        }   
        yield return 0;
    }
}
