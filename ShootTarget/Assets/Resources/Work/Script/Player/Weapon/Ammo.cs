using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Ammo : MonoBehaviour
{
    Gun gun;

    public GameObject[] pistolAmmos;
    public int pistolMaxAmmo = 7;
    public int pistolClipAmmo;
    public int pistolCurAmmo;

    public GameObject[] smgAmmos;
    public int smgMaxAmmo = 20;
    public int smgClipAmmo;
    public int smgCurAmmo;

    [HideInInspector] public Slider reloadTimeSlider;
    [HideInInspector] public Image reloadSliderBackground;
    [HideInInspector] public float reloadSliderBackgroundColor = 0;

    public bool AmmoFdae = false;

    public bool infityAmmo;

    // Start is called before the first frame update
    void Awake()
    {
        gun = GetComponent<Gun>();
        pistolAmmos = GameObject.FindGameObjectsWithTag("PistolAmmo").OrderBy(go => go.name).ToArray();
        smgAmmos = GameObject.FindGameObjectsWithTag("SmgAmmo").OrderBy(go => go.name).ToArray();
        reloadTimeSlider = GameObject.Find("ReloadTimeSlider").GetComponent<Slider>();
        reloadSliderBackground = GameObject.Find("ReloadSliderBackground").GetComponent<Image>();

        reloadTimeSlider.value = 0f;
        reloadSliderBackground.color = new Color(reloadSliderBackground.color.r, reloadSliderBackground.color.g, reloadSliderBackground.color.b, reloadSliderBackgroundColor);
        pistolCurAmmo = pistolClipAmmo;
        smgCurAmmo = smgClipAmmo;

        SwitchGunAmmoDisable();
        cheackAmmoAmount();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReloadAmmo()
    {
        pistolCurAmmo = pistolClipAmmo;
        smgCurAmmo = smgClipAmmo;

        cheackAmmoAmount();
    }

    public void MinusAmmo()
    {
        if (infityAmmo) return;

        if (gun.Pistol)
        {
            AmmoAnimation pistolAmmoAnim = pistolAmmos[pistolCurAmmo - 1].GetComponent<AmmoAnimation>();
            pistolAmmoAnim.AmmoOutAnim();
            pistolCurAmmo--;
        }
        else if (gun.Smg)
        {
            AmmoAnimation smgAmmoAnim = smgAmmos[smgCurAmmo - 1].GetComponent<AmmoAnimation>();
            smgAmmoAnim.AmmoOutAnim();
            smgCurAmmo--;
        }
    }

    public void AddAmmo(int addAmmo)
    {
        pistolClipAmmo += addAmmo;
        smgClipAmmo += addAmmo;

        pistolClipAmmo = Mathf.Clamp(pistolClipAmmo, 0, pistolMaxAmmo);
        smgClipAmmo = Mathf.Clamp(smgClipAmmo, 0, smgMaxAmmo);
        
        pistolCurAmmo = pistolClipAmmo;
        smgCurAmmo = smgClipAmmo;

        ClearAmmo();
        cheackAmmoAmount();
    }

    public void ClearAmmo()
    {
        if (gun.Pistol)
        {
            for (int i = 0; i < pistolMaxAmmo; i++)
            {
                if (pistolAmmos[i] == true)
                    pistolAmmos[i].SetActive(false);
            }
        }
        else if (gun.Smg)
        {
            for (int i = 0; i < smgMaxAmmo; i++)
            {
                if (smgAmmos[i] == true)
                    smgAmmos[i].SetActive(false);
            }
        }
    }

    public void SwitchGunAmmoDisable()
    {
        if (!gun.Pistol)
        {
            for (int i = 0; i < pistolMaxAmmo; i++)
            {
                if (pistolAmmos[i].activeInHierarchy == true)
                    pistolAmmos[i].SetActive(false);
            }
        }
        else if (!gun.Smg)
        {
            for (int i = 0; i < smgMaxAmmo; i++)
            {
                if (smgAmmos[i].activeInHierarchy == true)
                    smgAmmos[i].SetActive(false);
            }
        }
    }

    public void cheackAmmoAmount()
    {
        pistolClipAmmo = Mathf.Clamp(pistolClipAmmo, 0, pistolMaxAmmo);
        smgClipAmmo = Mathf.Clamp(smgClipAmmo, 0, smgMaxAmmo);

        if (gun.Pistol)
        {
            for (int i = 0; i < pistolMaxAmmo; i++)
            {
                if (i < pistolCurAmmo && pistolAmmos[i].activeInHierarchy == false)
                    pistolAmmos[i].SetActive(true);
                else if (i >= pistolCurAmmo && pistolAmmos[i].activeInHierarchy == true)
                    pistolAmmos[i].SetActive(false);
            }
        }
        else if (gun.Smg)
        {
            for (int i = 0; i < smgMaxAmmo; i++)
            {
                if (i < smgCurAmmo && smgAmmos[i].activeInHierarchy == false)
                    smgAmmos[i].SetActive(true);
                else if (i >= smgCurAmmo && smgAmmos[i].activeInHierarchy == true)
                    smgAmmos[i].SetActive(false);
            }
        }

        if (AmmoFdae)
            AmmoFade();
    }

    public void AmmoOrigFade()
    {
        AmmoFdae = false;

        for (int i = 0; i < smgMaxAmmo; i++)
        {
            AmmoAnimation smgAmmoAnim = smgAmmos[i].GetComponent<AmmoAnimation>();
            smgAmmoAnim.SmgAmmoDisFade();
        }
    }

    public void AmmoFade()
    {
        AmmoFdae = true;

        for (int i = 0; i < smgMaxAmmo; i++)
        {
            AmmoAnimation smgAmmoAnim = smgAmmos[i].GetComponent<AmmoAnimation>();
            smgAmmoAnim.SmgAmmoFade();
        }
    }

    public IEnumerator EnableReloadSlider()
    {
        while (reloadSliderBackgroundColor <= 1)
        {
            reloadSliderBackgroundColor += 2f * Time.unscaledDeltaTime;
            reloadSliderBackground.color = new Color(reloadSliderBackground.color.r, reloadSliderBackground.color.g, reloadSliderBackground.color.b, reloadSliderBackgroundColor);
            yield return null;
        }
    }

    public IEnumerator DisableReloadSlider()
    {
        while (reloadSliderBackgroundColor >= 0)
        {
            reloadSliderBackgroundColor -= 1f * Time.unscaledDeltaTime;
            reloadSliderBackground.color = new Color(reloadSliderBackground.color.r, reloadSliderBackground.color.g, reloadSliderBackground.color.b, reloadSliderBackgroundColor);
            yield return null;
        }
    }

}