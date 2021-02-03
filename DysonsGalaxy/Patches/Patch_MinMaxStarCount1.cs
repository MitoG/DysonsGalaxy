using System;
using HarmonyLib;
using UnityEngine.UI;

namespace DysonsGalaxy.Patches
{
    [HarmonyPatch(typeof(UIGalaxySelect))]
    [HarmonyPatch("_OnOpen")]
    public static class Patch_MinMaxStarCount1
    {
        /// <summary>
        /// Here I just attach the Postfix to the _OnOpen method in UIGalaxySelect to overwrite the limits on the star count slider.
        /// Sadly the sliders are not named so we have to select the correct slider based on the maximum value it can hold.
        /// </summary>
        /// <param name="__instance"></param>
        public static void Postfix(ref UIGalaxySelect __instance)
        {
            foreach (var child in __instance.transform.GetComponentsInChildren<Slider>())
            {
                DysonsGalaxy.Log($"Setting min star count to {DysonsGalaxyConfig.MinStarCount}");
                DysonsGalaxy.Log($"Setting max star count to {DysonsGalaxyConfig.MaxStarCount}");

                if (Math.Abs(child.maxValue - 64f) < 1f)
                {
                    child.minValue = DysonsGalaxyConfig.MinStarCount;
                    child.maxValue = DysonsGalaxyConfig.MaxStarCount;
                }
                else if (Math.Abs(child.maxValue - DysonsGalaxyConfig.MaxStarCount) < 1f)
                {
                    child.minValue = DysonsGalaxyConfig.MinStarCount;
                    child.maxValue = DysonsGalaxyConfig.MaxStarCount;
                }
            }
        }
    }
}