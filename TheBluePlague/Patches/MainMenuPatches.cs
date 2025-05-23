using HarmonyLib;
using Nautilus.Extensions;
using TheBluePlague.Mono;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TheBluePlague.Patches;

[HarmonyPatch(typeof(uGUI_MainMenu))]
public static class MainMenuPatches
{
    [HarmonyPatch(nameof(uGUI_MainMenu.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(uGUI_MainMenu __instance)
    {
        var buttonParent = __instance.gameObject.GetComponentInChildren<MainMenuPrimaryOptionsMenu>().transform
            .Find("PrimaryOptions/MenuButtons");
        var buttonData = MainMenuButtonUtils.GetButtonData();
        foreach (var button in buttonData)
        {
            CreateButton(buttonParent, button);
        }

        buttonParent.localPosition = new Vector3(0, 240, 0);

        var root = __instance.transform.root;
        var graphicsDevice = root.SearchChild("GraphicsDeviceName").gameObject;
        graphicsDevice.SetActive(true);
        var splash = Object.Instantiate(graphicsDevice, graphicsDevice.transform.parent);
        splash.name = "Splash";
        Object.Destroy(splash.GetComponent<uGUI_GraphicsDeviceName>());
        var splashTransform = splash.GetComponent<RectTransform>();
        splashTransform.localPosition = new Vector3(500, 450, 0);
        splashTransform.localEulerAngles = new Vector3(0, 0, 350);
        var splashText = splash.GetComponent<TextMeshProUGUI>();
        splashText.color = new Color(1, 1, 0);
        splashText.text = SplashTextManager.GetRandomSplashText();
        splashText.horizontalAlignment = HorizontalAlignmentOptions.Center;
        splash.AddComponent<SplashTextAnimate>();
        
        graphicsDevice.AddComponent<RainbowColorCycler>().graphics = new[] { graphicsDevice.GetComponent<Graphic>() };
    }

    private static void CreateButton(Transform buttonParent, MainMenuButtonUtils.ButtonData data)
    {
        var playButton = buttonParent.Find("ButtonPlay").gameObject;
        var newButton = Object.Instantiate(playButton);
        newButton.GetComponent<RectTransform>().SetParent(playButton.transform.parent, false);
        newButton.name = data.Name;
        var text = newButton.GetComponentInChildren<TextMeshProUGUI>();
        text.text = data.Name;
        Object.DestroyImmediate(text.gameObject.GetComponent<TranslationLiveUpdate>());
        newButton.transform.SetSiblingIndex(0);
        var button = newButton.GetComponent<Button>();
        button.onClick = new Button.ButtonClickedEvent();
        button.onClick.AddListener(new UnityAction(data.Action));
        if (data.OnButtonCreatedAction != null)
        {
            data.OnButtonCreatedAction.Invoke(newButton);
        }
    }

    private static void CreateSplashText()
    {
    }
}