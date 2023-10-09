using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Класс, инициализирующий объект-коллекцию, в котором лежат печатные издания: книги или журналы.
    /// </summary>
    /// <typeparam name="T">Типизированный параметр класса - печатное издание</typeparam>
    [DataContract, KnownType(typeof(Book)), KnownType(typeof(Magazine))]
    public class MyLibrary<T> : IEnumerable<T> where T : PrintEdition
    {
        /// <summary>
        /// Приватное поле класса: список печатных изданий.
        /// </summary>
        [DataMember]
        private List<T> _library;
        
        public event EventHandler<TakeEventArgs> onTake;
       
        /// <summary>
        /// Инициализирует экземпляр класса пустым списком печатных изданий.
        /// </summary>
        public MyLibrary()
        {
            _library = new List<T>();
        }
        /// <summary>
        /// Метод, вызывающий событие onTake и извлекающий из библиотеки все книги, начинающиеся на переданный символ.
        /// </summary>
        /// <param name="start">Символ, книги начинающиеся на который удаляются из библиотеки.</param>
        public void TakeBooks(char start)
        {
            for(int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Name[0] == start && _library[i].GetType() == typeof(Book))
                    _library.RemoveAt(i);
            }
            onTake?.Invoke(this, new TakeEventArgs(start));
        }
        /// <summary>
        /// Индексатор, возвращающий печатное издание из библиотеки по заданному индексу.
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Объект типизированного класса - класса печатного издания</returns>
        public T this[int index]
        {
            get
            {
                return _library[index];
            }
        }
        /// <summary>
        /// Добавляет в библиотеку новое печатное издание.
        /// </summary>
        /// <param name="printed">Добавляемое печатное издание.</param>
        public void Add(T printed) => _library.Add(printed);
        /// <summary>
        /// Возвращает перечислитель.
        /// </summary>
        /// <returns>Перечислитель.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new MyLibraryEnumerator<T>(_library);
        }
        /// <summary>
        /// Возвращает перечислитель.
        /// </summary>
        /// <returns>Перечислитель.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
       
        /// <summary>
        /// Возвращает среднее число страниц во всех книгах библиотеки
        /// </summary>
        public double GetAverageBookPages
        {
            get
            {
                double total_pages = 0, count = 0;
                for(int i = 0; i < _library.Count; i++)
                {
                    if (_library[i] is Book)
                    {
                        total_pages += _library[i].Pages;
                        count++;
                    }
                }
                if (count == 0)
                    return -1;
                else
                    return total_pages / count;
            }
        }
        /// <summary>
        /// Возвращает среднее число страниц во всех журналах библиотеки
        /// </summary>
        public double GetAverageMagazinePages
        {
            get
            {
                double total_pages = 0, count = 0;
                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i] is Magazine)
                    {
                        total_pages += _library[i].Pages;
                        count++;
                    }
                }
                if (count == 0)
                    return -1;
                else
                    return total_pages / count;
            }
        }


    }
}
