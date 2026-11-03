namespace Booking.DataAccess.Seeding;

internal static class BookingSeedData
{
    public const string DefaultPassword = "Test123!";

    public static readonly string[] Roles = ["Admin", "Tenant", "Landlord"];

    public static readonly string[] PropertyTypes =
        ["hotel", "apartman", "vila", "resort", "house"];

    public static readonly (string Country, string City)[] EuropeanDestinations =
    [
        ("Serbia", "Belgrade"),
        ("Croatia", "Dubrovnik"),
        ("Italy", "Rome"),
        ("Spain", "Barcelona"),
        ("France", "Paris"),
        ("Greece", "Athens"),
        ("Austria", "Vienna"),
        ("Germany", "Munich"),
        ("Portugal", "Lisbon"),
        ("Netherlands", "Amsterdam")
    ];

    public static readonly PropertySeed[] Properties =
    [
        new("Hotel Moskva", "Belgrade", "hotel",
            "Historic four-star hotel in the heart of Belgrade.",
            "Balkanska 1, Belgrade"),
        new("Hotel Excelsior", "Dubrovnik", "hotel",
            "Luxury seaside hotel with views of the Adriatic.",
            "Frana Supila 12, Dubrovnik"),
        new("Grand Hotel Roma", "Rome", "hotel",
            "Elegant hotel near Rome's historic center.",
            "Via Veneto 125, Rome"),
        new("Barcelo Resort Barcelona", "Barcelona", "resort",
            "Beachfront resort with spa and pool facilities.",
            "Passeig de Garcia Faria 37, Barcelona"),
        new("Paris Suites Montmartre", "Paris", "apartman",
            "Modern apartments close to Sacre-Coeur.",
            "Rue Lepic 18, Paris"),
        new("Athens Acropolis Hotel", "Athens", "hotel",
            "Boutique hotel with views of the Acropolis.",
            "Mitropoleos 15, Athens"),
        new("Hotel Sacher Vienna", "Vienna", "hotel",
            "Classic Viennese hotel near the opera house.",
            "Philharmoniker Strasse 4, Vienna"),
        new("Bavaria Guest House", "Munich", "house",
            "Cozy guest house in a quiet Munich neighborhood.",
            "Amalienstrasse 25, Munich"),
        new("Villa Lisboa Atlantico", "Lisbon", "vila",
            "Private villa with terrace overlooking the Atlantic.",
            "Avenida Brasilia 120, Lisbon"),
        new("Amsterdam Canal Apartments", "Amsterdam", "apartman",
            "Stylish canal-side apartments in the city center.",
            "Keizersgracht 174, Amsterdam")
    ];

    public static readonly UserSeed[] Users =
    [
        new("Admin", "User", "admin@booking.com", "Admin"),
        new("Nikola", "Vlasov", "nikola.vlasov@booking.com", "Landlord"),
        new("Jelena", "Petrovic", "jelena.petrovic@booking.com", "Landlord"),
        new("Marko", "Jovanovic", "marko.jovanovic@booking.com", "Tenant"),
        new("Ana", "Nikolic", "ana.nikolic@booking.com", "Tenant"),
        new("Luka", "Horvat", "luka.horvat@booking.com", "Tenant"),
        new("Sofia", "Rossi", "sofia.rossi@booking.com", "Tenant"),
        new("Emma", "Dubois", "emma.dubois@booking.com", "Tenant"),
        new("Lucas", "Schmidt", "lucas.schmidt@booking.com", "Tenant"),
        new("Ines", "Silva", "ines.silva@booking.com", "Tenant")
    ];

    internal record PropertySeed(
        string Name,
        string City,
        string PropertyType,
        string Description,
        string Address);

    internal record UserSeed(
        string FirstName,
        string LastName,
        string Email,
        string RoleName);
}
