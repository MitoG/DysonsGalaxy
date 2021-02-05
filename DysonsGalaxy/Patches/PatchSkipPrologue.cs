using HarmonyLib;

namespace DysonsGalaxy.Patches
{
    [HarmonyPatch(typeof(GameLoader))]
    [HarmonyPatch("FixedUpdate")]
    public class PatchSkipPrologue
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            DSPGame.SkipPrologue = DysonsGalaxyConfig.SkipPrologue;
        }
    }
}