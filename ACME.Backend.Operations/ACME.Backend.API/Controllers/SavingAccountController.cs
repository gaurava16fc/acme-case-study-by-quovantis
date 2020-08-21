using System;
using ACME.Backend.Core.DTO;
using ACME.Backend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace ACME.Backend.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/customers/{customerId}/[controller]")]
    [ApiController]
    public class SavingAccountController : ControllerBase
    {
        private readonly ISavingAccountRepository _repo;
        //private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<SavingAccountController> _logger;

        public SavingAccountController
            (
                IRepositoryWrapper repositoryWrapper,
                IMapper mapper,
                ILogger<SavingAccountController> logger
            )
        {
            this._repo = repositoryWrapper.SavingAccountRepository;
            //this._customerRepo = repositoryWrapper.CustomerRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        /* 
         * /api/customers/2/SavingAccount/ACMEIN9111000002/GetCustomerAccountDetail
         * */
        [HttpGet("{acctNum}")]
        public async Task<IActionResult> GetCustomerAccountDetail(int customerId, string acctNum)
        {
            var _customer = await _repo.FindByCondition(e => e.CustomerId == customerId).FirstOrDefaultAsync();
            if (_customer == null)
                return NotFound(@"Customer with Id = {" + Convert.ToString(customerId) + "} is not found");

            if (string.IsNullOrEmpty(acctNum))
                return BadRequest("Account Number can not be empty");

            var _custSavingAccountDetail = await _repo.GetCustomerAccountDetail(customerId, acctNum).FirstOrDefaultAsync();
            if (_custSavingAccountDetail == null)
            {
                return BadRequest($"Customer with Id = { Convert.ToString(customerId) } is not having Account# { acctNum }");
            }

            var _savingAccountToReturn = _mapper.Map<SavingAccountToReturnDTO>(_custSavingAccountDetail);
            return Ok(_savingAccountToReturn);
        }



        [HttpPost("{acctNum}")]
        public async Task<IActionResult> HandleTransactionOverCustomerAccount
            (
                int customerId,
                [FromBody] BankTransactionRequestDTO bankTransactionRequestDTO
            )
        {
            var _customer = await _repo.FindByCondition(e => e.CustomerId == customerId).FirstOrDefaultAsync();
            if (_customer == null)
                return BadRequest(@"Customer with Id = {" + Convert.ToString(customerId) + "} is invalid");

            if (customerId != bankTransactionRequestDTO.CustomerId)
                return BadRequest(@"Customer mismatch found, hence this transaction is aborted.");

            if (string.IsNullOrEmpty(bankTransactionRequestDTO.AccountNumber))
                return BadRequest("Account Number can not be empty");

            var _custSavingAccountDetail = await _repo.GetCustomerAccountDetail
                                                        (
                                                            customerId,
                                                            bankTransactionRequestDTO.AccountNumber
                                                        )
                                                        .FirstOrDefaultAsync();
            if (_custSavingAccountDetail == null)
                return BadRequest($"Customer with Id = { Convert.ToString(customerId) } is not having Account# { bankTransactionRequestDTO.AccountNumber }");

            var _totalBalancePriorThisTransaction = _custSavingAccountDetail.TotalBalance;
            string transTypeLabel = bankTransactionRequestDTO.TransactionType.ToString();

            if (bankTransactionRequestDTO.TransactionType == Core.Entities.Enums.AppEnums.TransactionType.Deposit)
            {
                var _totalBalanceAfterThisDeposit = _repo.DepositFund
                                                        (
                                                            customerId,
                                                            bankTransactionRequestDTO.AccountNumber,
                                                            bankTransactionRequestDTO.TransactionAmount
                                                        );

                if ((_totalBalanceAfterThisDeposit ?? 0) <= _totalBalancePriorThisTransaction)
                    return BadRequest("Funds Deposit: Invalid Transaction");

                _custSavingAccountDetail.TotalBalance = _totalBalanceAfterThisDeposit ?? _custSavingAccountDetail.TotalBalance;
                _custSavingAccountDetail.ModifiedOn = DateTime.Now;
                await _repo.Update(_custSavingAccountDetail);
                return Ok();
            }
            else if (bankTransactionRequestDTO.TransactionType == Core.Entities.Enums.AppEnums.TransactionType.Withdrawl)
            {
                if (_custSavingAccountDetail.IsAccountLocked)
                    return BadRequest("Funds Withdrawl: Transaction abort, as account is locked.");

                var _totalBalanceAfterThisWithdrawl = _repo.WithdrawFund
                                                        (
                                                            customerId,
                                                            bankTransactionRequestDTO.AccountNumber,
                                                            bankTransactionRequestDTO.TransactionAmount
                                                        );

                if ((_totalBalanceAfterThisWithdrawl ?? 0) >= _totalBalancePriorThisTransaction)
                    return BadRequest("Funds Withdrawl: Invalid Transaction");

                _custSavingAccountDetail.TotalBalance = _totalBalanceAfterThisWithdrawl ?? _custSavingAccountDetail.TotalBalance;
                _custSavingAccountDetail.ModifiedOn = DateTime.Now;
                await _repo.Update(_custSavingAccountDetail);
                return Ok();
            }

            return BadRequest($"Some issue with {transTypeLabel} transaction, please try again...");
        }



        [HttpPut("{acctNum}/lock")]
        public async Task<IActionResult> LockSavingAccount
            (
                int customerId,
                string acctNum
            )
        {
            var _customer = await _repo.FindByCondition(e => e.CustomerId == customerId).FirstOrDefaultAsync();
            if (_customer == null)
                return NotFound(@"Customer with Id = {" + Convert.ToString(customerId) + "} is not found");

            if (string.IsNullOrEmpty(acctNum))
                return BadRequest("Account Number can not be empty");

            var _custSavingAccountDetail = await _repo.GetCustomerAccountDetail(customerId, acctNum).FirstOrDefaultAsync();
            if (_custSavingAccountDetail == null)
                return BadRequest($"Customer with Id = { Convert.ToString(customerId) } is not having Account# { acctNum }");

            var _isAccountLocked = _custSavingAccountDetail.IsAccountLocked;
            if (_isAccountLocked)
                return BadRequest($"This account no# { acctNum } is already locked.");

            _custSavingAccountDetail.IsAccountLocked = true;
            _custSavingAccountDetail.ModifiedOn = DateTime.Now; ;

            await _repo.Update(_custSavingAccountDetail);
            return Ok();
        }


        [HttpPut("{acctNum}/unlock")]
        public async Task<IActionResult> UnlockSavingAccount
            (
                int customerId,
                string acctNum
            )
        {
            var _customer = await _repo.FindByCondition(e => e.CustomerId == customerId).FirstOrDefaultAsync();
            if (_customer == null)
                return NotFound(@"Customer with Id = {" + Convert.ToString(customerId) + "} is invalid");

            if (string.IsNullOrEmpty(acctNum))
                return BadRequest("Account Number can not be empty");

            var _custSavingAccountDetail = await _repo.GetCustomerAccountDetail(customerId, acctNum).FirstOrDefaultAsync();
            if (_custSavingAccountDetail == null)
                return BadRequest($"Customer with Id = { Convert.ToString(customerId) } is not having Account# { acctNum }");

            var _isAccountLocked = _custSavingAccountDetail.IsAccountLocked;
            if (!_isAccountLocked)
                return BadRequest($"This account no# { acctNum } is already in unlocked state.");

            _custSavingAccountDetail.IsAccountLocked = false;
            _custSavingAccountDetail.ModifiedOn = DateTime.Now;

            await _repo.Update(_custSavingAccountDetail);
            return Ok();
        }
    }
}
