using System;
using System.ComponentModel.DataAnnotations;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Models.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле \"Имя пользователя\" обязательно")]
        [StringLength(64, ErrorMessage = "Длина имени пользователя не может быть меньше {2} и не более {1} символов", MinimumLength = 6)]
        [RegularExpression("[A-Za-z0-9_-]+", ErrorMessage = "Имя пользователя может содержать только цифры, латинские буквы, дефисы (-) и нижние подчеркивания (_)")]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"Email\" обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"Пароль\" обязательно для заполнения")]
        [StringLength(64, ErrorMessage = "Длина пароля должна быть не менее {2} и не более {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [RegularExpression("([0-9][A-Za-z0-9]*[A-Z][A-Za-z0-9]*[a-z])|([0-9][A-Za-z0-9]*[a-z][A-Za-z0-9]*[A-Z])|([A-Z][A-Za-z0-9]*[0-9][A-Za-z0-9]*[a-z])|([A-Z][A-Za-z0-9]*[a-z][A-Za-z0-9]*[0-9])|([a-z][A-Za-z0-9]*[A-Z][A-Za-z0-9]*[0-9])|([a-z][A-Za-z0-9]*[0-9][A-Za-z0-9]*[A-Z])",
            ErrorMessage = "Пароль должен содержать как минимум одну заглавную латинскую букву, строчную и цифру. Другие символы не разрешены")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"Подтвердите пароль\" обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"ФИО\" обязательно для заполнения")]
        [StringLength(256, ErrorMessage = "Длина ФИО должна быть не менее {2} и не более {1} символов", MinimumLength = 1)]
        [RegularExpression("[А-ЯЁа-яё -]+", ErrorMessage = "ФИО может содержать только русские буквы, дефис (-) и пробелы")]
        [Display(Name = "ФИО")]
        public string FCs { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Пол")]
        public Gender Gender { get; set; }
    }
}
