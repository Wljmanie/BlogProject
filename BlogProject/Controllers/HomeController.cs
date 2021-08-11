﻿using BlogProject.Data;
using BlogProject.Models;
using BlogProject.Services;
using BlogProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogEmailSender _blogEmailSender;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender blogEmailSender, ApplicationDbContext context)
        {
            _logger = logger;
            _blogEmailSender = blogEmailSender;
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            //var blogs = await _context.Blogs.Include(b => b.BlogUser).ToListAsync();
            //return View(blogs);


            var pageNumber = page ?? 1;
            var pageSize = 6;

            var blogs = _context.Blogs.Include(b => b.BlogUser).Where(
                b => b.Posts.Any(p => p.ReadyStatus == Enums.ReadyStatus.ProductionReady))
                .OrderByDescending(b => b.Created)
                .ToPagedListAsync(pageNumber, pageSize);

            return View(await blogs);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            model.Message = $"{model.Message} <hr/> Dit werkt ofso.";
            await _blogEmailSender.SendContactEmailAsync(model.Email, model.Name, model.Subject, model.Message);


            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
