using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    /// <summary>
    /// Represents the methods for output information about car. Inheritor of abstract Transport class
    /// </summary>
    public class Car : Transport
    {
        /// <summary>
        /// Initializes a new instance of Car class
        /// </summary>
        /// <param name="model">Model of car</param>
        /// <param name="power">Power of car</param>
        public Car(string model, uint power) : base(model, power) { }

        /// <summary>
        /// Returns the information about model of current car and its sound of start engine
        /// </summary>
        /// <returns>The line that consists of information about model and sound of start engine of current car</returns>
        public override string StartEngine()
        {
            return $"{Model}: Vroom";
        }
        /// <summary>
        /// Returns the information about model of current car and its power
        /// </summary>
        /// <returns>The line that consists of information about model and power of current car</returns>
        public override string ToString()
        {
            return "Car. " + base.ToString();
        }
    }

}
