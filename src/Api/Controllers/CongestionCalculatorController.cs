using Domain.Models;
using Domain.Rules.Calculator;
using Domain.Vehicles;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionCalculatorController : ControllerBase
    {
        public CongestionCalculatorController(ILogger<CongestionCalculatorController> logger)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dates"></param>
        /// <param name="vehicle"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CongestionCalculator
        ///     {
        ///        "dates": "2013-02-07 06:23:27, 2013-02-07 15:27:00, 2013-02-08 06:27:00, 2013-02-08 06:20:27, 2013-02-08 14:35:00, 2013-02-08 15:29:00, 2013-02-08 15:47:00, 2013-02-08 16:01:00, 2013-02-08 16:48:00, 2013-02-08 17:49:00, 2013-02-08 18:29:00, 2013-02-08 18:35:00, 2013-03-26 14:25:00, 2013-03-28 14:07:27"
        ///        "vehicle": "Car"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public int Get([FromForm] string dates, [FromForm]string vehicle)
        {
            var dateValues = dates.Split(',').Select(it => DateTime.Parse(it))
                .ToArray();
            var car = VehicleFactory.GetInstance(vehicle);
            var command = new CongestionCommand()
            {
                Dates = dateValues,
                Vehicle = car
            };
            var calculator = new CalculatorRulesEngine();
            return calculator.Execute(command);
        }
    }
}