using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TODOApplication.Models.Repositories {
  /// <summary>
  /// This interface is for creating repositories
  /// </summary>
  /// <typeparam name="T">Table</typeparam>
  /// <typeparam name="TKey">Table Id type</typeparam>
  public interface IRepository<T, TKey> where T : class {
    Task<int> Count();
    Task<List<Todo>> GetAll(int skip, int take);

    /// <summary>
    /// Gets all objects of this type
    /// </summary>
    /// <returns>
    /// Returns a list of these objects
    /// </returns>
    Task<List<Todo>> GetAll();

    /// <summary>
    /// Gets one object of the given type
    /// </summary>
    /// <param name="id">Id of this element</param>
    /// <returns>given item</returns>
    Task<T> Get(TKey Id);

    /// <summary>
    /// Saves the given item to the database
    /// </summary>
    /// <param name="item">item</param>
    Task<bool> Save(T item);
    /// <summary>
    /// Removes the given item
    /// </summary>
    /// <param name="id">Id of this element</param>
    Task<bool> Delete(T item);
  }
}
