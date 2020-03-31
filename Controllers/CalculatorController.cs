using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeanMedianMode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Calculation Get(string nums)
        {
            //Parse input into List<int>
            List<int> list = nums.Split(",").Select(int.Parse).ToList();

            //Call methods to find Mean, Median, Mode, and create and return new Calculation Value 
            return new Calculation() { Mean = Average(list), Median = Median(list), Mode = Mode(list) };
        }

        private double Average(List<int> numbers)
        {
            //Return average using built in List method for Average 
            return numbers.Average();
        }

        private double Median(List<int> numbers)
        {
            //Sort numbers using built in List method for Sort 
            numbers.Sort();

            //Get the middle value in numbers
            return numbers[numbers.Count() / 2];
        }

        private double Mode(List<int> numbers)
        {
            //Group numbers using Linq to get number and associated count
            var groupedNumbers = numbers.GroupBy(value => value);

            //Get the max of the grouping count 
            int maxGroupCount = groupedNumbers.Max(group => group.Count());


            //Return the first number where the grouping count equals the max
            return groupedNumbers.First(group => group.Count() == maxGroupCount).Key;
        }

    }
}
