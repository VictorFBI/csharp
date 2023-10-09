using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EBookLib
{
    /// <summary>
    /// Класс книги.
    /// </summary>
    [DataContract]
    public class Book : PrintEdition
    {
        /// <summary>
        /// Возвращает и устанавливает имя автора книги.
        /// </summary>
        [DataMember]
        public string Author { get; private set; }
        /// <summary>
        /// Инициализует экземпляр класса названием книги, числом страниц, автором.
        /// </summary>
        /// <param name="name">Название книги.</param>
        /// <param name="pages">Число страниц книги.</param>
        /// <param name="author">Автор книги.</param>
        public Book(string name, int pages, string author) : base(name, pages)
        {
            Author = author;
        }
        /// <summary>
        /// Возвращает информацию о книге.
        /// </summary>
        /// <returns>Строка, содержащяя информацию о книге (название, число страниц, автор).</returns>
        public override string ToString()
        {
            return base.ToString() + $", author = {Author}";
        }
    }
}
