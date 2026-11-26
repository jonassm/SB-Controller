using System.Runtime.InteropServices.JavaScript;
using MongoDB.Bson.Serialization.Attributes;

namespace Core;

[BsonIgnoreExtraElements]
public class Item
{

    //vi behøver ikke ID da alle names bliver unikke.
    public string name { get; set; }
    public string category { get; set; }
    public int price { get; set; }
    //public int? amount { get; set }
    //amount er nullable da det ikke er en decideret del af item databasen og klassen, men vi bruger det
    //når en admin laver et produkt og ændrer mængden med det samme.

    public static List<string> categories { get; } = new List<string> 
        {"bottles","soda","drinks","non-alcoholic","draft"};
    /*
     * Ude for scope; decideret kategorier hver bar kan lave direkte i databasen.
     * At have det i klassen er pænere end at hardcode det ind i frontend, og dette gør det også nemmere
     * at ændre, da vi i frontend kalder denne liste.
     *
     * Static da det skal kunne kaldes som Item.Categories istedet for vi skal initiliasere et item først.
     */
}