using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        //Burada repository lerimize erişmemiz gerek

        //private - özel
        //readonly - salt okunur
        private readonly IPieRepository _pieRepository; //1.bunlar sadece alandır, otomatik olarak başlatılmazlar,
        private readonly ICategoryRepository _categoryRepository;//bunu constructor (yapıcı) kullanarak yaparız

        public PieController(IPieRepository pieRepository, //2.bunlar controller ımıza enjekte edilecek
            ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository; //3.artık controllerdaki model sınıflarımıza erişim var
        }

        public ViewResult List()
        {
            return View(_pieRepository.AllPies);
        }

    }
}
