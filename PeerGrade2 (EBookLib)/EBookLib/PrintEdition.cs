using System.Runtime.Serialization;

namespace EBookLib
{
    /// <summary>
    /// Абстрактный класс печатного издания.
    /// </summary>
    [DataContract]
    abstract public class PrintEdition : IPrinting
    {
        /// <summary>
        /// Приватное поле для хранения числа страниц в печатном издании.
        /// </summary>
        [DataMember]
        private int _pages;
        /// <summary>
        /// Возвращает и устанавливает имя печатного издания.
        /// </summary>
        [DataMember] 
        public string Name { get; private set; }
        /// <summary>
        /// Возвращает и устанавливает число страниц печатного издания и бросает исключение типа ArgumentException при установке значения, если число страниц издания является неположительным числом.
        /// </summary>
        public int Pages
        {
            get
            {
                return _pages;
            }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Ошибка: число страниц должно быть положительным числом");
                else
                    _pages = value;
            }
        }
        /// <summary>
        /// Инициализирует экземпляр класса названием печатного издания и числом страниц.
        /// </summary>
        /// <param name="name">Название печатного издания.</param>
        /// <param name="pages">Число страниц печатного издания.</param>
        public PrintEdition(string name, int pages)
        {
            Name = name;
            Pages = pages;
        }
        /// <summary>
        /// Событие.
        /// </summary>
        public event EventHandler onPrint;
        /// <summary>
        /// Вызывает событие onPrint.
        /// </summary>
        public void Print()
        {
            onPrint?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Возвращает информацию о печатном издании.
        /// </summary>
        /// <returns>Строка, содержащяя информацию о печатном издании (название, число страниц).</returns>
        public override string ToString()
        {
            return $"Name = {Name}, pages = {Pages:f2}";
        }
    }
}