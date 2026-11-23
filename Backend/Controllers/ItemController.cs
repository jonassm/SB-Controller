using Backend.Repository;
using Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core;

namespace Backend.Controllers;

[ApiController]
[Route("api/item")]
public class ItemController : ControllerBase
{
    private IItemRepo iRepo;

    public ItemController(IItemRepo repo)
    {
        iRepo = repo;
    }

    [HttpPost]
    [Route("add")]
    public void AddItem(Item newItem)
    {
        iRepo.AddItem(newItem); //Add er også repo funktion
        
        /* TODO
         * Vi skal få amount ind når vi laver et Item, så vi kan ændre direkte i storage når vi tilføjer et produkt.
         * Dvs. hvis en admin laver et produkt fordi der er kommet en kasse med 24 ind, skal de kunne ændr mængden direkte
         * i storage og ikke først ind og redigere i det.
         */
    }

    [HttpGet]
    [Route("get")]
    public List<Item> GetAllItems()
    {
        return iRepo.GetAllItems(); //kalder GetAll fra Repo.
        //Inde i item får vi kun dem der er registreret, og er ligeglad med stock mængder osv, det foregår i storage.
    }

    [HttpDelete]
    [Route("delete/{name}")]
    public void DeleteItem(string name)
    {
        iRepo.DeleteItem(name);
    }
    
}
