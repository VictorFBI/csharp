using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentDevices
{
    internal class PC : Device
    {
        private int graphics_cards_count, monitors_count;
        /// <summary>
        /// Initializes a new instance of the PC class without any parameters
        /// </summary>
        public PC() : this(0, "Unknown", 0, 0) { }
        /// <summary>
        /// Initializes a new instance of the PC class with id, model, graphics cards count and monitors сount
        /// </summary>
        /// <param name="id">Id of the PC</param>
        /// <param name="model">Model of the PC</param>
        /// <param name="graphics_cards_count">Graphics cards count of the PC</param>
        /// <param name="monitors_count">Monitors count of the PC</param>
        public PC(int id, string model, int graphics_cards_count, int monitors_count) : base(id, model)
        {
            GraphicsCardsCount = graphics_cards_count;
            MonitorsCount = monitors_count;
        }
        /// <summary>
        /// Returns the graphics cards count of the PC or sets it
        /// </summary>
        public int GraphicsCardsCount
        {
            get { return graphics_cards_count; }
            private set
            {
                graphics_cards_count = value;
            }
        }
        /// <summary>
        /// Returns the monitors count of the PC or sets it
        /// </summary>
        public int MonitorsCount
        {
            get { return monitors_count; }
            private set
            {
                monitors_count = value;
            }
        }
        /// <summary>
        /// Returns true if graphics cards count or monitors count no more than zero; otherwise, false
        /// </summary>
        /// <returns></returns>
        public bool HasProblems()
        {
            if (GraphicsCardsCount <= 0 || MonitorsCount <= 0) return true;
            else return false;
        }
        /// <summary>
        /// Returns the type of deivce
        /// </summary>
        public override string Type
        {
            get { return "Computer"; }
        }
        /// <summary>
        /// Returns the full information about device
        /// </summary>
        /// <returns>A string that contains full information about device</returns>
        public override string PrintInfo()
        {
            return $"Id: {Id}, model: {Model}, type: {Type}, graphics cards count: {GraphicsCardsCount}, " +
                $"monitors count: {MonitorsCount}, has problems or not: {HasProblems()}\n";
        }
        /// <summary>
        /// Returns the full information about device without explanations
        /// </summary>
        /// <returns>A string that contains full information about device without explanations</returns>
        public override string ToString()
        {
            return $"PC {Id} {Model} {GraphicsCardsCount} {MonitorsCount}";
        }
    }
}
