using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using DataBaseLibrary;

namespace LINQ.Controllers;

/// <summary>
/// Represents methods to work with the table of the goods.
/// </summary>
[Route("api/[controller]/")]
public class GoodsController : Controller
{
    private static readonly Func<Good> CreateGood = () => new Good("T-shirt", 0, "Clothes", 120);
    private static readonly Func<Good> CreateGood1 = () => new Good("T-shirt", 1, "Clothes", 130);
    private static readonly Func<Good> CreateGood2 = () => new Good("Jacket", 2, "Clothes", 250);
    private static readonly Func<Good> CreateGood3 = () => new Good("Axe", 6, "Weapon", 1);
    private static readonly Func<Good> CreateGood4 = () => new Good("Jeans", 3, "Clothes", 170);
    private static readonly Func<Good> CreateGood5 = () => new Good("Sneakers", 4, "Boots", 1200);
    private static readonly Func<Good> CreateGood6 = () => new Good("Socks", 5, "Clothes", 100);

    static GoodsController()
    {
        try
        {
            DataAccessLayer.Db.CreateTable<Good>();
            DataAccessLayer.Db.InsertInto(CreateGood);
            DataAccessLayer.Db.InsertInto(CreateGood1);
            DataAccessLayer.Db.InsertInto(CreateGood2);
            DataAccessLayer.Db.InsertInto(CreateGood3);
            DataAccessLayer.Db.InsertInto(CreateGood4);
            DataAccessLayer.Db.InsertInto(CreateGood5);
            DataAccessLayer.Db.InsertInto(CreateGood6);
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
                    DataAccessLayer.Db.Serialize<Good>(path);
    
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
                    DataAccessLayer.Db.Deserialize<Good>(path);
    
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
    /// Get the list of goods.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var goods = DataAccessLayer.Db.GetTable<Good>();
            return Ok(goods);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    /// <summary>
    /// Get the good by passed id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var good = DataAccessLayer.Db.GetTable<Good>().SingleOrDefault(g => g.Id == id);

            if (good is null)
            {
                return Ok("No good with such id. Please come again");
            }

            return Ok(good);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    /// <summary>
    /// Add a new good.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="shopId"></param>
    /// <param name="category"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    [HttpPost("{name}/{shopId}/{category}/{price}")]
    public IActionResult Post(string name, int shopId, string category, long price)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var good = new Good(name, shopId, category, price);
            DataAccessLayer.Db.InsertInto(() => good);
            return CreatedAtAction(nameof(Get), good);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }
}