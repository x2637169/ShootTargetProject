using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    RaycastHit hit;
    public GameObject gun;
    public LineRenderer gunLine;
    public int damage;

    Ammo ammo;

    public AudioSource gunAudio;
    public AudioClip pistolShootSound;
    public AudioClip pistolReloadSound;
    public AudioClip pistolSwitchSound;
    public AudioClip smgShootSound;
    public AudioClip smgReloadSound;
    public AudioClip smgSwitchSound;

    [HideInInspector] public float normalReloadTime;
    public float pistolReloadTime;
    public float pistolReloadSoundPitch;
    public float smgReloadTime;
    public float smgReloadSoundPitch;
    [HideInInspector] public float speedReloadTime;
    public float pistolSpeedReloadTime;
    public float pistolSpeedReloadSoundPitch;
    public float smgSpeedReloadTime;
    public float smgSpeedReloadSoundPitch;

    public float pistolNextFireTime;
    [HideInInspector] public float pistolFireTime;

    public float smgNextFireTime;
    [HideInInspector] public float smgFireTime;

    [HideInInspector] public float disableEffectsTime;

    public bool reload;
    public bool Pistol;
    public bool Smg;

    Menu menu;
    SlowMotionPower slowMotionPower;

    public GameObject crosshair;

    void Awake()
    {
        slowMotionPower = GameObject.FindWithTag("SlowMotionManager").GetComponent<SlowMotionPower>();
        menu = GameObject.FindWithTag("Manager").GetComponent<Menu>();
        ammo = GetComponent<Ammo>();
        gunAudio = GetComponent<AudioSource>();
        gunLine = GameObject.Find("GunLine").GetComponent<LineRenderer>();
        gunLine.enabled = false;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (reload) return;

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        MobileInput();
#else
        OtherInput();
#endif

        NextShootTime();
        CheckAmmo();
    }

    void MobileInput()
    {
        if (Input.touchCount <= 0) return;

        if (Input.touchCount == 1)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    if (Pistol && pistolFireTime >= pistolNextFireTime && ammo.pistolCurAmmo > 0)
                    {
                        Shoot();
                        pistolFireTime = 0;
                    }

                    if (Smg && smgFireTime >= smgNextFireTime && ammo.smgCurAmmo > 0)
                    {
                        Shoot();
                        smgFireTime = 0;
                    }
                }

                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    if (Smg && smgFireTime >= smgNextFireTime && ammo.smgCurAmmo > 0)
                    {
                        Shoot();
                        smgFireTime = 0;
                    }
                }
            }
        }
    }

    void OtherInput()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Pistol && pistolFireTime >= pistolNextFireTime)
            {
                if (Input.GetMouseButtonDown(0) && ammo.pistolCurAmmo > 0)
                {
                    Shoot();
                    pistolFireTime = 0;
                }
            }

            if (Smg && smgFireTime >= smgNextFireTime)
            {
                if (Input.GetMouseButton(0) && ammo.smgCurAmmo > 0)
                {
                    Shoot();
                    smgFireTime = 0;
                }
            }
        }
    }

    void NextShootTime()
    {
        if (pistolFireTime <= pistolNextFireTime && Pistol)
            pistolFireTime += Time.unscaledDeltaTime;

        if (smgFireTime <= smgNextFireTime && Smg)
            smgFireTime += Time.unscaledDeltaTime;
    }

    void CheckAmmo()
    {
        if (ammo.pistolCurAmmo <= 0 || ammo.smgCurAmmo <= 0 && !reload)
        {
            if (Input.GetMouseButton(0))
            {
                if (slowMotionPower.SlowMotionOn)
                    StartCoroutine(speedReload());
                else
                    StartCoroutine(Reload());
            }
        }

        if (ammo.pistolCurAmmo < ammo.pistolClipAmmo || ammo.smgCurAmmo < ammo.smgClipAmmo)
        {
            if (Input.GetKeyDown(KeyCode.R) && !slowMotionPower.SlowMotionOn)
                StartCoroutine(Reload());
            else if (Input.GetKeyDown(KeyCode.R) && slowMotionPower.SlowMotionOn)
                StartCoroutine(speedReload());
        }
    }

    void Shoot()
    {
        ammo.MinusAmmo();

        if (Pistol)
            gunAudio.PlayOneShot(pistolShootSound, 1.5f);
        if (Smg)
            gunAudio.PlayOneShot(smgShootSound, 1.5f);

        gunLine.SetPosition(0, gun.transform.position);

        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            gunLine.SetPosition(1, hit.point);

            EnemyHitBox enemyHitBox = hit.collider.GetComponent<EnemyHitBox>();
            if (enemyHitBox != null)
                enemyHitBox.TakeDamage(damage);

            Medkit medKit = hit.collider.GetComponent<Medkit>();
            if (medKit != null)
                medKit.HitMedKit();

            AmmoBox ammoBox = hit.collider.GetComponent<AmmoBox>();
            if (ammoBox != null)
                ammoBox.HitAmmoBox();

            GunKit gunKit = hit.collider.GetComponent<GunKit>();
            if (gunKit != null)
                gunKit.HitGunKit();
        }

        StartCoroutine(DisableEffects());
    }

    IEnumerator Reload()
    {
        reload = true;
        ammo.ClearAmmo();
        StartCoroutine(ammo.EnableReloadSlider());

        if (Pistol)
        {
            normalReloadTime = pistolReloadTime;
            gunAudio.pitch = pistolReloadSoundPitch;
            gunAudio.PlayOneShot(pistolReloadSound, 1f);
        }
        else if (Smg)
        {
            normalReloadTime = smgReloadTime;
            gunAudio.pitch = smgReloadSoundPitch;
            gunAudio.PlayOneShot(smgReloadSound, 1f);
        }

        if (reload)
        {
            ammo.reloadTimeSlider.maxValue = normalReloadTime;
            float normalReoladDeltaTime = 0;
            while (normalReoladDeltaTime < normalReloadTime)
            {
                if (menu.Win || menu.Dead)
                {
                    gunAudio.Stop();
                    yield break;
                }

                if (!reload)
                {
                    StartCoroutine(ammo.DisableReloadSlider());
                    break;
                }

                if (!menu.Stop)
                {
                    ammo.reloadTimeSlider.value = normalReoladDeltaTime * 0.4f;
                    normalReoladDeltaTime += Time.unscaledDeltaTime;

                    if (Pistol)
                        gunAudio.pitch = pistolReloadSoundPitch;
                    else if (Smg)
                        gunAudio.pitch = smgReloadSoundPitch;
                }
                else
                {
                    gunAudio.pitch = 0;
                }

                if (normalReoladDeltaTime > normalReloadTime - 0.1f)
                    StartCoroutine(ammo.DisableReloadSlider());

                yield return null;
            }
        }

        ammo.reloadTimeSlider.value = 0;
        gunAudio.pitch = 1f;
        ammo.ReloadAmmo();
        reload = false;
    }

    IEnumerator speedReload()
    {
        reload = true;
        ammo.ClearAmmo();
        StartCoroutine(ammo.EnableReloadSlider());

        if (Pistol)
        {
            speedReloadTime = pistolSpeedReloadTime;
            gunAudio.pitch = pistolSpeedReloadSoundPitch;
            gunAudio.PlayOneShot(pistolReloadSound, 1f);
        }

        if (Smg)
        {
            speedReloadTime = smgSpeedReloadTime;
            gunAudio.pitch = smgSpeedReloadSoundPitch;
            gunAudio.PlayOneShot(smgReloadSound, 1f);
        }

        if (reload)
        {
            ammo.reloadTimeSlider.maxValue = speedReloadTime;
            float speedReoladDeltaTime = 0;
            while (speedReoladDeltaTime < speedReloadTime)
            {
                if (menu.Win || menu.Dead)
                {
                    gunAudio.Stop();
                    yield break;
                }

                if (!reload)
                {
                    StartCoroutine(ammo.DisableReloadSlider());
                    break;
                }

                if (!menu.Stop)
                {
                    ammo.reloadTimeSlider.value = speedReoladDeltaTime * 0.4f;
                    speedReoladDeltaTime += Time.unscaledDeltaTime;

                    if (Pistol)
                    {
                        gunAudio.pitch = pistolSpeedReloadSoundPitch;
                    }
                    else if (Smg)
                    {
                        gunAudio.pitch = smgSpeedReloadSoundPitch;
                    }
                }
                else
                {
                    gunAudio.pitch = 0;
                }

                if (speedReoladDeltaTime > speedReloadTime - 0.1f)
                    StartCoroutine(ammo.DisableReloadSlider());

                yield return null;
            }
        }

        ammo.reloadTimeSlider.value = 0;
        gunAudio.pitch = 1f;
        ammo.ReloadAmmo();
        reload = false;
    }

    public void SwitchGun(float haveGunTime)
    {
        StartCoroutine(IESwitchGun(haveGunTime));
    }

    public IEnumerator IESwitchGun(float haveGunTime)
    {
        if (Pistol)
        {
            Pistol = false;
            Smg = true;
            reload = false;
            gunAudio.pitch = 1f;
            ammo.ReloadAmmo();
            ammo.SwitchGunAmmoDisable();
            gunAudio.Stop();
            gunAudio.PlayOneShot(smgSwitchSound, 2f);
            bool AmmoFade = false;

            float haveGunDeltaTime = 0;
            while (haveGunDeltaTime < haveGunTime)
            {
                if (!menu.Stop)
                {
                    haveGunDeltaTime += Time.unscaledDeltaTime;
                    if (haveGunDeltaTime > haveGunTime - 5 && !AmmoFade)
                    {
                        AmmoFade = true;
                        ammo.AmmoFade();
                    }
                }
                yield return null;
            }

            Smg = false;
            Pistol = true;
            reload = false;
            gunAudio.pitch = 1f;
            ammo.ReloadAmmo();
            ammo.SwitchGunAmmoDisable();
            gunAudio.Stop();
            gunAudio.PlayOneShot(pistolSwitchSound, 2f);
            ammo.AmmoOrigFade();
        }
    }

    IEnumerator DisableEffects()
    {
        if (Pistol)
            disableEffectsTime = pistolNextFireTime / 2;

        if (Smg)
            disableEffectsTime = smgNextFireTime / 1.2f;

        gunLine.enabled = true;
        yield return new WaitForSeconds(disableEffectsTime);
        gunLine.enabled = false;
    }
}
