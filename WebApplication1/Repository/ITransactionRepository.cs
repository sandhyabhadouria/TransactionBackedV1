using TransactionSystem.Models;

namespace TransactionSystem.Repository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>>GetAllAsync();
        Task<ApiResponse> CreateAsync(Transaction transaction);

    }
}
