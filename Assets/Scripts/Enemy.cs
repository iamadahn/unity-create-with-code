using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isDead = false;
    public int health;
    public float rotationSpeed;
    public GameObject projectilePrefub;
    public int projectileBurst;
    public bool isBoss = false;
    public ParticleSystem explosionParticle;

    private AudioSource audioSource;
    public AudioClip deathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(FireBurst), Random.Range(0.0f, 2.0f), Random.Range(2.0f, 5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        transform.Rotate(Time.deltaTime * rotationSpeed * Vector3.up);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            var playerProjectile = other.gameObject.GetComponent<Projectile>();
            health -= playerProjectile.damage;
            Destroy(other.gameObject);
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject, deathSound.length);
        }
    }

    void FireBurst()
    {
        for (int i = 0; i < projectileBurst; i++)
        {
            Invoke(nameof(FireProjectile), i * 0.33f);
        }
    }
    void FireProjectile()
    {
        Instantiate(projectilePrefub, transform.position, projectilePrefub.transform.rotation);
    }
}
