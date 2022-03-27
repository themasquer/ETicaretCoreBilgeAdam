﻿using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IKullaniciService : IService<KullaniciModel, Kullanici, ETicaretContext>
    {
        Result<List<KullaniciModel>> KullanicilariGetir();
        Result<KullaniciModel> KullaniciGetir(int id);
    }

    public class KullaniciService : IKullaniciService
    {
        public RepoBase<Kullanici, ETicaretContext> Repo { get; set; }

        private readonly ETicaretContext _eTicaretContext;
        private readonly RepoBase<KullaniciDetayi, ETicaretContext> _kullaniciDetayiRepo;
        private readonly RepoBase<Rol, ETicaretContext> _rolRepo;
        private readonly RepoBase<Ulke, ETicaretContext> _ulkeRepo;
        private readonly RepoBase<Sehir, ETicaretContext> _sehirRepo;

        public KullaniciService()
        {
            // Query'de join'de kullanılan tüm repository'ler aynı DbContext'i kullanmalı, bu yüzden ETicaretContext hepsine enjekte edilmeli
            _eTicaretContext = new ETicaretContext();
            Repo = new Repo<Kullanici, ETicaretContext>(_eTicaretContext); // Kullanici Repository
            _kullaniciDetayiRepo = new Repo<KullaniciDetayi, ETicaretContext>(_eTicaretContext);
            _rolRepo = new Repo<Rol, ETicaretContext>(_eTicaretContext);
            _ulkeRepo = new Repo<Ulke, ETicaretContext>(_eTicaretContext);
            _sehirRepo = new Repo<Sehir, ETicaretContext>(_eTicaretContext);
        }

        public IQueryable<KullaniciModel> Query()
        {
            var kullaniciQuery = Repo.Query();
            var kullaniciDetayiQuery = _kullaniciDetayiRepo.Query();
            var rolQuery = _rolRepo.Query();
            var ulkeQuery = _ulkeRepo.Query();
            var sehirQuery = _sehirRepo.Query();

            // SQL benzeri LINQ inner join:
            var query = from kullanici in kullaniciQuery
                        join kullaniciDetayi in kullaniciDetayiQuery
                        on kullanici.Id equals kullaniciDetayi.KullaniciId
                        join rol in rolQuery
                        on kullanici.RolId equals rol.Id
                        join ulke in ulkeQuery
                        on kullaniciDetayi.UlkeId equals ulke.Id
                        join sehir in sehirQuery
                        on kullaniciDetayi.SehirId equals sehir.Id
                        //where kullanici.AktifMi // bir veya birden çok koşul and veya or ile kullanılabilir, tüm aktif veya aktif olmayan kullanıcılar gelmeli.
                        orderby rol.Adi, kullanici.KullaniciAdi // bir veya birdan çok entity özelliği kullanılabilir
                        select new KullaniciModel()
                        {
                            Id = kullanici.Id,
                            KullaniciAdi = kullanici.KullaniciAdi,
                            Sifre = kullanici.Sifre,
                            AktifMi = kullanici.AktifMi,
                            KullaniciDetayi = new KullaniciDetayiModel()
                            {
                                Cinsiyet = kullaniciDetayi.Cinsiyet,
                                Eposta = kullaniciDetayi.Eposta,
                                UlkeId = kullaniciDetayi.UlkeId,
                                UlkeAdiDisplay = ulke.Adi,
                                SehirId = kullaniciDetayi.SehirId,
                                SehirAdiDisplay = sehir.Adi,
                                Adres = kullaniciDetayi.Adres
                            },
                            RolId = kullanici.RolId,
                            RolAdiDisplay = rol.Adi,
                            AktifDisplay = kullanici.AktifMi ? "Evet" : "Hayır"
                        };
            return query;
        }

        public Result Add(KullaniciModel model)
        {
            if (Repo.Query().Any(k => k.KullaniciAdi.ToUpper() == model.KullaniciAdi.ToUpper().Trim()))
                return new ErrorResult("Girilen kullanıcı adına sahip kullanıcı kaydı bulunmaktadır!");
            if (Repo.Query("KullaniciDetayi").Any(k => k.KullaniciDetayi.Eposta.ToUpper() == model.KullaniciDetayi.Eposta.ToUpper().Trim()))
                return new ErrorResult("Girilen e-postaya sahip kullanıcı kaydı bulunmaktadır!");
            var entity = new Kullanici()
            {
                AktifMi = model.AktifMi,
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                RolId = model.RolId.Value,
                KullaniciDetayi = new KullaniciDetayi()
                {
                    Adres = model.KullaniciDetayi.Adres.Trim(),
                    Cinsiyet = model.KullaniciDetayi.Cinsiyet,
                    Eposta = model.KullaniciDetayi.Eposta.Trim(),
                    SehirId = model.KullaniciDetayi.SehirId.Value,
                    UlkeId = model.KullaniciDetayi.UlkeId.Value
                }
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Update(KullaniciModel model)
        {
            if (Repo.Query().Any(k => k.KullaniciAdi.ToUpper() == model.KullaniciAdi.ToUpper().Trim() && k.Id != model.Id))
                return new ErrorResult("Girilen kullanıcı adına sahip kullanıcı kaydı bulunmaktadır!");
            if (Repo.Query("KullaniciDetayi").Any(k => k.KullaniciDetayi.Eposta.ToUpper() == model.KullaniciDetayi.Eposta.ToUpper().Trim() && k.Id != model.Id))
                return new ErrorResult("Girilen e-postaya sahip kullanıcı kaydı bulunmaktadır!");
            var entity = Repo.Query(k => k.Id == model.Id, "KullaniciDetayi").SingleOrDefault();
            entity.AktifMi = model.AktifMi;
            entity.KullaniciAdi = model.KullaniciAdi;
            entity.Sifre = model.Sifre;
            entity.RolId = model.RolId.Value;
            entity.KullaniciDetayi.Cinsiyet = model.KullaniciDetayi.Cinsiyet;
            entity.KullaniciDetayi.Adres = model.KullaniciDetayi.Adres.Trim();
            entity.KullaniciDetayi.Cinsiyet = model.KullaniciDetayi.Cinsiyet;
            entity.KullaniciDetayi.Eposta = model.KullaniciDetayi.Eposta.Trim();
            entity.KullaniciDetayi.SehirId = model.KullaniciDetayi.SehirId.Value;
            entity.KullaniciDetayi.UlkeId = model.KullaniciDetayi.UlkeId.Value;
            Repo.Update(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            var entity = Repo.Query(k => k.Id == id).SingleOrDefault();
            _kullaniciDetayiRepo.Delete(kd => kd.KullaniciId == entity.Id, false);
            Repo.Delete(entity);
            return new SuccessResult();
        }

        public void Dispose()
        {
            Repo.Dispose();
            _kullaniciDetayiRepo.Dispose();
            _rolRepo.Dispose();
            _ulkeRepo.Dispose();
            _sehirRepo.Dispose();
        }

        public Result<List<KullaniciModel>> KullanicilariGetir()
        {
            var kullanicilar = Query().ToList();
            if (kullanicilar.Count == 0)
                return new ErrorResult<List<KullaniciModel>>("Kullanıcı bulunamadı!");
            return new SuccessResult<List<KullaniciModel>>(kullanicilar.Count + " kullanıcı bulundu.", kullanicilar);
        }

        public Result<KullaniciModel> KullaniciGetir(int id)
        {
            var kullanici = Query().SingleOrDefault(k => k.Id == id);
            if (kullanici == null)
                return new ErrorResult<KullaniciModel>("Kullanıcı bulunamadı!");
            return new SuccessResult<KullaniciModel>(kullanici);
        }
    }
}
