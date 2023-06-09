using AutoMapper;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class WalletsController : ControllerBase
    {
        private readonly BackendDbContext db;
        private readonly IMapper mapper;

        public WalletsController(BackendDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet>> GetById(string id)
        {
            var wallet = await db.Wallets.FindAsync(id);
            return wallet is null ? (ActionResult<Wallet>)NotFound() : (ActionResult<Wallet>)wallet;
        }

        [HttpGet("user-wallets")]
        public ActionResult<IEnumerable<Wallet>> GetUserWallets([FromQuery] string userId)
        {
            return db.Wallets.Where(w => w.OwnerId == userId).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Wallet>> List()
        {
            return db.Wallets.ToList();
        }

        // [HttpPost]
        // public async Task<ActionResult<Wallet>> Create(WalletCreateDTO data)
        // {
        //     Currency currency = await db.Currencies.FindAsync(data.CurrencyId);
        //     if (currency is null)
        //         return BadRequest(new { Error = "" });

        //     Wallet existingWallet = db.Wallets.FirstOrDefault(w => w.CurrencyId == currency.Id);
        //     if (existingWallet is not null)
        //     {
        //         return BadRequest(new { Error = "Un portefeuille avec la devise spécifiée existe déja." });
        //     }

        //     Wallet wallet = mapper.Map<Wallet>(data);

        //     Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Wallet> addResult = await db.Wallets.AddAsync(wallet);
        //     _ = await db.SaveChangesAsync();

        //     return wallet;
        // }
    }
}
