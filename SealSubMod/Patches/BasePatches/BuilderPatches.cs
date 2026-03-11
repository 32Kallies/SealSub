using HarmonyLib;
using SealSubMod.MonoBehaviours;
using System;

namespace SealSubMod.Patches.BasePatches;

[HarmonyPatch(typeof(Builder))]
internal class BuilderPatches//Allows you to build base pieces even though the game thinks they intersect with an ACU built on another floor
{
    [HarmonyPatch(nameof(Builder.GetObstacles))]
    public static void Prefix(ref Func<Collider, bool> filter)
    {
        if (Player.main.GetCurrentSub() is SealSubRoot)
        {
            //the interfering collider is part of another base piece, so ignore it
            filter += col => col.GetComponentInParent<BasePieceLocationMarker>();
        }
    }
}
