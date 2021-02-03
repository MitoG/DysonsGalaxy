using System;
using UModFramework.API;

namespace DysonsGalaxy
{
    public class DysonsGalaxyConfig
    {
        private static readonly string configVersion = "1.2";

        //Add your config vars here.
        public static bool Logging;
        public static int MinStarCount;
        public static int MaxStarCount;
        public static double MinStarDistance;

        internal static void Load()
        {
            DysonsGalaxy.Log("Loading settings.");
            try
            {
                using (var cfg = new UMFConfig())
                {
                    var cfgVer = cfg.Read("ConfigVersion", new UMFConfigString());
                    if (cfgVer != string.Empty && cfgVer != configVersion)
                    {
                        cfg.DeleteConfig(false);
                        DysonsGalaxy.Log("The config file was outdated and has been deleted. A new config will be generated.", false, true);
                    }

                    //cfg.Write("SupportsHotLoading", new UMFConfigBool(false)); //Uncomment if your mod can't be loaded once the game has started.
                    cfg.Write("ModDependencies", new UMFConfigStringArray(new []{""})); //A comma separated list of mod/library names that this mod requires to function. Format: SomeMod:1.50,SomeLibrary:0.60
                    cfg.Read("LoadPriority", new UMFConfigString("Normal"));
                    cfg.Write("MinVersion", new UMFConfigString("0.53.0"));
                    cfg.Write("MaxVersion", new UMFConfigString("0.54.99999.99999")); //This will prevent the mod from being loaded after the next major UMF release
                    cfg.Write("UpdateURL", new UMFConfigString("https://umodframework.com/updatemod?id=27"));
                    cfg.Write("ConfigVersion", new UMFConfigString(configVersion));

                    DysonsGalaxy.Log("Finished UMF Settings.");

                    //Add your settings here
                    Logging = cfg.Read("EnableLogging", new UMFConfigBool(false, false, false),
                        "This will enable logging to [GameDirectory]/uModFramework/Logs/DysonsGalaxy.log.",
                        "Warning: This can bloat the log file. Please only enable it if you encounter issues with this mod or suspect this mod of being the cause of issues with the game.");

                    MinStarCount = cfg.Read("MinStarCount", new UMFConfigInt(32, 5, 32, 32, true),
                        "Maximum stars that can be created when starting a new game. (32 = default)",
                        " I have to make the minimum value 5 since going lower than this the game will really not be happy. sorry, no 1 System challenge just yet.");

                    MaxStarCount = cfg.Read("MaxStarCount", new UMFConfigInt(64, 64, 255, 64, true),
                        "Maximum stars that can be created when starting a new game. (64 = default) ",
                        "The maximum is 255 since the game handles the planet count with a byte.",
                        "And I'm somewhat sure that even 255 will make the game burn through the CPU. So USE AT YOUR OWN RISK.");

                    MinStarDistance = cfg.Read("MinStarDistance", new UMFConfigDouble(2d, 0.0d, 1000d, 1, 2d, false),
                        "Minimum distance between stars when generating a new galaxy. (2 = default)",
                        "The minimum of 0 will possibly result in all stars becoming one and the Universe ending. I have not tested it.",
                        "The maximum will likely result in other stars being pretty much absent from the game. I have not tested this either.");

                    DysonsGalaxy.Log("Finished loading settings.");
                }
            }
            catch (Exception e)
            {
                DysonsGalaxy.Log("Error loading mod settings: " + e.Message + "(" + e.InnerException?.Message + ")", false, true);
            }
        }
    }
}