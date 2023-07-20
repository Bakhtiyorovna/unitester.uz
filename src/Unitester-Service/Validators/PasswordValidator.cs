
namespace Unitester_Service.Validators;

public class PasswordValidator
{
    public static string Symbols { get; } = "~`!@#$%^&*()_-+={[}]|\\:;\"'<,>.?/";

    public static (bool IsValid, string Message) IsStrongPassword(string password)
    {
        if (password.Length < 8) return (IsValid: false, Message: "Parol 8 ta belgidan kam bo'lmasligi kerak!");

        bool isUpperCaseExists = false;
        bool isNumberExists = false;
        bool isLowerCaseExists = false;
        bool isCharacterExists = false;

        foreach (var item in password)
        {
            if (char.IsUpper(item)) isUpperCaseExists = true;
            if (char.IsLower(item)) isLowerCaseExists = true;
            if (char.IsDigit(item)) isNumberExists = true;
            if (Symbols.Contains(item)) isCharacterExists = true;
        }

        if (isNumberExists == false) return (IsValid: false, Message: "Parolda kamida bitta raqam bo'lishi kerak!");
        if (isUpperCaseExists == false) return (IsValid: false, Message: "Parolda kamida bitta katta harf bo'lishi kerak!");
        if (isLowerCaseExists == false) return (IsValid: false, Message: "Parolda kamida bitta kichik harf boʻlishi kerak!");
        if (isCharacterExists == false) return (IsValid: false, Message: "Parolda kamida bitta belgi bo'lishi kerak (#@$%.!)!");

        return (IsValid: true, "");
    }
}
