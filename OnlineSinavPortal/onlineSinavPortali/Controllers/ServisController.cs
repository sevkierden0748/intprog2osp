using onlineSinavPortali.Models;
using onlineSinavPortali.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace onlineSinavPortali.Controllers
{
    public class ServisController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();


        #region Kategori

        [HttpGet]
        [Route("api/kategoriliste")]

        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.katId == katId).Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi,
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.katAdi == model.katAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlı...Lütfen Tekrar Deneyiniz! ";

                return sonuc;
            }

            Kategori yeni = new Kategori(); yeni.katAdi = model.katAdi; db.Kategori.Add(yeni); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == model.katId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }

            kayit.katAdi = model.katAdi; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Yeniden Düzenlendi.";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == katId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";

                return sonuc;
            }

            if (db.Sinav.Count(s => s.sinavKatId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Sınav Kayıtlı Olan Kategori Silinemez!";

                return sonuc;
            }

            db.Kategori.Remove(kayit); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi.";

            return sonuc;
        }
        #endregion

        #region Ogrenci

        [HttpGet]
        [Route("api/ogrenciliste")]
        public List<OgrenciModel> OgrenciListe()
        {
            List<OgrenciModel> liste = db.Ogrenci.Select(x => new OgrenciModel()
            {
                ogrId = x.ogrId,
                ogrAdi = x.ogrAdi,
                ogrSoyadi = x.ogrSoyadi,
                ogrTel = x.ogrTel,


            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/ogrenciById/{OgrenciId}")]
        public OgrenciModel OgrenciById(int OgrenciId)
        {
            OgrenciModel kayit = db.Ogrenci.Where(s => s.ogrId == OgrenciId).Select(x => new OgrenciModel()
            {
                ogrId = x.ogrId,
                ogrAdi = x.ogrAdi,
                ogrSoyadi = x.ogrSoyadi,
                ogrTel = x.ogrTel,

            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/ogrenciEkle")]
        public SonucModel OgrenciEkle(OgrenciModel model)
        {
            if (db.Ogrenci.Count(s => s.ogrAdi == model.ogrAdi || s.ogrTel == model.ogrTel) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcının Telefon Numarası Kayıtlı Değildir.";
                return sonuc;
            }

            Ogrenci yeni = new Ogrenci();
            yeni.ogrAdi = model.ogrAdi;
            yeni.ogrSoyadi = model.ogrSoyadi;
            yeni.ogrId = model.ogrId;
            yeni.ogrTel = model.ogrTel;

            db.Ogrenci.Add(yeni);

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/ogrenciDuzenle")]
        public SonucModel OgrenciDuzenle(OgrenciModel model)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == model.ogrId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Kaydı Bulunamadı";

                return sonuc;
            }
            kayit.ogrId = model.ogrId;
            kayit.ogrAdi = model.ogrAdi;
            kayit.ogrSoyadi = model.ogrSoyadi;
            kayit.ogrTel = model.ogrTel;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Kaydı Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/ogrenciSil/{ogrId}")]
        public SonucModel OgrenciSil(int OgrId)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == OgrId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Kaydı Bulunamadı";

                return sonuc;
            }

            if (db.Sinav.Count(s => s.sinavKatId == OgrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sınav Kaydı Olan Öğrenci Silinemez!";

                return sonuc;
            }


            db.Ogrenci.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi";

            return sonuc;
        }

        #endregion

        #region Sinav

        [HttpPost]
        [Route("api/sinavekle")]
        public SonucModel SinavEkle(SinavModel model)
        {
            if (db.Sinav.Count(s => s.sinavAdi == model.sinavAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sınav Adı Kayıtlı...Lütfen Tekrar Deneyiniz! ";

                return sonuc;
            }

            Sinav yeni = new Sinav(); yeni.sinavAdi = model.sinavAdi; db.Sinav.Add(yeni); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yeni Sınav Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/sinavduzenle")]
        public SonucModel SinavDuzenle(SinavModel model)
        {
            Sinav kayit = db.Sinav.Where(s => s.sinavId == model.sinavId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sınav Kaydı Bulunamadı!";

                return sonuc;
            }

            kayit.sinavAdi = model.sinavAdi; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sınav Kaydı Yeniden Düzenlendi.";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/sinavsil/{sinavId}")]
        public SonucModel SinavSil(int sinavId)
        {
            Sinav kayit = db.Sinav.Where(s => s.sinavId == sinavId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sınav Kaydı Bulunamadı!";

                return sonuc;
            }

            if (db.Sinav.Count(s => s.sinavKatId == sinavId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Sınav Kaydı Olan Kategori Silinemez!";

                return sonuc;
            }

            db.Sinav.Remove(kayit); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sınav Kaydı Silindi.";

            return sonuc;
        }

        #endregion

        #region Soru

        [HttpGet]
        [Route("api/Soruliste")]
        public List<SoruModel> SoruListe()
        {
            List<SoruModel> liste = db.Soru.Select(x => new SoruModel()
            {
                soruId = x.soruId,
                soruMetin = x.soruMetin,
                soruSinavId = x.soruSinavId,


            }).ToList();

            return liste;
        }

        


        [HttpGet]
        [Route("api/SoruById/{SoruId}")]
        public SoruModel SoruById(int SoruId)
        {
            SoruModel kayit = db.Soru.Where(s => s.soruId == SoruId).Select(x => new SoruModel()
            {
                soruId = x.soruId,
                soruMetin = x.soruMetin,
                soruSinavId = x.soruSinavId,

            }).SingleOrDefault();

            return kayit;
        }

        [HttpGet]
        [Route("api/secenekliste")]

        public List<SecenekModel> SecenekListe()
        {
            List<SecenekModel> liste = db.Secenek.Select(x => new SecenekModel()
            {
                secId = x.secId,
                secMetin = x.secMetin,
                secSoruId = x.secSoruId,


            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/secenekbyid/{SecId}")]
        public SecenekModel SecenekById(int SecId)
        {
            SecenekModel kayit = db.Secenek.Where(s => s.secId == SecId).Select(x => new SecenekModel()
            {
                secId = x.secId,
                secMetin = x.secMetin,
                secSoruId = x.secSoruId,

            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/Soruekle")]
        public SonucModel SoruEkle(SoruModel model)
        {
            if (db.Soru.Count(s => s.soruId == model.soruId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bir Yanlışlık Oldu.Lütfen Tekrar Deneyiniz!";

                return sonuc;
            }

            Soru yeni = new Soru(); yeni.soruId = model.soruId; db.Soru.Add(yeni); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yeni Soru Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/soruduzenle")]
        public SonucModel SoruDuzenle(SoruModel model)
        {
            Soru kayit = db.Soru.Where(s => s.soruId == model.soruId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Düzenlenemedi!";

                return sonuc;
            }

            kayit.soruId = model.soruId; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Soru Yeniden Düzenlendi.";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/sorusil/{SoruId}")]
        public SonucModel SoruSil(int SoruId)
        {
            Soru kayit = db.Soru.Where(s => s.soruId == SoruId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Silindi!";

                return sonuc;
            }

            if (db.Secenek.Count(s => s.secSoruId == SoruId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Seçenekleri Silinmeden Soru Silinemez!";

                return sonuc;
            }

            db.Soru.Remove(kayit); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Soru Silindi.";

            return sonuc;
        }

        #endregion

        #region Cevap 

        [HttpPost]
        [Route("api/cevapekle")]
        public SonucModel CevapEkle(CevapModel model)
        {
            if (db.Cevap.Count(s => s.cevapId == model.cevapId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bir Yanlışlık Oldu.Lütfen Tekrar Deneyiniz!";

                return sonuc;
            }

            Cevap yeni = new Cevap(); yeni.cevapId = model.cevapId; db.Cevap.Add(yeni); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yeni Cevap Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/cevapduzenle")]
        public SonucModel CevapDuzenle(CevapModel model)
        {
            Cevap kayit = db.Cevap.Where(s => s.cevapId == model.cevapId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Düzenlenemedi!";

                return sonuc;
            }

            kayit.cevapId = model.cevapId; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Cevap Yeniden Düzenlendi.";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/cevapsil/{CevapId}")]
        public SonucModel CevapSil(int CevapId)
        {
            Cevap kayit = db.Cevap.Where(s => s.cevapId == CevapId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Silindi!";

                return sonuc;
            }

            if (db.Secenek.Count(s => s.secId == CevapId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Seçeneği Silinmeden Diğer Seçenekler Silinemez!";

                return sonuc;
            }

            if (db.Ogrenci.Count(s => s.ogrId == CevapId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Seçeneği Silinmeden Öğrenci Kaydı Silinemez!";

                return sonuc;
            }

            db.Cevap.Remove(kayit); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Cevap Silindi.";

            return sonuc;
        }

        #endregion

        #region Secenek


        [HttpPost]
        [Route("api/secenekekle")]
        public SonucModel SecenekEkle(SecenekModel model)
        {
            if (db.Secenek.Count(s => s.secMetin == model.secMetin) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Seçenek Kayıtlı...Lütfen Tekrar Deneyiniz! ";

                return sonuc;
            }

            Secenek yeni = new Secenek(); yeni.secMetin = model.secMetin; db.Secenek.Add(yeni); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yeni Seçenek Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/secenekduzenle")]
        public SonucModel SecenekDuzenle(SecenekModel model)
        {
            Secenek kayit = db.Secenek.Where(s => s.secMetin == model.secMetin).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Düzenlenecek Seçenek Bulunamadı!";

                return sonuc;
            }

            kayit.secMetin = model.secMetin; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Seçenek Yeniden Düzenlendi.";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/seceneksil/{secId}")]
        public SonucModel SecenekSil(int secId)
        {
            Secenek kayit = db.Secenek.Where(s => s.secId == secId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Seçenek Bulunamadı!";

                return sonuc;
            }

            if (db.Soru.Count(s => s.soruId == secId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Soru Kayıtlı Olan Seçecek Silinemez!";

                return sonuc;
            }

            db.Secenek.Remove(kayit); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Seçenek Silindi.";

            return sonuc;
        }
        #endregion

    }
}
