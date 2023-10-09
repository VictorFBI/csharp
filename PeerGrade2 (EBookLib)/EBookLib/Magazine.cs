using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Класс журнала.
    /// </summary>
    [DataContract]
    public class Magazine : PrintEdition
    {
        [DataMember]
        private int _period;
        /// <summary>
        /// Возвращает и устанавливает периодичность издания журнала и бросает исключение типа ArgumentException при установке значения, если периодичность издания журнала является неположительным числом.
        /// </summary>
        public int Period
        {
            get
            {
                return _period;
            }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Ошибка: период издания журнала должен быть положительным числом");
                else
                    _period = value;
            }
        }
        /// <summary>
        /// Инициализует экземпляр класса навзанием журнала, числом страниц, периодичностью издания.
        /// </summary>
        /// <param name="name">Название журнала.</param>
        /// <param name="pages">Число страниц журнала.</param>
        /// <param name="period">Периодичность издания журнала.</param>
        public Magazine(string name, int pages, int period) : base(name, pages)
        {          
            Period = period;
        }
        /// <summary>
        /// Возвращает информацию о журнале.
        /// </summary>
        /// <returns>Строка, содержащяя информацию о журнале (название, число страниц, периодичность).</returns>
        public override string ToString()
        {
            return base.ToString() + $", period = {Period:f2}";
        }
    }
}
