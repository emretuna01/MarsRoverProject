using MarsRover.Models;
using System.Collections.Generic;

namespace MarsRover.Dtos
{
    public class RoversLastOutput
    {
        public int RoverId{ get; set; }
        public string RoverFirstLocation { get; set; }
        public string RoverLastLocation { get; set; } 
        public string PlateauBoundaries { get; set; }
        public List<RoverLocation> roverLocationHistory { get; set; }
    }
}
