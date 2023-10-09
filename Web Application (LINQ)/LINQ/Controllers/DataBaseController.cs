using Microsoft.AspNetCore.Mvc;
using DataBaseLibrary;

namespace LINQ.Controllers;

/// <summary>
/// Represents methods to work with the database.
/// </summary>
[Route("api/[controller]/")]
public class DataBaseController : Controller
{
    /// <summary>
    /// Get the results of the linq-methods.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get(int number)
    {
        try
        {
            var dal = new DataAccessLayer();
            switch (number)
            {
                case 1:
                    return Ok(dal.GetAllGoodsOfLongestNameBuyer(DataAccessLayer.Db));
                case 2:
                    return Ok(dal.GetMostExpensiveGoodCategory(DataAccessLayer.Db));
                case 3:
                    return Ok(dal.GetMinimumSalesCity(DataAccessLayer.Db));
                case 4:
                    return Ok(dal.GetMostPopularGoodBuyers(DataAccessLayer.Db));
                case 5:
                    return Ok(dal.GetMinimumNumberOfShopsInCountry(DataAccessLayer.Db));
                case 6:
                    return Ok(dal.GetOtherCitySales(DataAccessLayer.Db));
                case 7:
                    return Ok(dal.GetTotalSalesValue(DataAccessLayer.Db));
                default:
                    return Ok("Incorrect number. Please, come again with numbers from 1 to 7");
            }
        }
        catch (Exception ex)
        {
            return Ok($"{ex.Message}, create the corresponding one with get the command");
        }
    }
}