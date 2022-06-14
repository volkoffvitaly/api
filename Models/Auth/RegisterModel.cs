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
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"Подтвердите пароль\" обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"Фамилия\" обязательно для заполнения")]
        [StringLength(256, ErrorMessage = "Длина фамилии должна быть не менее {2} и не более {1} символов", MinimumLength = 1)]
        [RegularExpression("[А-ЯЁа-яё-]+", ErrorMessage = "Фамилия может содержать только русские буквы и дефис (-)")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [StringLength(256, ErrorMessage = "Длина отчества должна быть не менее {2} и не более {1} символов", MinimumLength = 1)]
        [RegularExpression("[А-ЯЁа-яё-]+", ErrorMessage = "Отчество может содержать только русские буквы и дефис (-)")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"Имя\" обязательно для заполнения")]
        [StringLength(256, ErrorMessage = "Длина имени должна быть не менее {2} и не более {1} символов", MinimumLength = 1)]
        [RegularExpression("[А-ЯЁа-яё-]+", ErrorMessage = "Имя может содержать только русские буквы и дефис (-)")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Курс")]
        public Grade? Grade { get; set; }

        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        public string Role { get; set; } = ApplicationRoles.Student; 
    }
}
