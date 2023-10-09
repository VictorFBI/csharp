using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    /// <summary>
    /// An abstract class that represents the methods for output information about transport. Can be inherited
    /// </summary>
    public abstract class Transport
    {
        private string m_model;
        private uint m_power;
        /// <summary>
        /// Initializes a new instance of an inheritor of the Transport class
        /// </summary>
        /// <param name="model">Model of transport</param>
        /// <param name="power">Power of transport</param>
        protected Transport(string model, uint power)
        {
            Model = model;
            Power = power;

        }
        /// <summary>
        /// Returns the model of transport or sets it if the model is good otherwise throws TransportException with special message
        /// </summary>
        protected string Model
        {
            get
            {
                return m_model;
            }
            set
            {
                if (Methods.CheckModel(value)) m_model = value;
                else throw new TransportException($"Недопустимая модель {value}");
            }
        }
        /// <summary>
        /// Returns the power of transport or sets it if the power more or equal than 20 otherwise throws TransportException with special message
        /// </summary>
        protected uint Power
        {
            get
            {
                return m_power;
            }
            set
            {
                if (value >= 20) m_power = value;
                else throw new TransportException("Мощность не может быть меньше 20 л.с.");
            }
        }

        /// <summary>
        /// Returns the information about model of current transport and its sound of start engine
        /// </summary>
        /// <returns>The line that consists of information about model and sound of start engine of current transport</returns>
        public abstract string StartEngine();

        /// <summary>
        /// Returns the information about model of current transport and its power
        /// </summary>
        /// <returns>The line that consists of information about model and power of current transport</returns>
        public override string ToString()
        {
            return $"Model: {Model}, Power: {Power}";
        }
    }
}
