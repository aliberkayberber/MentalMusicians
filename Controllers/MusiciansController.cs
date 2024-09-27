using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentalMusicians.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MentalMusicians.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusiciansController : ControllerBase
    {
        private static List<MusicianEntity> _musicians = new List<MusicianEntity>()
        {
            new MusicianEntity{Id =1,Name="Ahmet Çalgı",Profession="Ünlü Çalgı Çalar",SpecialAct="Her zaman yanlış nota çalar, ama çok eğlenceli"},
            new MusicianEntity{Id =2,Name="Zeynep Melodi",Profession="Popüler Melodi Yazarı",SpecialAct="Şarkıları yanlış anlaşılıyor ama çok popüler"},
            new MusicianEntity{Id =3,Name="Cemil Akor",Profession="Çalgın Akoristi",SpecialAct="Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli"},
            new MusicianEntity{Id =4,Name="Fatma Nota",Profession="Sürpriz Nota Üreticisi",SpecialAct="Nota üretirken sürekli sürprizler hazırlar"},
            new MusicianEntity{Id =5,Name="Hasan Ritim",Profession="Ritim Canavarı",SpecialAct="Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir"},
            new MusicianEntity{Id =6,Name="Elif Armoni",Profession="Armoni Ustası",SpecialAct="Armonilerini bazen yanlış çalar, ama çok yaratıcıdır"},
            new MusicianEntity{Id =7,Name="Ali Perde",Profession="Perde Uygulayıcı",SpecialAct="Her perdeyi farklı şekilde çalar, her zaman sürprizlidir"},
            new MusicianEntity{Id =8,Name="Ayşe Rezonans",Profession="Rezonans Uzmanı",SpecialAct="Rezonans konusunda uzman, ama bazen çok gürültü çıkarır"},
            new MusicianEntity{Id =9,Name="Murat Ton",Profession="Tonlama Meraklısı",SpecialAct="Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç"},
            new MusicianEntity{Id =10,Name="Selin Akor",Profession="Akor Sihirbazı",SpecialAct="Akorları değiştirdiğinde bazen sihirli bir hava yaratır"}
        };

        [HttpGet]
        public IActionResult GetAll() // tüm müzisyenleri gönderir 
        {
            return Ok(_musicians); // listede olan tüm müzisyenleri döner
        }

        [HttpGet("{id:min(1):int}",Name ="GetById")]
        public IActionResult Get(int id) // id kullanarak eşleşen bir müzisyeni gönderir 
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id); // verilen id ile eşlen müzisyen bulunur

            if(musician is null) // eğer verilen id listemizde yoksa not found dönülür
                return NotFound();

            return Ok(musician); // müzisyen dönülür 200 status code ile
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword) // anahtar kelime ile aratılıp kelimeyi içeren tüm müzisyenleri geri gönderir
        { 
            var musician = _musicians.Where(x => x.Name.Contains(keyword) || x.Profession.Contains(keyword) || x.SpecialAct.Contains(keyword)).ToList(); // kelimeyi içeriyor mu diye kontrol ediliyor

            if(musician.Count == 0) // eğer eşleşen yoksa not found döndürür
                return NotFound();

            return Ok(musician); // eşleşen müzisyenleri döndürür
        }

        [HttpPost]
        public IActionResult Create([FromBody] MusicianEntity musicianEntity) // yeni bir müzisyen eklemek için kullanılır
        {
            if(!ModelState.IsValid) // modele uygun mu kontrol edilir eğer uygun değilse bad request döner
                return BadRequest(ModelState);

            var id = _musicians.Max(x => x.Id) +1; 
            
            musicianEntity.Id = id; // id listemizin max id +1 olarak ayarlanır

            _musicians.Add(musicianEntity); // yeni müzisyen listeye eklenir

            return CreatedAtRoute("GetById",new {id= musicianEntity.Id},musicianEntity); // yeni müzisyen gönderilir 201 status code ile
        }

        [HttpPatch("rename/{id:int:min(1)}/{newName:minlength(3)}")]
        public IActionResult Rename(int id, string newName, [FromBody] MusicianEntity musicianEntity) // müzisyenin ismini güncellemek //için kullanılır
        {
            if(!ModelState.IsValid) // modele uygun değilse bad request döner
                return BadRequest(ModelState);

            var musician = _musicians.FirstOrDefault(x => x.Id == id); // id si verilen müzisyen bulunur

            if(musician is null) // eğer verilen id listemizde yoksa not found dönülür
                return NotFound();

            musician.Name = newName; // yeni ismi müzisyene aktarılır

            return Ok(musician); // müzisyeni döndürür 200 status code ile
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) // listeden müzisyen silmek için kullanılır
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id); // verilen id ile eşlen müzisyen bulunur

            if(musician is null) // eğer verilen id eşleşmezse notfound dönülür
                return NotFound();

            musician.IsDeleted = true; // id varsa soft delete yapılır

            return NoContent(); // status code gönderilir
        }


    }
}