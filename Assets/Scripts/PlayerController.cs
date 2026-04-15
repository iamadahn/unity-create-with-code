using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private bool isDead = false;
    public int lives;
    public float speed = 10.0f;
    private float boundZ = 11.0f;
    private float boundX = 23.0f;
    private bool hasPowerup = false;
    private Rigidbody rigidbodyPl;
    public GameObject powerupIndicator;
    public GameObject projectilePrefub;
    public ParticleSystem explosionParticle;

    private AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip fireSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodyPl = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        ConstrainPlayerPos();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefub, transform.position, projectilePrefub.transform.rotation);
            audioSource.PlayOneShot(fireSound);
        }

        if (lives <= 0)
        {
            Die();
        }
    }

    void MovePlayer()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(verticalInput * speed * Vector3.forward);
        transform.Translate(horizontalInput * speed * Vector3.right);
    }

    void ConstrainPlayerPos()
    {
        // Z bounds
        if (transform.position.z < -boundZ)
        {
            transform.position = new(transform.position.x, transform.position.y, -boundZ);
            ResetLinearVelocityZ();
        }
        else if (transform.position.z > boundZ)
        {
            transform.position = new(transform.position.x, transform.position.y, boundZ);
            ResetLinearVelocityZ();
        }

        // X bounds
        if (transform.position.x < -boundX)
        {
            transform.position = new(-boundX, transform.position.y, transform.position.z);
            ResetLinearVelocityX();
        }
        else if (transform.position.x > boundX)
        {
            transform.position = new(boundX, transform.position.y, transform.position.z);
            ResetLinearVelocityX();
        }
    }

    void ResetLinearVelocityX()
    {
        rigidbodyPl.linearVelocity = new (0, rigidbodyPl.linearVelocity.y, rigidbodyPl.linearVelocity.z);
    }

    void ResetLinearVelocityZ()
    {
       rigidbodyPl.linearVelocity = new (rigidbodyPl.linearVelocity.x, rigidbodyPl.linearVelocity.y, 0); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
            if (lives < 5)
            {
                lives += 1;                
            }
        }

        if (other.CompareTag("EnemyProjectile") && !hasPowerup)
        {
            var enemyProjectile = other.gameObject.GetComponent<Projectile>();
            lives -= enemyProjectile.damage;
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!hasPowerup)
            {
                lives = 0;
            }
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    void Die()
    {
        if (!isDead)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject, deathSound.length);
            isDead = true;
        }
    }

    public int Lives()
    {
        return lives;
    }
}
