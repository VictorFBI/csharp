using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using DataBaseLibrary;
using NewVariant.Exceptions;

namespace LINQ.Controllers;

/// <summary>
/// Represents methods to work with the table of the buyers.
/// </summary>
[Route("api/[controller]/")]
public class BuyersController : Controller
{
    private static readonly Func<Buyer> CreateBuyer = () =>
        new Buyer("Fedor", "Dostoevskiy", "Saint-Petersburg", "Russia");

    private static readonly Func<Buyer> CreateBuyer1 = () => new Buyer("Mikhail", "Bulgakov", "Moscow", "Russia");
    private static readonly Func<Buyer> CreateBuyer2 = () => new Buyer("Mario", "Puzo", "Milano", "Italy");
    private static readonly Func<Buyer> CreateBuyer3 = () => new Buyer("Andrea", "Camillery", "Milano", "Italy");
    private static readonly Func<Buyer> CreateBuyer4 = () => new Buyer("Agatha", "Cristie", "London", "England");

    private static readonly Func<Buyer>
        CreateBuyer5 = () => new Buyer("Antoine", "de Saint-Exupery", "Paris", "France");

    private static readonly Func<Buyer> CreateBuyer6 = () => new Buyer("George", "Orwell", "Boston", "America");

    static BuyersController()
    {
        try
        {
            DataAccessLayer.Db.CreateTable<Buyer>();
            DataAccessLayer.Db.InsertInto(CreateBuyer);
            DataAccessLayer.Db.InsertInto(CreateBuyer1);
            DataAccessLayer.Db.InsertInto(CreateBuyer2);
            DataAccessLayer.Db.InsertInto(CreateBuyer3);
            DataAccessLayer.Db.InsertInto(CreateBuyer4);
            DataAccessLayer.Db.InsertInto(CreateBuyer5);
            DataAccessLayer.Db.InsertInto(CreateBuyer6);
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
                    DataAccessLayer.Db.Serialize<Buyer>(path);

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
                    DataAccessLayer.Db.Deserialize<Buyer>(path);

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
    /// Get the list of buyers.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var buyers = DataAccessLayer.Db.GetTable<Buyer>();
            return Ok(buyers);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    /// <summary>
    /// Get the buyer by passed id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var buyer = DataAccessLayer.Db.GetTable<Buyer>().SingleOrDefault(b => b.Id == id);

            if (buyer is null)
            {
                return Ok("No buyers with such id. Please come again");
            }

            return Ok(buyer);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    /// <summary>
    /// Add a new buyer.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="surname"></param>
    /// <param name="city"></param>
    /// <param name="country"></param>
    /// <returns></returns>
    [HttpPost("{name}/{surname}/{city}/{country}")]
    public IActionResult Post(string name, string surname, string city, string country)
    {
        try
        {
            var buyer = new Buyer(name, surname, city, country);
            DataAccessLayer.Db.InsertInto(() => buyer);
            return CreatedAtAction(nameof(Get), buyer);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }
}