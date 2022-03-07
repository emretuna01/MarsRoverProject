using MarsRover.Dtos;
using MarsRover.Manager;
using MarsRover.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;

namespace MarsRover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarsRoverController : ControllerBase
    {
        public readonly InformationOfMarsRoversDto informationOfMarsRoversDto;

        private readonly MarsRoverManager _marsRoverManager;
        private readonly RoverControlServices _roverControlServices;

        public MarsRoverController()
        {
            _marsRoverManager = new MarsRoverManager();
            _roverControlServices = new RoverControlServices();
        }

        [HttpPost("RoversDeployer")]
        public Response<List<RoversLastOutput>> RoversDeployer(InformationOfMarsRoversDto informationOfMarsRoversDto)
        {
            List<RoverInterInfoHolder> roverInterInfoHolders = _marsRoverManager.RoverInterInfoHoldersCreator(informationOfMarsRoversDto);
            List<RoversLastOutput> roversLastOutputList = _marsRoverManager.RoverResultServiceReceiver(roverInterInfoHolders);
            return new Response<List<RoversLastOutput>>() { ResponseItem = roversLastOutputList, Message = "OK", Status = "OK" };
        }
      
    }
}
