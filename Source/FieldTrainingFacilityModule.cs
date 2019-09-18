namespace FieldTrainingFacility
{
    public class FieldTrainingFacilityModule : PartModule
    {
        string[] trainingArr =
        {
            "",
            "Training1",
            "Training2",
            "Training3",
            "Training4",
            "Training5"
        };

        string[] crewListArr =
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

        float[] levelUpExpTable = { 2, 6, 8, 16, 32, 0 };

        string[] levelNumber = { "null", "1st", "2nd", "3rd", "4th", "5th" };

        ProtoCrewMember[] crewArr = new ProtoCrewMember[8];
        int crewCnt = 0;

        [KSPField]
        public float TimeFactor = 426 * 6 * 60 * 60; // 1Year = 426day, 1day = 6hour, 1hour = 60minutes, 1min = 60sec

        [KSPField]
        public float ECFactor = 4;

        [KSPField]
        public float SpaceFactor = 4f;

        [KSPField]
        public float LandedFactor = 6f;

        [KSPField(isPersistant = true, guiActive = true, guiName = "Training Status")]
        public bool TrainingStatus = false;

        [KSPField(isPersistant = true)]
        public double LastTimeSigniture = -1;

        [KSPEvent(guiActive = true, guiName = "Start Training")]
        public void ToggleTraining()
        {
            if(TrainingStatus == false)
            {
                TrainingStatus = true;
                LastTimeSigniture = Planetarium.GetUniversalTime();
                Events["ToggleTraining"].guiName = "Stop Training";
            }
            else
            {
                stopTraining();
            }
        }

        [KSPField(guiActive = false)]
        public string BoardKerbal0;
        [KSPField(guiActive = false)]
        public string BoardKerbal1;
        [KSPField(guiActive = false)]
        public string BoardKerbal2;
        [KSPField(guiActive = false)]
        public string BoardKerbal3;
        [KSPField(guiActive = false)]
        public string BoardKerbal4;
        [KSPField(guiActive = false)]
        public string BoardKerbal5;
        [KSPField(guiActive = false)]
        public string BoardKerbal6;
        [KSPField(guiActive = false)]
        public string BoardKerbal7;

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);

            if(TrainingStatus == true) Events["ToggleTraining"].guiName = "Stop Training";
        }

        public void FixedUpdate()
        {
            if (TrainingStatus == true)
            {
                if (consumeEC(crewCnt, TimeWarp.fixedDeltaTime) == false)
                {
                    stopTraining();
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

                    int crewLevel = getCrewTrainedLevel(crew);
                    if (crewLevel < 5)
                    {
                        // calculating exp section
                        double exp = getKerbalTrainingExp(crew);
                        exp += calculateExp(vessel, nowTime - LastTimeSigniture);

                        // crew leveling section
                        if (getCrewTrainedLevel(crew) == 0 && exp > TimeFactor * levelUpExpTable[0] / 64)
                        {
                            setCrewTrainingLevel(crew, 1);
                            exp -= TimeFactor * levelUpExpTable[0] / 64;
                        }
                        if (getCrewTrainedLevel(crew) == 1 && exp > TimeFactor * levelUpExpTable[1] / 64)
                        {
                            setCrewTrainingLevel(crew, 2);
                            exp -= TimeFactor * levelUpExpTable[1] / 64;
                        }
                        if (getCrewTrainedLevel(crew) == 2 && exp > TimeFactor * levelUpExpTable[2] / 64)
                        {
                            setCrewTrainingLevel(crew, 3);
                            exp -= TimeFactor * levelUpExpTable[2] / 64;
                        }
                        if (getCrewTrainedLevel(crew) == 3 && exp > TimeFactor * levelUpExpTable[3] / 64)
                        {
                            setCrewTrainingLevel(crew, 4);
                            exp -= TimeFactor * levelUpExpTable[3] / 64;
                        }
                        if (getCrewTrainedLevel(crew) == 4 && exp > TimeFactor * levelUpExpTable[4] / 64)
                        {
                            setCrewTrainingLevel(crew, 5);
                            exp -= TimeFactor * levelUpExpTable[4] / 64;
                        }
                        
                        if (getCrewTrainedLevel(crew) < 5)
                        {
                            setKerbalTrainingExp(crew, exp);
                            expStrArr[index] = " (" + (exp * 100 / (TimeFactor * levelUpExpTable[crewLevel] / 64)).ToString("F2") + "%)";
                        }
                        else removeKerbalTrainingExp(crew);
                    }

                    Fields[crewListArr[index]].guiName = "Lv" + getCrewTrainedLevel(crew);
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

                    int crewLevel = getCrewTrainedLevel(crew);
                    if (crewLevel < 5)
                    {
                        // calculating exp section
                        double exp = getKerbalTrainingExp(crew);

                        if (getCrewTrainedLevel(crew) < 5)
                        {
                            setKerbalTrainingExp(crew, exp);
                            expStrArr[index] = " (" + (exp * 100 / (TimeFactor * levelUpExpTable[crewLevel] / 64)).ToString("F2") + "%)";
                        }
                        else removeKerbalTrainingExp(crew);
                    }

                    Fields[crewListArr[index]].guiName = "Lv" + getCrewTrainedLevel(crew);
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

        private void stopTraining()
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

        private int getCrewTrainedLevel(ProtoCrewMember crew)
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

        private void setCrewTrainingLevel(ProtoCrewMember crew, int level)
        {
            crew.flightLog.AddEntry(new FlightLog.Entry(crew.flightLog.Flight, trainingArr[level], "Kerbin"));
            ScreenMessages.PostScreenMessage(levelNumber[level] + " Training Complete : " + crew.name);
        }

        private double getKerbalTrainingExp(ProtoCrewMember crew)
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
                    if (entry.flight <= deadFlight) removeKerbalTrainingExp(crew);
                    else lastExpStr = entry.target;
                }
            }

            return double.Parse(lastExpStr);
        }

        private void removeKerbalTrainingExp(ProtoCrewMember crew)
        {
            foreach (FlightLog.Entry entry in crew.careerLog.Entries.ToArray())
                if (entry.type == "TrainingExp")
                    crew.careerLog.Entries.Remove(entry);
            foreach (FlightLog.Entry entry in crew.flightLog.Entries.ToArray())
                if (entry.type == "TrainingExp")
                    crew.flightLog.Entries.Remove(entry);
        }

        private void setKerbalTrainingExp(ProtoCrewMember crew, double exp)
        {
            removeKerbalTrainingExp(crew);

            crew.flightLog.Entries.Add(new FlightLog.Entry(crew.flightLog.Flight, "TrainingExp", exp.ToString()));
        }

        private double calculateExp(Vessel vessel, double elapsed)
        {
            if (this.vessel.mainBody.bodyName == "Kerbin" && this.vessel.LandedOrSplashed) return elapsed;
            else if (this.vessel.LandedOrSplashed) return elapsed * LandedFactor;
            else return elapsed * SpaceFactor;
        }

        public bool consumeEC(int numCrew, double elapsed)
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
    }
}
