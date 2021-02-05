using UModFramework.API;

namespace DysonsGalaxy
{
    class DysonsGalaxy
    {
        internal static void Log(string text, bool clean = false, bool force = false)
        {
            if (!DysonsGalaxyConfig.Logging || !force) return;
            using (var log = new UMFLog()) log.Log(text, clean);
        }

        [UMFConfig]
        public static void LoadConfig()
        {
            DysonsGalaxyConfig.Load();
        }

		[UMFHarmony(4)] //Set this to the number of harmony patches in your mod.
        public static void Start()
		{
			Log("DysonsGalaxy v" + UMFMod.GetModVersion(), true);
		}
    }
}