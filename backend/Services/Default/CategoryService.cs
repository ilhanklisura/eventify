namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Mapping;
using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Request.Category;
using Eventify.Backend.Models.Response.Category;
using Eventify.Backend.Services.Result;
using Microsoft.EntityFrameworkCore;

public class CategoryService : Service, ICategoryService
{
    private readonly DataContext _db;
    private readonly IMapper<Category, CategoryModel> _mapper;

    public CategoryService(IServiceProvider sp, DataContext db, IMapper<Category, CategoryModel> mapper) : base(sp)
    {
        _db = db;
        _mapper = mapper;
    }

    public ServiceResult<CategoryModel> GetById(int id)
    {
        var e = _db.Categories.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return e == null ? NotFound() : Ok(_mapper.Map(e));
    }

    public ServiceResult<List<CategoryModel>> GetAll()
    {
        var list = _db.Categories.AsNoTracking().OrderBy(x => x.Name).ToList().Select(_mapper.Map).ToList();
        return Ok(list);
    }

    public ServiceResult<CategoryModel> Create(CreateCategoryRequestModel model)
    {
        if (_db.Categories.Any(x => x.Name == model.Name))
            return ExistingEntity("Name");
        var e = new Category { Name = model.Name };
        _db.Categories.Add(e);
        _db.SaveChanges();
        return Ok(_mapper.Map(e));
    }

    public ServiceResult<CategoryModel> Update(UpdateCategoryRequestModel model)
    {
        var e = _db.Categories.Find(model.Id);
        if (e == null) return NotFound();
        e.Name = model.Name;
        _db.SaveChanges();
        return Ok(_mapper.Map(e));
    }

    public ServiceResult Delete(int id)
    {
        var e = _db.Categories.Find(id);
        if (e == null) return NotFound();
        _db.Categories.Remove(e);
        _db.SaveChanges();
        return Ok();
    }

}
