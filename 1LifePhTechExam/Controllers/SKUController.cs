using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using CommonLibrary.Models;

namespace _1LifePhTechExam.Controllers
{
    public class SKUController : Controller
    {
        private readonly string _connectionString;

        public SKUController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultDataContext");
        }

        public IActionResult Index()
        {
            return View();
        }

        #region oldcodes
        //public async Task<SKUGVM> GetSKU(int SKUId)
        //{
        //    try
        //    {
        //        using var db = new SqlConnection(_connectionString);
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@SKUID", SKUId);
        //        var res = await db.QueryFirstOrDefaultAsync<SKUGVM>("GetSKU", parameters, commandType: CommandType.StoredProcedure);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw new Exception("Failed to fetch the data");
        //    }
        //}

        //public async Task<List<SKUGVM>> GetAllSKU()
        //{
        //    using var db = new SqlConnection(_connectionString);
        //    var res = (await db.QueryAsync<SKUGVM>("GetAllSKU", null, commandType: CommandType.StoredProcedure)).ToList();
        //    return res;
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateSKU([FromForm] SKUGVM data)
        //{
        //    try
        //    {
        //        using var db = new SqlConnection(_connectionString);

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var errors = new Dictionary<string, string>();

        //        //async Task ValidateField(string input, string valType, string fieldName)
        //        //{
        //        //    var param = new DynamicParameters();
        //        //    param.Add("@Input", input);
        //        //    param.Add("@ValType", valType);
        //        //    var exists = await db.QueryFirstOrDefaultAsync<bool>("ValidateInput", param, commandType: CommandType.StoredProcedure);
        //        //    if (exists)
        //        //    {
        //        //        errors[fieldName] = $"{valType} already exists.";
        //        //    }
        //        //}

        //        //await ValidateField(data.FirstName, "First Name", "firstName");
        //        //await ValidateField(data.LastName, "Last Name", "lastName");
        //        //await ValidateField(data.MobileNumber, "Mobile Number", "mobileNumber");

        //        if (errors.Any())
        //        {
        //            return BadRequest(new { errors });
        //        }

        //        var parameters = new DynamicParameters();
        //        parameters.Add("@SkuName", data.SkuName);
        //        parameters.Add("@SkuCode", data.SkuCode);
        //        parameters.Add("@UnitPrice", data.UnitPrice);
        //        parameters.Add("@IsActive", data.IsActive);
        //        var res = await db.QueryFirstOrDefaultAsync<SKUGVM>("CreateSKU", parameters, commandType: CommandType.StoredProcedure);

        //        if (data.ImageFile == null || data.ImageFile.Length == 0)
        //        {
        //            return BadRequest("Image file is required.");
        //        }

        //        // Example: Save the image and SKU data
        //        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/uploads/sku{res.SkuID.ToString("000#")}");
        //        if (!Directory.Exists(uploadPath))
        //            Directory.CreateDirectory(uploadPath);

        //        string filePath = Path.Combine(uploadPath, data.ImageFile.FileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await data.ImageFile.CopyToAsync(stream);
        //        }

        //        await db.ExecuteAsync($"UPDATE SKU SET ImagePath = '/uploads/sku{res.SkuID.ToString("000#")}/{data.ImageFile.FileName}' WHERE SKUID = {res.SkuID}",null, commandType: CommandType.Text);

        //        return Ok(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw new Exception("Failed to save the data");
        //    }

        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateSKU([FromBody] SKUGVM data)
        //{
        //    try
        //    {
        //        using var db = new SqlConnection(_connectionString);

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var errors = new Dictionary<string, string>();

        //        //async Task ValidateField(string input, string valType, string fieldName)
        //        //{
        //        //    var param = new DynamicParameters();
        //        //    param.Add("@Input", input);
        //        //    param.Add("@ValType", valType);
        //        //    param.Add("@SKUID", data.SKUId);
        //        //    var exists = await db.QueryFirstOrDefaultAsync<bool>("ValidateInput", param, commandType: CommandType.StoredProcedure);
        //        //    if (exists)
        //        //    {
        //        //        errors[fieldName] = $"{valType} already exists.";
        //        //    }
        //        //}

        //        //await ValidateField(data.FirstName, "First Name", "firstName");
        //        //await ValidateField(data.LastName, "Last Name", "lastName");
        //        //await ValidateField(data.MobileNumber, "Mobile Number", "mobileNumber");

        //        if (errors.Any())
        //        {
        //            return BadRequest(new { errors });
        //        }

        //        var parameters = new DynamicParameters();
        //        parameters.Add("@SkuID", data.SkuID);
        //        parameters.Add("@SkuName", data.SkuName);
        //        parameters.Add("@SkuCode", data.SkuCode);
        //        parameters.Add("@UnitPrice", data.UnitPrice);
        //        parameters.Add("@IsActive", data.IsActive);
        //        var res = await db.QueryFirstOrDefaultAsync<SKUGVM>("UpdateSKU", parameters, commandType: CommandType.StoredProcedure);

        //        return Ok(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw new Exception("Failed to save the data");
        //    }


        //}

        //[HttpDelete]
        //public async Task<SKUGVM> DeleteSKU(int SKUId)
        //{
        //    using var db = new SqlConnection(_connectionString);
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@SKUId", SKUId);
        //    return await db.QueryFirstOrDefaultAsync<SKUGVM>("", parameters, commandType: CommandType.StoredProcedure);
        //}
        #endregion
    }
}
