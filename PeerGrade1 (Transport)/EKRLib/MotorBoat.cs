using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    /// <summary>
    /// Represents the methods for output information about motorboat. Inheritor of abstract Transport class
    /// </summary>
    public class MotorBoat : Transport
    {
        /// <summary>
        /// Initializes a new instance of MotorBoat class
        /// </summary>
        /// <param name="model">Model of motorboat</param>
        /// <param name="power">Power of motorboat</param>
        public MotorBoat(string model, uint power) : base(model, power) { }

        /// <summary>
        /// Returns the information about model of current motorboat and its sound of start engine
        /// </summary>
        /// <returns>The line that consists of information about model and sound of start engine of current motorboat</returns>
        public override string StartEngine()
        {
            return $"{Model}: Brrrbrr";
        }
        /// <summary>
        /// Returns the information about model of current motorboat and its power
        /// </summary>
        /// <returns>The line that consists of information about model and power of current motorboat</returns>
        public override string ToString()
        {
            return "MotorBoat. " + base.ToString();
        }
    }

}
