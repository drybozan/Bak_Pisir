using AutoMapper.QueryableExtensions;
using BakPisir.CORE.Helper;
using BakPisir.CORE.Result;
using BakPisir.CORE.UnitOfWork;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

public class CommentApiService
{
    // Önce Database Context dosyamın nesnesini oluşturuyorum.
    private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

    // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
    private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);

    public string GetAllComment()
    {
        //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
        var comments = efUnitOfWork.CommentTemplate.GetAll(i => i.isDelete == false).ProjectTo<CommentDto>().ToList();
        return JsonConvert.SerializeObject(comments);
    }

    //Verilen id değerine sahip yorum nesnesini veritabanında bulur ve döndürür.
    public string GetCommentById(int id)
    {
        var comment = efUnitOfWork.CommentTemplate.GetById(id).MapTo<CommentDto>();
        return JsonConvert.SerializeObject(comment);
    }

    //Verilen id değerine sahip yorum nesnesini veritabanından siler.
    public Result DeleteComment(int id)
    {
        var result = Result.Instance.Warning("HATA! Böyle bir yorum yok.");
        var comment = efUnitOfWork.CommentTemplate.GetById(id);
        if (comment != null)
        {
            efUnitOfWork.CommentTemplate.Delete(comment);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Yorum başarıyla silindi.");
            return result;
        }

        return result;
    }

    //Yeni yorum ekler.
    public Result AddComment(CommentDto commentDto)
    {
        var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

        if (commentDto != null) //girilen bilgilerin kontrolü yapılır.
        {
            var mappedComment = commentDto.MapTo<CommentTBL>();           
            mappedComment.commentDate = DateTime.Now;
            efUnitOfWork.CommentTemplate.Add(mappedComment);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Yorumunuz başarıyla eklendi.");

            return result;
        }
        return result;
    }

    //Verilen id değerine sahip yorum nesnesini günceller.
    public Result UpdateComment(int id, CommentDto commentDto)
    {
        var result = Result.Instance.Warning("HATA! Güncelleme istediğiniz yorum bulunamadı.");

        // istenilen id mevcutsa güncellenecek data commente atanır.
        var comment = efUnitOfWork.CommentTemplate.GetById(id).MapTo<CommentDto>();
        if (comment != null)
        {
            var mapped = commentDto.MapTo<CommentTBL>();
            mapped.commentId = comment.commentId;

            efUnitOfWork.CommentTemplate.Update(mapped);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Yorum başarıyla güncellendi.");
            return result;
        }
        return result;
    }


    public Result UploadCommentPicture(int id, string filePath)
    {
        var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı.");

        // gelen id yi sorgula db de sorgula. 
        var comment = efUnitOfWork.CommentTemplate.GetById(id).MapTo<CommentDto>();

        if (comment != null)
        {
            //yoruma gönderilen dosyanın yolunu gönder.
            comment.commentImageUrl = filePath;

            //veritabanı nesnesi ile maple.
            var mappedComment = comment.MapTo<CommentTBL>();

            // yapılan değişikliği güncelle
            efUnitOfWork.CommentTemplate.Update(mappedComment);

            // değişiklikleri kaydet ve veritabanına yansıt
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Yoruma fotoğraf başarıyla eklendi.");
            return result;
        }
        return result;

    }

}