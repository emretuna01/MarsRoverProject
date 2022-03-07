using MarsRover.Models;
using System.Collections.Generic;

namespace MarsRover.Dtos
{
    public class RoverInterInfoHolder
    {
        public int RoverId { get; set; }
        public PlateauBoundaries PlateauBoundaries{ get; set; }    
        public RoverLocation Location { get; set; }
        public List<string> RoverMoveInstructionList { get; set; }
    }
}
