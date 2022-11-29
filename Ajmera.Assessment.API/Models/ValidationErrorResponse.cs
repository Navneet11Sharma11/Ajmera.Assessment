namespace Ajmera.Assessment.API.Models
{
    public class ValidationErrorResponse
    {
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }
}
