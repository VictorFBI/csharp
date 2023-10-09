using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseLibrary;

/// <summary>
/// Represents Linq-methods corresponding to the task condition.
/// </summary>
public class DataAccessLayer : IDataAccessLayer
{
    public static readonly DataBase Db = new();

    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public DataAccessLayer()
    {
    }

    /// <summary>
    /// Returns the list of all goods bought by the buyer with the longest name. If there are any longest names, returns the list for the last one in the lexicographical order of the name.
    /// </summary>
    /// <param name="dataBase">Database.</param>
    /// <returns></returns>
    public IEnumerable<Good> GetAllGoodsOfLongestNameBuyer(IDataBase dataBase)
    {
        var buyers = dataBase.GetTable<Buyer>();
        var goods = dataBase.GetTable<Good>();
        var sales = dataBase.GetTable<Sale>();
        var longestNameLength = buyers.Max(b => b.Name.Length);

        var idOfBuyer = (from b in buyers
            where b.Name.Length == longestNameLength
            select new { b.Id, b.Name }).OrderBy(b => b.Name).Select(b => b.Id).Last();

        var goodIds = (from s in sales
                group s.GoodId by s.BuyerId
                into g
                select new { g.Key, Collection = g.ToList() }).Where(t => t.Key == idOfBuyer).Select(g => g.Collection)
            .SingleOrDefault();

        var result = goods.IntersectBy(goodIds, g => g.Id);

        return result;
    }

    /// <summary>
    /// Returns the name of the category of the most expensive good. If there are any most expensive goods, returns the category for any of them.
    /// </summary>
    /// <param name="dataBase">Database.</param>
    /// <returns></returns>
    public string? GetMostExpensiveGoodCategory(IDataBase dataBase)
    {
        var goods = dataBase.GetTable<Good>();
        var result = (from g in goods
            where g.Price == goods.Max(t => t.Price)
            select g.Category).First();

        return result;
    }

    /// <summary>
    /// Returns the name of the city where the least money was spent. If there are several cities, make returns for any of them.
    /// </summary>
    /// <param name="dataBase">Database.</param>
    /// <returns></returns>
    public string? GetMinimumSalesCity(IDataBase dataBase)
    {
        var goods = dataBase.GetTable<Good>();
        var sales = dataBase.GetTable<Sale>();
        var shops = dataBase.GetTable<Shop>();

        var result = (from s in sales
                group goods.Where(t => t.Id == s.GoodId).Select(t => t.Price).First() * s.GoodCount
                    by shops.Where(t => t.Id == s.ShopId).Select(t => t.City).First()
                into g
                orderby g.First()
                select new { g.Key, Sum = g.Sum() }).OrderBy(g => g.Sum)
            .Select(g => g.Key)
            .First();

        return result;
    }

    /// <summary>
    /// Returns the list of the buyers who bought the most popular good. If there are several most popular goods, returns the list for any of them.
    /// </summary>
    /// <param name="dataBase">Database.</param>
    /// <returns></returns>
    public IEnumerable<Buyer> GetMostPopularGoodBuyers(IDataBase dataBase)
    {
        var sales = dataBase.GetTable<Sale>();
        var buyers = dataBase.GetTable<Buyer>();
        var idMostPopularGood = (from s in sales
                group s.GoodCount by s.GoodId
                into g
                select new { g.Key, Sum = g.Sum() }).OrderByDescending(g => g.Sum)
            .Select(g => g.Key)
            .First();
        var buyersIdOfMostPopularGoods = from s in sales
            where s.GoodId == idMostPopularGood
            select s.BuyerId;
        var result = buyers.IntersectBy(buyersIdOfMostPopularGoods, t => t.Id);

        return result;
    }

    /// <summary>
    /// Determines for each country the count of its shops. Returns the least gotten value.
    /// </summary>
    /// <param name="dataBase">Database.</param>
    /// <returns></returns>
    public int GetMinimumNumberOfShopsInCountry(IDataBase dataBase)
    {
        var shops = dataBase.GetTable<Shop>();
        var result = (from s in shops
                group s.Id by s.Country
                into g
                select new { g.Key, Count = g.Count() }).OrderBy(g => g.Count)
            .Select(g => g.Count)
            .First();

        return result;
    }

    /// <summary>
    /// Returns the list of the sales made by the buyers in all cities other than their hometown.
    /// </summary>
    /// <param name="dataBase">Database.</param>
    /// <returns></returns>
    public IEnumerable<Sale> GetOtherCitySales(IDataBase dataBase)
    {
        var sales = dataBase.GetTable<Sale>();
        var shops = dataBase.GetTable<Shop>();
        var buyers = dataBase.GetTable<Buyer>();
        var result = from s in sales
            where (buyers.Where(t => t.Id == s.BuyerId).Select(t => t.City)).First() !=
                  (shops.Where(t => t.Id == s.ShopId).Select(t => t.City)).First()
            select s;

        return result;
    }

    /// <summary>
    /// Returns the total cost of sales made by all buyers.
    /// </summary>
    /// <param name="dataBase">Database.</param>
    /// <returns></returns>
    public long GetTotalSalesValue(IDataBase dataBase)
    {
        var sales = dataBase.GetTable<Sale>();
        var goods = dataBase.GetTable<Good>();
        var result = (from s in sales
            group goods.Where(t => t.Id == s.GoodId).Select(t => t.Price).First() * s.GoodCount
                by s.GoodId
            into g
            select new { g.Key, Sum = g.Sum() }).Sum(g => g.Sum);

        return result;
    }
}