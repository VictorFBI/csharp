using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Класс вручную реализованного обощённого итератора.
    /// </summary>
    /// <typeparam name="U">Типизированный параметр класса.</typeparam>
    class MyLibraryEnumerator<U> : IEnumerator<U>
    {
        /// <summary>
        /// Приватное поле, представляющее позицию каретки.
        /// </summary>
        private int _position = -1;
        /// <summary>
        /// Итерируемый список.
        /// </summary>
        private List<U> _list;
        
        public MyLibraryEnumerator(List<U> list)
        {
            _list = list;
        }
        /// <summary>
        /// Возвращает текущую позицию каретки.
        /// </summary>
        public U Current => _list[_position];
        /// <summary>
        /// Возвращает текущую позицию каретки.
        /// </summary>
        object? IEnumerator.Current => Current;

        public void Dispose()
        {

        }
        
        public IEnumerator<U> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (_position < _list.Count - 1)
            {
                _position++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _position = -1;
        }

    }
}
