using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Класс для обощения стандратного шаблона событий.
    /// </summary>
    public class TakeEventArgs : EventArgs
    {
        /// <summary>
        /// Возвращает и устанавливает начальный символ.
        /// </summary>
        public char StartSymbol { get; set; }
        /// <summary>
        /// Инициализирует экземпляр класса начальным символом.
        /// </summary>
        /// <param name="startSymbol">Начальный символ.</param>
        public TakeEventArgs(char startSymbol)
        {
            StartSymbol = startSymbol;
        }
    }
}
