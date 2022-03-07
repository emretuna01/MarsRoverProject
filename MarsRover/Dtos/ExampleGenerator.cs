using System.Collections.Generic;

namespace MarsRover.Dtos
{
    public class ExampleGenerator
    {
        public InformationOfMarsRoversDto InformationOfMarsRoversDtoGenerator()
        {
            return new InformationOfMarsRoversDto()
            {
                CoorOfRightUpperPointOfPlateau = "5 5",
                LocationOfMarsRoversDto = new List<LocationOfMarsRoversDto>() {

                                            new LocationOfMarsRoversDto(){
                                                RoverId=1,
                                                InstructionsOfMoving="LMLMLMLMM",
                                                Location="1 2 N"
                                            },
                                            new LocationOfMarsRoversDto(){
                                                RoverId=2,
                                                InstructionsOfMoving="MMRMMRMRRM",
                                                Location="3 3 E"
                                            },
                                            new LocationOfMarsRoversDto(){
                                                RoverId=3,
                                                InstructionsOfMoving="LLLRLRLRLRLRLRLRLMRLRLMLLLLMRLRLMLRLRLRLMLRLRMLLLMRRRLMLMMRMMRLMMMMM",
                                                Location="2 2 N"
                                            }
                }
            };
        }
    }
}
