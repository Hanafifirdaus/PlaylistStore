using PlaylistStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistStore.Controllers
{
    public class GenreController : Controller
    {
        private OperationDataContext context = null;
        public GenreController()
        {
            context = new OperationDataContext();
        }
        public ActionResult Index()
        {
            List<GenreModel> genreList = new List<GenreModel>();
            var query = from genre in context.Genres select genre;

            var genres = query.ToList();

            foreach (var a in genres) //genres -> denre di database
            {
                genreList.Add(new GenreModel()
                {
                    Id = a.Id,
                    Name = a.Name
                });
            }  

            return View(genreList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GenreModel model)
        {
            try
            {
                Genre genre = new Genre()
                {
                    Name = model.Name
                };

                context.Genres.InsertOnSubmit(genre);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            
        }
    }
}