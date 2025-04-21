namespace ConstantReminders.Contracts.Models;

public class TwilioResponse
{
    public bool isSuccessful { get; set; }
    public string errorMessage { get; set; }

    public TwilioResponse(bool success, string message)
    {
        isSuccessful = success;
        errorMessage = message;
    }
}
