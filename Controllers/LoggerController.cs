using LoggerApi.Services;
using Microsoft.AspNetCore.Mvc;
using LoggerApi.Models;

namespace LoggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        public LoggerController(ILoggerService loggerService) => _loggerService = loggerService;

        [HttpGet("get-custom-fields-by-id/{id:length(24)}")]
        public async Task<IActionResult> GetCustomFieldsById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("id cannot be null or empty");
            }

            var customFields = await _loggerService.GetCustomFieldsAsync(id);

            return Ok(customFields);
        }

        [HttpGet("get-custom-fields-id-by-app-name/{appName}/{environment}")]
        public async Task<IActionResult> GetCustomFieldsIdByAppName(string appName, string environment)
        {
            if (string.IsNullOrEmpty(appName) || string.IsNullOrEmpty(environment))
            {
                return BadRequest("appName and environment cannot be null or empty, each must have a value");
            }

            var id = await _loggerService.GetCustomFieldsIdByAppName(appName, environment);

            if (id is null) { return NotFound(); }

            return Ok(id);
        }

        [HttpGet]
        [Route("get-logs/{appName}/{environment}/{startDate:datetime}/{endDate:datetime}")]
        public async Task<IActionResult> GetLogs(string appName, string environment, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (string.IsNullOrEmpty(appName) && string.IsNullOrEmpty(environment))
            {
                return BadRequest("appName and environment cannot be null or empty, each must have a value");
            }

            var logs = await _loggerService.GetLogsAsync(appName, environment, startDate, endDate);

            return Ok(logs);
        }

        [HttpPost("add-new-custom-fields")]
        public async Task<IActionResult> AddNewCustomFields(CustomFields newCustomFields)
        {
            var isCustomFieldsNew = _loggerService.GetCustomFieldsIdByAppName(newCustomFields.AppName, newCustomFields.Environment).Result;
            if (!string.IsNullOrEmpty(isCustomFieldsNew))
            {
                return BadRequest($"Custom Fields for {newCustomFields.AppName} in {newCustomFields.Environment} already exists, use 'UpdateCustomFields' instead.");
            }

            try
            {
                await _loggerService.CreateCustomFieldsAsync(newCustomFields);

                return CreatedAtAction(nameof(GetCustomFieldsById), new { id = newCustomFields.Id }, newCustomFields);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("log")]
        public async Task<IActionResult> CreateNewLog(Log log)
        {
            try
            {
                await _loggerService.CreateLogAsync(log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return NoContent();
        }

        [HttpPut("update-custom-fields/{id:length(24)}")]
        public async Task<IActionResult> UpdateCustomFields(string id, CustomFields updatedCustomFields)
        {
            var customFields = await _loggerService.GetCustomFieldsAsync(id);

            if (customFields is null) { return NotFound(); }

            updatedCustomFields.Id = customFields.Id;
            try
            {
                await _loggerService.UpdateCustomFieldsAsync(id, updatedCustomFields);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("delete-custom-fields/{id:length(24)}")]
        public async Task<IActionResult> DeleteCustomFieldsAsync(string id)
        {
            var customFields = await _loggerService.GetCustomFieldsAsync(id);

            if (customFields is null) { return NotFound(); }

            await _loggerService.DeleteCustomFieldsAsync(id);

            return NoContent();
        }
    }
}
