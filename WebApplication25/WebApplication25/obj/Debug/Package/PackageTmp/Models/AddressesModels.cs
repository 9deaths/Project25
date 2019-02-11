using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WebApplication25.Models
{
    //объекты POCO-класса (Plain Old CLR Object), содержащий 4 свойства, совпадающих с колонками базы данных
    public class AddressBookEntry

    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Страна")]
        [Required]
        [RegularExpression(@"^[a-zA-Z\sА-Яа-я]{1,40}$", ErrorMessage = "Некорректные символы в поле <Страна>")]
        public string Country { get; set; }

        [Display(Name = "Город")]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\sА-Яа-я]{1,30}$", ErrorMessage = "Некорректные символы в поле <Город>")]
        public string City { get; set; }

        [Display(Name = "Улица")]        
        [RegularExpression(@"^[a-zA-Z0-9\sА-Яа-я]{0,30}$", ErrorMessage = "Некорректные символы в поле <Улица>")]
        public string Street { get; set; }

        
       
        [Display(Name = "Номер дома")]
        [RegularExpression(@"^[0-9]{0,4}$", ErrorMessage = "Некорректные символы в поле <Номер дома>")]
        public int? HomeNumber { get; set; }

        public DateTime DateAdded { get; set; }
       
    }
}