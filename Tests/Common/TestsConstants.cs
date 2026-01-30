namespace Deve.Tests;

public static class TestsConstants
{
    public static readonly string UserUsernameValid = "tests";
    public static readonly string UserPasswordValid = "tests";

    public static readonly string UserUsernameInactive = "tests2";
    public static readonly string UserPasswordInactive = "tests2";

    public static readonly Guid DefaultValidId = new("c1d2e3f4-5678-90ab-cdef-1234567890ab");   // Valid Guid for Get tests (it must exist in the test data mock)
    public static readonly Guid DefaultInvalidId = Guid.Empty;
    public static readonly Guid DefaultValidIdDelete = Guid.NewGuid();  // Any non empty guid will be valid for delete tests

    public static readonly string TokenExpired = "P83hovvDJI9+6LMyV9Tv/MCBELipU06iTIqm9IqsTTMNjLPaYmSvarlIxOst+2ZId4dHPK2xkqKD1hQL6Iy3gf7DEg8y+3N2K4REL2A0FVA=";

    public static readonly string JwtSigningSecretKey = "73b€27f9ce1e4@e789.973cO9feB6ae!";  //Must be 32 bytes
    public static readonly string JwtEncryptionSecretKey = "7Gb86@04036b42!a85fa646b73b29f-d";  //Must be 32 bytes

    public static readonly string CryptAesKey = "38!6c3aScjdc4e5ba-5c28@ej9f9~3d5"; //len 32
    public static readonly string CryptAesIV = "e3@75/4di-4e7l62";                 //len 16

    public static readonly string CryptDecryptedText = "Original Text";
    public static readonly string CryptEncryptedText = "0kGaNGFe3O8sgMXZsTAP8w==";  // You should change this value when you have changed the Crypt implementation or the keys used to encrypt/decrypt


    //Countries
    internal static readonly Guid SpainCountryId = Guid.Parse("c1d2e3f4-5678-90ab-cdef-1234567890ab");
    internal static readonly Guid UsaCountryId = Guid.Parse("aa40d411-9b5f-41ea-9f5d-c8d2a10ba606");
    internal static readonly Guid FranceCountryId = Guid.Parse("194f6398-9e6e-4535-bb07-6082bbef35b9");

    //States
    internal static readonly Guid BarcelonaStateId = Guid.Parse("c1d2e3f4-5678-90ab-cdef-1234567890ab");
    internal static readonly Guid WashingtonStateId = Guid.Parse("b1c3f4d5-5f6a-4e7b-8c9d-0a1b2c3d4e5f");
    internal static readonly Guid MadridStateId = Guid.Parse("194f6398-9e6e-4535-bb07-6082bbef35b9");

    //Cities
    internal static readonly Guid SantpedorCityId = Guid.Parse("c1d2e3f4-5678-90ab-cdef-1234567890ab");
    internal static readonly Guid BarcelonaCityId = Guid.Parse("d4e5f6a7-8901-2345-6789-0abcdef12345");
    internal static readonly Guid WashingtonDCCityId = Guid.Parse("194f6398-9e6e-4535-bb07-6082bbef35b9");
    internal static readonly Guid RedmondCityId = Guid.Parse("f6a7b8c9-0123-4567-8901-2cdef3456789");

    //Clients
    internal static readonly Guid TeracatClientId = Guid.Parse("c1d2e3f4-5678-90ab-cdef-1234567890ab");
    internal static readonly Guid MicrosoftClientId = Guid.Parse("2a3b4c5d-6e7f-8901-2345-67890abcdef1");
    internal static readonly Guid FakeCompanyClientId = Guid.Parse("194f6398-9e6e-4535-bb07-6082bbef35b9");

    // Users
    internal static readonly Guid ValidUserId = Guid.Parse("c1d2e3f4-5678-90ab-cdef-1234567890ab");
    internal static readonly Guid InactiveUserId = Guid.Parse("4d5e6f70-8192-0123-4567-890abcdef123");
    internal static readonly Guid FakeUserId = Guid.Parse("194f6398-9e6e-4535-bb07-6082bbef35b9");
    internal static readonly Guid UpdateUserId = Guid.Parse("57b42e55-b933-4425-bf2e-c1b7f7720faa");
}
