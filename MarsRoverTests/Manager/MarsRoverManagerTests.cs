using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Dtos;
using MarsRover.Models;
using MarsRover.Const;

namespace MarsRover.Manager.Tests
{
    [TestClass()]
    public class MarsRoverManagerTests
    {
        [TestMethod()]
        public void Trigger()
        {
            InformationOfMarsRoversDto informationOfMarsRoversDto = new InformationOfMarsRoversDto();
            ExampleGenerator exampleGenerator = new ExampleGenerator();
            informationOfMarsRoversDto = exampleGenerator.InformationOfMarsRoversDtoGenerator();
            var testJson = System.Text.Json.JsonSerializer.Serialize(informationOfMarsRoversDto);
        }
    }
}

