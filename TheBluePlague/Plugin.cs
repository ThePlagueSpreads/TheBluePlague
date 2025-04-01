using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheBluePlague;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("com.snmodding.nautilus")]
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

        // register harmony patches, if there are any
        Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void RegisterButtons()
    {
        MainMenuButtonUtils.RegisterButton("Play 2", () => { uGUI_MainMenu.main.OnButtonLoad(); });
        MainMenuButtonUtils.RegisterButton("Delete main menu", () => { Destroy(uGUI_MainMenu.main.gameObject); });
        MainMenuButtonUtils.RegisterButton("Play FNAF world",
            () => { Application.OpenURL("https://gamejolt.com/games/fnaf-world/124921"); });
        MainMenuButtonUtils.RegisterButton("Clown mode",
            () => { Application.OpenURL("https://youtu.be/Yp8o35zahgo?si=PJsYO5khCuo9BIR0"); });
        MainMenuButtonUtils.RegisterButton("Red Plague Act 2 (FREE)", () =>
        {
            Application.OpenURL("https://www.nexusmods.com/subnautica/mods/2080");
            new GameObject("B3NT").AddComponent<WaterSurfaceOnCamera>();
            for (int i = 0; i < 20000; i++)
            {
                Logger.LogWarning(ObfuscateText("THE PLAGUE HEART HAS BREACHED CONTAINMENT."));
                Logger.LogMessage(
                    ObfuscateText("He knows his creation. He will help you. Trust him. He is your only hope."));
            }
            Application.Quit();
        });
        MainMenuButtonUtils.RegisterButton("Red Plague Act Blue", () =>
        {
            BluePlagueManager.IsBluePlagueActive = true;
            uGUI_MainMenu.main.OnButtonSurvival();
        },
        button =>
        {
            button.GetComponent<Image>().color = Color.black;
            button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.2f, 0.4f, 1f);
        });
        MainMenuButtonUtils.RegisterButton("Night", () =>
        {
            uSkyManager.main.Timeline = 7;
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

        return new string(characters);
    }
}