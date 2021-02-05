using HarmonyLib;

namespace DysonsGalaxy.Patches
{
    [HarmonyPatch(typeof(UniverseGen))]
    [HarmonyPatch("GenerateTempPoses")]
    static class PatchMinStarDistance
    {
        /// <summary>
        /// This interestingly was the easiest part.
        /// I grab the minDist value before the GenerateTempPoses method can access it and overwrite it
        /// with the desired value.
        /// </summary>
        public static void Prefix(ref double minDist)
        {
            DysonsGalaxy.Log($"Setting the minimum distance between stars to {DysonsGalaxyConfig.MinStarDistance}");
            minDist = DysonsGalaxyConfig.MinStarDistance;
        }
    }
}