using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentDevices
{
    internal class Phone : Device
    {
        private double screenDiagonal;
        /// <summary>
        /// Initializes a new instance of the PC class without any parameters
        /// </summary>
        public Phone() : this(0, "Unknown", 0) { }
        /// <summary>
        /// Initializes a new instance of the Phone class with id, model and screen diagonal
        /// </summary>
        /// <param name="id">Id of the phone</param>
        /// <param name="model">Model of the phone</param>
        /// <param name="screenDiagonal">Screen diagonal of the phone</param>
        public Phone(int id, string model, double screenDiagonal) : base(id, model)
        {
            ScreenDiagonal = screenDiagonal;
        }
        /// <summary>
        /// Returns the value of phone screen diagonal or sets it
        /// </summary>
        public double ScreenDiagonal
        {
            get { return screenDiagonal; }
            private set
            {
                if (value >=3 && value <= 8) screenDiagonal = value;
            }
        }
        /// <summary>
        /// Returns the type of deivce
        /// </summary>
        public override string Type
        {
            get { return "Phone"; }
        }
        /// <summary>
        /// Returns the full information about device
        /// </summary>
        /// <returns>A string that contains full information about device</returns>
        public override string PrintInfo()
        {
            return $"Id: {Id}, model: {Model}, type: {Type}, screen diagonal: {ScreenDiagonal}\n";
        }
        /// <summary>
        /// Returns the full information about device without explanations
        /// </summary>
        /// <returns>A string that contains full information about device without explanations</returns>
        public override string ToString()
        {
            return $"{Type} {Id} {Model} {ScreenDiagonal}";
        }
    }
}
