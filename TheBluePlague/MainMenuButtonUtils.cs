using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheBluePlague;

public static class MainMenuButtonUtils
{
    private static readonly List<ButtonData> Data = new();
    
    public static void RegisterButton(string buttonName, Action action)
    {
        Data.Add(new ButtonData(buttonName, action));
    }
    
    public static void RegisterButton(string buttonName, Action action, Action<GameObject> onButtonCreatedAction)
    {
        Data.Add(new ButtonData(buttonName, action, onButtonCreatedAction));
    }

    public static IEnumerable<ButtonData> GetButtonData()
    {
        return Data;
    }

    public struct ButtonData
    {
        public string Name { get; }
        public Action Action { get; }
        public Action<GameObject> OnButtonCreatedAction { get; }

        public ButtonData(string name, Action action)
        {
            Name = name;
            Action = action;
        }

        public ButtonData(string name, Action action, Action<GameObject> onButtonCreatedAction)
        {
            Name = name;
            Action = action;
            OnButtonCreatedAction = onButtonCreatedAction;
        }
    }
}