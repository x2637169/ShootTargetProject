using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject SettingMenu;
    private Text qualityText;
    private Image qualityImage;
    private List<Sprite> qualitySprites = new List<Sprite>();
    private int qualityNum;
    public GameObject stopMenu;
    public bool Stop;
    public GameObject highScoreMenu;
    public GameObject winMenu;
    [HideInInspector] public bool Win;
    public GameObject deadMenu;
    [HideInInspector] public bool Dead;

    [HideInInspector] public AudioSource bgmAudio;
    public AudioClip startMusic;
    public AudioClip gameBgm;
    public AudioClip winMusic;
    public AudioClip deadMusic;

    [HideInInspector] public AudioSource buttonAudio;
    public AudioClip buttonClickSound;
    public AudioClip buttonEnterSound;

    public Slider[] BgmSlider;
    private float musicVolume = 0.8f;

    private PlayerHealth playerHealth;

    void Awake()
    {
        if (PlayerPrefs.GetInt("FirstTimeSetting") == 0)
            CheckSaveKey();

        bgmAudio = GameObject.Find("BgmAudio").GetComponentInChildren<AudioSource>();
        buttonAudio = GameObject.Find("ButtonAudio").GetComponentInChildren<AudioSource>();

        Time.timeScale = 0f;

        winMenu.gameObject.SetActive(false);
        stopMenu.gameObject.SetActive(false);
        SettingMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);

        musicVolume = PlayerPrefs.GetFloat("BgmVolume");
        bgmAudio.volume = musicVolume;

        bgmAudio.Stop();
        bgmAudio.clip = startMusic;
        bgmAudio.Play();

        qualityNum = PlayerPrefs.GetInt("Quality");

        SetBgmSliderVolume();
        AddSprites();
    }

    void AddSprites()
    {
        qualitySprites.Add(Resources.Load<Sprite>("Work/Image/Quality/Quality_Low"));
        qualitySprites.Add(Resources.Load<Sprite>("Work/Image/Quality/Quality_VeryLow"));
        qualitySprites.Add(Resources.Load<Sprite>("Work/Image/Quality/Quality_Medium"));
        qualitySprites.Add(Resources.Load<Sprite>("Work/Image/Quality/Quality_High"));
        qualitySprites.Add(Resources.Load<Sprite>("Work/Image/Quality/Quality_VeryHigh"));
        qualitySprites.Add(Resources.Load<Sprite>("Work/Image/Quality/Quality_Ultra"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerPrefs.DeleteAll();
    }

    public void RankMenu()
    {
        startMenu.gameObject.SetActive(false);
        highScoreMenu.gameObject.SetActive(true);
    }

    public void returnStartMenu()
    {
        startMenu.gameObject.SetActive(true);
        SettingMenu.gameObject.SetActive(false);
        highScoreMenu.gameObject.SetActive(false);
    }

    public void WinMenu()
    {
        Win = true;
        Time.timeScale = 0f;

        if (playerHealth == null)
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.damageImage.SetActive(false);

        Cursor.visible = true;
        winMenu.gameObject.SetActive(true);

        bgmAudio.Stop();
        bgmAudio.clip = winMusic;
        bgmAudio.Play();
    }

    public void DeadMenu()
    {
        Dead = true;
        Time.timeScale = 0f;

        if (playerHealth == null)
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.damageImage.SetActive(false);

        Cursor.visible = true;
        deadMenu.gameObject.SetActive(true);

        bgmAudio.Stop();
        bgmAudio.clip = deadMusic;
        bgmAudio.Play();
    }

    public void StartButton()
    {
        startMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;

        bgmAudio.Stop();
        bgmAudio.clip = gameBgm;
        bgmAudio.Play();
    }

    public void RestStartButton()
    {
        SceneManager.LoadScene("ShootTarget");
    }

    public void StopButton()
    {
        Stop = true;
        Time.timeScale = 0f;
        stopMenu.gameObject.SetActive(true);
        Cursor.visible = true;
    }

    public void CancelButton()
    {
        Stop = false;
        Time.timeScale = 1f;
        stopMenu.gameObject.SetActive(false);
        Cursor.visible = false;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ButtonClickSound()
    {
        buttonAudio.PlayOneShot(buttonClickSound, 2f);
    }

    public void ButtonEnterSound()
    {
        buttonAudio.PlayOneShot(buttonEnterSound, 2f);
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        bgmAudio.volume = musicVolume;

        PlayerPrefs.SetFloat("BgmVolume", musicVolume);

        SetBgmSliderVolume();
    }

    void SetBgmSliderVolume()
    {
        for (int a = 0; a < BgmSlider.Length; a++)
        {
            BgmSlider[a].value = musicVolume;
        }
    }

    public void SettingButton()
    {
        startMenu.gameObject.SetActive(false);
        SettingMenu.gameObject.SetActive(true);

        if (qualityText == null)
            qualityText = GameObject.Find("SettingMenu/QualitySetting/QualityText").GetComponentInChildren<Text>();
        if (qualityImage == null)
            qualityImage = GameObject.Find("SettingMenu/QualityImage").GetComponentInChildren<Image>();

        QualitySettings.SetQualityLevel(qualityNum, true);
        qualityText.text = ("Quality" + "\n" + QualitySettings.names[qualityNum]);
        qualityImage.sprite = qualitySprites[qualityNum];
    }

    public void QualityLeftButton()
    {
        qualityNum--;

        if (qualityNum < 0)
            qualityNum = 0;

        QualitySettings.SetQualityLevel(qualityNum, true);
        qualityText.text = ("Quality" + "\n" + QualitySettings.names[qualityNum]);
        qualityImage.sprite = qualitySprites[qualityNum];

        PlayerPrefs.SetInt("Quality", qualityNum);
    }

    public void QualityRightButton()
    {
        qualityNum++;

        if (qualityNum > 5)
            qualityNum = 5;

        QualitySettings.SetQualityLevel(qualityNum, true);
        qualityText.text = ("Quality" + "\n" + QualitySettings.names[qualityNum]);
        qualityImage.sprite = qualitySprites[qualityNum];

        PlayerPrefs.SetInt("Quality", qualityNum);
    }

    void CheckSaveKey()
    {
        PlayerPrefs.SetInt("FirstTimeSetting", 1);

        if (PlayerPrefs.GetInt("Quality") == 0)
            PlayerPrefs.SetInt("Quality", 5);

        if (PlayerPrefs.GetFloat("BgmVolume") == 0)
            PlayerPrefs.SetFloat("BgmVolume", 1);
    }
}