using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

//CustomEditor(typeof()) 用于关联你要自定义的脚本
//[CustomEditor(typeof(Gun))]
//必须要让该类继承自Editor,且不需要导入UnityEditor程序集
public class GunEditor : Editor
{
    Gun gun;

    public float switchGunTime = 10f;

    void OnEnable()
    {
        //获取当前编辑自定义Inspector的对象
        gun = (Gun)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        //绘制palyer的基本信息
        EditorGUILayout.LabelField("Gun Info");
        gun.gun = (GameObject)EditorGUILayout.ObjectField("Gun", gun.gun, typeof(GameObject), true);
        gun.gunLine = (LineRenderer)EditorGUILayout.ObjectField("GunLaser", gun.gunLine, typeof(LineRenderer), true);
        gun.damage = EditorGUILayout.IntField("GunDamage", gun.damage);
        gun.reload = EditorGUILayout.Toggle("Reload", gun.reload);

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal("BOX");
        Texture2D pistolTexture = Resources.Load<Texture2D>("Work/Image/pistol");
        GUILayout.Label(pistolTexture, GUILayout.Height(125), GUILayout.Width(200));
        GUILayout.BeginVertical("BOX");
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Pistol");
        gun.gunAudio = (AudioSource)EditorGUILayout.ObjectField("Gun Audio", gun.gunAudio, typeof(AudioSource), true);
        gun.pistolShootSound = (AudioClip)EditorGUILayout.ObjectField("Pistol Shoot Sound", gun.pistolShootSound, typeof(AudioClip), true);
        gun.pistolReloadSound = (AudioClip)EditorGUILayout.ObjectField("Pistol Reload Sound", gun.pistolReloadSound, typeof(AudioClip), true);
        gun.pistolSwitchSound = (AudioClip)EditorGUILayout.ObjectField("Pistol Switch Sound", gun.pistolSwitchSound, typeof(AudioClip), true);
        gun.pistolNextFireTime = EditorGUILayout.FloatField("Pistol Next Fire Time", gun.pistolNextFireTime);
        gun.pistolReloadTime = EditorGUILayout.FloatField("Pistol Reolad Time", gun.pistolReloadTime);
        gun.pistolReloadSoundPitch = EditorGUILayout.FloatField("Pistol Reolad Sound Pitch", gun.pistolReloadSoundPitch);
        gun.pistolSpeedReloadTime = EditorGUILayout.FloatField("Pistol Speed Reolad Time", gun.pistolSpeedReloadTime);
        gun.pistolSpeedReloadSoundPitch = EditorGUILayout.FloatField("Pistol Speed Reolad Sound Pitch", gun.pistolSpeedReloadSoundPitch);
        gun.Pistol = EditorGUILayout.Toggle("Pistol", gun.Pistol);
        EditorGUILayout.Space();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("BOX");
        Texture2D smgTexture = Resources.Load<Texture2D>("Work/Image/uzi");
        GUILayout.Label(smgTexture, GUILayout.Height(125), GUILayout.Width(200));
        GUILayout.BeginVertical("BOX");
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Smg");
        gun.gunAudio = (AudioSource)EditorGUILayout.ObjectField("Gun Audio", gun.gunAudio, typeof(AudioSource), true);
        gun.smgShootSound = (AudioClip)EditorGUILayout.ObjectField("Smg Shoot Sound", gun.smgShootSound, typeof(AudioClip), true);
        gun.smgReloadSound = (AudioClip)EditorGUILayout.ObjectField("Smg Reload Sound", gun.smgReloadSound, typeof(AudioClip), true);
        gun.smgSwitchSound = (AudioClip)EditorGUILayout.ObjectField("Smg Switch Sound", gun.smgSwitchSound, typeof(AudioClip), true);
        gun.smgNextFireTime = EditorGUILayout.FloatField("Smg Next Fire Time", gun.smgNextFireTime);
        gun.smgReloadTime = EditorGUILayout.FloatField("Smg Reolad Time", gun.smgReloadTime);
        gun.smgReloadSoundPitch = EditorGUILayout.FloatField("Smg Reolad Sound Pitch", gun.smgReloadSoundPitch);
        gun.smgSpeedReloadTime = EditorGUILayout.FloatField("Smg Speed Reolad Time", gun.smgSpeedReloadTime);
        gun.smgSpeedReloadSoundPitch = EditorGUILayout.FloatField("Smg Speed Reolad Sound Pitch", gun.smgSpeedReloadSoundPitch);
        gun.Smg = EditorGUILayout.Toggle("Smg", gun.Smg);
        EditorGUILayout.Space();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("BOX");
        if (GUILayout.Button("Swtich Gun"))
        {
            gun.StartCoroutine(gun.IESwitchGun(switchGunTime));
        }
        switchGunTime = EditorGUILayout.FloatField("Switch Gun Time", switchGunTime);
        GUILayout.EndHorizontal();
    }
}