﻿using AutoMapper.QueryableExtensions;
using BakPisir.CORE.Helper;
using BakPisir.CORE.Paging;
using BakPisir.CORE.Result;
using BakPisir.CORE.UnitOfWork;
using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using BakPisir.EF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BakPisir.API.Services
{
    public class RequestApiService
    {
        // Önce Database Context dosyamın nesnesini oluşturuyorum.
        private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

         // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
        private  EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);



        public string GetAllRequest()
        {

            //projecTo, autommaper aracı. DB varlığımı dto ya mapler.

            var requests = efUnitOfWork.RequestTemplate.GetAll(i => i.isDelete == false)
                .OrderBy(o => o.requestId) // gelen datayı id ye göre sırala
                .ProjectTo<RequestDto>()
                .ToList();
            return JsonConvert.SerializeObject(requests);
        }


        //Verilen id değerine sahip başvuru verisini veritabanında bulur ve döndürür.
        public  string GetRequestById(int id)
        {
            var request = efUnitOfWork.RequestTemplate.GetById(id).MapTo<RequestDto>();
            return JsonConvert.SerializeObject(request);
        }

        //Verilen id değerine sahip başvuru verisini veritabanından siler.
        public  Result DeleteRequest(int id)
        {
            var result = Result.Instance.Warning("HATA! Böyle bir talep kaydı yok.");
            var request = efUnitOfWork.RequestTemplate.GetById(id);
            if (request != null)
            {
                efUnitOfWork.RequestTemplate.Delete(request);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Başarıyla silindi.");
                return result;
            }

            return result;
        }

        //Yeni başvuru ekler.
        public  Result AddRequest(RequestDto requestDto)
        {
            var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

            if (requestDto != null) //girilen bilgilerin kontrolü yapılır.
            {
                //başvuru yapan kişinin tc si doğrulamaya gönderilir 
                bool verificationIdentityNumber = VerificationIdentityNumber(requestDto);
                var mappedRequest = requestDto.MapTo<RequestTBL>();
                if (verificationIdentityNumber == true) {
                    // kullanıcı sözleşmeyi imzalamış mı ? evet ise tüm bilgiler kaydedilir.
                    if (requestDto.agreement == true)
                    {
                        mappedRequest.requestDate = DateTime.Now;
                        efUnitOfWork.RequestTemplate.Add(mappedRequest);
                        efUnitOfWork.SaveChanges();

                        result = Result.Instance.Success("Kayıt talebiniz başarıyla alındı.");
                        return result;
                    }
                    else
                    {
                        result = Result.Instance.Info("Kullanıcı sözleşmesini imzalayınız.");
                        return result;
                    }
                }
                return result;


            }
            return result;
        }

        //Verilen id değerine sahip başvuru verisini günceller.
        public  Result UpdateRequest(int id, RequestDto requestDto)
        {
            var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı."); 

            // istenilen id mevcutsa güncellenecek data requeste atanır.
            var request = efUnitOfWork.RequestTemplate.GetById(id).MapTo<RequestDto>();
            if (request != null)
            {
                var mapped = requestDto.MapTo<RequestTBL>();
                mapped.requestId = request.requestId;

                efUnitOfWork.RequestTemplate.Update(mapped);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Kayıt talebi başarıyla güncellendi.");
                return result;
            }
            return result;
        }

        public  bool VerificationIdentityNumber(RequestDto requestDto)
        {
            bool dogrulamaSonucu = false;

            try
            {
                MernisService.KPSPublicSoapClient mernisClient = new MernisService.KPSPublicSoapClient();

                var tcKimlikDogrulamaServisResponse = mernisClient.TCKimlikNoDogrulaAsync(long.Parse(requestDto.identityNumber.ToString()), requestDto.name, requestDto.surname, requestDto.birthdate.Year).GetAwaiter().GetResult();
                dogrulamaSonucu = tcKimlikDogrulamaServisResponse.Body.TCKimlikNoDogrulaResult;
               
            }
            catch (Exception ex)
            {
                dogrulamaSonucu = false;
            }

            return dogrulamaSonucu;
        }
        //Verilen mail adresine random oluşturulan şifreyi yollar.
        public Result SendMailPassword(string mail)
        {
            var result = Result.Instance.Warning("HATA! Kayıtlı email adresi bulunamadı.");

            var email = efUnitOfWork.RequestTemplate.Get(x => x.mail == mail && x.isDelete == false).MapTo<RequestDto>();
            if (email != null)
            {
                Guid randomkey = Guid.NewGuid(); //32 karakterli kodu ürettik
                string Password = randomkey.ToString().Substring(0, 6); /// 6 haneli keyi password için aldık"
                Mail sender = new Mail();
                sender.sendMail(mail.ToString(), "Şifre Bildirimi", "Sitemize üye olduğunuz için teşekkürler. Size verdiğimiz şifre ile giriş yapabilirsiniz.\n Şifre : " + Password);
                email.password = Password;
               
                //oluşturulan başvuruyu günceller.
                UpdateRequest(email.requestId, email);

                result = Result.Instance.Success("Başvuru onay maili gonderildi.");
                return result;
            }
            else
            {
                return result;
            }
        }

        //Verilen mail adresine başvurunun reddini bildirir.
        public Result SendMailRejection(string mail)
        {
            var result = Result.Instance.Warning("HATA! Kayıtlı email adresi bulunamadı.");

            var email = efUnitOfWork.RequestTemplate.Get(x => x.mail == mail && x.isDelete == false).MapTo<RequestDto>();
            if (email != null)
            {
                Mail sender = new Mail();
                sender.sendMail(mail.ToString(), "Üyelik Bildirimi", "Sitemize üye olma talebiniz reddedilmiştir. Sağlıklı günler dileriz.");

                result = Result.Instance.Success("Bilgilendirme maili yollandı.");
                return result;
            }
            else
            {
                return result;
            }
        }

        //Verilen mail adresine başvurunun alındığını mail atar.
        public Result SendMailInfo(string mail)
        {
            var result = Result.Instance.Success("HATA! Kayıtlı email adresi bulunamadı.");
            var email = efUnitOfWork.RequestTemplate.Get(x => x.mail == mail).MapTo<RequestDto>();
            var user = efUnitOfWork.UserTemplate.Get(x => x.mail == mail).MapTo<UserDto>();
            if (user != null)
            {
              result= Result.Instance.Warning("HATA! Bu mail adresine kayıtlı üye mevcuttur.");
              return result;
            }
            else if (email != null)
            {
                Mail sender = new Mail();
                sender.sendMail(mail.ToString(), "Üyelik Bildirimi", "Sitemize üye olma talebiniz alınmıştır. Başvurunuz en kısa sürede değerlendirilecektir ve size geri dönüş sağlanacaktır.Sağlıklı günler dileriz.");
                return result = Result.Instance.Warning("Başarılı! Bilgilendirme mesajı gönderildi.");

            }
            return result;
           
        }



    }
}

