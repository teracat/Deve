namespace Deve.Tests
{
    public static class TestsConstants
    {
        public static readonly string UserUsernameValid = "tests";
        public static readonly string UserPasswordValid = "tests";

        public static readonly string UserUsernameInactive = "tests2";
        public static readonly string UserPasswordInactive = "tests2";

        public static readonly long DefaultValidId = 1;
        public static readonly long DefaultInvalidId = 999999;
        public static readonly long DefaultValidIdDelete = 3;

        public static readonly string TokenExpired = "P83hovvDJI9+6LMyV9Tv/MCBELipU06iTIqm9IqsTTMNjLPaYmSvarlIxOst+2ZId4dHPK2xkqKD1hQL6Iy3gf7DEg8y+3N2K4REL2A0FVA=";

        public static readonly string JwtSigningSecretKey = "73b€27f9ce1e4@e789.973cO9feB6ae!";  //Must be 32 bytes
        public static readonly string JwtEncryptionSecretKey = "7Gb86@04036b42!a85fa646b73b29f-d";  //Must be 32 bytes

        public static readonly string CryptAesKey = "38!6c3aScjdc4e5ba-5c28@ej9f9~3d5"; //len 32
        public static readonly string CryptAesIV = "e3@75/4di-4e7l62";                 //len 16

        public static readonly string CryptDecryptedText = "Original Text";
        public static readonly string CryptEncryptedText = "0kGaNGFe3O8sgMXZsTAP8w==";  // You should change this value when you have changed the Crypt implementation or the keys used to encrypt/decrypt
    }
}