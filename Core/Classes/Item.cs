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
}