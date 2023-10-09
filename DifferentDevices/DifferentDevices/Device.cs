using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentDevices
{
    internal abstract class Device
    {
        private int id;
        /// <summary>
        /// Initializes a new instance of the Device class without any parameters
        /// </summary>
        public Device() : this(0, "Unknown") { }
        /// <summary>
        /// Initializes a new instance of the Device class with id and model
        /// </summary>
        /// <param name="id">Id of the deivce</param>
        /// <param name="model">Id of the model</param>
        public Device(int id, string model)
        {
            Id = id;
            Model = model;  
        }
        /// <summary>
        /// Returns the Id of the device or sets it
        /// </summary>
        public int Id
        {
            get { return id; }
            private set
            {
                if (value < 1 || value > 1000) throw new ArgumentOutOfRangeException("Wrond id");
                else id = value;
            }
        }
        /// <summary>
        /// Returns the model of the device or sets it
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Returns the type of the device
        /// </summary>
        public virtual string Type
        {
            get { return "Device"; }
        }
        /// <summary>
        /// Returns the full information about device
        /// </summary>
        /// <returns></returns>
        public virtual string PrintInfo()
        {
            return $"Id: {Id}, model: {Model}, type: {Type}\n";
        }
        /// <summary>
        /// Returns the full information about device without explanations
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Type} {Id} {Model}";
        }
    }
}
