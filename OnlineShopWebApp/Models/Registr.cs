 using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Registr
    {
        [Required(ErrorMessage = "Не указано имя пользователя"), EmailAddress(ErrorMessage = "Введите адрес электронной почты")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан пароль"), StringLength(100, MinimumLength = 4, ErrorMessage = "Пароль должен быть от 4 до 100 знаков")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}