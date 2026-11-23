using Core;

namespace Backend.Repository.Interfaces;

public interface IItemRepo
{
    public void AddItem(Item item);

    public void DeleteItem(string name);

    public List<Item> GetAllItems();

}