using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using NewVariant.Exceptions;

namespace DataBaseLibrary
{
    /// <summary>
    /// Represents methods to work with databases. Implements IDataBase interface.
    /// </summary>
    public class DataBase : IDataBase
    {
        private List<IEntity>? _buyerTable;
        private List<IEntity>? _saleTable;
        private List<IEntity>? _shopTable;
        private List<IEntity>? _goodTable;

        public List<IEntity>? BuyerTable => _buyerTable;
        public List<IEntity>? SaleTable => _saleTable;
        public List<IEntity>? ShopTable => _shopTable;
        public List<IEntity>? GoodTable => _goodTable;
       

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public DataBase()
        {
        }

        /// <summary>
        /// Creates a new table of T. If the corresponding table already exists, throws DataBaseException 
        /// </summary>
        /// <typeparam name="T">Generalized class</typeparam>
        /// <exception cref="DataBaseException">Thrown if the table already exists</exception>
        public void CreateTable<T>() where T : IEntity
        {
            if (typeof(T) == typeof(Buyer))
            {
                if (_buyerTable != null)
                    throw new DataBaseException("Table of buyers already exists");
                _buyerTable = new();
            }

            if (typeof(T) == typeof(Sale))
            {
                if (_saleTable != null)
                    throw new DataBaseException("Table of sales already exists");
                _saleTable = new();
            }

            if (typeof(T) == typeof(Shop))
            {
                if (_shopTable != null)
                    throw new DataBaseException("Table of shops already exists");
                _shopTable = new();
            }

            if (typeof(T) == typeof(Good))
            {
                if (_goodTable != null)
                    throw new DataBaseException("Table of goods already exists");
                _goodTable = new();
            }
        }

        /// <summary>
        /// Inserts a new line in the table of type T.
        /// </summary>
        /// <param name="getEntity">Returns an instance of the corresponding type.</param>
        /// <typeparam name="T">Generalized class.</typeparam>
        /// <exception cref="DataBaseException">Thrown if the table does not exist.</exception>
        public void InsertInto<T>(Func<T> getEntity) where T : IEntity
        {
            if (typeof(T) == typeof(Buyer))
            {
                if (_buyerTable is null)
                    throw new DataBaseException("This table of buyers does not exist");
                _buyerTable.Add(getEntity.Invoke());
            }

            if (typeof(T) == typeof(Sale))
            {
                if (_saleTable is null)
                    throw new DataBaseException("This table of sales does not exist");
                _saleTable.Add(getEntity.Invoke());
            }

            if (typeof(T) == typeof(Shop))
            {
                if (_shopTable is null)
                    throw new DataBaseException("This table of shops does not exist");
                _shopTable.Add(getEntity.Invoke());
            }

            if (typeof(T) == typeof(Good))
            {
                if (_goodTable is null)
                    throw new DataBaseException("This table of goods does not exist");
                _goodTable.Add(getEntity.Invoke());
            }
        }

        /// <summary>
        /// Gets the link to the table of type T.
        /// </summary>
        /// <typeparam name="T">Generalized class.</typeparam>
        /// <returns></returns>
        /// <exception cref="DataBaseException">Thrown if the table does not exist.</exception>
        public IEnumerable<T> GetTable<T>() where T : IEntity
        {
            if (typeof(T) == typeof(Buyer))
            {
                if (_buyerTable is null)
                    throw new DataBaseException("Table of buyers does not exist");
                return _buyerTable.Select(buyer => (T)buyer);
            }

            if (typeof(T) == typeof(Sale))
            {
                if (_saleTable is null)
                    throw new DataBaseException("Table of sales does not exist");
                return _saleTable.Select(sale => (T)sale);
            }

            if (typeof(T) == typeof(Shop))
            {
                if (_shopTable is null)
                    throw new DataBaseException("Table of shops does not exist");
                return _shopTable.Select(shop => (T)shop);
            }

            if (typeof(T) == typeof(Good))
            {
                if (_goodTable is null)
                    throw new DataBaseException("Table of goods does not exist");
                return _goodTable.Select(good => (T)good);
            }

            return null;
        }

        /// <summary>
        /// Serializes the table of type T into the file located on the passed path. Uses JSON-serialization.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <typeparam name="T">Generalized class.</typeparam>
        /// <exception cref="DataBaseException">Thrown if the table does not exist.</exception>
        public void Serialize<T>(string path) where T : IEntity
        {
            if (typeof(T) == typeof(Buyer))
            {
                if (_buyerTable is not null)
                {
                    using (var sw = File.CreateText(path))
                    {
                        sw.WriteLine(JsonSerializer.Serialize(_buyerTable.Select(x => (T)x)));
                    }

                    return;
                }

                throw new DataBaseException("This table does not exist");
            }

            if (typeof(T) == typeof(Sale))
            {
                if (_saleTable is not null)
                {
                    using (var sw = File.CreateText(path))
                    {
                        sw.WriteLine(JsonSerializer.Serialize(_saleTable.Select(x => (T)x)));
                    }

                    return;
                }

                throw new DataBaseException("This table does not exist");
            }

            if (typeof(T) == typeof(Shop))
            {
                if (_shopTable is not null)
                {
                    using (var sw = File.CreateText(path))
                    {
                        sw.WriteLine(JsonSerializer.Serialize(_shopTable.Select(x => (T)x)));
                    }

                    return;
                }

                throw new DataBaseException("This table does not exist");
            }

            if (typeof(T) == typeof(Good))
            {
                if (_goodTable is not null)
                {
                    using (var sw = File.CreateText(path))
                    {
                        sw.WriteLine(JsonSerializer.Serialize(_goodTable.Select(x => (T)x)));
                    }
                    
                    return;
                }
                
                throw new DataBaseException("This table does not exist");
            }
        }

        /// <summary>
        /// Deserializes and saves the table of type T located on the passed path. Uses JSON-serialization.
        /// </summary>
        /// <param name="path">Path of the file/</param>
        /// <typeparam name="T">Generalized class/</typeparam>
        /// <exception cref="FileNotFoundException">Thrown if the path of the file does not exist/</exception>
        /// <exception cref="DataBaseException">Thrown if the table does not exist/</exception>
        public void Deserialize<T>(string path) where T : IEntity
        {
            if (typeof(T) == typeof(Buyer))
            {
                if (_buyerTable is not null)
                {
                    if (!File.Exists(path))
                        throw new FileNotFoundException("Incorrect path");

                    var entities = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path));
                    
                    _buyerTable = entities.Select(x => (IEntity)x).ToList();

                    return;
                }

                throw new DataBaseException("This table does not exist");
            }

            if (typeof(T) == typeof(Sale))
            {
                if (_saleTable is not null)
                {
                    if (!File.Exists(path))
                        throw new FileNotFoundException("Incorrect path");

                    var entities = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path));

                    _saleTable = entities.Select(x => (IEntity)x).ToList();
                    
                    return;
                }

                throw new DataBaseException("This table does not exist");
            }

            if (typeof(T) == typeof(Shop))
            {
                if (_shopTable is not null)
                {
                    if (!File.Exists(path))
                        throw new FileNotFoundException("Incorrect path");

                    var entities = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path));

                    _shopTable = entities.Select(x => (IEntity)x).ToList();

                    return;
                }

                throw new DataBaseException("This table does not exist");
            }

            if (typeof(T) == typeof(Good))
            {
                if (_goodTable is not null)
                {
                    if (!File.Exists(path))
                        throw new FileNotFoundException("Incorrect path");
                    
                    var entities = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path));
                    
                    _goodTable = entities.Select(x => (IEntity)x).ToList();

                    return;
                }

                throw new DataBaseException("This table does not exist");
            }
        }
    }
}