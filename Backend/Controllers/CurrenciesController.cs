using Microsoft.AspNetCore.Mvc;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;
using ExMoney.Backend.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class CurrenciesController: ControllerBase
{
    private readonly BackendDbContext db;
    private readonly IMapper mapper;

    public CurrenciesController(BackendDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Currency>> Get(int id)
    {
        var currency = await db.Currencies.FindAsync(id);
        if(currency is null)
            return NotFound();
        return currency;
    }

    [HttpGet("list")]
    public Task<List<Currency>> List()
    {
        return db.Currencies.ToListAsync();
    }

    [HttpPost("add")]
    public async Task<ActionResult<Currency>> Add(CurrencyCreateDTO data)
    {
        var currency = mapper.Map<Currency>(data);

        try
        {
            await db.Currencies.AddAsync(currency);
        }
        catch (System.Exception)
        {
            return new ObjectResult(new ProblemDetails{
                Status = 500,
                Title = "Unknowerror"
            });
        }

        return Created(nameof(Add), currency);      
    }


    [HttpPut("update/{id:int}")]
    public async Task<ActionResult<Currency>> Add(int id, CurrencyCreateDTO data)
    {
        // var currency = mapper.Map<Currency>(data);
        var currency = await db.Currencies.FindAsync(id);
        if(currency is null)
            return NotFound();
        
        currency = mapper.Map(data, currency);
        
        try
        {
            db.Currencies.Update(currency);
        }
        catch (Exception){
            return new ObjectResult(new ProblemDetails{
                Status = 500,
                Title = "Unknowerror"
            });   
        }
        
        return Accepted(currency);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var currency = await db.Currencies.FindAsync(id);
        if(currency is null)
            return NotFound();
        
        db.Remove(currency);

        return NoContent(); 
    }

}