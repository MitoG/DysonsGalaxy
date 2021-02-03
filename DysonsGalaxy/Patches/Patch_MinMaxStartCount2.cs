using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

namespace DysonsGalaxy.Patches
{
    [HarmonyPatch(typeof(UIGalaxySelect))]
    [HarmonyPatch("OnStarCountSliderValueChange")]
    public class Patch_MinMaxStartCount2
    {
        /// <summary>
        /// If I could I would paste the corresponding IL code here, but that would be illegal.
        /// Look it up @ UIGalaxySelect -> OnStarCountSliderValueChange
        /// IL0013 to IL002D is the if/else block.
        ///
        /// According to the HarmonyLib Documentation it should be generally fine to nuke the if/else block
        /// but since this crashes DSP I just replace the whole block with NOP which ain't pretty but it works
        /// </summary>
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            Harmony.DEBUG = true;
            var beginningFound = false;
            var endingFound = false;
            var beginning = -1;
            var ending = -1;

            var codes = new List<CodeInstruction>(instructions);
            for (var i = 0; i < codes.Count; i++)
            {
                DysonsGalaxy.Log($"{codes[i]}");

                if (codes[i].opcode.Equals(OpCodes.Stloc_0) && codes[i + 1].opcode.Equals(OpCodes.Ldloc_0) && !beginningFound)
                {
                    beginning = i + 1;
                    beginningFound = true;
                    DysonsGalaxy.Log($"BEGINNING AT => {i}");
                }
                else if (
                    (codes[i].opcode.Equals(OpCodes.Ldc_I4_S)
                     && codes[i].operand.Equals((sbyte) 80))
                    && codes[i + 1].opcode.Equals(OpCodes.Stloc_0) 
                    && beginningFound)
                {
                    ending = i + 1;
                    endingFound = true;
                    DysonsGalaxy.Log($"ENDING AT => {i}");
                }

                if (beginningFound && endingFound)
                {
                    break;
                }
            }

            
            if (beginning > -1 && ending > -1)
            {
                DysonsGalaxy.Log($"NOPed: {beginning} to {ending} from {codes.Count}");
                for (var i = beginning; i <= ending; i++)
                {
                    codes[i] = new CodeInstruction(OpCodes.Nop);
                    DysonsGalaxy.Log($"{codes[i]}");
                }
                DysonsGalaxy.Log($"NOPed!");
            }
            else
            {
                DysonsGalaxy.Log($"Beginning => {beginning}");
                DysonsGalaxy.Log($"Ending => {ending}");
            }
            Harmony.DEBUG = false;
            return codes.AsEnumerable();
        }
    }
}