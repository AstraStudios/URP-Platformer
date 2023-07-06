using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    GameObject player;
    Vector2 playerSpawn;

    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip deathSound;
    AudioSource laserSource;

    // Start is called before the first frame update
    void Start()
    {
        laserSource = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpawn = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            player.transform.position = playerSpawn;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ParticleSystem spawnedParticles = Instantiate(deathParticles, player.transform.position, gameObject.transform.rotation);
            spawnedParticles.Play();
            laserSource.clip = deathSound;
            laserSource.Play();
            player.transform.position = playerSpawn;
        }
    }
}
