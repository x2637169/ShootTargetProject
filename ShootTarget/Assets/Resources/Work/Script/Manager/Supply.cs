using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    public GameObject MedKit;
    public GameObject AmmoKit;
    public GameObject GunKit;

    public float medKitMinTime, medKitMaxTime;
    public float ammoKitMinTime, ammoKitMaxTime;
    public float gunKitMinTime, gunKitMaxTime;

    private float instantiateMedKitDeltaTime;
    private float instantiateAmmoKitDeltaTime;
    private float instantiateGunKitDeltaTime;

    private float instantiateMedTime;
    private float instantiateAmmoTime;
    private float instantiateGunTime;

    public bool instantiateMedKit;
    public bool instantiateAmmoKit;
    public bool instantiateGunKit;

    void Awake()
    {
        instantiateMedTime = Random.Range(medKitMinTime, medKitMaxTime);
        instantiateAmmoTime = Random.Range(ammoKitMinTime, ammoKitMaxTime);
        instantiateGunTime = Random.Range(gunKitMinTime, gunKitMaxTime);
    }

    void Start()
    {

    }


    void Update()
    {
        if (Time.timeScale == 0) return;
        if (instantiateMedKit)
        {
            instantiateMedKit = false;
            Instantiate(MedKit, new Vector3(Random.Range(20, 35), 0, Random.Range(10, 20)), MedKit.transform.rotation);
        }
        if (instantiateAmmoKit)
        {
            instantiateAmmoKit = false;
            Instantiate(AmmoKit, new Vector3(Random.Range(20, 35), 0, Random.Range(10, 20)), AmmoKit.transform.rotation);
        }
        if (instantiateGunKit)
        {
            instantiateGunKit = false;
            Instantiate(GunKit, new Vector3(Random.Range(20, 35), 0, Random.Range(10, 20)), GunKit.transform.rotation);
        }

        InstantiateMedKit();
        InstantiateAmmoKit();
        InstantiateGunKit();
    }

    void InstantiateMedKit()
    {
        instantiateMedKitDeltaTime += Time.deltaTime;

        if (instantiateMedTime < instantiateMedKitDeltaTime)
        {
            Instantiate(MedKit, new Vector3(Random.Range(20, 35), 0, Random.Range(10, 20)), MedKit.transform.rotation);

            instantiateMedKitDeltaTime = 0;

            instantiateMedTime = Random.Range(medKitMinTime, medKitMaxTime);
        }
    }

    void InstantiateAmmoKit()
    {
        instantiateAmmoKitDeltaTime += Time.deltaTime;

        if (instantiateAmmoTime < instantiateAmmoKitDeltaTime)
        {
            Instantiate(AmmoKit, new Vector3(Random.Range(20, 35), 0, Random.Range(10, 20)), AmmoKit.transform.rotation);

            instantiateAmmoKitDeltaTime = 0;

            instantiateAmmoTime = Random.Range(ammoKitMinTime, ammoKitMaxTime);
        }
    }

    void InstantiateGunKit()
    {
        instantiateGunKitDeltaTime += Time.deltaTime;

        if (instantiateGunTime < instantiateGunKitDeltaTime)
        {
            Instantiate(GunKit, new Vector3(Random.Range(20, 35), 0, Random.Range(10, 20)), GunKit.transform.rotation);

            instantiateGunKitDeltaTime = 0;

            instantiateGunTime = Random.Range(gunKitMinTime, gunKitMaxTime);
        }
    }
}