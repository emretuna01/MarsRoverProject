using System.Collections.Generic;

namespace MarsRover.Dtos
{
    public class InformationOfMarsRoversDto
    {
        public string CoorOfRightUpperPointOfPlateau { get; set; }
        public List<LocationOfMarsRoversDto> LocationOfMarsRoversDto { get; set; }
    }
}