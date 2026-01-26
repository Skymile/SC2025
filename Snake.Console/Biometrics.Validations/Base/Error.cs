namespace Biometrics.Validations.Base;

public record Error(
    string ParamName, 
    string Message
);
