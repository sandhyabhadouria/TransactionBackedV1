using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionSystem.Repository;
using TransactionSystem.Models;

namespace TransactionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        [HttpGet]
        [Route("GetAllTransactions")]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return Ok(transactions);
        }
        [HttpPost]
        [Route("AddTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            var response = await _transactionRepository.CreateAsync(transaction);
            return Ok(response);
        }

    }
}
