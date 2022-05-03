using System;
using System.Runtime.Serialization;

namespace TinkoffWatcher_Api.Filters
{
    /// <summary>
    /// Базовый фильтр
    /// </summary>
    public class BaseFilter
    {
        public const string DescendingKeyword = "DESC";
        public const string AscendingKeyword = "ASC";
        public const int DefaultPageSizeConst = 20;
        public const int MaxPageSizeConst = 1000;

        public BaseFilter()
        {
            Page = 1;
        }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int Page { get; set; }


        private int _pageSize;

        /// <summary>
        /// Всего записей
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Колонка для сортировки
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Сортировка по убыванию
        /// </summary>
        public bool SortDesc { get; set; }

        /// <summary>
        /// Колонка для сортировки по умолчанию
        /// </summary>
        [IgnoreDataMember]
        public string DefaultSortColumn { get; set; }

        /// <summary>
        /// Сортировка по убыванию по умолчанию
        /// </summary>
        [IgnoreDataMember]
        public bool? DefaultSortDesc { get; set; }

        /// <summary>
        /// Выражение для сортировки
        /// </summary>
        [IgnoreDataMember]
        public string SortExpression =>
            !string.IsNullOrEmpty(SortColumn) ? $"{SortColumn} {(SortDesc ? DescendingKeyword : AscendingKeyword)}" : null;

        /// <summary>
        /// Количество пропущенных записей
        /// </summary>
        public int SkipCount => DefaultPageSizeConst * (Page - 1);
    }
}