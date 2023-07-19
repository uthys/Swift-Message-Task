using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Contexts;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwiftController : ControllerBase
    {
        private readonly ThisDBContext _dbContext;

        public SwiftController(ThisDBContext dbContext){
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetSwiftMessages()
        {
            var messages = _dbContext.ThisModels
                .Include(m => m.Field4)
                .Include(m => m.Field5) 
                .ToList();

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSwiftMessage(IFormFile file)
        {
            if (file == null || file.Length == 0){
                return BadRequest("Invalid data");
            }

            using var reader = new StreamReader(file.OpenReadStream());
            string swiftMessage = await reader.ReadToEndAsync();
            swiftMessage = swiftMessage.Replace(" ", "").Replace("\n", "").Replace("\r", "");

            Formating formating = new Formating();
            SwiftMessage message = formating.FormatToSwiftMessage(swiftMessage);

            if (message != null){
                _dbContext.ThisModels.Add(message);

                if (message.Field4 != null){
                    _dbContext.ContentModel.Add(message.Field4);
                }
                if (message.Field5 != null){
                    _dbContext.ChecksumModel.Add(message.Field5);
                }
                _dbContext.SaveChanges();
            }


            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll(){
            try{
                _dbContext.ContentModel.RemoveRange(_dbContext.ContentModel);
                _dbContext.ChecksumModel.RemoveRange(_dbContext.ChecksumModel);
                _dbContext.ThisModels.RemoveRange(_dbContext.ThisModels);

                await _dbContext.SaveChangesAsync();
                string resetSequenceSql = @"
                    DELETE FROM sqlite_sequence WHERE name = 'SwiftMessage';
                    DELETE FROM sqlite_sequence WHERE name = 'SwiftMessageContent';
                    DELETE FROM sqlite_sequence WHERE name = 'SwiftMessageChecksum';";

                _dbContext.Database.ExecuteSqlRaw(resetSequenceSql);

                return Ok("All items deleted successfully.");
            }
            catch (Exception ex){
                return StatusCode(500, $"An error occurred while deleting items: {ex.Message}");
            }
        }
    }
}