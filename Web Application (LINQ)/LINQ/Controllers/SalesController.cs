using Microsoft.AspNetCore.Mvc;
using DataBaseLibrary;

namespace LINQ.Controllers;

/// <summary>
/// Represents methods to work with the table of the sales.
/// </summary>
[Route("api/[controller]/")]
public class SalesController : Controller
{
    private static readonly Func<Sale> CreateSale = () => new Sale(0, 6, 3, 1);
    private static readonly Func<Sale> CreateSale1 = () => new Sale(1, 0, 0, 2);
    private static readonly Func<Sale> CreateSale2 = () => new Sale(1, 3, 4, 4);
    private static readonly Func<Sale> CreateSale3 = () => new Sale(4, 5, 6, 1);
    private static readonly Func<Sale> CreateSale4 = () => new Sale(5, 4, 5, 1);
    private static readonly Func<Sale> CreateSale5 = () => new Sale(3, 2, 2, 1);
    private static readonly Func<Sale> CreateSale6 = () => new Sale(3, 4, 5, 5);

    static SalesController()
    {
        try
        {
            DataAccessLayer.Db.CreateTable<Sale>();
            DataAccessLayer.Db.InsertInto(CreateSale);
            DataAccessLayer.Db.InsertInto(CreateSale1);
            DataAccessLayer.Db.InsertInto(CreateSale2);
            DataAccessLayer.Db.InsertInto(CreateSale3);
            DataAccessLayer.Db.InsertInto(CreateSale4);
            DataAccessLayer.Db.InsertInto(CreateSale5);
            DataAccessLayer.Db.InsertInto(CreateSale6);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    /// <summary>
    /// Serialize buyer table into the file by passed path and deserialize buyer table from the file by passed path. Use command s to serialize and d to deserialize.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    [HttpGet("{path}/{command}")]
    public IActionResult MakeSerializationAndDeserialization(string path, string command)
    {
        switch (command)
        {
            case "s":
            {
                try
                {
                    DataAccessLayer.Db.Serialize<Sale>(path);
    
                    return Ok("Serialization was successful");
                }
                catch (Exception ex)
                {
                    return Ok($"Serialization was not successful: {ex.Message}");
                }
            }
            case "d":
            {
                try
                {
                    DataAccessLayer.Db.Deserialize<Sale>(path);
    
                    return Ok("Deserialization was successful");
                }
                catch (Exception ex)
                {
                    return Ok($"Deserialization was not successful: {ex.Message}");
                }
            }
            default:
            {
                return Ok("Wrong command, come again with command s to serialize and d to deserialize");
            }
        }
    }

    /// <summary>
    /// Get the list of sales.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var sales = DataAccessLayer.Db.GetTable<Sale>();
            return Ok(sales);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    /// <summary>
    /// Get the sale by passed id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var sale = DataAccessLayer.Db.GetTable<Sale>().SingleOrDefault(s => s.Id == id);

            if (sale is null)
            {
                return Ok("No sales with such id. Please come again");
            }

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    /// <summary>
    /// Add a new sale.
    /// </summary>
    /// <param name="buyerId"></param>
    /// <param name="shopId"></param>
    /// <param name="goodId"></param>
    /// <param name="goodCount"></param>
    /// <returns></returns>
    [HttpPost("{buyerId}/{shopId}/{goodId}/{goodCount}")]
    public IActionResult Post(int buyerId, int shopId, int goodId, long goodCount)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sale = new Sale(buyerId, shopId, goodId, goodCount);
            DataAccessLayer.Db.InsertInto(() => sale);
            return CreatedAtAction(nameof(Get), sale);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }
}