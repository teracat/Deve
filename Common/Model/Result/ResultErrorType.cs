﻿namespace Deve.Model
{
    public enum ResultErrorType
    {
        Unknown = 0,
        Unauthorized = 1,
        MissingRequiredField = 2,
        InvalidId = 3,
        NotFound = 4,
        DuplicatedValue = 5,
        NotAllowed = 6,
        TooManyAttempts = 7,
        Locked = 8,
    }
}
