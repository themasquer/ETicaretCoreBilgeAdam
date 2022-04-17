﻿using AppCore.Business.Models.Paging;
using AppCore.Business.Models.Results;
using Business.Models.Filters;
using Business.Models.Reports;
using Business.Services;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UrunlerRaporController : Controller
    {
        private readonly IUrunService _urunService;
        private readonly IKategoriService _kategoriService;

        public UrunlerRaporController(IUrunService urunService, IKategoriService kategoriService)
        {
            _urunService = urunService;
            _kategoriService = kategoriService;
        }

        //public IActionResult Index()
        //public IActionResult Index(int? kategoriId)
        public async Task<IActionResult> Index(int? kategoriId) // asenkron metodlar mutlaka async Task dönmeli ve içinde çağrılan asenkron metodla birlikte await kullanılmalı!
        {
            #region Filtreleme
            UrunRaporFilterModel filtre = new UrunRaporFilterModel()
            {
                KategoriId = kategoriId
            };
            #endregion

            #region Sayfalama
            PageModel sayfa = new PageModel();
            #endregion

            Result<List<UrunRaporModel>> result = await _urunService.RaporGetirAsync(filtre, sayfa);
            ViewBag.Sonuc = result.Message;

            UrunlerRaporIndexViewModel viewModel = new UrunlerRaporIndexViewModel()
            {
                UrunlerRapor = result.Data,
                UrunlerFiltre = filtre,

                //KategorilerSelectList = new SelectList(await _kategoriService.KategorileriGetirAsync(), "Id", "Adi")
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UrunlerRaporIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Result<List<UrunRaporModel>> result = await _urunService.RaporGetirAsync(viewModel.UrunlerFiltre);
                ViewBag.Sonuc = result.Message;
                viewModel.UrunlerRapor = result.Data;
            }
            return PartialView("_UrunlerRapor", viewModel.UrunlerRapor);
        }
    }
}
