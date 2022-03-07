using MarsRover.Const;
using MarsRover.Dtos;
using MarsRover.Models;
using MarsRover.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Manager
{
    public class MarsRoverManager
    {
        private RoverCommandService _roverCommandService;

        public MarsRoverManager()
        {
            _roverCommandService = new RoverCommandService();
        }

        public List<RoverInterInfoHolder> RoverInterInfoHoldersCreator(InformationOfMarsRoversDto informationOfMarsRoversDto)
        {
            List<RoverInterInfoHolder> roverInterInfoHolders = new List<RoverInterInfoHolder>();

            informationOfMarsRoversDto.LocationOfMarsRoversDto.ForEach(x =>
            {
                roverInterInfoHolders.Add(
                            new RoverInterInfoHolder()
                            {
                                Location = _roverCommandService.RoverLocationMapperFromString(x.Location),
                                PlateauBoundaries = _roverCommandService.PlateauBoundariesMapperFromString(informationOfMarsRoversDto.CoorOfRightUpperPointOfPlateau),
                                RoverId = x.RoverId,
                                RoverMoveInstructionList = _roverCommandService.ArrangeList(x.InstructionsOfMoving)
                            }
                    );
            });

            return roverInterInfoHolders;
        }

        public List<RoversLastOutput> RoverResultServiceReceiver(List<RoverInterInfoHolder> roverInterInfoHolders)
        {
            List<RoversLastOutput> roversLastOutputs = new List<RoversLastOutput>();

            foreach (RoverInterInfoHolder Rover in roverInterInfoHolders)
            {
                RoversLastOutput roversLastOutputInf = new RoversLastOutput();

                roversLastOutputInf.RoverId = Rover.RoverId;
                roversLastOutputInf.PlateauBoundaries = $"{Rover.PlateauBoundaries.X} {Rover.PlateauBoundaries.Y}";
                roversLastOutputInf.RoverFirstLocation = $"{Rover.Location.X} {Rover.Location.Y} {Rover.Location.Orientation}";

                List<(int, string)> maneuverList = _roverCommandService.CreateManeuverList(Rover.RoverMoveInstructionList);
                List<(int, string)> simpleManeuverList = _roverCommandService.SimplifyManeuverList(maneuverList);
                List<(int, string)> cleanManeuverList = _roverCommandService.CleanZeroManeuverInList(simpleManeuverList);
                List<(int, string)> resultManeuverList = _roverCommandService.ShrinkTheList(cleanManeuverList);

                var movedRoverIbf = _roverCommandService.RoverMover(resultManeuverList, Rover.Location);
                roversLastOutputInf.RoverLastLocation = movedRoverIbf.RoverLastLocation;
                roversLastOutputInf.roverLocationHistory = movedRoverIbf.roverLocationHistory;

                roversLastOutputs.Add(roversLastOutputInf);
            }

            return roversLastOutputs;


        }

    }
}
