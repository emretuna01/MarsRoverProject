using MarsRover.Const;
using MarsRover.Dtos;
using MarsRover.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Services
{
    public class RoverCommandService
    {
        public List<string> ArrangeList(string instructionString)
        {
            List<string> ArrangededList = null;
            if (!string.IsNullOrEmpty(instructionString))
            {
                ArrangededList = new List<string>();
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < instructionString.Length; i++)
                {
                    if (instructionString[i] != 'M')
                    {
                        stringBuilder.Append(instructionString[i]);
                    }
                    else if (instructionString[i] == 'M')
                    {
                        if (string.IsNullOrEmpty(stringBuilder.ToString()) != true)
                        {
                            ArrangededList.Add(stringBuilder.ToString());
                        }
                        stringBuilder = new StringBuilder();
                        stringBuilder.Append(instructionString[i]);
                        ArrangededList.Add(stringBuilder.ToString());
                        stringBuilder = new StringBuilder();

                    }

                }


            }
            return ArrangededList;


        }

        public List<(int, string)> CreateManeuverList(List<string> arrangededList)
        {
            //add control just contain L R M letter            
            List<(int, string)> RoverInstructionRawList = null;
            if (arrangededList != null)
            {
                RoverInstructionRawList = new List<(int, string)>();
                for (int i = 0; i < arrangededList.Count; i++)
                {
                    string elementOfResult = arrangededList[i];

                    int countL = 0;
                    int countR = 0;

                    if (elementOfResult != "M")
                    {
                        for (int j = 0; j < elementOfResult.Length; j++)
                        {
                            if (elementOfResult[j] == 'R')
                            {
                                countR++;
                            }
                            else if (elementOfResult[j] == 'L')
                            {
                                countL++;
                            }
                        }

                        if (countL > countR)
                        {
                            int difference = countL - countR;
                            RoverInstructionRawList.Add(new(difference, "L"));
                        }
                        else if (countR > countL)
                        {
                            int difference = countR - countL;
                            RoverInstructionRawList.Add(new(difference, "R"));
                        }
                        countL = 0;
                        countR = 0;
                    }
                    else
                    {
                        RoverInstructionRawList.Add(new(1, "M"));
                    }
                }

            }
            return RoverInstructionRawList;

        }

        public List<(int, string)> SimplifyManeuverList(List<(int, string)> arrangedResult)
        {
            List<(int, string)> simplifyManeuverList = new List<(int, string)>();
            if (arrangedResult != null)
            {
                arrangedResult.ForEach(z =>
                {
                    if (z.Item2 != "M")
                    {
                        simplifyManeuverList.Add(new(z.Item1 % 4, z.Item2.ToString()));
                    }
                    else if (z.Item2 == "M")
                    {
                        simplifyManeuverList.Add(new(z.Item1, z.Item2.ToString()));
                    }

                });
            }

            return simplifyManeuverList;

        }

        public List<(int, string)> CleanZeroManeuverInList(List<(int, string)> simplifyManeuverList)
        {
            List<(int, string)> cleanZeroManeuverInList = new List<(int, string)>();

            if (simplifyManeuverList != null)
            {
                simplifyManeuverList.ForEach(z =>
                {
                    if (z.Item1 > 0)
                    {
                        cleanZeroManeuverInList.Add(new(z.Item1, z.Item2));
                    }
                });

            }

            return cleanZeroManeuverInList;
        }

        public List<(int, string)> ShrinkTheList(List<(int, string)> cleanZeroManeuverInList)
        {
            List<(int, string)> ShrinkList = new List<(int, string)>();
            int stackManeuverCount = 0;
            int lastMember = cleanZeroManeuverInList.Count - 1;

            for (int i = 0; i < cleanZeroManeuverInList.Count; i++)
            {
                if (i != lastMember)
                {

                    if (cleanZeroManeuverInList[i].Item2 == "M" && cleanZeroManeuverInList[i + 1].Item2 == "M")
                    {
                        stackManeuverCount += cleanZeroManeuverInList[i].Item1;
                    }
                    else if (cleanZeroManeuverInList[i].Item2 == "M" && cleanZeroManeuverInList[i + 1].Item2 != "M")
                    {
                        stackManeuverCount += cleanZeroManeuverInList[i].Item1;
                        ShrinkList.Add(new(stackManeuverCount, "M"));
                        stackManeuverCount = 0;

                    }
                    else if (cleanZeroManeuverInList[i].Item2 != "M")
                    {
                        ShrinkList.Add(new(cleanZeroManeuverInList[i].Item1, cleanZeroManeuverInList[i].Item2));
                    }

                }
                else if (i == lastMember)
                {
                    if (cleanZeroManeuverInList[i].Item2 != "M")
                    {
                        ShrinkList.Add(new(cleanZeroManeuverInList[i].Item1, cleanZeroManeuverInList[i].Item2));
                    }
                    else if (cleanZeroManeuverInList[i].Item2 == "M")
                    {
                        if (stackManeuverCount > 0)
                        {
                            stackManeuverCount += cleanZeroManeuverInList[i].Item1;
                            ShrinkList.Add(new(stackManeuverCount, "M"));
                        }
                        else
                        {
                            ShrinkList.Add(new(cleanZeroManeuverInList[i].Item1, cleanZeroManeuverInList[i].Item2));
                            stackManeuverCount = 0;
                        }
                    }
                }

            }

            return ShrinkList;
        }

        public PlateauBoundaries PlateauBoundariesMapperFromString(string plateauBoundariesString)
        {
            var plateauBoundaries = plateauBoundariesString.Split(' ');
            return new PlateauBoundaries()
            {
                X = Int32.Parse(plateauBoundaries[0]),
                Y = Int32.Parse(plateauBoundaries[1])
            };
        }

        public RoverLocation RoverLocationMapperFromString(string roverLocationString)
        {
            var roverLocationArray = roverLocationString.Split(' ');
            return new RoverLocation()
            {
                X = Int32.Parse(roverLocationArray[0]),
                Y = Int32.Parse(roverLocationArray[1]),
                Orientation = roverLocationArray[2],
            };
        }

        public RoversLastOutput RoverMover(List<(int, string)> maneuverList, RoverLocation roverLocation)
        {
            //(int, int) plateauBoundary = (plateauBoundaries.X, plateauBoundaries.Y);

            List<RoverLocation> RoverLocarionHistory = new List<RoverLocation>();
            RoverLocation firstRoverLocation = new RoverLocation()
            {
                Orientation = roverLocation.Orientation,
                X = roverLocation.X,
                Y = roverLocation.Y
            };

            RoverLocarionHistory.Add(firstRoverLocation);

            RoverLocation roverLocationContainer = roverLocation;

            maneuverList.ForEach(z =>
            {
                if (z.Item2 == "L")
                {
                    for (int i = 0; i < z.Item1; i++)
                    {
                        RoverOrientationLeftTurner(roverLocation);
                        RoverLocarionHistory.Add(new RoverLocation()
                        {
                            Orientation = roverLocation.Orientation,
                            X = roverLocation.X,
                            Y = roverLocation.Y
                        });
                    }

                }
                else if (z.Item2 == "R")
                {
                    for (int i = 0; i < z.Item1; i++)
                    {
                        RoverOrientationRightTurner(roverLocation);
                        RoverLocarionHistory.Add(new RoverLocation()
                        {
                            Orientation = roverLocation.Orientation,
                            X = roverLocation.X,
                            Y = roverLocation.Y
                        });
                    }
                }
                else if (z.Item2 == "M")
                {
                    for (int i = 0; i < z.Item1; i++)
                    {
                        RoverMover(roverLocation);
                        RoverLocarionHistory.Add(new RoverLocation()
                        {
                            Orientation = roverLocation.Orientation,
                            X = roverLocation.X,
                            Y = roverLocation.Y
                        });
                    }
                }



            });

            string lastRoverLoc = $"{roverLocation.X} {roverLocation.Y} {roverLocation.Orientation}";

            return new RoversLastOutput
            {
                RoverLastLocation = lastRoverLoc,
                roverLocationHistory = RoverLocarionHistory
            };
        }

        public void RoverOrientationLeftTurner(RoverLocation roverLocation)
        {

            switch (roverLocation.Orientation)
            {
                case "N":
                    roverLocation.Orientation = Direction.W.ToString();
                    break;
                case "S":
                    roverLocation.Orientation = Direction.E.ToString();
                    break;
                case "E":
                    roverLocation.Orientation = Direction.N.ToString();
                    break;
                case "W":
                    roverLocation.Orientation = Direction.S.ToString();
                    break;
                default:
                    break;

            }

        }

        public void RoverOrientationRightTurner(RoverLocation roverLocation)
        {

            switch (roverLocation.Orientation)
            {
                case "N":
                    roverLocation.Orientation = Direction.E.ToString();
                    break;
                case "S":
                    roverLocation.Orientation = Direction.W.ToString();
                    break;
                case "E":
                    roverLocation.Orientation = Direction.S.ToString();
                    break;
                case "W":
                    roverLocation.Orientation = Direction.N.ToString();
                    break;
                default:
                    break;
            }

        }

        public void RoverMover(RoverLocation roverLocation)
        {
            switch (roverLocation.Orientation)
            {
                case "N":
                    roverLocation.Y++;
                    break;
                case "S":
                    roverLocation.Y--;
                    break;
                case "E":
                    roverLocation.X++;
                    break;
                case "W":
                    roverLocation.X--;
                    break;
                default:
                    break;
            }

        }
    }
}
