using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Modelsw
{
    public class Kullanici
    {
        private string _Name;
        private string _Surname;
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int YetkiId { get; set; }
        [NotMapped]
        public virtual bool BeniHatirla { get; set; }
        public string Name { get { if (_Name == null) { return "YOK"; } else { return _Name; } } set { _Name = value; } }

        public string Surname { get { if (_Surname == null) { return "YOK"; } else { return _Surname; } } set { _Surname = value; } }

    }
}
