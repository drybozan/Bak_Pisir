using AutoMapper.QueryableExtensions;
using BakPisir.CORE.Helper;
using BakPisir.CORE.Paging;
using BakPisir.CORE.Result;
using BakPisir.CORE.UnitOfWork;
using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using BakPisir.EF.Models;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Linq;
using System.Web.UI;

public class RecipeApiService
{
    // Önce Database Context dosyamın nesnesini oluşturuyorum.
    private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

    // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
    private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);

    public string GetAllRecipe(int page, int pageSize)
    {
        //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
        var recipes = efUnitOfWork.RecipeTemplate.GetAll(i => i.isDelete == false)
            .OrderBy(o => o.recipeId) // gelen datayı id ye göre sırala
            .Skip((page - 1) * pageSize) //Sayfa numrası * sayfa boytuna göre belrli bir kayıt kümesini atlar.
            .Take(pageSize)  //Yalnızca sayfa boyutuna göre belirlenen gerekli miktarda veriyi alır.
            .ProjectTo<RecipeDto>()
            .ToList()
            .ToPaginate(page, pageSize);

        RecipeListModel mappedRecipeListModel = recipes.MapTo<RecipeListModel>();
        return JsonConvert.SerializeObject(mappedRecipeListModel);
    }

    //Verilen id değerine sahip tarifi veritabanında bulur ve döndürür.
    public string GetRecipeById(int id)
    {
        var recipe = efUnitOfWork.RecipeTemplate.GetById(id).MapTo<RecipeDto>();
        return JsonConvert.SerializeObject(recipe);
    }

    //Verilen id değerine sahip tarifi veritabanından siler.
    public Result DeleteRecipe(int id)
    {
        var result = Result.Instance.Warning("HATA! Böyle bir tarif kaydı yok.");
        var recipe = efUnitOfWork.RecipeTemplate.GetById(id);
        if (recipe != null)
        {
            efUnitOfWork.RecipeTemplate.Delete(recipe);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Tarif başarıyla silindi.");
            return result;
        }

        return result;
    }

    //Yeni tarif ekler.
    public Result AddRecipe(RecipeDto recipeDto)
    {
        var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

        if (recipeDto != null) //girilen bilgilerin kontrolü yapılır.
        {
            var mappedRecipe = recipeDto.MapTo<RecipeTBL>();
            efUnitOfWork.RecipeTemplate.Add(mappedRecipe);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Tarif kaydınız başarıyla alındı.");

            return result;
        }
        return result;
    }

    //Verilen id değerine sahip tarifi günceller.
    public Result UpdateRecipe(int id, RecipeDto recipeDto)
    {
        var result = Result.Instance.Warning("HATA! Güncelleme istediğiniz tarif bulunamadı.");

        // istenilen id mevcutsa güncellenecek data recipe ye atanır.
        var recipe = efUnitOfWork.RecipeTemplate.GetById(id).MapTo<RecipeDto>();
        if (recipe != null)
        {
            var mapped = recipeDto.MapTo<RecipeTBL>();
            mapped.recipeId = recipe.recipeId;

            efUnitOfWork.RecipeTemplate.Update(mapped);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Tarif başarıyla güncellendi.");
            return result;
        }
        return result;
    }

    public Result UploadRecipeVideo(int id, string filePath)
    {
        var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı.");

        // gelen id yi sorgula db de sorgula. 
        var recipe = efUnitOfWork.RecipeTemplate.GetById(id).MapTo<RecipeDto>();

        if (recipe != null)
        {
            //gönderilen dosyanın yolunu gönder.
            recipe.recipeVideoUrl = filePath;

            //veritabanı nesnesi ile maple.
            var mappedRecipe = recipe.MapTo<RecipeTBL>();

            // yapılan değişikliği güncelle
            efUnitOfWork.RecipeTemplate.Update(mappedRecipe);

            // değişiklikleri kaydet ve veritabanına yansıt
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Video başarı ile yüklendi.");
            return result;
        }
        return result;

    }

}