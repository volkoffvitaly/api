using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.User
{
    public class FullUserInfoEditDto
    {
        // From IdentityUser
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"Email\" обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[+]?[7-8][ -]?((\\([0-9]{3}\\))|([0-9]{3}))[ -]?[0-9]{3}[-]?[0-9]{2}[-]?[0-9]{2}$", ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; }


        // From ApplicationUser
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
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Discord { get; set; }
        public string Skype { get; set; }
        public bool IsTelegram { get; set; }
        public bool IsWhatsApp { get; set; }
        public bool IsViber { get; set; }

        public Guid? CvId { get; set; }
        public Guid? CompanyId { get; set; }
        public string Post { get; set; }
    }
}
