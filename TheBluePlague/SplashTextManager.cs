using UnityEngine;

namespace TheBluePlague;

public static class SplashTextManager
{
    private static readonly string[] SplashTexts = {
        "The plague is blue!",
        "The blue must bluer!",
        "Erm, what the shrimp?",
        "Also try The Red Plague!",
        "I hear a call, and it is... THE CALL OF THE VOID!",
        "The Ancients Haven't Returned",
        "Subnautica 2 has been released!",
        "Red Plague Act 6 is here!",
        "What the sigma?",
        "Hello everybody my name is markups, welcome back to Seven Sharks at Subnautica's.",
        "I have a plague, I have a red- UH! RED PLAGUE!",
        "Don't bite the diver that feeds you if it's holding a knife",
        "Hallucinations not included",
        "Look behind you\nRun\nHide\n",
        "Now with 100% more bees",
        "Bau bau",
        "They are all dead\nTurn back\nLeave\nEscape\nIt's not too late\n\n\nIt’s too late",
        "As a child, I yearned for the seas",
        "Also try Calculator!",
        "As a dnandjfnbskaijwn help us knnkiandbboa",
        "Prototech is the only tech",
        "The cinny is rolling",
        "Dante",
        "Desmos?",
        "KooKoo doesn't get one because he smells",
        "Subnautica backwards is Aci... tuan... bus?",
        "It’s not purple it’s royal blue",
        "Jack? Like the green one?",
        "Octo link?",
        "Mumtoes",
        "Chicken of the sea",
        "Flint and steel",
        "Chicken jockey",
        "CHICKEN JOCKEY!",
        "The nether",
        "Use wheel of fortune to see act 2’s release date….",
        "Ant horse farm nia",
        "I Go Purple…?",
        "The middle ham",
        "Chomp",
        "The guy with a beard from Virginia",
        "Sponsored by… wait who are we sponsored by again?",
        "I am placing rocks and s*** cause I am a level designer",
        "So scary!",
        "Act two uh",
        "February 31st 2024",
        "I was threatened and forced to put these here, somebody help me please",
        "Boat goes binted",
        "Oh yeah the name of the turtle-like thing in TRP that leads you to places is the 'Hippopenomenon'",
        "A group of them is called a Hippopenomena."
    };

    public static string GetRandomSplashText()
    {
        return SplashTexts[Random.Range(0, SplashTexts.Length)];
    }
}