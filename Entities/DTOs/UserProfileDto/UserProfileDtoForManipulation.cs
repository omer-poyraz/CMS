using System.Text.Json;

namespace Entities.DTOs.UserProfileDto
{
    public abstract record UserProfileDtoForManipulation
    {
        public string? Profile { get; init; }
        public string? Gender { get; init; }
        public string? Weight { get; init; }
        public string? TargetWeight { get; init; }
        public string? DiseaseInfo { get; init; }
        public string? MedicationInfo { get; init; }
        public string? NutritionHabits { get; init; }
        public bool? HasParentalApproval { get; init; }
        public bool? KVKKApproval { get; init; }
        public string? IdentityNumber { get; init; }
        public string? Occupation { get; init; }
        public string? ContactMethod { get; init; }
        public string? Reference { get; init; }
        public string? Height { get; init; }
        public int? MealsPerDay { get; init; }
        public JsonDocument? DislikedFoods { get; init; }
        public JsonDocument? AllergyFoodSelection { get; init; }
        public JsonDocument? HealthIssue { get; init; }
        public bool? UnderDoctorControl { get; init; }
        public bool? BloodThinner { get; init; }
        public string? RegularMedications { get; init; }
        public string? OperationHistory { get; init; }
        public string? Antidepressants { get; init; }
        public string? Vitamins { get; init; }
        public bool? Pregnancy { get; init; }
        public DateTime? BirthDate { get; set; }
        public DateTime? ProgramStartDate { get; set; }
        public string? UserId { get; init; }
    }
}
