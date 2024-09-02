using TransactionSystem.Repository;
using TransactionSystem.Models;
using TransactionSystem.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using TransactionSystem.Enums;

namespace TransactionSystem.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> CreateAsync(Transaction transaction)
        {
            ApiResponse response = new(); ;
            string message = string.Empty;
            int lastRemainingAmount = 0;
            // Calculate the total balance from the transaction table
            //var totalBalance = await _context.Transaction.SumAsync(t => t.Amount);
            var lastTransaction = await _context.Transaction.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            if (lastTransaction != null) {
                lastRemainingAmount = (int)(lastTransaction?.RunningBalance);

            }

            if (transaction.Type == TransactionType.Debit)
            {
                if (lastRemainingAmount >= transaction.Amount)
                {
                    transaction.RunningBalance = lastRemainingAmount-transaction.Amount;
                    await _context.Transaction.AddAsync(transaction);
                    await _context.SaveChangesAsync();
                    message = $"Your account balance is  {transaction.RunningBalance}";
                    response.Transaction = message;
                }
                else
                {
                    message = $"Your account balance is  {lastRemainingAmount}, So you can't debit {transaction.Amount}";
                    response.Transaction = message;
                }
            }
            else if (transaction.Type == TransactionType.Credit)
            {
                if (transaction.Amount > 0)
                {
                    transaction.RunningBalance = lastRemainingAmount + transaction.Amount;
                    await _context.Transaction.AddAsync(transaction);
                    await _context.SaveChangesAsync();
                    message = $"Your account balance is  {lastRemainingAmount + transaction.Amount}";
                    response.Transaction = message;
                }
                else
                {
                    message = "Please Enter Valid Amount";
                    response.Transaction = message; 
                }
            }

            else
            {
                message = "Please Select Valid TransactionType (Debit/Credit)";
                response.Transaction = message; 
            }
            return response;
        }


        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transaction.OrderByDescending(t => t.Id).ToListAsync();
        }



    }
}
