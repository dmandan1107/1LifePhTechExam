using CommonLibrary.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SKUController : ControllerBase
    {
        public ISkuService _svc;
        public SKUController(ISkuService svc)
        {
            _svc = svc;
        }
        [HttpGet("getSKU")]
        public async Task<SKUGVM> GetSKU(int SKUId)
        {
            try
            {
                var res = await _svc.GetSKU(SKUId);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }

        [HttpGet("getAllSKU")]
        public async Task<List<SKUGVM>> GetAllSKU()
        {            
            var res = await _svc.GetAllSKU();
            return res;
        }

        [HttpPost("createSKU")]
        public async Task<IActionResult> CreateSKU([FromForm] SKUGVM data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var errors = new Dictionary<string, string>();

                var val1 = await _svc.ValidateField(data.SkuCode, "SKU Code", "skuCode");
                if (val1.FirstOrDefault().Key != null)
                    errors.Add(val1.FirstOrDefault().Key, val1.FirstOrDefault().Value);

                val1 = await _svc.ValidateField(data.SkuName, "SKU Name", "skuName");
                if (val1.FirstOrDefault().Key != null)
                    errors.Add(val1.FirstOrDefault().Key, val1.FirstOrDefault().Value);

                if (errors.Any())
                {
                    return BadRequest(new { errors });
                }

                var res = await _svc.CreateSKU(data);

                if (!(data.ImageFile == null || data.ImageFile.Length == 0))
                {
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/uploads/sku{res.SkuID.ToString("000#")}");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    string filePath = Path.Combine(uploadPath, data.ImageFile.FileName);

                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.ImageFile.CopyToAsync(stream);
                    }
                    res.ImagePath = data.ImagePath;
                    res.ImageFile = data.ImageFile;
                    await _svc.UpdateSKUImage(res);
                }               

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }

        }

        [HttpPut("updateSKU")]
        public async Task<IActionResult> UpdateSKU([FromForm] SKUGVM data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var errors = new Dictionary<string, string>();

                var val1 = await _svc.ValidateField(data.SkuCode, "SKU Code", "skuCode", data.SkuID);
                if (val1.FirstOrDefault().Key != null)
                    errors.Add(val1.FirstOrDefault().Key, val1.FirstOrDefault().Value);

                val1 = await _svc.ValidateField(data.SkuName, "SKU Name", "skuName", data.SkuID);
                if (val1.FirstOrDefault().Key != null)
                    errors.Add(val1.FirstOrDefault().Key, val1.FirstOrDefault().Value);

                if (errors.Any())
                {
                    return BadRequest(new { errors });
                }

                var res = await _svc.UpdateSKU(data);

                if (!(data.ImageFile == null || data.ImageFile.Length == 0))
                {
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/uploads/sku{res.SkuID.ToString("000#")}");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    string filePath = Path.Combine(uploadPath, data.ImageFile.FileName);

                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.ImageFile.CopyToAsync(stream);
                    }
                    res.ImagePath = data.ImagePath;
                    res.ImageFile = data.ImageFile;
                    await _svc.UpdateSKUImage(res);
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }


        }

        [HttpDelete("deleteSKU")]
        public async Task<SKUGVM> DeleteSKU(int SKUId)
        {
            try
            {
                return await _svc.DeleteSKU(SKUId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }
            
        }
    }
}
