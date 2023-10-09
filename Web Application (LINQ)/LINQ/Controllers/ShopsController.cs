using Microsoft.AspNetCore.Mvc;
using DataBaseLibrary;

namespace LINQ.Controllers;

/// <summary>
/// Represents methods to work with the table of the shops.
/// </summary>
[Route("api/[controller]/")]
public class ShopsController : Controller
{
    private static readonly Func<Shop> CreateShop = () => new Shop("Waikiki", "Moscow", "Russia");
    private static readonly Func<Shop> CreateShop1 = () => new Shop("Waikiki", "Istanbul", "Turkey");
    private static readonly Func<Shop> CreateShop2 = () => new Shop("H&M", "Milano", "Italy");
    private static readonly Func<Shop> CreateShop3 = () => new Shop("Gloria Jeans", "Khabarovsk", "Russia");
    private static readonly Func<Shop> CreateShop4 = () => new Shop("Adidas", "Cologne", "Germany");
    private static readonly Func<Shop> CreateShop5 = () => new Shop("Adidas", "Moscow", "Russia");
    private static readonly Func<Shop> CreateShop6 = () => new Shop("Gucci", "Milano", "Italy");

    static ShopsController()
    {
        try
        {
            DataAccessLayer.Db.CreateTable<Shop>();
            DataAccessLayer.Db.InsertInto(CreateShop);
            DataAccessLayer.Db.InsertInto(CreateShop1);
            DataAccessLayer.Db.InsertInto(CreateShop2);
            DataAccessLayer.Db.InsertInto(CreateShop3);
            DataAccessLayer.Db.InsertInto(CreateShop4);
            DataAccessLayer.Db.InsertInto(CreateShop5);
            DataAccessLayer.Db.InsertInto(CreateShop6);
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
                    DataAccessLayer.Db.Serialize<Shop>(path);
    
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
                    DataAccessLayer.Db.Deserialize<Shop>(path);
    
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
    /// Get the list of shops.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var shops = DataAccessLayer.Db.GetTable<Shop>();
            return Ok(shops);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    /// <summary>
    /// Get the shop by passed id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var shop = DataAccessLayer.Db.GetTable<Shop>().SingleOrDefault(s => s.Id == id);

            if (shop is null)
            {
                return Ok("No shops with such id. Please come again");
            }

            return Ok(shop);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    /// <summary>
    /// Add a new shop.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="city"></param>
    /// <param name="country"></param>
    /// <returns></returns>
    [HttpPost("{name}/{city}/{country}")]
    public IActionResult Post(string name, string city, string country)
    {
        try
        {
            var shop = new Shop(name, city, country);
            DataAccessLayer.Db.InsertInto(() => shop);
            return CreatedAtAction(nameof(Get), shop);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }
}