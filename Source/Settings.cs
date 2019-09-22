using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

// This will add a tab to the Stock Settings in the Difficulty settings called "On Demand Fuel Cells"
// To use, reference the setting using the following:
//
//  HighLogic.CurrentGame.Parameters.CustomParams<ODFC_Options>().needsECtoStart
//
// As it is set up, the option is disabled, so in order to enable it, the player would have
// to deliberately go in and change it
//
namespace FieldTrainingFacility
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings

    public class ODFC_Options : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "Default Settings"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "ODFCr"; } }
        public override string DisplaySection { get { return "On Demand Fuel Cells"; } }
        public override int SectionOrder { get { return 1; } }


        [GameParameters.CustomParameterUI("Require EC to run",
            toolTip = "if set to yes, the fuel cells will 'stall' if the vessels total electric charge reaches zero and will not function until vessel electric charge is above zero.",
            newGameOnly = false,
            unlockedDuringMission = true
            )]
        public bool needsECtoStart = false;

        [GameParameters.CustomParameterUI("Auto Fuel Mode Switch",
            toolTip = "if current fuel mode becomes fuel deprived, will 'hunt' or 'search' for a fuel mode that has fuel.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool autoSwitch = true;

        [GameParameters.CustomParameterUI("PAW Color",
            toolTip = "allow color coding in ODC PAW (part action window) / part RMB (right menu button).",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool coloredPAW = true;

        // If you want to have some of the game settings default to enabled,  change 
        // the "if false" to "if true" and set the values as you like


#if true
        public override bool HasPresets { get { return true; } }
        public override void SetDifficultyPreset(GameParameters.Preset preset)
        {
            Debug.Log("Setting difficulty preset");
            switch (preset)
            {
                case GameParameters.Preset.Easy:
                    needsECtoStart = false;
                    autoSwitch = true;
                    break;

                case GameParameters.Preset.Normal:
                    needsECtoStart = false;
                    autoSwitch = true;
                    break;

                case GameParameters.Preset.Moderate:
                    needsECtoStart = true;
                    autoSwitch = true;
                    break;

                case GameParameters.Preset.Hard:
                    needsECtoStart = true;
                    autoSwitch = false;
                    break;
            }
        }

#else
        public override bool HasPresets { get { return false; } }
        public override void SetDifficultyPreset(GameParameters.Preset preset) { }
#endif

        public override bool Enabled(MemberInfo member, GameParameters parameters) { return true; }
        public override bool Interactible(MemberInfo member, GameParameters parameters) { return true; }
        public override IList ValidValues(MemberInfo member) { return null; }
    }
}

   