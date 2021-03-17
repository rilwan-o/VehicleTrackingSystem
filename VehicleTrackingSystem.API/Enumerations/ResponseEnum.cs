namespace VehicleTrackingSystem.API.Enumerations
{
    public enum ResponseEnum
    {
        [EnumDisplay(Name = "Approved Or Completed Successfully", Description = "Operation was successful")]
        ApprovedOrCompletedSuccesfully = 00,

        [EnumDisplay(Name = "Duplicate Vehicle", Description = "Vehicle already exists")]
        DuplicateVehicle = 01,        

        [EnumDisplay(Name = "Invalid vehicle Id", Description = "Invalid vehicle Id")]
        InvalidVehicleId = 02,

        [EnumDisplay(Name = "Duplicate User", Description = "User already exists")]
        DuplicateUser = 04,

        [EnumDisplay(Name = "User Creation Failed", Description = "User creation failed! Please check user details and try again. Password must contain Uppercase, lowercase, special characters")]
        UserCreationFailed = 05,

        [EnumDisplay(Name = "User Login Failed", Description = "Invalid Username or password")]
        UserLoginFailed = 06,

        [EnumDisplay(Name = "System Malfunction ", Description = "System malfunction ")]
        SystemMalfunction = 96,
    }
}
