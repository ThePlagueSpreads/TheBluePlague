using System.Collections;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Utility;
using TheBluePlague.Mono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheBluePlague;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("com.snmodding.nautilus", "1.0.0.34")]
public class Plugin : BaseUnityPlugin
{
    public new static ManualLogSource Logger { get; private set; }

    private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

    public static AssetBundle Bundle { get; } = AssetBundleLoadingUtils.LoadFromAssetsFolder(Assembly, "theblueplague");

    private void Awake()
    {
        // set project-scoped logger instance
        Logger = base.Logger;

        RegisterButtons();

        SaveUtils.RegisterOnQuitEvent(OnGameQuit);

        // register harmony patches, if there are any
        Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void RegisterButtons()
    {
        MainMenuButtonUtils.RegisterButton("Play 2", () =>
        {
            uGUI_MainMenu.main.OnButtonLoad();
            var text = new BasicText();
            text.ShowMessage("Subnautica BUT BETTER!");
            text.SetColor(Color.red);
        });
        MainMenuButtonUtils.RegisterButton("Delete main menu", () => { Destroy(uGUI_MainMenu.main.gameObject); });
                MainMenuButtonUtils.RegisterButton("Blue", () =>
                {
                    var logo = GameObject.Find("subnautica_logo(Clone)");
                    if (!logo) return;
                    var material = logo.GetComponentInChildren<Renderer>().material;
                    material.color = new Color(0f, 0f, 5f);
                    material.SetColor(ShaderPropertyID._SpecColor, new Color(0, 0, 1.5f));
                    material.SetColor(ShaderPropertyID._GlowColor, new Color(0, 0, 1.5f));
                },
                button => { button.GetComponentsInChildren<Image>().ForEach(i => i.color = Color.green); });
        MainMenuButtonUtils.RegisterButton("Calculator",
            () =>
            {
                var oldMode = Screen.fullScreenMode;
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Application.OpenURL("calc.exe");
                StartCoroutine(RestoreScreenMode(oldMode));
            });
        MainMenuButtonUtils.RegisterButton("Wheel of Fortune", () => { SplashTextAnimate.Main.RandomizeText(); });
        MainMenuButtonUtils.RegisterButton(
            "<color=#FF0000>Cl</color><color=#FF7F00>own</color> <color=#00FF00>m</color><color=#0000FF>o</color><color=#4B0082>d</color><color=#8B00FF>e</color>",
            () =>
            {
                Application.OpenURL("https://youtu.be/Yp8o35zahgo?si=PJsYO5khCuo9BIR0");
                var player = new GameObject("Party Time").AddComponent<AudioSource>();
                player.clip =
                    Bundle.LoadAsset<AudioClip>("Party time");
                player.Play();
                if (FindObjectOfType<AudioListener>() == null)
                    Camera.current.gameObject.AddComponent<AudioListener>();
                var madeInHeaven = new GameObject("MadeInHeaven");
                madeInHeaven.AddComponent<MadeInHeaven>();
                Destroy(madeInHeaven, 20);
            },
            button =>
            {
                button.AddComponent<RainbowColorCycler>().graphics = button.GetComponentsInChildren<Graphic>();
            });
        MainMenuButtonUtils.RegisterButton("Red Plague Act 2 (FREE)", () =>
            {
                Application.OpenURL("https://www.nexusmods.com/subnautica/mods/2080");
                new GameObject("B3NT").AddComponent<WaterSurfaceOnCamera>();
                for (int i = 0; i < 20000; i++)
                {
                    Logger.LogWarning(ObfuscateText("THE PLAGUE HEART HAS BREACHED CONTAINMENT."));
                    Logger.LogMessage(
                        ObfuscateText(
                            "He can harness his creation. He will help you. Trust him. He is your only hope."));
                }

                Application.Quit();
            },
            button => { button.GetComponentsInChildren<Image>().ForEach(i => i.color = Color.red); });
        MainMenuButtonUtils.RegisterButton("Nightnautica", () => { uSkyManager.main.Timeline = 5; });

        MainMenuButtonUtils.RegisterButton("Nothing", () =>
            {
                NothingManager.NothingActive = true;
                new GameObject("Silence").AddComponent<Silence>();
                uGUI_MainMenu.main.OnButtonHardcore();
            },
            button =>
            {
                button.GetComponentsInChildren<Image>().ForEach(i => i.color = Color.gray);
                button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 0, 0);
            });
        MainMenuButtonUtils.RegisterButton("Do you believe in gravity?", () =>
        {
            var madeInHeaven = new GameObject("MadeInHeaven");
            madeInHeaven.AddComponent<MadeInHeaven>();
            Destroy(madeInHeaven, 3);
            var gameObjects = FindObjectsOfType<GameObject>();
            foreach (var go in gameObjects)
            {
                var name = go.name.ToLower();
                if (!name.Contains("logo") && !name.Contains("title"))
                    continue;
                if (go.GetComponent<RectTransform>())
                    continue;
                if (!name.Contains("overlay"))
                {
                    foreach (var renderer in go.GetComponentsInChildren<MeshRenderer>())
                    {
                        var boundsObject = new GameObject("Collider");
                        boundsObject.transform.position = renderer.bounds.center;
                        boundsObject.transform.localScale = renderer.bounds.size / 1.15f;
                        boundsObject.gameObject.AddComponent<BoxCollider>();
                        boundsObject.transform.SetParent(go.transform, true);
                    }
                }

                var rb = go.EnsureComponent<Rigidbody>();
                rb.mass = 500;
                rb.useGravity = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                go.EnsureComponent<FakeWaterPhysics>();
            }
        });
        MainMenuButtonUtils.RegisterButton("Red Plague Act Blue", () =>
            {
                BluePlagueManager.IsBluePlagueActive = true;
                uGUI_MainMenu.main.OnButtonSurvival();
            },
            button =>
            {
                button.GetComponentsInChildren<Image>().ForEach(i => i.color = Color.black);
                button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.2f, 0.4f, 1f);
            });
    }

    private static string ObfuscateText(string text)
    {
        var characters = text.ToCharArray();

        for (int i = 0; i < characters.Length; i++)
        {
            if (Random.value < 0.5f)
            {
                characters[i] = (char)Random.Range(0, 1000);
            }
        }

        var txt = new string(characters);
        ErrorMessage.AddMessage(txt);
        return txt;
    }

    private static IEnumerator RestoreScreenMode(FullScreenMode mode)
    {
        yield return new WaitForSeconds(1);
        Screen.fullScreenMode = mode;
    }

    private static void OnGameQuit()
    {
        BluePlagueManager.IsBluePlagueActive = false;
        NothingManager.NothingActive = false;
    }
}