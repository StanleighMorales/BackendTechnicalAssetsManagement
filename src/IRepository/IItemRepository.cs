using BackendTechnicalAssetsManagement.src.Classes;

namespace BackendTechnicalAssetsManagement.src.IRepository

{
    public interface IItemRepository
    {
        /// <summary>
        /// Gets a single item by its primary key.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item if found; otherwise, null.</returns>
        Task<Item?> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets a single item by its unique serial number.
        /// </summary>
        /// <param name="serialNumber">The serial number of the item.</param>
        /// <returns>The item if found; otherwise, null.</returns>
        Task<Item?> GetBySerialNumberAsync(string serialNumber);

        /// <summary>
        /// Gets a list of all items from the database.
        /// </summary>
        /// <returns>An enumerable collection of all items.</returns>
        Task<IEnumerable<Item>> GetAllAsync();

        /// <summary>
        /// Gets Item By Barcode from the database.
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        Task<Item?> GetByBarcodeAsync(string barcode);


        /// <summary>
        /// Adds a new item to the database context. This does not save to the database yet.
        /// </summary>
        /// <param name="item">The new item to add.</param>
        /// <returns>The item that was added to the context.</returns>
        Task<Item> AddAsync(Item item);

        /// <summary>
        /// Marks an existing item as updated in the database context.
        /// </summary>
        /// <param name="item">The item with updated values.</param>
        Task UpdateAsync(Item item);

        /// <summary>
        /// Removes an item from the database context by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        // Saves all pending changes in the context to the database.
        /// </summary>
        /// <returns>True if at least one change was successfully saved; otherwise, false.</returns>
        Task<bool> SaveChangesAsync();
    }
}
