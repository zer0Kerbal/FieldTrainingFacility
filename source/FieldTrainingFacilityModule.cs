/* Field Training Facility (FTF)
 * Kerbals gain experience (stars) using time and electric charge. For Kerbal Space Program.
 * Copyright (C) 2016 EFour
 * Copyright (C) 2019, 2022 zer0Kerbal (zer0Kerbal at hotmail dot com)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP;
using KSPAssets;
using KSP.Localization;
using System.Web.UI.WebControls.WebParts;

namespace FieldTrainingFacility
{
    public class FieldTrainingFacility : PartModule
    {
        readonly string[] trainingArr =
        {
            "",
            "Training1",
            "Training2",
            "Training3",
            "Training4",
            "Training5"
        };

        readonly string[] crewListArr =
        {
            "BoardKerbal0",
            "BoardKerbal1",
            "BoardKerbal2",
            "BoardKerbal3",
            "BoardKerbal4",
            "BoardKerbal5",
            "BoardKerbal6",
            "BoardKerbal7"
        };

        readonly float[] levelUpExpTable = { 2, 6, 8, 16, 32, 0 };

        readonly string[] levelNumber = { "null", "1st", "2nd", "3rd", "4th", "5th" };

        readonly ProtoCrewMember[] crewArr = new ProtoCrewMember[8];
        int crewCnt = 0,
            maxCrew = 0;

        [KSPField]
        public float TimeFactor = 426 * 6 * 60 * 60; // 1Year = 426day, 1day = 6hour, 1hour = 60minutes, 1min = 60sec

        [KSPField]
        public float ECFactor = 4;

        [KSPField]
        public float SpaceFactor = 4f;

        [KSPField]
        public float LandedFactor = 6f;

        [KSPField(isPersistant = true, guiActive = true, guiName = "Training Status", groupName = "Training", groupDisplayName = "Training Facility " + Version.SText, groupStartCollapsed = true)]
        public bool TrainingStatus = false;

        [KSPField(isPersistant = true)]
        public double LastTimeSigniture = -1;

        [KSPEvent(guiActive = true, guiName = "Start Training", groupName = "Training")]
        public void ToggleTraining()
        {
            if(TrainingStatus == false)
            {
                TrainingStatus = true;
                LastTimeSigniture = Planetarium.GetUniversalTime();
                Events["ToggleTraining"].guiName = "Stop Training";
            }
            else StopTraining();
        }

        [KSPField(guiActive = false, groupName = "Training")]
        public string BoardKerbal0;
        [KSPField(guiActive = false, groupName = "Training")]
        public string BoardKerbal1;
        [KSPField(guiActive = false, groupName = "Training")]
        public string BoardKerbal2;
        [KSPField(guiActive = false, groupName = "Training")]
        public string BoardKerbal3;
        [KSPField(guiActive = false, groupName = "Training")]
        public string BoardKerbal4;
        [KSPField(guiActive = false, groupName = "Training")]
        public string BoardKerbal5;
        [KSPField(guiActive = false, groupName = "Training")]
        public string BoardKerbal6;
        [KSPField(guiActive = false, groupName = "Training")]
        public string BoardKerbal7;

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);

            if(TrainingStatus == true) Events["ToggleTraining"].guiName = "Stop Training";
            maxCrew = part.CrewCapacity;
        }

        public void FixedUpdate()
        {
            if (TrainingStatus == true)
            {
                if (ConsumeEC(crewCnt, TimeWarp.fixedDeltaTime) == false)
                {
                    StopTraining();
                    ScreenMessages.PostScreenMessage("Electric Charge Depleted. Stopping Training.");
                }
            }

            base.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            if (TrainingStatus == true)
            {
                double nowTime = Planetarium.GetUniversalTime();
                string[] expStrArr = new string[8];
                int index = 0;

                foreach (ProtoCrewMember crew in part.protoModuleCrew)
                {
                    if (index >= crewArr.Length) break;

                    crewArr[index] = crew;
                    expStrArr[index] = "";

                    int crewLevel = GetCrewTrainedLevel(crew);
                    if (crewLevel < 5)
                    {
                        // calculating exp section
                        double exp = GetKerbalTrainingExp(crew);
                        exp += CalculateExp(vessel, nowTime - LastTimeSigniture);

                        // crew leveling section
                        if (GetCrewTrainedLevel(crew) == 0 && exp > TimeFactor * levelUpExpTable[0] / 64)
                        {
                            SetCrewTrainingLevel(crew, 1);
                            exp -= TimeFactor * levelUpExpTable[0] / 64;
                        }
                        if (GetCrewTrainedLevel(crew) == 1 && exp > TimeFactor * levelUpExpTable[1] / 64)
                        {
                            SetCrewTrainingLevel(crew, 2);
                            exp -= TimeFactor * levelUpExpTable[1] / 64;
                        }
                        if (GetCrewTrainedLevel(crew) == 2 && exp > TimeFactor * levelUpExpTable[2] / 64)
                        {
                            SetCrewTrainingLevel(crew, 3);
                            exp -= TimeFactor * levelUpExpTable[2] / 64;
                        }
                        if (GetCrewTrainedLevel(crew) == 3 && exp > TimeFactor * levelUpExpTable[3] / 64)
                        {
                            SetCrewTrainingLevel(crew, 4);
                            exp -= TimeFactor * levelUpExpTable[3] / 64;
                        }
                        if (GetCrewTrainedLevel(crew) == 4 && exp > TimeFactor * levelUpExpTable[4] / 64)
                        {
                            SetCrewTrainingLevel(crew, 5);
                            exp -= TimeFactor * levelUpExpTable[4] / 64;
                        }
                        
                        if (GetCrewTrainedLevel(crew) < 5)
                        {
                            SetKerbalTrainingExp(crew, exp);
                            expStrArr[index] = " (" + (exp * 100 / (TimeFactor * levelUpExpTable[crewLevel] / 64)).ToString("F2") + "%)";
                        }
                        else RemoveKerbalTrainingExp(crew);
                    }

                    Fields[crewListArr[index]].guiName = "Lv" + GetCrewTrainedLevel(crew);
                    Fields[crewListArr[index]].guiActive = true;

                    index++;
                }

                // save crew number
                crewCnt = index;

                // reset unused spaces
                for (; index < crewArr.Length; index++)
                {
                    Fields[crewListArr[index]].guiActive = false;
                    crewArr[index] = null;
                }

                BoardKerbal0 = (crewArr[0] != null ? crewArr[0].name + expStrArr[0] : "");
                BoardKerbal1 = (crewArr[1] != null ? crewArr[1].name + expStrArr[1] : "");
                BoardKerbal2 = (crewArr[2] != null ? crewArr[2].name + expStrArr[2] : "");
                BoardKerbal3 = (crewArr[3] != null ? crewArr[3].name + expStrArr[3] : "");
                BoardKerbal4 = (crewArr[4] != null ? crewArr[4].name + expStrArr[4] : "");
                BoardKerbal5 = (crewArr[5] != null ? crewArr[5].name + expStrArr[5] : "");
                BoardKerbal6 = (crewArr[6] != null ? crewArr[6].name + expStrArr[6] : "");
                BoardKerbal7 = (crewArr[7] != null ? crewArr[7].name + expStrArr[7] : "");

                LastTimeSigniture = Planetarium.GetUniversalTime();
            }
            else
            {
                string[] expStrArr = new string[8];
                int index = 0;

                foreach (ProtoCrewMember crew in part.protoModuleCrew)
                {
                    if (index >= crewArr.Length) break;

                    crewArr[index] = crew;
                    expStrArr[index] = "";

                    int crewLevel = GetCrewTrainedLevel(crew);
                    if (crewLevel < 5)
                    {
                        // calculating exp section
                        double exp = GetKerbalTrainingExp(crew);

                        if (GetCrewTrainedLevel(crew) < 5)
                        {
                            SetKerbalTrainingExp(crew, exp);
                            expStrArr[index] = " (" + (exp * 100 / (TimeFactor * levelUpExpTable[crewLevel] / 64)).ToString("F2") + "%)";
                        }
                        else RemoveKerbalTrainingExp(crew);
                    }

                    Fields[crewListArr[index]].guiName = "Lv" + GetCrewTrainedLevel(crew);
                    Fields[crewListArr[index]].guiActive = true;

                    index++;
                }

                // reset unused spaces
                for (; index < crewArr.Length; index++)
                {
                    Fields[crewListArr[index]].guiActive = false;
                    crewArr[index] = null;
                }

                BoardKerbal0 = (crewArr[0] != null ? crewArr[0].name + expStrArr[0] : "");
                BoardKerbal1 = (crewArr[1] != null ? crewArr[1].name + expStrArr[1] : "");
                BoardKerbal2 = (crewArr[2] != null ? crewArr[2].name + expStrArr[2] : "");
                BoardKerbal3 = (crewArr[3] != null ? crewArr[3].name + expStrArr[3] : "");
                BoardKerbal4 = (crewArr[4] != null ? crewArr[4].name + expStrArr[4] : "");
                BoardKerbal5 = (crewArr[5] != null ? crewArr[5].name + expStrArr[5] : "");
                BoardKerbal6 = (crewArr[6] != null ? crewArr[6].name + expStrArr[6] : "");
                BoardKerbal7 = (crewArr[7] != null ? crewArr[7].name + expStrArr[7] : "");
            }

            base.OnUpdate();
        }

        private void StopTraining()
        {
            TrainingStatus = false;

            BoardKerbal0 = "";
            BoardKerbal1 = "";
            BoardKerbal2 = "";
            BoardKerbal3 = "";
            BoardKerbal4 = "";
            BoardKerbal5 = "";
            BoardKerbal6 = "";
            BoardKerbal7 = "";

            for (int index = 0; index < crewListArr.Length; index++) Fields[crewListArr[index]].guiActive = false;

            Events["ToggleTraining"].guiName = "Start Training";
        }


        private int GetCrewTrainedLevel(ProtoCrewMember crew)
        {
            int lastLog = 0;
            FlightLog totalLog = crew.careerLog.CreateCopy();
            totalLog.MergeWith(crew.flightLog.CreateCopy());

            int deadFlight = -1;
            foreach (FlightLog.Entry entry in totalLog.Entries)
            {
                if (entry.flight <= deadFlight) continue;
                if (entry.type == "Die") deadFlight = entry.flight;
            }
            foreach (FlightLog.Entry entry in totalLog.Entries)
            {
                if (entry.flight <= deadFlight) continue;
                if (lastLog < 1 && entry.type == "Training1") lastLog = 1;
                if (lastLog < 2 && entry.type == "Training2") lastLog = 2;
                if (lastLog < 3 && entry.type == "Training3") lastLog = 3;
                if (lastLog < 4 && entry.type == "Training4") lastLog = 4;
                if (lastLog < 5 && entry.type == "Training5") lastLog = 5;
            }

            return lastLog;
        }

        private void SetCrewTrainingLevel(ProtoCrewMember crew, int level)
        {
            crew.flightLog.AddEntry(new FlightLog.Entry(crew.flightLog.Flight, trainingArr[level], "Kerbin"));
            ScreenMessages.PostScreenMessage(levelNumber[level] + " Training Complete : " + crew.name);
        }

        private double GetKerbalTrainingExp(ProtoCrewMember crew)
        {
            string lastExpStr = "0";

            FlightLog totalLog = crew.careerLog.CreateCopy();
            totalLog.MergeWith(crew.flightLog.CreateCopy());

            int deadFlight = -1;
            foreach (FlightLog.Entry entry in totalLog.Entries)
            {
                if (entry.flight <= deadFlight) continue;
                if (entry.type == "Die") deadFlight = entry.flight;
            }

            foreach (FlightLog.Entry entry in totalLog.Entries)
            {
                if (entry.type == "TrainingExp")
                {
                    if (entry.flight <= deadFlight) RemoveKerbalTrainingExp(crew);
                    else lastExpStr = entry.target;
                }
            }

            return double.Parse(lastExpStr);
        }

        private void RemoveKerbalTrainingExp(ProtoCrewMember crew)
        {
            foreach (FlightLog.Entry entry in crew.careerLog.Entries.ToArray())
                if (entry.type == "TrainingExp")
                    crew.careerLog.Entries.Remove(entry);
            foreach (FlightLog.Entry entry in crew.flightLog.Entries.ToArray())
                if (entry.type == "TrainingExp")
                    crew.flightLog.Entries.Remove(entry);
        }

        private void SetKerbalTrainingExp(ProtoCrewMember crew, double exp)
        {
            RemoveKerbalTrainingExp(crew);

            crew.flightLog.Entries.Add(new FlightLog.Entry(crew.flightLog.Flight, "TrainingExp", exp.ToString()));
        }

        private double CalculateExp(Vessel vessel, double elapsed)
        {
            if (this.vessel.mainBody.bodyName == "Kerbin" && this.vessel.LandedOrSplashed) return elapsed;
            else if (this.vessel.LandedOrSplashed) return elapsed * LandedFactor;
            else return elapsed * SpaceFactor;
        }

        public bool ConsumeEC(int numCrew, double elapsed)
        {
            if (CheatOptions.InfiniteElectricity == true) return true;

            double ec = 0;
            int tanks = 0;
            foreach (Part part in vessel.parts)
            {
                foreach (PartResource res in part.Resources)
                {
                    if (res.resourceName == "ElectricCharge" && res.amount > 0)
                    {
                        ec += res.amount;
                        tanks++;
                    }
                }
            }

            if (tanks == 0 || ec <= 0) return false;

            foreach (Part part in vessel.parts)
            {
                foreach (PartResource res in part.Resources)
                {
                    if (res.resourceName == "ElectricCharge" && res.amount > 0) res.amount -= numCrew * ECFactor * elapsed / tanks;
                    if (res.amount < 0) res.amount = 0;
                }
            }

            return true;
        }

        /// <summary>Converts consumption rate into /s /m /hour and returns a formate string.</summary>
        /// <param name="Rate">The rate.</param>
        /// <returns>RateString="Rate"</returns>
        private static string RateString(double rate)
        {
            //  double rate = double.Parse(value.value);
            string sfx = "/s";
            if (rate <= 0.004444444f)
            {
                rate *= 3600;
                sfx = "/h";
            }
            else if (rate < 0.2666667f)
            {
                rate *= 60;
                sfx = "/m";
            }
            // limit decimal places to 10 and add sfx
            //return String.Format(FuelRateFormat, Rate, sfx);
            return rate.ToString("###.#####") + " EC" + sfx;
        }
        /// <summary>Module information shown in editors</summary>
        private string info = string.Empty;

        public override string GetInfo()
        {
            //? this is what is show in the editor
            //? As annoying as it is, pre-parsing the config MUST be done here, because this is called during part loading.
            //? The config is only fully parsed after everything is fully loaded (which is why it's in OnStart())
            if (info == string.Empty)
            {   
                info += Localizer.Format("#FieldTrainingFacility_manu"); // #FieldTrainingFacility_manu = Kerbalnaut Training Industries, Inc.
                info += "\n v" + Version.SText; // FTF Version Number text
                info += "\n<color=#b4d455FF>" + Localizer.Format("#FieldTrainingFacility_desc"); // #FieldTrainingFacility_desc = Train Kerbals using time and Electric Charge
                info += "\n\n<color=orange>Requires:</color> \n - <color=white><b>" + Localizer.Format("#autoLOC_252004"); // #autoLOC_252004 = ElectricCharge
                info += "</b>: \n <color=#99FF00FF>  - Per Crew: </b></color><color=white>" + RateString(ECFactor) + " </color>";
                info += "</b>: \n <color=#99FF00FF>  - Max Crew: </b></color><color=white>" + RateString(maxCrew * ECFactor) + "</color>";
            }
            // #autoLOC_252004 = ElectricCharge
            // #FieldTrainingFacility_titl = FieldTrainingFacility
            // #FieldTrainingFacility_manu = Kerbalnaut Training Industries, Inc.
            // #FieldTrainingFacility_desc = Train Kerbals using time and Electric Charge
            return info;
        }
    }
}
