using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
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

        //public ViewResult List()        
        //{
        //    //ViewBag.CurrentCategory = "Cheese cakes";

        //    PiesListViewModel piesListViewModel = new PiesListViewModel();
        //    piesListViewModel.Pies = _pieRepository.AllPies;

        //    piesListViewModel.CurrentCategory = "Cheese cakes";
        //    return View(piesListViewModel);
        //}

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }


        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie==null)
            {
                return NotFound();
            }
            return View(pie);
        }

    }
}
