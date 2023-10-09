using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Предоставляет событие, основанное на стандартном шаблоне, и метод для вызова этого события.
    /// </summary>
    public interface IPrinting
    {
        /// <summary>
        /// Событие, основанное на стандартном шаблоне.
        /// </summary>
        event EventHandler onPrint;
        /// <summary>
        /// Метод для вызова события.
        /// </summary>
        void Print();
    }
}
