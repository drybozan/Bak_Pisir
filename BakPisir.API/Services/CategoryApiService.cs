using AutoMapper.QueryableExtensions;
using BakPisir.CORE.Helper;
using BakPisir.CORE.Result;
using BakPisir.CORE.UnitOfWork;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;
using Newtonsoft.Json;
using System.Linq;

public class CategoryApiService
{
    // Önce Database Context dosyamın nesnesini oluşturuyorum.
    private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

    // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
    private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);

    public string GetAllCategory()
    {
        CategoryDto categoryDto = new CategoryDto();    
        //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
        var categories = efUnitOfWork.CategoryTemplate.GetAll(i=>i.isDelete==false).ProjectTo<CategoryDto>().ToList();
      
        return JsonConvert.SerializeObject(categories);
    }

    //Verilen id değerine sahip kategoriyi veritabanında bulur ve döndürür.
    public string GetCategoryById(int id)
    {
        var categories = efUnitOfWork.CategoryTemplate.GetById(id).MapTo<CategoryDto>();
        return JsonConvert.SerializeObject(categories);
    }

    //Verilen id değerine sahip kategori veritabanından siler.
    public Result DeleteCategory(int id)
    {
        var result = Result.Instance.Warning("HATA! Böyle bir katagori yok.");
        var category = efUnitOfWork.CategoryTemplate.GetById(id);
        if (category != null)
        {
            efUnitOfWork.CategoryTemplate.Delete(category);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Kategori başarıyla silindi.");
            return result;
        }

        return result;
    }

    //Yeni kategori ekler.
    public Result AddCategory(CategoryDto categoryDto)
    {
        var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

        if (categoryDto != null) //girilen bilgilerin kontrolü yapılır.
        {
            var mappedCategory = categoryDto.MapTo<CategoryTBL>();
            efUnitOfWork.CategoryTemplate.Add(mappedCategory);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Kayıt talebiniz başarıyla alındı.");

            return result;
        }
        return result;
    }

    //Verilen id değerine sahip kategoriyi günceller.
    public Result UpdateCategory(int id, CategoryDto categoryDto)
    {
        var result = Result.Instance.Warning("HATA! Güncelleme istediğiniz kategori bulunamadı.");

        // istenilen id mevcutsa güncellenecek data categorye atanır.
        var category = efUnitOfWork.CategoryTemplate.GetById(id).MapTo<CategoryDto>();
        if (category != null)
        {
           
            var mapped = categoryDto.MapTo<CategoryTBL>();
            mapped.categoryId = category.categoryId;
            efUnitOfWork.CategoryTemplate.Update(mapped);
            efUnitOfWork.SaveChanges();

            result = Result.Instance.Success("Kategori başarıyla güncellendi.");
            return result;
        }
        return result;
    }

}