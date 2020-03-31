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
            List<double> list = nums.Split(",").Select(double.Parse).ToList();

            //Call methods to find Mean, Median, Mode, and create and return new Calculation Value 
            return new Calculation()
            {
                Mean = Average(list),
                Median = Median(list),
                Mode = Mode(list)
            };
        }

        private double Average(List<double> numbers)
        {
            //Return average using built in List method for Average 
            return numbers.Average();
        }

        private double Median(List<double> numbers)
        {
            //Sort numbers using built in List method for Sort 
            numbers.Sort();

            //Get the middle value in numbers
            return numbers[numbers.Count() / 2];
        }

        private double?[] Mode(List<double> numbers)
        {
            //Group numbers using Linq to get number and associated count
            var groupedNumbers = numbers.GroupBy(value => value);

            //Get the max of the grouping count 
            int maxGroupCount = groupedNumbers.Max(group => group.Count());

            //If numbers has more than 1 value, and the max count == 1, then all values are unique so no mode
            if(maxGroupCount == 1 && numbers.Count() > 1)
            {
                return null;
            }

            //Return array of numbers where the grouping count equals the max
            return groupedNumbers.Where(group => group.Count() == maxGroupCount).Select(x => x.Key as double?).ToArray();
        }

    }
}
