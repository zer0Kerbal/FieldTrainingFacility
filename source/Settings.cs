/* Field Training Facility (FTF)
 * Kerbals gain experience (stars) using time and electric charge. For Kerbal Space Program.
 * Copyright (C) 2016 EFour
 * Copyright (C) 2019, 2022, 2023 zer0Kerbal (zer0Kerbal at hotmail dot com)
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using KSP.Localization;

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
        public override string Title { get { return "[WIP] Field Training Facility Settings"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "[WIP] Field Training"; } }
        public override string DisplaySection { get { return "[WIP] Field Training"; } }
        public override int SectionOrder { get { return 2; } }


        [GameParameters.CustomParameterUI("Enable the Field Training Facility?",
            toolTip = "Field Training Facilities are enabled if set to yes.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool enable = true;

        [GameParameters.CustomStringParameterUI("Payment Label",
            toolTip = "Science/Reputation/Funds",
            autoPersistance = true,
            lines = 2,
            title = "How would you like to pay for your kerbal training?",
            unlockedDuringMission = true)]
        public string UIstring = "";

        /// <summary>require science points
        /// to gain experience</summary>        
        [GameParameters.CustomParameterUI("Require Science Points to advance",
            toolTip = "If enabled, requires expending Science points to gain experience.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool requireSciencePoints = true;

        /// <summary>number of science points per
        /// experience point</summary> 
       [GameParameters.CustomFloatParameterUI("Science : Experience",
        toolTip = "Ratio of Science Points per Experience Point.",
            newGameOnly = false,
            unlockedDuringMission = true,
            minValue = 0.0f,
            maxValue = 100.0f,
            stepCount = 1)]
       public double costScience = 20.0f;

        /// <summary>require Reputation
        /// to gain experience</summary>
        [GameParameters.CustomParameterUI("Require Reputation to advance",
            toolTip = "If enabled, requires expending Reputation to gain experience.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool requireReputationPoints = false;

        /// <summary>number of Reputation per
        /// experience point</summary>
        [GameParameters.CustomFloatParameterUI("Reputation : Experience",
         toolTip = "Ratio of Reputation per Experience Point.",
             newGameOnly = false,
             unlockedDuringMission = true,
             minValue = 0.0f,
             maxValue = 50.0f)]
        public double costReputation = 0.1f;

        /// <summary>require Funds
        /// to gain experience</summary>       
        [GameParameters.CustomParameterUI("Require Funds to advance",
        toolTip = "If enabled, requires expending Funds to gain experience.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool requireFunds = false;

        /// <summary>amount of Funds per
        /// experience point</summary>  
        [GameParameters.CustomFloatParameterUI("Funds : Experience",
         toolTip = "Ratio of Funds per Experience Point.",
             newGameOnly = false,
             unlockedDuringMission = true,
             minValue = 0.0f,
             maxValue = 5000.0f,
             stepCount = 1)]
        public double costFunds = 1000f;

        [GameParameters.CustomParameterUI("KSPMail",
            toolTip = "Recieve a colorful Joyntmail announcing graduation to a new rank?.",
            newGameOnly = false,
            unlockedDuringMission = true)]
        public bool KSPMail = true;

        [GameParameters.CustomParameterUI("PAW Color",
            toolTip = "allow color coding in Field Training Lab PAW (part action window) / RMB (right menu button).",
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
                    enable = true;
                    requireSciencePoints = true;
                    requireReputationPoints = false;
                    requireFunds = false;
                    costScience = 15;
                    costFunds = 100;
                    costReputation = .1;
                   // autoSwitch = true;
                    break;

                case GameParameters.Preset.Normal:
                    enable = true;
                    requireSciencePoints = true;
                    requireFunds = true;
                    requireReputationPoints = false;
                    costScience = 20;
                    costFunds = 1000;
                    costReputation = 1;
                    // autoSwitch = true;
                    break;

                case GameParameters.Preset.Moderate:
                    enable = true;
                    requireSciencePoints = true;
                    requireFunds = true;
                    requireReputationPoints = true;
                    costScience = 25;
                    costFunds = 1000;
                    costReputation = 1.5;
                    //autoSwitch = true;
                    break;

                case GameParameters.Preset.Hard:
                    enable = false;
                    requireSciencePoints = true;
                    requireFunds = true;
                    requireReputationPoints = true;
                    costScience = 30;
                    costFunds = 1000;
                    costReputation = 2.0;
                    //autoSwitch = false;
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

   */