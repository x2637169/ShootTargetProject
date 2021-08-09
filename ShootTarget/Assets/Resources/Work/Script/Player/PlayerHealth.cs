using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]
    public float maxHealth;
    public float playerHealth;
    public float extraHealth;
    public float maxExtraHealth = 50;
    public float lostExtraHealthSpeed;
    public Slider healthSlider;
    public Slider extraHealhSlider;

    public GameObject damageImage;

    private float randomX;
    private float randomY;

    public float disGuiTime;

    private AudioSource playerAudio;
    public AudioClip playeHurt;

    public GameObject canvas;
    public GameObject enemyCar;

    CameraShake cam;
    public float camShakeDuration;
    public float camShakeMagnitude;

    private Menu menu;

    public bool GetDamage;
    public bool GodMode;

    void Awake()
    {
        menu = GameObject.FindWithTag("Manager").GetComponent<Menu>();
        playerAudio = GetComponent<AudioSource>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();

        maxHealth = playerHealth;

        damageImage.SetActive(false);

        CheackHealth();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (extraHealth > 0 && Time.timeScale != 0 && !menu.Stop)
        {
            extraHealth -= lostExtraHealthSpeed * Time.unscaledDeltaTime;
            extraHealhSlider.value = extraHealth;
        }
    }

    void CheackHealth()
    {
        if (playerHealth > maxHealth)
        {
            extraHealth = (extraHealth + playerHealth) - maxHealth;
            if (extraHealth > maxExtraHealth)
                extraHealth = maxExtraHealth;

            playerHealth = maxHealth;
        }

        if (playerHealth <= 0)
        {
            playerHealth = 0;

            PlayerDead();
        }

        extraHealhSlider.value = extraHealth;
        healthSlider.value = playerHealth;
    }

    public void PlayerTakeDamage(int damage)
    {
        if (GodMode) return;

        GetDamage = true;

        CreateBulletHole();
        
        damageImage.SetActive(true);

        StartCoroutine(cam.CamShake(camShakeDuration, camShakeMagnitude));

        playerAudio.PlayOneShot(playeHurt, 2f);

        if (extraHealth > 0)
        {
            extraHealth -= damage;
        }

        if (extraHealth <= 0)
        {
            playerHealth -= damage + Mathf.Abs(extraHealth);
            extraHealth = 0;
        }

        CheackHealth();
    }

    public void AddHealth(int addHealth)
    {
        playerHealth += addHealth;

        CheackHealth();
    }

    void PlayerDead()
    {
        damageImage.SetActive(false);

        menu.DeadMenu();
    }

    void CreateBulletHole()
    {
        randomX = Random.Range(-Screen.width / 2, Screen.width / 2);
        randomY = Random.Range(-Screen.height / 2, Screen.height / 2);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        var createImage = Instantiate(enemyCar, spawnPosition, Quaternion.identity) as GameObject;
        createImage.transform.SetParent(canvas.transform, false);
    }
}
