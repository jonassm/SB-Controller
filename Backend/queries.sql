

user kommando ser sådan ud: (se pass er null)
db.User.insertOne({ username: "admin", password: null, role: "admin" });

tilføjelse af fks. tuborg classic:
db.Item.insertOne({ name: "Tuborg Classic", category: "Øl", price: 20 });
