using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MentalMusicians.Entities
{
    public class MusicianEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Boş bırakılamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Boş bırakılamaz")]
        public string Profession { get; set; }

        [Required(ErrorMessage ="Boş bırakılamaz")]
        [StringLength(250,MinimumLength =20,ErrorMessage ="Özel hareket 20 ile 250 karekter arasında olmalı.")]
        public string SpecialAct { get; set; }

        public bool IsDeleted { get; set; }
    }
}